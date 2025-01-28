using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class SyncProjectsBackgroundService : IHostedService, IDisposable
{
    private readonly ILogger<SyncProjectsBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer _timer;

    // Interval for the background task to run (set to 15 minutes).
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(15); // Adjust the sync interval here

    /// <summary>
    /// Initializes the SyncProjectsBackgroundService.
    /// </summary>
    /// <param name="logger">Logger for logging information and errors.</param>
    /// <param name="serviceScopeFactory">Factory to create service scopes for resolving scoped services.</param>
    public SyncProjectsBackgroundService(ILogger<SyncProjectsBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    /// <summary>
    /// Starts the background service and begins the periodic syncing of projects.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for service shutdown requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Log the start of the service.
        _logger.LogInformation("Sync Projects Background Service is starting.");

        // Start the periodic task immediately and repeat every _interval (e.g., every 15 minutes).
        _timer = new Timer(DoWork, null, TimeSpan.Zero, _interval);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Executes the sync task to update projects based on external sales data.
    /// </summary>
    /// <param name="state">State parameter for the timer callback (not used here).</param>
    private async void DoWork(object state)
    {
        try
        {
            // Log the start of the sync task.
            _logger.LogInformation("Sync Projects task is starting.");

            // Create a new scope to resolve scoped services (needed because background services are singleton).
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // Get the SyncProjectsService from the scope.
                var syncProjectsService = scope.ServiceProvider.GetRequiredService<SyncProjectsService>();

                // Call the sync logic to synchronize the projects.
                await syncProjectsService.SyncProjectsAsync();
            }

            // Log completion of the sync task.
            _logger.LogInformation("Sync Projects task completed.");
        }
        catch (Exception ex)
        {
            // Log any errors encountered during the sync process.
            _logger.LogError($"An error occurred while syncing projects: {ex.Message}");
        }
    }

    /// <summary>
    /// Stops the background service and cleans up resources.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for service shutdown requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Log the stop of the service.
        _logger.LogInformation("Sync Projects Background Service is stopping.");

        // Dispose the timer to stop the periodic sync.
        _timer?.Dispose();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Disposes the background service and its resources.
    /// </summary>
    public void Dispose()
    {
        // Dispose of the timer to release resources.
        _timer?.Dispose();
    }
}
