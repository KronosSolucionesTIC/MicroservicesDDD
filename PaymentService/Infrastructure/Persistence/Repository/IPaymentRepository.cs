using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task SaveChangesAsync();
    }
}