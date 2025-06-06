using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Contract.Requests;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Service;
using WorkshopHub.Service.Commands;
using WorkshopHub.Service.Commands.DeleteHandlers;

namespace WorkshopHub.Web.Controllers
{
    public class WorkshopsController : Controller
    {
        private readonly IRequestHandler<List<WorkshopResponse>> _getWorkshopsHandler;
        private readonly IRequestHandler<List<CategoryResponse>> _getCategoriesHandler;
        private readonly IRequestHandler<List<TrainerResponse>> _getTrainersHandler;
        private readonly IRequestHandler<WorkshopCommand, WorkshopResponse> _upsertWorkshopHandler;
        private readonly IRequestHandler<DeleteWorkshopCommand, bool> _deleteWorkshopHandler;
        private readonly WorkshopHubDbContext _context;

        public WorkshopsController(
            IRequestHandler<List<WorkshopResponse>> getWorkshopsHandler,
            IRequestHandler<WorkshopCommand, WorkshopResponse> upsertWorkshopHandler,
            IRequestHandler<DeleteWorkshopCommand, bool> deleteWorkshopHandler,
            WorkshopHubDbContext context)
        {
            _getWorkshopsHandler = getWorkshopsHandler;
            _upsertWorkshopHandler = upsertWorkshopHandler;
            _deleteWorkshopHandler = deleteWorkshopHandler;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var workshops = await _getWorkshopsHandler.Handle();
            return View(workshops);
        }

        // GET: Upsert
        public async Task<IActionResult> Upsert(int? id)
        {
            var categories = await _context.Categories.ToListAsync();
            var trainers = await _context.Trainers.ToListAsync();

            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "Name");
            ViewBag.TrainerId = new SelectList(trainers, "TrainerId", "Name");


            if (id == null)
                return View(new WorkshopCommand());

            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null)
                return NotFound();

            return View(new WorkshopCommand
            {
                WorkshopId = workshop.WorkshopId,
                Title = workshop.Title,
                Description = workshop.Description,
                Duration = workshop.Duration,
                CategoryId = workshop.CategoryId,
                TrainerId = workshop.TrainerId
            });
        }

        // POST: Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(WorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Trainers = await _context.Trainers.ToListAsync();
                return View(command);
            }

            await _upsertWorkshopHandler.Handle(command);
            return RedirectToAction(nameof(Index));
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deleteWorkshopHandler.Handle(new DeleteWorkshopCommand { WorkshopId = id });
            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
