namespace ValidationService.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public string UserId { get; private set; }
        public bool IsValid { get; private set; }

        public Payment(Guid id, decimal amount, string userId)
        {
            Id = id;
            Amount = amount;
            UserId = userId;
        }

        public void Validate()
        {
            IsValid = Amount > 0; // Ejemplo simple de validación
        }
    }
}
