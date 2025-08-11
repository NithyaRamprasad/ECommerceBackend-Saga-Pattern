namespace ECommerce.SagaOrchestrator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Saga Orchestrator is running.");
            return Task.CompletedTask;
        }
    }
}
