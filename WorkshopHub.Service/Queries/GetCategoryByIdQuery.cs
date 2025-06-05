using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;

namespace WorkshopHub.Service.Queries
{
    public class GetCategoryByIdQuery : IRequestHandler<int, CategoryResponse>
    {
        private readonly WorkshopHubDbContext _context;

        public GetCategoryByIdQuery(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new CategoryResponse
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    WorkshopCount = c.Workshops.Count
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }

}
