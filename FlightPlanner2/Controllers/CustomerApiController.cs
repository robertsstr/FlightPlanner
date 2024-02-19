using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var airport = AirportStorage.GetAirport(search);
            return Ok(airport);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            Flight flight = FlightStorage.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest request)
        {
            if (request.From == null || request.To == null || request.DepartureDate == null)
            {
                return BadRequest("Invalid request");
            }

            if(request.From == request.To)
            {
                return BadRequest("Same airports");
            }

            var flights = FlightStorage.GetFlights(request);
            var pageNumber = !flights.Any() ? 0 : 1;

            var response = new
            {
                items = flights.ToList(),
                page = pageNumber,
                totalItems = flights.Count()
            };
            return Ok(response);
        }
    }
}
