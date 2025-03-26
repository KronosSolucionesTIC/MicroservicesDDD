using ValidationService.Application.Services;
using ValidationService.Domain.Entities;

namespace ValidationService.Infrastructure.Services
{
    public class ValidationServiceImpl : IValidationService
    {
        public Payment ValidatePayment(Payment payment)
        {
            payment.Validate();
            return payment;
        }
    }
}
