using PaymentService.Domain.Enums;
using PaymentService.Domain.Exceptions;

namespace PaymentService.Domain.Aggregates
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public string? UserId { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Payment() { }

        public Payment(string userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Status = PaymentStatus.Started;
            CreatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status != PaymentStatus.Started)
                throw new InvalidPaymentException("Solo se pueden cancelar pagos iniciados.");

            Status = PaymentStatus.Canceled;
        }
    }

}
