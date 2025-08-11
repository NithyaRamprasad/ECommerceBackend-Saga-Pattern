namespace ECommerce.SagaOrchestrator.Contracts
{
    public record BorrowRequested(Guid CorrelationId, Guid ProductId, Guid RequesterId, DateTime RequestedAt);
    public record BorrowApprovalRequested(Guid CorrelationId, Guid ProductId, Guid RequesterId);
    public record BorrowApproved(Guid CorrelationId);
    public record BorrowRejected(Guid CorrelationId);
}
