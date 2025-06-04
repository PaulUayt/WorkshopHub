using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands
{
    public class SessionCommand
    {
        public int SessionId { get; set; }
        public int WorkshopId { get; set; }
        public DateTime StartTime { get; set; }

        public Session UpsertSession() => new Session
        {
            SessionId = SessionId,
            WorkshopId = WorkshopId,
            StartTime = StartTime
        };
    }
}
