using MassTransit;

namespace ECommerce.SagaOrchestrator.Sagas
{
    public class BorrowState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Guid RequesterId { get; set; }
        public DateTime RequestedAt { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
