using Newtonsoft.Json;
using PaymentService.Domain.Events;
using RabbitMQ.Client;
using System.Text;

namespace PaymentService.Infrastructure.Messaging
{
    public class RabbitMqPublisher
    {
        private readonly string _queueName = "payment_started_queue";
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqPublisher()
        {
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

                _channel.QueueDeclare(queue: _queueName,
                      durable: true,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

                Console.WriteLine($"📌 Declarando la cola: {_queueName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al conectar a RabbitMQ: {ex.Message}");
            }
        }

        public void Publish<T>(T integrationEvent) where T : IntegrationEvent
        {
            if (_channel == null)
            {
                Console.WriteLine("❌ No se pudo publicar el mensaje porque la conexión a RabbitMQ no está disponible.");
                return;
            }

            var message = JsonConvert.SerializeObject(integrationEvent);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            Console.WriteLine($"[x] Sent: {message}");
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
