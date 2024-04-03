using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationCleanup.ClearData;

public record ClearCommand : IRequest<ServiceResult>;