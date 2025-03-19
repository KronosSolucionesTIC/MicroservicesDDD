namespace PaymentService.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
