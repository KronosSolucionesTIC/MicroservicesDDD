using PaymentService.Application.Events;
using PaymentService.Domain.Aggregates;
using PaymentService.Domain.Exceptions;
using PaymentService.Infrastructure.Messaging;
using PaymentService.Infrastructure.Persistence;

namespace PaymentService.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _repository;
        private readonly RabbitMqPublisher _eventBus;

        public PaymentService(PaymentRepository repository, RabbitMqPublisher eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public Payment StartPayment(string userId)
        {
            var payment = new Payment(userId);
            _repository.Add(payment);

            _eventBus.Publish(new PaymentStartedEvent(payment.Id, userId));

            return payment;
        }

        public void CancelPayment(Guid paymentId)
        {
            var payment = _repository.GetById(paymentId);
            if (payment == null) throw new InvalidPaymentException("Pago no encontrado.");

            payment.Cancel();
            _eventBus.Publish(new PaymentCanceledEvent(paymentId));
        }
    }
}
