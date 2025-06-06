using WorkshopHub.Data.Entities;

namespace WorkshopHub.Service.Commands
{
    public class CategoryCommand
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public Category UpsertCategory() => new Category
        {
            CategoryId = CategoryId,
            Name = Name,
            Workshops = new List<Workshop>()
        };
    }
}
