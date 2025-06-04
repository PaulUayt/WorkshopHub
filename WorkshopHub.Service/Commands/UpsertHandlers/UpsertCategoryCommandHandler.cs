using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.UpsertHandlers
{
    public class UpsertCategoryCommandHandler : IRequestHandler<CategoryCommand, CategoryResponse>
    {
        private readonly WorkshopHubDbContext _context;
        public UpsertCategoryCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryResponse> Handle(CategoryCommand command, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(command.CategoryId, cancellationToken);

            if (category == null)
            {
                category = command.UpsertCategory();
                await _context.Categories.AddAsync(category, cancellationToken);
            }
            
            category.Name = command.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return new CategoryResponse
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                WorkshopCount = category.Workshops.Count
            };
        }

        private async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default) => 
            await _context.Categories
            .Include(c => c.Workshops)
            .SingleOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);

    }
}
