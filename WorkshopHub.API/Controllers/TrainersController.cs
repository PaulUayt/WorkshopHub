using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopHub.Contract.Requests;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Service;
using WorkshopHub.Service.Commands;
using WorkshopHub.Service.Commands.DeleteHandlers;

namespace WorkshopHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTrainersAsync([FromServices] IRequestHandler<List<TrainerResponse>> getTrainersQuery)
        {
            return Ok(await getTrainersQuery.Handle());
        }

        [HttpGet("{trainerId:int}")]
        public async Task<IActionResult> GetTrainerByIdAsync(int trainerId, [FromServices] IRequestHandler<int, TrainerResponse> getTrainerByIdQuery)
        {
            var trainer = await getTrainerByIdQuery.Handle(trainerId);
            if (trainer == null)
            {
                return NotFound();
            }
            return Ok(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertTrainerAsync([FromServices] IRequestHandler<TrainerCommand, TrainerResponse> upsertTrainerCommand,
            [FromBody] UpsertTrainerRequest trainerRequest)
        {
            var trainer = await upsertTrainerCommand.Handle(new TrainerCommand
            {
                TrainerId = trainerRequest.TrainerId,
                Name = trainerRequest.Name,
                Email = trainerRequest.Email,
                PhoneNumber = trainerRequest.PhoneNumber
            });
            return Ok(trainer);
        }

        [HttpDelete("{trainerId:int}")]
        public async Task<IActionResult> DeleteTrainerAsync(int trainerId, [FromServices] IRequestHandler<DeleteTrainerCommand, bool> deleteTrainerByIdCommand)
        {
            var result = await deleteTrainerByIdCommand.Handle(new DeleteTrainerCommand { TrainerId = trainerId });
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
