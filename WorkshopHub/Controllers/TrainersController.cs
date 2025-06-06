using Microsoft.AspNetCore.Mvc;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Service.Commands.DeleteHandlers;
using WorkshopHub.Service.Commands;
using WorkshopHub.Service;
using Microsoft.EntityFrameworkCore;

namespace WorkshopHub.Web.Controllers
{
    public class TrainersController : Controller
    {
        private readonly IRequestHandler<List<TrainerResponse>> _getTrainerHandler;
        private readonly IRequestHandler<TrainerCommand, TrainerResponse> _upsertTrainerHandler;
        private readonly IRequestHandler<DeleteTrainerCommand, bool> _deleteTrainerHandler;
        private readonly WorkshopHubDbContext _context;

        public TrainersController(
            IRequestHandler<List<TrainerResponse>> getTrainerHandler,
            IRequestHandler<TrainerCommand, TrainerResponse> upsertTrainerHandler,
            IRequestHandler<DeleteTrainerCommand, bool> deleteTrainerHandler,
            WorkshopHubDbContext context)
        {
            _getTrainerHandler = getTrainerHandler;
            _upsertTrainerHandler = upsertTrainerHandler;
            _deleteTrainerHandler = deleteTrainerHandler;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _getTrainerHandler.Handle();
            return View(trainers);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null)
            {
                return View(new TrainerResponse());
            }

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            var trainerResponse = new TrainerResponse
            {
                TrainerId = trainer.TrainerId,
                Name = trainer.Name,
                Email = trainer.Email,
                PhoneNumber = trainer.PhoneNumber,
                Bio = trainer.Bio
            };

            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrainerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _upsertTrainerHandler.Handle(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteTrainerCommand { TrainerId = id };
            var result = await _deleteTrainerHandler.Handle(command);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
