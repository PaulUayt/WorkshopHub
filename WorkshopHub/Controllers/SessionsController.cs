using Microsoft.AspNetCore.Mvc;
using WorkshopHub.Contract.Responses;
using WorkshopHub.Data.Context;
using WorkshopHub.Service.Commands.DeleteHandlers;
using WorkshopHub.Service.Commands;
using WorkshopHub.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Web.Controllers
{
    public class SessionsController : Controller
    {
        private readonly IRequestHandler<List<SessionResponse>> _getSessionsHandler;
        private readonly IRequestHandler<List<WorkshopResponse>> _getWorkshopsHandler;
        private readonly IRequestHandler<SessionCommand, SessionResponse> _upsertSessionHandler;
        private readonly IRequestHandler<DeleteSessionCommand, bool> _deleteSessionHandler;
        private readonly WorkshopHubDbContext _context;

        public SessionsController(
            IRequestHandler<List<SessionResponse>> getSessionsHandler,
            IRequestHandler<List<WorkshopResponse>> getWorkshopsHandler,
            IRequestHandler<SessionCommand, SessionResponse> upsertSessionHandler,
            IRequestHandler<DeleteSessionCommand, bool> deleteSessionHandler,
            WorkshopHubDbContext context)
        {
            _getSessionsHandler = getSessionsHandler;
            _getWorkshopsHandler = getWorkshopsHandler;
            _upsertSessionHandler = upsertSessionHandler;
            _deleteSessionHandler = deleteSessionHandler;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var sessions = await _getSessionsHandler.Handle();
            return View(sessions);
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            var workshops = await _getWorkshopsHandler.Handle();
            ViewBag.Workshops = new SelectList(workshops, "WorkshopId", "Title");

            if (id == null)
            {
                return View(new SessionCommand());
            }

            var session = await _context.Sessions.FindAsync(id.Value);
            if (session == null)
            {
                return NotFound();
            }
            return View(new SessionCommand
            {
                SessionId = session.SessionId,
                StartTime = session.StartTime,
                WorkshopId = session.WorkshopId
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SessionCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _upsertSessionHandler.Handle(command);
                return RedirectToAction(nameof(Index));
            }
            var workshops = await _getWorkshopsHandler.Handle();
            ViewBag.Workshops = new SelectList(workshops, "WorkshopId", "Title");

            return View(command); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSessionCommand { SessionId = id };
            var result = await _deleteSessionHandler.Handle(command);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

    }
}
