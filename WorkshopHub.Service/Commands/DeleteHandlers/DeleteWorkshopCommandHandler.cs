using Microsoft.EntityFrameworkCore;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.DeleteHandlers
{
    public class DeleteWorkshopCommand
    {
        public int WorkshopId { get; set; }
    }

    public class DeleteWorkshopCommandHandler : IRequestHandler<DeleteWorkshopCommand, bool>
    {
        private readonly WorkshopHubDbContext _context;
        public DeleteWorkshopCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteWorkshopCommand command, CancellationToken cancellationToken = default)
        {
            var workshop = await GetWorkshopAsync(command.WorkshopId, cancellationToken);
            if (workshop != null)
            {
                _context.Remove(workshop);
                await _context.SaveChangesAsync(cancellationToken);
                return true; 
            }
            return false;
        }

        private async Task<Workshop> GetWorkshopAsync(int workshopId, CancellationToken cancellationToken = default) =>
            await _context.Workshops
            .Include(w => w.Trainer)
            .Include(w => w.Category)
            .SingleOrDefaultAsync(w => w.WorkshopId == workshopId, cancellationToken);
    }
}
