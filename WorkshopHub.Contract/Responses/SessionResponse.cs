namespace WorkshopHub.Contract.Responses
{
    public class SessionResponse
    {
        public int SessionId { get; set; }

        public int WorkshopId { get; set; }

        public DateTime StartTime { get; set; }

        public string WorkshopTitle { get; set; }
    }
}
