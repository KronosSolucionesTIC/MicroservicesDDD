using PaymentService.Domain.Events;

namespace PaymentService.Application.Events
{
    public class PaymentStartedEvent : IntegrationEvent
    {
        public Guid PaymentId { get; }
        public string? UserId { get; }

        public PaymentStartedEvent(Guid paymentId, string userId)
        {
            PaymentId = paymentId;
            UserId = userId;
        }
    }
}
