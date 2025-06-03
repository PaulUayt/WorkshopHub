using Microsoft.EntityFrameworkCore;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Data.Context
{
    public class WorkshopHubContext : DbContext
    {
        public WorkshopHubContext(DbContextOptions<WorkshopHubContext> options) : base(options) { }

        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }

}
