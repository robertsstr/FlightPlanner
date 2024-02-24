using FlightPlanner.Models;
using FlightPlanner.Service;
using FlightPlanner.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerApiController : ControllerBase
    {
        private readonly FlightService _flightService;
        private readonly AirportService _airportService;


        public CustomerApiController(FlightService flightService, AirportService airportService)
        {
            _flightService = flightService;
            _airportService = airportService;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var airport = _airportService.GetAirport(search);
            return Ok(airport);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFlightById(id);
            return flight == null ? NotFound() : Ok(flight);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest request)
        {
            if (SearchFlightValidator.IsSearchFlightDataValid(request))
            {
                return BadRequest("Invalid request data.");
            }

            if (SearchFlightValidator.AreAirportsDifferent(request))
            {
                return BadRequest("Same airports.");
            }

            var response = _flightService.GetFlights(request);
            return Ok(response);
        }
    }
}
