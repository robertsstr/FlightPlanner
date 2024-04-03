using FlightPlanner.Api.Extensions;
using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.MediationAirport.SearchAirport;
using FlightPlanner.UseCases.MediationFlights.GetFlight;
using FlightPlanner.UseCases.MediationSearchFlights.SearchFlights;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Api.Controllers;

[ApiController]
[Route("api")]
public class CustomerApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerApiController(IMediator mediator)
    {
        _mediator = mediator;
            
    }

    [HttpGet]
    [Route("airports")]
    public async Task<IActionResult> SearchAirport(string search)
    {
        return (await _mediator.Send(new SearchAirportQuery(search))).ToActionResult();
    }

    [HttpGet]
    [Route("flights/{id}")]
    public async Task<IActionResult> GetFlightById(int id)
    {
        return (await _mediator.Send(new GetFlightQuery(id))).ToActionResult();
    }

    [HttpPost]
    [Route("flights/search")]
    public async Task<IActionResult> SearchFlights(SearchFlightsRequest request)
    {
        return (await _mediator.Send(new SearchFlightsQuery(request))).ToActionResult();
    }
}