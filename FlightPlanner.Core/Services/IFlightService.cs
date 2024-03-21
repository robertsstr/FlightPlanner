using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IFlightService : IEntityService<Flight>
{
    Flight? GetFullFlightById(int id);
    bool HasDuplicateFlight(Flight flight); 
    Flight CreateFlight(Flight flight);
}