using ECommerce.SagaOrchestrator;
using ECommerce.SagaOrchestrator.Consumers;
using ECommerce.SagaOrchestrator.Contracts;
using ECommerce.SagaOrchestrator.Sagas;
using ECommerce.SagaOrchestrator.StateMachines;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //services.AddDbContext<SagaDbContext>(options =>
        //    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddMassTransit(x =>
        {
            //x.AddSagaStateMachine<BorrowStateMachine, BorrowState>()
            // .EntityFrameworkRepository(r =>
            // {
            //     r.ConcurrencyMode = ConcurrencyMode.Optimistic;
            //     r.ExistingDbContext<SagaDbContext>();
            // });

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

            x.SetKebabCaseEndpointNameFormatter();

            x.AddSagaStateMachine<BorrowStateMachine, BorrowState>()
                .InMemoryRepository();

            x.AddRider(rider =>
            {
                rider.AddConsumer<BorrowRequestedConsumer>();
                rider.AddConsumer<BorrowApprovedConsumer>();
                rider.AddConsumer<BorrowRejectedConsumer>();

                rider.UsingKafka((context, k) =>
                {
                    k.Host("localhost:9092");

                    k.TopicEndpoint<BorrowRequested>("borrow-requested", "saga-group", e =>
                    {
                        e.ConfigureConsumer<BorrowRequestedConsumer>(context);
                    });

                    k.TopicEndpoint<BorrowApproved>("borrow-approved", "saga-group", e =>
                    {
                        e.ConfigureConsumer<BorrowApprovedConsumer>(context);
                    });

                    k.TopicEndpoint<BorrowRejected>("borrow-rejected", "saga-group", e =>
                    {
                        e.ConfigureConsumer<BorrowRejectedConsumer>(context);
                    });
                });
            });
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
