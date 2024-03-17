using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationCleanup.ClearData;

public class ClearCommandHandler : IRequestHandler<ClearCommand, ServiceResult>
{
    private readonly ICleanupService _cleanupService;

    public ClearCommandHandler(ICleanupService cleanupService)
    {
        _cleanupService = cleanupService;
    }

    public Task<ServiceResult> Handle(ClearCommand request, CancellationToken cancellationToken)
    { 
        _cleanupService.Cleanup();

        return Task.FromResult(new ServiceResult());
    }
}