using PaymentService.Domain.Aggregates;

namespace PaymentService.Infrastructure.Persistence
{
    public class PaymentRepository
    {
        private readonly List<Payment> _payments = new();

        public void Add(Payment payment)
        {
            _payments.Add(payment);
        }
        
        public Payment? GetById(Guid id)
        {
            return _payments.FirstOrDefault(p => p.Id == id);
        }
    }
}
