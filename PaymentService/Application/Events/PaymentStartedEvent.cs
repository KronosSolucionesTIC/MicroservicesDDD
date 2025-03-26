using PaymentService.Domain.Events;

namespace PaymentService.Application.Events
{
    public class PaymentStartedEvent : IntegrationEvent
    {
        public Guid PaymentId { get; }
        public decimal Amount { get; }
        public string? UserId { get; }       

        public PaymentStartedEvent(Guid paymentId, decimal amount, string userId)
        {
            PaymentId = paymentId;
            Amount = amount;
            UserId = userId;
        }
    }
}
