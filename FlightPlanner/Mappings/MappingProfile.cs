using AutoMapper;
using FlightPlanner.Api.Dtos;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Airport, AirportViewModel>()
                .ForMember(destination => destination.Airport,
                    options => options
                        .MapFrom(source => source.AirportCode));
            CreateMap<AirportViewModel, Airport>()
                .ForMember(destination => destination.AirportCode,
                    options => options
                        .MapFrom(source => source.Airport));

            CreateMap<AddFlightRequest, Flight>();
            CreateMap<Flight, FlightViewResponse>();
        }
    }
}
