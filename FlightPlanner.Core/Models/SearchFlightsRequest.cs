namespace FlightPlanner.Core.Models
{
    public class SearchFlightsRequest
    {
        public required string From { get; set; }
        public required string To { get; set; }
        public required string DepartureDate { get; set; }
    }
}