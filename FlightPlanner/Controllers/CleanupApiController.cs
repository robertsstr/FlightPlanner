using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Api.Controllers
{
    [ApiController]
    [Route("testing-api")]

    public class CleanupApiController : ControllerBase
    {
        private readonly ICleanupService _cleanupService;

        public CleanupApiController(ICleanupService cleanupService)
        {
            _cleanupService = cleanupService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _cleanupService.Cleanup();
            return Ok();
        }
    }
}
