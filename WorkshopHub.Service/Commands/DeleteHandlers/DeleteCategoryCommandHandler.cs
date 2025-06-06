using Microsoft.EntityFrameworkCore;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands.DeleteHandlers
{
    public class DeleteCategoryCommand
    {
        public int CategoryId { get; set; }
    }


    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly WorkshopHubDbContext _context;

        public DeleteCategoryCommandHandler(WorkshopHubDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(command.CategoryId, cancellationToken);

            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            return false;
        }

        private async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default) =>
            await _context.Categories
            .Include(c => c.Workshops)
            .SingleOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);
    }
}
