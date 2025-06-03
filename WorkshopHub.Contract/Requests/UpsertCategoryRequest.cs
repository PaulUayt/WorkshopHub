using System.ComponentModel.DataAnnotations;


namespace WorkshopHub.Contract.Requests
{
    public class UpsertCategoryRequest
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

    }
}
