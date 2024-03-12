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
    }
}
