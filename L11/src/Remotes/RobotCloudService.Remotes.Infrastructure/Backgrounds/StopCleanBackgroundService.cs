using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.Events;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Infrastructure.Backgrounds;

internal class StopCleanBackgroundService(IServiceScopeFactory factory) : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(_period);
        while (
            !cancellationToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(cancellationToken))
        {
            var scope = factory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var cache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();

            var json = await cache.GetStringAsync("jobs", cancellationToken);
            List<CleanStarted.Notification> jobs = null!;
            if (json is null)
            {
                continue;
            }
            jobs = JsonSerializer.Deserialize<List<CleanStarted.Notification>>(json) ?? [];
            var finishedJobs = jobs.Where(x => x.CalculatedFinishTime <= DateTime.UtcNow).ToList();
            foreach (var job in finishedJobs) 
            {
                var user = await unitOfWork.Users.GetByIdAsync(job.UserId, cancellationToken);
                if(user is null)
                {
                    continue;
                }
                user.StopClean(RobotId.Create(job.RobotId));
                jobs.Remove(job);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await cache.SetStringAsync("jobs", JsonSerializer.Serialize(jobs), cancellationToken);
        }
    }
}
