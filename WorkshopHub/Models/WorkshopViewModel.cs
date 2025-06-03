namespace WorkshopHub.Web.Models
{
    public class WorkshopViewModel
    {
        public int WorkshopId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Duration { get; set; }

        public string CategoryName { get; set; }

        public string TrainerFullName { get; set; }

        public int SessionCount { get; set; }
    }
}

