using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Queries
{
    public class GetWorkshopByIdQuery : IRequestHandler<int, WorkshopResponse>
    {
        private readonly WorkshopHubDbContext _context;

        public GetWorkshopByIdQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<WorkshopResponse> Handle(int workshopId, CancellationToken cancellationToken = default)
        {
            var workshop = await GetWorkshopAsync(workshopId, cancellationToken);

            if (workshop == null)
                return null;

            return new WorkshopResponse
            {
                WorkshopId = workshop.WorkshopId,
                Title = workshop.Title,
                Description = workshop.Description,
                Duration = workshop.Duration,
                CategoryName = workshop.Category.Name,
                TrainerName = workshop.Trainer.Name,
                SessionCount = workshop.Sessions.Count
            };
        }

        private async Task<Workshop> GetWorkshopAsync(int workshopId, CancellationToken cancellationToken = default) =>
            await _context.Workshops
            .Include(w => w.Trainer)
            .Include(w => w.Category)
            .Include(w => w.Sessions)
            .SingleOrDefaultAsync(w => w.WorkshopId == workshopId, cancellationToken);
    }
}
