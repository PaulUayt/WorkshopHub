using System.ComponentModel.DataAnnotations;

namespace WorkshopHub.Contract.Requests
{
    public class UpsertTrainerRequest
    {
        public int TrainerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Bio { get; set; }
    }
}
