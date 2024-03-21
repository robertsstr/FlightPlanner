using System.Net;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Dtos;
using FluentValidation;
using MediatR;

namespace FlightPlanner.UseCases.MediationSearchFlights.SearchFlights;

public class SearchFlightsQueryHandler : IRequestHandler<SearchFlightsQuery, ServiceResult>
{
    private readonly ISearchFlightsService _searchFlightsService;
    private readonly IValidator<SearchFlightsRequest> _validator;


    public SearchFlightsQueryHandler(ISearchFlightsService searchFlightsService,
        IValidator<SearchFlightsRequest> validator)
    {
        _searchFlightsService = searchFlightsService;
        _validator = validator;
    }

    public Task<ServiceResult> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
    {
        var response = new ServiceResult();

        var validationResult = _validator.Validate(request.SearchFlightsRequest);
        if (!validationResult.IsValid)
        {
            response.ResultObject = validationResult.Errors;
            response.Status = HttpStatusCode.BadRequest;
            return Task.FromResult(response);
        }

        response.ResultObject = _searchFlightsService.SearchFlightsByRequest(request.SearchFlightsRequest);
        response.Status = HttpStatusCode.OK;
        return Task.FromResult(response);
    }
}