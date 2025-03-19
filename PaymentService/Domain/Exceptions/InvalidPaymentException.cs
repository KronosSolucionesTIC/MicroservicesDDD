namespace PaymentService.Domain.Exceptions
{
    public class InvalidPaymentException : Exception
    {
        public InvalidPaymentException(string message) : base(message) { }
    }
}
