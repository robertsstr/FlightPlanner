using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationSearchFlights.SearchFlights;

public record SearchFlightsQuery(SearchFlightsRequest SearchFlightsRequest) : IRequest<ServiceResult>;