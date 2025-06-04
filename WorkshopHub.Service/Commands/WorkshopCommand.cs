using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands
{
    public class WorkshopCommand
    {
        public int WorkshopId { get; set; }
        public int CategoryId { get; set; }
        public int TrainerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duration { get; set; }

        public Workshop UpsertWorkshop() => new Workshop
        {
            WorkshopId = WorkshopId,
            CategoryId = CategoryId,
            TrainerId = TrainerId,
            Title = Title,
            Description = Description,
            Duration = Duration
        };
    }
}
