using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

namespace WorkshopHub.Service.Queries
{
    public class GetCategoriesQuery : IRequestHandler<List<CategoryResponse>>
    {
        private readonly WorkshopHubDbContext _context;
        public GetCategoriesQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .Include(c => c.Workshops)
                .Select(c => new CategoryResponse
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    WorkshopCount = c.Workshops.Count
                })
                .ToListAsync(cancellationToken);
        }
    }
}
