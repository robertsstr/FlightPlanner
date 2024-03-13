namespace FlightPlanner.Core.Models
{
    public class SearchFlightsResponse
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public ICollection<Flight>? Items { get; set; }
    }
}