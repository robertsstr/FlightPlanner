using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class SearchFlightsService : DbService, ISearchFlightsService
    {
        public SearchFlightsService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public SearchFlightsResponse SearchFlightsByRequest(SearchFlightsRequest request)
        {
            var flights = _context.Flights
                .Include(flight => flight.To)
                .Include(flight => flight.From)
                .Where(flight =>
                    flight.DepartureTime.Substring(0, 10) == request.DepartureDate &&
                    flight.From.AirportCode == request.From &&
                    flight.To.AirportCode == request.To)
                .ToList();

            var response = new SearchFlightsResponse();
            int pageSize = 10;
            int totalItems = flights.Count;
            int page = (totalItems + pageSize - 1) / pageSize;

            response.Page = page;
            response.TotalItems = totalItems;
            response.Items = flights;

            return response;
        }
    }
}
