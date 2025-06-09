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
    public class SessionsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSessionsAsync([FromServices] IRequestHandler<List<SessionResponse>> getSessionsQuery)
        {
            return Ok(await getSessionsQuery.Handle());
        }

        [HttpGet("{sessionId:int}")]
        public async Task<IActionResult> GetSessionByIdAsync(int sessionId, [FromServices] IRequestHandler<int, SessionResponse> getSessionByIdQuery)
        {
            var session = await getSessionByIdQuery.Handle(sessionId);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertSessionAsync([FromServices] IRequestHandler<SessionCommand, SessionResponse> upsertSessionCommand,
            [FromBody] UpsertSessionRequest sessionRequest)
        {
            var session = await upsertSessionCommand.Handle(new SessionCommand
            {
                SessionId = sessionRequest.SessionId,
                WorkshopId = sessionRequest.WorkshopId,
                StartTime = sessionRequest.StartTime
            });
            return Ok(session);
        }

        [HttpDelete("{sessionId:int}")]
        public async Task<IActionResult> DeleteSessionAsync(int sessionId, [FromServices] IRequestHandler<DeleteSessionCommand, bool> deleteSessionByIdCommand)
        {
            var result = await deleteSessionByIdCommand.Handle(new DeleteSessionCommand { SessionId = sessionId });
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
