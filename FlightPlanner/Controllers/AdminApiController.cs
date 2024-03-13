using AutoMapper;
using FlightPlanner.Api.Dtos;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("admin-api")]

    public class AdminApiController : ControllerBase
    {
        private static readonly object _lockObject = new object();
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IValidator<AddFlightRequest> _validator;
        private readonly IMapper _mapper;

        public AdminApiController(IFlightService flightService, IMapper mapper,
            IValidator<AddFlightRequest> validator, IAirportService airportService)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (_lockObject)
            {
                var flight = _flightService.GetFullFlightById(id);
                if (flight == null)
                    return NotFound($"flight {id} does not exist.");
                    
                return Ok(_mapper.Map<FlightViewResponse>(flight));
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            lock (_lockObject)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var flight = _mapper.Map<Flight>(request);
                if (_flightService.HasDuplicateFlight(flight))
                 return Conflict("Duplicate request.");

                _flightService.CreateFlight(flight);
                return Created($"{flight.Id}", _mapper.Map<FlightViewResponse>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lockObject)
            {
                var flight = _flightService.GetById(id);
                if (flight != null)
                {
                    _flightService.Delete(flight);
                    _airportService.DeleteUnusedAirports();
                }

                return Ok();
            }
        }
    }
}
