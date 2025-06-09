using System.ComponentModel.DataAnnotations;

namespace WorkshopHub.Web.Models
{
    public class CategoryViewModel
    {
        public int? CategoryId { get; set; } // null → створення

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int WorkshopCount { get; set; }
    }
}
