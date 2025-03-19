using Domain.Entities;
using Domain.Repositories;

namespace PaymentService.Application.Services
{
    public class PaymentServiceImpl : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentServiceImpl(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Guid> StartPaymentAsync(decimal amount, Guid userId)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            var payment = new Payment(amount, userId);

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return payment.Id;
        }
    }
}
