using FlightPlanner.Models;
using FlightPlanner.Service;
using FlightPlanner.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("admin-api")]

    public class AdminApiController : ControllerBase
    {
        private static readonly object LockObject = new object();
        private readonly FlightService _flightService;

        public AdminApiController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFlightById(id);
            return flight == null ? NotFound() : Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
            lock (LockObject)
            {
                if (_flightService.HasDuplicateFlight(flight))
                {
                    return Conflict("Duplicate flight.");
                }

                if (FlightValidator.IsFlightDataValid(flight))
                {
                    return BadRequest("Invalid flight data.");
                }

                if (FlightValidator.AreAirportsDifferent(flight))
                {
                    return BadRequest("Same airport.");
                }

                if (FlightValidator.IsDateValid(flight))
                {
                    return BadRequest("Strange date.");
                }

                _flightService.AddFlight(flight);
                return Created($"{flight.Id}", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (LockObject)
            {
                _flightService.RemoveFlightById(id);
                return Ok();
            }
        }
    }
}
