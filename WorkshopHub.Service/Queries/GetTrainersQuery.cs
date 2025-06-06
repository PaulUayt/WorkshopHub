using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

namespace WorkshopHub.Service.Queries
{
    public class GetTrainersQuery : IRequestHandler<List<TrainerResponse>>
    {
        private readonly WorkshopHubDbContext _context;

        public GetTrainersQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainerResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Trainers
                .Select(t => new TrainerResponse
                {
                    TrainerId = t.TrainerId,
                    Name = t.Name,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    WorkshopCount = t.Workshops.Count
                })
                .ToListAsync(cancellationToken);
        }
    }
}
