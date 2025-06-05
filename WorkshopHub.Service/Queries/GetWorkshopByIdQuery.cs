using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

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
            return await _context.Workshops
                .AsNoTracking()
                .Where(w => w.WorkshopId == workshopId)
                .Select(w => new WorkshopResponse
                {
                    WorkshopId = w.WorkshopId,
                    Title = w.Title,
                    Description = w.Description,
                    Duration = w.Duration,
                    CategoryName = w.Category.Name,
                    TrainerName = w.Trainer.Name,
                    SessionCount = w.Sessions.Count
                })
                .SingleOrDefaultAsync(cancellationToken);
        }


    }
}
