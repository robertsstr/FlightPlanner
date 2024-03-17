using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class AirportService : EntityService<Airport>, IAirportService
{
    private static readonly object _lock = new object();

    public AirportService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public ICollection<Airport> GetAirportByKeyword(string search)
    {
        search = search.ToUpper().Trim();

        return _context.Airports
            .Where(a =>
                a.Country.ToUpper().Contains(search) ||
                a.City.ToUpper().Contains(search) ||
                a.AirportCode.ToUpper().Contains(search))
            .ToList();
    }

    public void DeleteUnusedAirports()
    {
        lock (_lock)
        {
            var unusedAirports = _context.Airports
                .Where(a => !_context.Flights.Any(f => f.From.Id == a.Id || f.To.Id == a.Id))
                .ToList();

            _context.Airports.RemoveRange(unusedAirports);
            _context.SaveChanges();
        }
    }
}