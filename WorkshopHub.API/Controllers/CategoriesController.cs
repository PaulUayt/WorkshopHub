using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopHub.Contract.Requests;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Service;
using WorkshopHub.Service.Commands;
using WorkshopHub.Service.Commands.DeleteHandlers;

namespace WorkshopHub.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync([FromServices] IRequestHandler<List<CategoryResponse>> getCategoriesQuery)
        {
            return Ok(await getCategoriesQuery.Handle());
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryResponse> getCategoryByIdQuery)
        {
            var category = await getCategoryByIdQuery.Handle(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCategoryAsync([FromServices] IRequestHandler<CategoryCommand, CategoryResponse> upsertCategoryCommand,
            [FromBody] UpsertCategoryRequest categoryRequest)
        {
            var category = await upsertCategoryCommand.Handle(new CategoryCommand
            {
                CategoryId = categoryRequest.CategoryId,
                Name = categoryRequest.Name
            });
            return Ok(category);
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(int categoryId, [FromServices] IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryByIdCommand)
        {
            var result = await deleteCategoryByIdCommand.Handle(new DeleteCategoryCommand { CategoryId = categoryId });
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
