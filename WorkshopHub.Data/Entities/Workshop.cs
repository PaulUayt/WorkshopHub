using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WorkshopHub.Data.Entities
{
    [Table("Workshop")]
    public class Workshop
    {
        [Key]
        public int WorkshopId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int TrainerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }

        public virtual Category Category { get; set; }
        public virtual Trainer Trainer { get; set; }

        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    }
}
