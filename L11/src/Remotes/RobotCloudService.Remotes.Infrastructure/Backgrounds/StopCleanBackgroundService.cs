using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Infrastructure.Backgrounds;

internal class StopCleanBackgroundService(IServiceScopeFactory factory) : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(_period);
        while (
            !cancellationToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(cancellationToken))
        {
            var scope = factory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var users = await unitOfWork.Users.GetAllAsync(cancellationToken);
            users.ForEach(u =>
            {
                foreach (var robot in u.Robots)
                {
                    if (robot.RobotState == State.Cleaning
                    && robot.CalculatedTimeOfCleaningOver < DateTime.UtcNow)
                    {
                        u.StopClean(robot.RobotId);
                    }
                }
            });

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
