using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands
{
    public class TrainerCommand
    {
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }

        public Trainer UpsertTrainer() => new Trainer
        {
            TrainerId = TrainerId,
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Bio = Bio
        };
    }
}
