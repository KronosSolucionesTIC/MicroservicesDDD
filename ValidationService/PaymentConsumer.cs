using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class PaymentConsumer
{
    private readonly string _queueName = "payment_queue";  // Nombre de la cola
    private readonly string _hostName = "rabbitmq";        // Nombre del servicio en Docker
    private readonly string _userName = "admin";
    private readonly string _password = "admin";

    public void StartConsuming()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            UserName = _userName,
            Password = _password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"📥 Mensaje recibido en ValidationService: {message}");

            // Aquí puedes agregar la lógica de validación y procesamiento
            ProcessPayment(message);

            // Confirmar que el mensaje fue procesado
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        channel.BasicConsume(queue: _queueName,
                             autoAck: false,
                             consumer: consumer);

        Console.WriteLine("⏳ Esperando mensajes...");
        Thread.Sleep(Timeout.Infinite);  // Mantener la ejecución
    }

    private void ProcessPayment(string message)
    {
        Console.WriteLine($"✅ Procesando pago: {message}");
        // Aquí puedes agregar lógica de validación, persistencia en BD, etc.
    }
}
