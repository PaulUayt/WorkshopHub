using Microsoft.EntityFrameworkCore;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.DeleteHandlers
{
    public class DeleteSessionCommand
    {
        public int SessionId { get; set; }
    }

    public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, bool>
    {
        private readonly WorkshopHubDbContext _context;

        public DeleteSessionCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSessionCommand command, CancellationToken cancellationToken = default)
        {
            var session = await GetSessionAsync(command.SessionId, cancellationToken);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        private async Task<Session> GetSessionAsync(int sessionId, CancellationToken cancellationToken = default) =>
            await _context.Sessions
            .Include(s => s.Workshop)
            .SingleOrDefaultAsync(s => s.SessionId == sessionId, cancellationToken);
    }
}
