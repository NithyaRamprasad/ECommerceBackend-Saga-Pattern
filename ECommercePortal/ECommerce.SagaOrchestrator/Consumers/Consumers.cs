using ECommerce.SagaOrchestrator.Contracts;
using MassTransit;

namespace ECommerce.SagaOrchestrator.Consumers
{
    public class BorrowRequestedConsumer : IConsumer<BorrowRequested>
    {
        public Task Consume(ConsumeContext<BorrowRequested> context)
        {
            // Message automatically handled by state machine
            return Task.CompletedTask;
        }
    }

    public class BorrowApprovedConsumer : IConsumer<BorrowApproved>
    {
        public Task Consume(ConsumeContext<BorrowApproved> context)
        {
            return Task.CompletedTask;
        }
    }

    public class BorrowRejectedConsumer : IConsumer<BorrowRejected>
    {
        public Task Consume(ConsumeContext<BorrowRejected> context)
        {
            return Task.CompletedTask;
        }
    }
}
