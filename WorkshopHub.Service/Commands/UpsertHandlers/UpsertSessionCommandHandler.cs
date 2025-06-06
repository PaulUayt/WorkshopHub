﻿using Microsoft.EntityFrameworkCore;
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
                WorkshopTitle = await GetWorkshopTitleAsync(session.WorkshopId, cancellationToken),
                WorkshopResponse = session.Workshop != null ? new WorkshopResponse
                {
                    WorkshopId = session.Workshop.WorkshopId,
                    Title = session.Workshop.Title,
                    Duration = session.Workshop.Duration,
                    Description = session.Workshop.Description,
                    CategoryName = session.Workshop.Category != null ? await GetCategoryNameAsync(session.Workshop.Category.CategoryId, cancellationToken) : string.Empty,
                    TrainerName = session.Workshop.Trainer != null ? await GetTrainerNameAsync(session.Workshop.Trainer.TrainerId, cancellationToken) : string.Empty,
                    SessionCount = await GetSessionCountAsync(session.Workshop.WorkshopId, cancellationToken),
                } : null
            };
        }


        private async Task<Session> GetSessionAsync(int sessionId, CancellationToken cancellationToken = default) => 
            await _context.Sessions
                .Include(s => s.Workshop)
                    .ThenInclude(w => w.Category)
                .Include(s => s.Workshop)
                    .ThenInclude(w => w.Trainer)
                .SingleOrDefaultAsync(s => s.SessionId == sessionId, cancellationToken);


        private async Task<string> GetWorkshopTitleAsync(int workshopId, CancellationToken cancellationToken = default)
        {
            var workshop = await _context.Workshops
                .Where(w => w.WorkshopId == workshopId)
                .Select(w => w.Title)
                .SingleOrDefaultAsync(cancellationToken);
            return workshop ?? string.Empty;
        }

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
