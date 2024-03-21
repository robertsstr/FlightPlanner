using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationFlights.GetFlight;

public record GetFlightQuery(int Id) : IRequest<ServiceResult>;