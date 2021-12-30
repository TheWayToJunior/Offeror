using Offeror.TelegramBot.Commands;

namespace Offeror.TelegramBot.BackgroundServices
{
    public class CommandCleanerHostedService : IHostedService, IDisposable
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly ILogger<CommandCleanerHostedService> _logger;
        private readonly CancellationTokenSource _tokenSource;

        private Timer? _timer = null;
        private Task? _executingTask = null;

        public CommandCleanerHostedService(ICommandExecutor commandExecutor, ILogger<CommandCleanerHostedService> logger)
        {
            _commandExecutor = commandExecutor;
            _logger = logger;

            _tokenSource = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Command Cleaner Hosted Service running.");

            _timer = new Timer((state) => _executingTask = ExecuteTaskAsync(_tokenSource.Token),
                null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
        }

        private async Task ExecuteTaskAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Clear Outdated Commands executing.");
            await _commandExecutor.ClearOutdatedCommands();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Command Cleaner Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);

            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _tokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(
                    _executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _timer?.Dispose();
        }
    }
}
