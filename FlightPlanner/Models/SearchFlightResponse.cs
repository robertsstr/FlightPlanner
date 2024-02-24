namespace FlightPlanner.Models
{
    public class SearchFlightResponse
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }
    }
}
