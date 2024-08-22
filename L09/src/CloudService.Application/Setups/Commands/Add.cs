using CloudService.Application.Base.Repositories;
using CloudService.Application.Exceptions;
using CloudService.Domain;
using FluentValidation;
using MediatR;

namespace CloudService.Application.Setups.Commands;

public class Add
{
    public record Command(
        Guid UserId, 
        Guid CpuId,
        Guid RamId,
        Guid RomId,
        Guid IpId,
        Guid OsId) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.CpuId).NotEmpty();
            RuleFor(m => m.RamId).NotEmpty();
            RuleFor(m => m.RomId).NotEmpty();
            RuleFor(m => m.IpId).NotEmpty();
            RuleFor(m => m.OsId).NotEmpty();
        }
    }

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var user = await unitOfWork.Users.GetByIdAsync(command.UserId, isTrack: true, includeOrderDetails: true, cancellationToken)
                ?? throw new NotFoundException("User not found");
                   
            var cpu = await unitOfWork.Devices.GetCpuById(command.CpuId, cancellationToken)
                ?? throw new NotFoundException("Cpu not found");
                       
            var ram = await unitOfWork.Devices.GetRamById(command.RamId, cancellationToken)
                ?? throw new NotFoundException("Ram not found");
            
            var rom = await unitOfWork.Devices.GetRomById(command.RomId, cancellationToken)
                ?? throw new NotFoundException("Rom not found");

            var ip = await unitOfWork.Devices.GetIpById(command.IpId, cancellationToken)
                ?? throw new NotFoundException("Ip not found");
            
            var os = await unitOfWork.Devices.GetOsById(command.OsId, cancellationToken)
                ?? throw new NotFoundException("Os not found");

                var configuration = new Setup()
                {
                    UserId = command.UserId,
                    CpuId = command.CpuId,
                    RamId = command.RamId,
                    RomId = command.RomId,
                    IpId = command.IpId,
                    OsId = command.OsId,
                };
            
            try
            {
                user.Configs.Add(configuration);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (System.Exception)
            {

                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}