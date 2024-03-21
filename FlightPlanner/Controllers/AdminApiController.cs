using FlightPlanner.Api.Extensions;
using FlightPlanner.UseCases.Dtos;
using FlightPlanner.UseCases.MediationFlights.AddFlight;
using FlightPlanner.UseCases.MediationFlights.DeleteFlight;
using FlightPlanner.UseCases.MediationFlights.GetFlight;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Api.Controllers;

[Authorize]
[ApiController]
[Route("admin-api")]

public class AdminApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("flights/{id}")]
    public async Task<IActionResult> GetFlight(int id)
    {
        return (await _mediator.Send(new GetFlightQuery(id))).ToActionResult();
    }

    [HttpPut]
    [Route("flights")]
    public async Task<IActionResult> AddFlight(AddFlightRequest request)
    {
        return (await _mediator.Send(new AddFlightCommand(request))).ToActionResult();
    }

    [HttpDelete]
    [Route("flights/{id}")]
    public async Task<IActionResult> DeleteFlight(int id)
    {
        return (await _mediator.Send(new DeleteFlightCommand(id))).ToActionResult();
    }
}