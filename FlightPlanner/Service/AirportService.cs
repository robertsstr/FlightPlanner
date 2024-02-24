using FlightPlanner.Models;

namespace FlightPlanner.Service
{
    public class AirportService
    {
        private readonly FlightPlannerDbContext _context;

        public AirportService(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public List<Airport>? GetAirport(string search)
        {
            search = search.ToUpper().Trim();

            return _context.Airports
                .Where(a =>
                    a.Country.ToUpper().Contains(search) ||
                    a.City.ToUpper().Contains(search) ||
                    a.AirportCode.ToUpper().Contains(search))
                .ToList();
        }

        public void Clear()
        {
            _context.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }
    }
}
