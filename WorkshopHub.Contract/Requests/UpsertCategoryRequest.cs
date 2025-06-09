using System.ComponentModel.DataAnnotations;


namespace WorkshopHub.Contract.Requests
{
    public class UpsertCategoryRequest
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}

