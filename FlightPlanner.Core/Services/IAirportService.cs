using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IAirportService : IEntityService<Airport>
{
    ICollection<Airport> GetAirportByKeyword(string searchPhrase);
    void DeleteUnusedAirports();
}