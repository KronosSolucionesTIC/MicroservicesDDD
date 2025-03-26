using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ValidationService.Domain.Entities;
using ValidationService.Infrastructure.Services;

namespace ValidationService.Infrastructure.Messaging
{
    public class RabbitMQConsumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ValidationServiceImpl _validationService;

        public RabbitMQConsumer(ValidationServiceImpl validationService)
        {
            _validationService = validationService;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",  // Cambia si usas Docker en otra IP
                Port = 5672,             // Puerto de RabbitMQ
                UserName = "admin",      // Usuario por defecto
                Password = "admin"       // Contraseña por defecto
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                Console.WriteLine("Conexión a RabbitMQ establecida correctamente.");

                _channel.QueueDeclare(queue: "payment_started_queue",
                                      durable: true,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al conectar a RabbitMQ: {ex.Message}");
            }
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var payment = JsonConvert.DeserializeObject<Payment>(message);

                Console.WriteLine($"[x] Received Payment: {payment.Id}");

                var validatedPayment = _validationService.ValidatePayment(payment);

                Console.WriteLine($"Validation result: {(validatedPayment.IsValid ? "Valid" : "Invalid")}");

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: "payment_started_queue",
                                  autoAck: false,
                                  consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
