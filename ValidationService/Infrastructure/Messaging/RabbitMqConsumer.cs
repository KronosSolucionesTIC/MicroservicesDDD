using System.Text;
using Newtonsoft.Json;
using PaymentService.Application.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ValidationService.Infrastructure.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "payment_canceled_queue";
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqConsumer()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "host.docker.internal",  // Cambia si usas Docker en otra IP
                Port = 5672,             // Puerto de RabbitMQ
                UserName = "guest",      // Usuario por defecto
                Password = "guest"       // Contraseña por defecto
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                Console.WriteLine("Conexión a RabbitMQ establecida correctamente.");

                _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al conectar a RabbitMQ: {ex.Message}");
            }
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var paymentEvent = JsonConvert.DeserializeObject<PaymentCanceledEvent>(message);

                Console.WriteLine($"[x] Received PaymentCanceledEvent - PaymentId: {paymentEvent.PaymentId}");
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            Console.WriteLine("Listening for messages...");
        }
    }
}
