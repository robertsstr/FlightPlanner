using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public ICollection<Airport> GetAirportByKeyword(string searchPhrase)
        {
            return _context.Airports
                .Where(a =>
                    a.Country.ToUpper().Contains(searchPhrase) ||
                    a.City.ToUpper().Contains(searchPhrase) ||
                    a.AirportCode.ToUpper().Contains(searchPhrase))
                .ToList();
        }
    }
}
