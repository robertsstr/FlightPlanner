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

        public Airport AddAirport(Airport airport)
        {
            airport.AirportUsage = 1;
            _context.Airports.Add(airport);
            _context.SaveChanges();
            return airport;
        }

        public Airport? GetAirportByCode(string airportCode)
        {
            return _context.Airports.SingleOrDefault(a => a.AirportCode == airportCode);
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

        public void IncrementAirportUsage(int fromToId)
        {
            var airport = _context.Airports.FirstOrDefault(a => a.Id == fromToId);
            if (airport != null)
            {
                airport.AirportUsage++;
                _context.SaveChanges();
            }
        }

        public void DecrementAirportUsage(int fromToId)
        {
            var airport = _context.Airports.FirstOrDefault(a => a.Id == fromToId);
            if (airport != null)
            {
                if (airport.AirportUsage > 0)
                {
                    airport.AirportUsage--;
                    if (airport.AirportUsage == 0)
                    {
                        _context.Airports.Remove(airport);
                    }

                    _context.SaveChanges();
                }
            }
        }

        public void Clear()
        {
            _context.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }
    }
}