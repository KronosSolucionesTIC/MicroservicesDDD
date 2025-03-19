namespace PaymentService.Application.Commands
{
    public class StartPaymentCommand
    {
        public string? UserId { get; set; }

        public StartPaymentCommand(string userId)
        { 
            UserId = userId;
        }
    }
}
