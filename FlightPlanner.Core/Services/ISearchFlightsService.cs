using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface ISearchFlightsService : IDbService
{
    SearchFlightsResponse SearchFlightsByRequest(SearchFlightsRequest request);
}