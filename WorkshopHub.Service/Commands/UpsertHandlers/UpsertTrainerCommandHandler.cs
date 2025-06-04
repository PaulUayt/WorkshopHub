using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.UpsertHandlers
{
    public class UpsertTrainerCommandHandler : IRequestHandler<TrainerCommand, TrainerResponse>
    {
        private readonly WorkshopHubDbContext _context;

        public UpsertTrainerCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<TrainerResponse> Handle(TrainerCommand command, CancellationToken cancellationToken = default)
        {
            var trainer = await GetTrainerAsync(command.TrainerId, cancellationToken);
            if (trainer == null)
            {
                trainer = command.UpsertTrainer();
                await _context.Trainers.AddAsync(trainer, cancellationToken);
            }
            trainer.Name = command.Name;
            trainer.Bio = command.Bio;
            await _context.SaveChangesAsync(cancellationToken);
            return new TrainerResponse
            {
                TrainerId = trainer.TrainerId,
                Name = trainer.Name,
                Bio = trainer.Bio,
                WorkshopCount = await GetWorkshopCount(trainer.TrainerId, cancellationToken)
            };
        }

        private async Task<Trainer> GetTrainerAsync(int trainerId, CancellationToken cancellationToken = default) =>
            await _context.Trainers
            .Include(t => t.Workshops)
            .SingleOrDefaultAsync(t => t.TrainerId == trainerId, cancellationToken);

        private async Task<int> GetWorkshopCount(int trainerId, CancellationToken cancellationToken = default) =>
            await _context.Workshops
            .CountAsync(w => w.TrainerId == trainerId, cancellationToken);
        }
}
