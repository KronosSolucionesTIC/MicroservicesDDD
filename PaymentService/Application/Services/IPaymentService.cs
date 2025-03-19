namespace PaymentService.Application.Services
{
    public interface IPaymentService
    {
        Task<Guid> StartPaymentAsync(decimal amount, Guid userId);
    }
}
