using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.UpsertHandlers
{
    public class UpsertSessionCommandHandler : IRequestHandler<SessionCommand, SessionResponse>
    {
        private readonly WorkshopHubDbContext _context;

        public UpsertSessionCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<SessionResponse> Handle(SessionCommand command, CancellationToken cancellationToken = default)
        {
            var session = await GetSessionAsync(command.SessionId, cancellationToken);
            if (session == null)
            {
                session = command.UpsertSession();
                await _context.Sessions.AddAsync(session, cancellationToken);
            }
            session.WorkshopId = command.WorkshopId;
            session.StartTime = command.StartTime;

            await _context.SaveChangesAsync(cancellationToken);
            return new SessionResponse
            {
                SessionId = session.SessionId,
                WorkshopId = session.WorkshopId,
                StartTime = session.StartTime,
                WorkshopTitle = await GetWorkshopTitleAsync(session.WorkshopId, cancellationToken)
            };
        }


        private async Task<Session> GetSessionAsync(int sessionId, CancellationToken cancellationToken = default) =>
            await _context.Sessions
            .Include(s => s.Workshop)
            .SingleOrDefaultAsync(s => s.SessionId == sessionId, cancellationToken);

        private async Task<string> GetWorkshopTitleAsync(int workshopId, CancellationToken cancellationToken = default)
        {
            var workshop = await _context.Workshops
                .Where(w => w.WorkshopId == workshopId)
                .Select(w => w.Title)
                .SingleOrDefaultAsync(cancellationToken);
            return workshop ?? string.Empty;
        }
}
