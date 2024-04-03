using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationFlights.AddFlight;

public record AddFlightCommand(AddFlightRequest AddFlightRequest) : IRequest<ServiceResult>;