using Microsoft.EntityFrameworkCore;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.DeleteHandlers
{
    public class DeleteTrainerCommand
    {
        public int TrainerId { get; set; }
    }

    public class DeleteTrainerCommandHandler : IRequestHandler<DeleteTrainerCommand, bool>
    {
        private readonly WorkshopHubDbContext _context;

        public DeleteTrainerCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTrainerCommand command, CancellationToken cancellationToken = default)
        {
            var trainer = await GetTrainerAsync(command.TrainerId, cancellationToken);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }

        private async Task<Trainer> GetTrainerAsync(int trainerId, CancellationToken cancellationToken = default) =>
            await _context.Trainers
            .Include(t => t.Workshops)
            .SingleOrDefaultAsync(t => t.TrainerId == trainerId, cancellationToken);
    }
}
