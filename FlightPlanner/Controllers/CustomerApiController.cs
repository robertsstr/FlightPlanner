using AutoMapper;
using FlightPlanner.Api.Dtos;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly ISearchFlightsService _searchFlightsService;
        private readonly IValidator<SearchFlightsRequest> _validator;
        private readonly IMapper _mapper;

        public CustomerApiController(IFlightService flightService, IAirportService airportService,
            IMapper mapper, ISearchFlightsService searchFlightsService, IValidator<SearchFlightsRequest> validator)
        {
            _flightService = flightService;
            _airportService = airportService;
            _searchFlightsService = searchFlightsService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var airports = _airportService.GetAirportByKeyword(search);
            return Ok(_mapper.Map<List<AirportViewModel>>(airports));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight == null)
                return NotFound($"flight {id} does not exist.");

            return Ok(_mapper.Map<FlightViewResponse>(flight));
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = _searchFlightsService.SearchFlightsByRequest(request);
            return Ok(response);
        }
    }
}
