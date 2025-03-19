using PaymentService.Domain.Events;

namespace PaymentService.Application.Events
{
    public class PaymentCanceledEvent : IntegrationEvent
    {
        public Guid PaymentId { get; }

        public PaymentCanceledEvent(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
