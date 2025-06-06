using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

namespace WorkshopHub.Service.Queries
{
    public class GetWorkshopsQuery : IRequestHandler<List<WorkshopResponse>>
    {
        private readonly WorkshopHubDbContext _context;

        public GetWorkshopsQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkshopResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Workshops
                .AsNoTracking()
                .Include(w => w.Trainer)
                .Include(w => w.Category)
                .Select(w => new WorkshopResponse
                {
                    WorkshopId = w.WorkshopId,
                    Title = w.Title,
                    Description = w.Description,
                    Duration = w.Duration,
                    CategoryName = w.Category.Name,
                    TrainerName = w.Trainer.Name,
                    SessionCount = w.Sessions.Count,
                    CategoryResponse = new CategoryResponse
                    {
                        CategoryId = w.Category.CategoryId,
                        Name = w.Category.Name
                    },
                    TrainerResponse = new TrainerResponse
                    {
                        TrainerId = w.Trainer.TrainerId,
                        Name = w.Trainer.Name,
                        Email = w.Trainer.Email,
                        PhoneNumber = w.Trainer.PhoneNumber,
                        Bio = w.Trainer.Bio
                    }
                })
                .ToListAsync(cancellationToken);
        }
    }
}
