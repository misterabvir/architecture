using CloudService.Application.Base.Repositories;
using CloudService.Application.Exceptions;
using CloudService.Domain;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Setups.Commands;

public class Update
{
    public record Command(
        Guid UserId,
        Guid SetupId,
        Guid? CpuId = null,
        Guid? RamId = null,
        Guid? RomId = null,
        Guid? IpId = null,
        Guid? OsId = null) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.SetupId).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, cancellationToken)
                ?? throw new NotFoundException("User not found");

            var setup = user.Setups.FirstOrDefault(c => c.SetupId == command.SetupId)
                ?? throw new NotFoundException("Setup not found");

            if (command.CpuId is not null)
            {
                var cpu = await unitOfWork.Devices.GetCpuById(command.CpuId.Value, cancellationToken)
                    ?? throw new NotFoundException("Cpu not found");
                setup.Update(cpu);
            }

            if (command.RamId is not null)
            {
                var ram = await unitOfWork.Devices.GetRamById(command.RamId.Value, cancellationToken)
                    ?? throw new NotFoundException("Ram not found");
                setup.Update(ram);
            }

            if (command.RomId is not null)
            {
                var rom = await unitOfWork.Devices.GetRomById(command.RomId.Value, cancellationToken)
                    ?? throw new NotFoundException("Rom not found");
                setup.Update(rom);
            }

            if (command.IpId is not null)
            {
                var ip = await unitOfWork.Devices.GetIpById(command.IpId.Value, cancellationToken)
                    ?? throw new NotFoundException("Ip not found");
                setup.Update(ip);
            }

            if (command.OsId is not null)
            {
                var os = await unitOfWork.Devices.GetOsById(command.OsId.Value, cancellationToken)
                    ?? throw new NotFoundException("Os not found");
                setup.Update(os);
            }

            setup.Update(Status.Offline);

            try
            {
                await unitOfWork.Users.UpdateAsync(user, cancellationToken);
                
                await unitOfWork.SaveChangesAsync(cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {

                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}