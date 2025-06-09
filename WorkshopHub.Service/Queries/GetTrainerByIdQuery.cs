using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

namespace WorkshopHub.Service.Queries
{
    public class GetTrainerByIdQuery : IRequestHandler<int, TrainerResponse>
    {
        private readonly WorkshopHubDbContext _context;

        public GetTrainerByIdQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<TrainerResponse> Handle(int trainerId, CancellationToken cancellationToken)
        {
            return await _context.Trainers
                .AsNoTracking()
                .Where(t => t.TrainerId == trainerId)
                .Select(t => new TrainerResponse
                {
                    TrainerId = t.TrainerId,
                    Name = t.Name,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    WorkshopCount = t.Workshops.Count
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
