using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Events;
using PaymentService.Application.Services;
using PaymentService.Infrastructure.Messaging;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        private readonly RabbitMqPublisher _publisher;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _publisher = new RabbitMqPublisher();
        }

        [HttpPost("start")]
        public async Task<IActionResult> GetPaymentAsync()
        {
            var userId = Guid.NewGuid();
            var paymentId = await _paymentService.StartPaymentAsync(10000, userId);
            var paymentEvent = new PaymentStartedEvent(paymentId, userId.ToString());
            _publisher.Publish(paymentEvent);
            Console.WriteLine("Evento enviado a RabbitMQ desde el GET");

            return Ok(new { message = "GetPayment", eventId = paymentEvent.PaymentId });
        }
    }
}
