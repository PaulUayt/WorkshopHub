namespace WorkshopHub.Contract.Responses
{
    public class WorkshopWithSessionsResponse
    {
        public int WorkshopId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TimeSpan Duration { get; set; }
        public string CategoryName { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public List<SessionResponse> Sessions { get; set; } = new();
    }
}
