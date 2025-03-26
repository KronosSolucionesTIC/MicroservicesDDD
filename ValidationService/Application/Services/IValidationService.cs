using ValidationService.Domain.Entities;

namespace ValidationService.Application.Services
{
    public interface IValidationService
    {
        Payment ValidatePayment(Payment payment);
    }
}