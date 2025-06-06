using Microsoft.EntityFrameworkCore;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Data.Context
{
    public class WorkshopHubDbContext : DbContext
    {
        public WorkshopHubDbContext(DbContextOptions<WorkshopHubDbContext> options) : base(options) { }

        public virtual DbSet<Workshop> Workshops { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
