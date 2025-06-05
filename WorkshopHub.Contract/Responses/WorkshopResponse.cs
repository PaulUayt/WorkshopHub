namespace WorkshopHub.Contract.Responses
{
    public class WorkshopResponse
    {
        public int WorkshopId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duration { get; set; }
        public string CategoryName { get; set; }
        public string TrainerName { get; set; }
        public int SessionCount { get; set; }

        public CategoryResponse CategoryResponse { get; set; }

        public TrainerResponse TrainerResponse { get; set; }
    }

}
