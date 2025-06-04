using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.UpsertHandlers
{
    public class UpsertWorkshopCommandHandler : IRequestHandler<WorkshopCommand, WorkshopResponse>
    {
        private readonly WorkshopHubDbContext _context;

        public UpsertWorkshopCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<WorkshopResponse> Handle(WorkshopCommand command, CancellationToken cancellationToken = default)
        {
            var workshop = await GetWorkshopAsync(command.WorkshopId, cancellationToken);
            if (workshop == null)
            {
                workshop = command.UpsertWorkshop();
                await _context.Workshops.AddAsync(workshop, cancellationToken);
            }

            workshop.Title = command.Title;
            workshop.Description = command.Description;
            workshop.Duration = command.Duration;
            workshop.CategoryId = command.CategoryId;
            workshop.TrainerId = command.TrainerId;

            await _context.SaveChangesAsync(cancellationToken);
            return new WorkshopResponse
            {
                Title = command.Title,
                Description = command.Description,
                Duration = command.Duration,
                CategoryName = await GetCategoryNameAsync(command.CategoryId, cancellationToken),
                TrainerName = await GetTrainerNameAsync(command.TrainerId, cancellationToken),
                SessionCount = await GetSessionCountAsync(command.WorkshopId, cancellationToken)
            };
        }

        private async Task<Workshop> GetWorkshopAsync(int workshopId, CancellationToken cancellationToken = default) =>
            await _context.Workshops
            .Include(w => w.Trainer)
            .Include(w => w.Category)
            .SingleOrDefaultAsync(w => w.WorkshopId == workshopId, cancellationToken);


        private async Task<string> GetCategoryNameAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories
                .Where(c => c.CategoryId == categoryId)
                .Select(c => c.Name)
                .SingleOrDefaultAsync(cancellationToken);
            return category ?? string.Empty;
        }

        private async Task<string> GetTrainerNameAsync(int trainerId, CancellationToken cancellationToken = default)
        {
            var trainer = await _context.Trainers
                .Where(t => t.TrainerId == trainerId)
                .Select(t => t.Name)
                .SingleOrDefaultAsync(cancellationToken);
            return trainer ?? string.Empty;
        }

        private async Task<int> GetSessionCountAsync(int workshopId, CancellationToken cancellationToken = default) =>
            await _context.Sessions
            .CountAsync(s => s.WorkshopId == workshopId, cancellationToken);
    }
}
