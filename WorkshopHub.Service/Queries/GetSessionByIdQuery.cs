using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

namespace WorkshopHub.Service.Queries
{
    public class GetSessionByIdQuery : IRequestHandler<int, SessionResponse>
    {
        private readonly WorkshopHubDbContext _context;
        public GetSessionByIdQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }
        public async Task<SessionResponse> Handle(int sessionId, CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Where(s => s.SessionId == sessionId)
                .Select(s => new SessionResponse
                {
                    SessionId = s.SessionId,
                    WorkshopId = s.WorkshopId,
                    StartTime = s.StartTime,
                    WorkshopTitle = s.Workshop.Title,
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
