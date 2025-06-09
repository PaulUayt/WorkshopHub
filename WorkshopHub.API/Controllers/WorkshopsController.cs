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
    public class WorkshopsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetWorkshopsAsync([FromServices] IRequestHandler<List<WorkshopResponse>> getWorkshopsQuery)
        {
            return Ok(await getWorkshopsQuery.Handle());
        }

        [HttpGet("{workshopId:int}")]
        public async Task<IActionResult> GetWorkshopByIdAsync(int workshopId, [FromServices] IRequestHandler<int, WorkshopResponse> getWorkshopByIdQuery)
        {
            return Ok(await getWorkshopByIdQuery.Handle(workshopId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertWorkshopAsync([FromServices] IRequestHandler<WorkshopCommand, WorkshopResponse> upsertWorkshopCommand,
            [FromBody] UpsertWorkshopRequest workshopRequest)
        {
            var workshop = await upsertWorkshopCommand.Handle(new WorkshopCommand
            {
                WorkshopId = workshopRequest.WorkshopId,
                CategoryId = workshopRequest.CategoryId,
                TrainerId = workshopRequest.TrainerId,
                Title = workshopRequest.Title,
                Description = workshopRequest.Description,
                Duration = workshopRequest.Duration
            });

            return Ok(workshop);
        }

        [HttpDelete("{workshopId:int}")]
        public async Task<IActionResult> DeleteWorkshopAsync(int workshopId, [FromServices] IRequestHandler<DeleteWorkshopCommand, bool> deleteWorkshopbyIdCommand)
        {
            var result = await deleteWorkshopbyIdCommand.Handle(new DeleteWorkshopCommand { WorkshopId = workshopId });
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
