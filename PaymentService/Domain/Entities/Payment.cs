namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Status { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private static readonly List<string> ValidStatuses = new() { "Pending", "Confirmed", "Cancelled" };

        public Payment(decimal amount, Guid userId)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Status = "Pending";
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public void Confirm()
        {
            if (Status != "Pending")
                throw new InvalidOperationException("Only pending payments can be confirmed.");

            Status = "Confirmed";
        }

        public void Cancel()
        {
            if (Status != "Pending")
                throw new InvalidOperationException("Only pending payments can be cancelled.");

            Status = "Cancelled";
        }
    }
}
