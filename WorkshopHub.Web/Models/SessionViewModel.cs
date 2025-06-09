using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WorkshopHub.Web.Models
{
    public class SessionViewModel
    {
        public int? SessionId { get; set; } // null → створення; значення → оновлення

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required]
        public int WorkshopId { get; set; }

        public List<SelectListItem> Workshops { get; set; } = new();
    }
}
