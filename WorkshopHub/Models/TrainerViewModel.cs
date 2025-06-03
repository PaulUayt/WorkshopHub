using System.ComponentModel.DataAnnotations;

namespace WorkshopHub.Web.Models
{
    public class TrainerViewModel
    {
        public int? TrainerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        public int WorkshopCount { get; set; }
    }
}
