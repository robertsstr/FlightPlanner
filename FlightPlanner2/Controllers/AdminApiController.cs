using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("admin-api")]

    public class AdminApiController : ControllerBase
    {
        private static readonly object _lockObject = new object();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            Flight flight = FlightStorage.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
            lock (_lockObject)
            {
                if (FlightStorage.HasDuplicateFlight(flight))
                {
                    return Conflict("Duplicate flight.");
                }

                if (string.IsNullOrEmpty(flight.From.Country) ||
                    string.IsNullOrEmpty(flight.From.City) ||
                    string.IsNullOrEmpty(flight.From.AirportCode) ||
                    string.IsNullOrEmpty(flight.To.Country) ||
                    string.IsNullOrEmpty(flight.To.City) ||
                    string.IsNullOrEmpty(flight.To.AirportCode) ||
                    string.IsNullOrEmpty(flight.Carrier) ||
                    string.IsNullOrEmpty(flight.DepartureTime) ||
                    string.IsNullOrEmpty(flight.ArrivalTime))
                {
                    return BadRequest();
                }

                if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
                {
                    return BadRequest("Same airport");
                }

                if (DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime))
                {
                    return BadRequest("Strange date");
                }

                FlightStorage.AddFlight(flight);
                return Created($"{flight.Id}", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lockObject)
            {
                FlightStorage.RemoveFlight(id);
                return Ok();
            }
        }
    }
}
