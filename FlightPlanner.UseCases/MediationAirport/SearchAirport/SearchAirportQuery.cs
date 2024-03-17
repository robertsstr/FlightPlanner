using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationAirport.SearchAirport;

public record SearchAirportQuery(string Search) : IRequest<ServiceResult>;