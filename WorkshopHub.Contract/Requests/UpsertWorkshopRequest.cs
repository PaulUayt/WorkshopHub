using System.ComponentModel.DataAnnotations;


namespace WorkshopHub.Contract.Requests
{
    public class UpsertWorkshopRequest
    {
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
        [DataType(DataType.DateTime)]
        public DateTime Duration { get; set; }
    }
}
