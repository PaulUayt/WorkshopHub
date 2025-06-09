using System.ComponentModel.DataAnnotations;

namespace WorkshopHub.Contract.Requests
{
    public class UpsertSessionRequest
    {
        public int SessionId { get; set; }

        [Required]
        public int WorkshopId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
    }
}
