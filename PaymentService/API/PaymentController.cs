using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Events;
using PaymentService.Infrastructure.Messaging;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly RabbitMqPublisher _publisher;

        public PaymentController()
        {
            _publisher = new RabbitMqPublisher();
        }

        [HttpPost("cancel")]
        public IActionResult CancelPayment()
        {
            var paymentEvent = new PaymentCanceledEvent(Guid.NewGuid());
            _publisher.Publish(paymentEvent);
            Console.WriteLine("Evento enviado a RabbitMQ desde el POST");

            return Ok(new { message = "Evento enviado a RabbitMQ", eventId = paymentEvent.PaymentId });
        }

        [HttpPost("start")]
        public IActionResult GetPayment()
        {
            var paymentEvent = new PaymentStartedEvent(Guid.NewGuid(), Guid.NewGuid().ToString());
            _publisher.Publish(paymentEvent);
            Console.WriteLine("Evento enviado a RabbitMQ desde el GET");

            return Ok(new { message = "GetPayment", eventId = paymentEvent.PaymentId });
        }
    }
}
