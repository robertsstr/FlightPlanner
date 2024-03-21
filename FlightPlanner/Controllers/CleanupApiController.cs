using FlightPlanner.Api.Extensions;
using FlightPlanner.UseCases.MediationCleanup.ClearData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Api.Controllers;

[ApiController]
[Route("testing-api")]

public class CleanupApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public CleanupApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("clear")]
    public async Task<IActionResult> Clear()
    {
        return (await _mediator.Send(new ClearCommand())).ToActionResult();
    }
}