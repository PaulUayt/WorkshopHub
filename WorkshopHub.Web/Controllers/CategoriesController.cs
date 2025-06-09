using Microsoft.AspNetCore.Mvc;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Service.Commands.DeleteHandlers;
using WorkshopHub.Service.Commands;
using WorkshopHub.Service;

namespace WorkshopHub.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRequestHandler<List<CategoryResponse>> _getCategoriesHandler;
        private readonly IRequestHandler<CategoryCommand, CategoryResponse> _upsertCategoryHandler;
        private readonly IRequestHandler<DeleteCategoryCommand, bool> _deleteCategoryHandler;
        private readonly WorkshopHubDbContext _context;


        public CategoriesController(
            IRequestHandler<List<CategoryResponse>> getCategoriesHandler,
            IRequestHandler<CategoryCommand, CategoryResponse> upsertCategoryHandler,
            IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryHandler,
            WorkshopHubDbContext context)
        {
            _getCategoriesHandler = getCategoriesHandler;
            _upsertCategoryHandler = upsertCategoryHandler;
            _deleteCategoryHandler = deleteCategoryHandler;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var categories = await _getCategoriesHandler.Handle();
            return View(categories);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null)
            {
                return View(new CategoryCommand());
            }

            var category = await _context.Categories.FindAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(new CategoryCommand
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(CategoryCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _upsertCategoryHandler.Handle(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCategoryCommand { CategoryId = id };
            var result = await _deleteCategoryHandler.Handle(command);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
