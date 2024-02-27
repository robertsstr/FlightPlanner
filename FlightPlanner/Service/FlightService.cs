using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Service
{
    public class FlightService
    {
        private readonly FlightPlannerDbContext _context;
        private AirportService _airportService;

        public FlightService(FlightPlannerDbContext context, AirportService airportService)
        {
            _context = context;
            _airportService = airportService;
        }

        public void AddFlight(Flight flight)
        {
            var fromAirport = _airportService.GetAirportByCode(flight.From.AirportCode);
            if (fromAirport == null)
            {
                fromAirport = _airportService.AddAirport(flight.From);
            }
            else
            {
                _airportService.IncrementAirportUsage(fromAirport.Id);
            }

            var toAirport = _airportService.GetAirportByCode(flight.To.AirportCode);
            if (toAirport == null)
            {
                toAirport = _airportService.AddAirport(flight.To);
            }
            else
            {
                _airportService.IncrementAirportUsage(toAirport.Id);
            }

            flight.From = fromAirport;
            flight.To = toAirport;

            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public Flight? GetFlightById(int id)
        {
            return _context.Flights
                .Include(flight => flight.To)
                .Include(flight => flight.From)
                .SingleOrDefault(flight => flight.Id == id); ;
        }

        public SearchFlightResponse GetFlights(SearchFlightsRequest request)
        {
             var flights = _context.Flights
                .Include(flight => flight.To)
                .Include(flight => flight.From)
                .Where(flight =>
                    flight.DepartureTime.Substring(0, 10) == request.DepartureDate &&
                    flight.From.AirportCode == request.From &&
                    flight.To.AirportCode == request.To)
                .ToList();

             var response = new SearchFlightResponse();
             int pageSize = 10;
             int totalItems = flights.Count;
             int page = (totalItems + pageSize - 1) / pageSize;

             response.Page = page;
             response.TotalItems = totalItems;
             response.Items = flights;

             return response;
        }

        public bool HasDuplicateFlight(Flight flight)
        {
            return _context.Flights.Any(f =>
                f.From.AirportCode == flight.From.AirportCode &&
                f.To.AirportCode == flight.To.AirportCode &&
                f.Carrier == flight.Carrier &&
                f.DepartureTime == flight.DepartureTime &&
                f.ArrivalTime == flight.ArrivalTime);
        }

        public void RemoveFlightById(int id)
        {
            var flightToRemove = _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .FirstOrDefault(flight => flight.Id == id);

            if (flightToRemove != null)
            {
                _context.Flights.Remove(flightToRemove);
                _context.SaveChanges();

                _airportService.DecrementAirportUsage(flightToRemove.FromId);
                _airportService.DecrementAirportUsage(flightToRemove.ToId);
            }
        }

        public void Clear()
        {
            _context.RemoveRange(_context.Flights);
            _context.SaveChanges();
        }
    }
}
