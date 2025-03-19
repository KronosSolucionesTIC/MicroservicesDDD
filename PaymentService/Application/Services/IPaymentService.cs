using PaymentService.Domain.Aggregates;

namespace PaymentService.Application.Services
{
    public interface IPaymentService
    {
        Payment StartPayment(string userId);

        void CancelPayment(Guid paymentId);
    }
}
