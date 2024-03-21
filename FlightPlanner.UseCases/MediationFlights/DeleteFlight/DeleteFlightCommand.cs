using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationFlights.DeleteFlight;

public record DeleteFlightCommand(int Id) : IRequest<ServiceResult>;