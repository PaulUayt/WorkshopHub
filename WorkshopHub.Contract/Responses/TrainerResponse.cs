namespace WorkshopHub.Contract.Responses
{
    public class TrainerResponse
    {
        public int TrainerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? Bio { get; set; }

        public int WorkshopCount { get; set; }
    }
}

