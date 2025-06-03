using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WorkshopHub.Data.Entities
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public int SessionId { get; set; }

        [Required]
        public int WorkshopId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        public virtual Workshop Workshop { get; set; }
    }
}
