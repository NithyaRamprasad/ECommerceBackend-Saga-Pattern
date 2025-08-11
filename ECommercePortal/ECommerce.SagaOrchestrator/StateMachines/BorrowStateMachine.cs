using ECommerce.SagaOrchestrator.Contracts;
using ECommerce.SagaOrchestrator.Sagas;
using MassTransit;

namespace ECommerce.SagaOrchestrator.StateMachines
{
    public class BorrowStateMachine : MassTransitStateMachine<BorrowState>
    {
        public State WaitingForApproval { get; private set; } = null!;
        public State Approved { get; private set; } = null!;
        public State Rejected { get; private set; } = null!;

        public Event<BorrowRequested> BorrowRequested { get; private set; } = null!;
        public Event<BorrowApproved> BorrowApproved { get; private set; } = null!;
        public Event<BorrowRejected> BorrowRejected { get; private set; } = null!;

        public BorrowStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => BorrowRequested, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => BorrowApproved, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => BorrowRejected, x => x.CorrelateById(m => m.Message.CorrelationId));

            Initially(
                When(BorrowRequested)
                    .ThenAsync(async context =>
                    {
                        var saga = context.Saga;
                        var message = context.Message;

                        saga.ProductId = message.ProductId;
                        saga.RequesterId = message.RequesterId;
                        saga.RequestedAt = message.RequestedAt;

                        await context.Publish(new BorrowApprovalRequested(
                            saga.CorrelationId,
                            saga.ProductId,
                            saga.RequesterId));
                    })
                    .TransitionTo(WaitingForApproval)
            );

            During(WaitingForApproval,
                When(BorrowApproved)
                    .TransitionTo(Approved),
                When(BorrowRejected)
                    .TransitionTo(Rejected)
            );
        }
    }
}
