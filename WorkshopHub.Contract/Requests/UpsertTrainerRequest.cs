using System.ComponentModel.DataAnnotations;

namespace WorkshopHub.Contract.Requests
{
    public class UpsertTrainerRequest
    {
        public int TrainerId { get; set; } // null → створення, є значення → оновлення

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Bio { get; set; }
    }
}

