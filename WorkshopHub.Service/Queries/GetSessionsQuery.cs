using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

namespace WorkshopHub.Service.Queries
{
    public class GetSessionsQuery : IRequestHandler<List<SessionResponse>>
    {
        private readonly WorkshopHubDbContext _context;

        public GetSessionsQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<List<SessionResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .Include(s => s.Workshop)
                    .ThenInclude(w => w.Category)
                .Include(s => s.Workshop)
                    .ThenInclude(w => w.Trainer)
                .Select(s => new SessionResponse
                {
                    SessionId = s.SessionId,
                    WorkshopId = s.WorkshopId,
                    StartTime = s.StartTime,
                    WorkshopTitle = s.Workshop.Title,
                    WorkshopResponse = new WorkshopResponse
                    {
                        WorkshopId = s.Workshop.WorkshopId,
                        Title = s.Workshop.Title,
                        Duration = s.Workshop.Duration,
                        Description = s.Workshop.Description,
                        CategoryName = s.Workshop.Category.Name,
                        TrainerName = s.Workshop.Trainer.Name,
                        SessionCount = _context.Sessions.Count(ss => ss.WorkshopId == s.WorkshopId)
                    }
                })
                .ToListAsync(cancellationToken);
        }
    }
}
