namespace PaymentService.Application.Commands
{
    public class CancelPaymentCommand
    {
        public Guid PaymentId { get; set; }

        public CancelPaymentCommand(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
