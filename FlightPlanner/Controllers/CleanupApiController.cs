using FlightPlanner.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]

    public class CleanupApiController : ControllerBase
    {
        private readonly FlightService _flightService;
        private readonly AirportService _airportService;

        public CleanupApiController(FlightService flightService, AirportService airportService)
        {
            _flightService = flightService;
            _airportService = airportService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightService.Clear();
            _airportService.Clear();
            return Ok();
        }
    }
}
