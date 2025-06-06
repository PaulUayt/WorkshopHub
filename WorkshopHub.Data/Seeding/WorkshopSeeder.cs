using System;
using System.Linq;
using System.Threading.Tasks;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Entities;

namespace WorkshopHub.Data.Seeding
{
    public static class WorkshopSeeder
    {
        public static async Task SeedAsync(WorkshopHubDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Програмування" },
                    new Category { Name = "Дизайн" },
                    new Category { Name = "Бізнес" },
                    new Category { Name = "Маркетинг" },
                    new Category { Name = "Фотографія" }
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            if (!context.Trainers.Any())
            {
                var trainers = new[]
                {
                    new Trainer { Name = "Іван Петренко", Email = "ivan@example.com", PhoneNumber = "+380501234567", Bio = "Senior .NET Developer." },
                    new Trainer { Name = "Олена Шевченко", Email = "olena@example.com", PhoneNumber = "+380931112233", Bio = "UI/UX Designer." },
                    new Trainer { Name = "Максим Коваль", Email = "maksym@example.com", PhoneNumber = "+380671234567", Bio = "Маркетолог з 10-річним досвідом." },
                    new Trainer { Name = "Світлана Литвин", Email = "svitlana@example.com", PhoneNumber = "+380631112233", Bio = "Професійний фотограф." }
                };
                context.Trainers.AddRange(trainers);
                await context.SaveChangesAsync();
            }

            if (!context.Workshops.Any())
            {
                var progCategoryId = context.Categories.First(c => c.Name == "Програмування").CategoryId;
                var designCategoryId = context.Categories.First(c => c.Name == "Дизайн").CategoryId;
                var marketingCategoryId = context.Categories.First(c => c.Name == "Маркетинг").CategoryId;
                var photoCategoryId = context.Categories.First(c => c.Name == "Фотографія").CategoryId;

                var ivanId = context.Trainers.First(t => t.Name == "Іван Петренко").TrainerId;
                var olenaId = context.Trainers.First(t => t.Name == "Олена Шевченко").TrainerId;
                var maksimId = context.Trainers.First(t => t.Name == "Максим Коваль").TrainerId;
                var svitlanaId = context.Trainers.First(t => t.Name == "Світлана Литвин").TrainerId;

                var workshops = new[]
                {
                    new Workshop
                    {
                        Title = "ASP.NET Core для початківців",
                        Description = "Опануйте основи веб-розробки на .NET",
                        Duration = TimeSpan.FromHours(4),
                        CategoryId = progCategoryId,
                        TrainerId = ivanId
                    },
                    new Workshop
                    {
                        Title = "UI/UX дизайн з нуля",
                        Description = "Практичний курс по дизайну інтерфейсів",
                        Duration = TimeSpan.FromHours(3),
                        CategoryId = designCategoryId,
                        TrainerId = olenaId
                    },
                    new Workshop
                    {
                        Title = "Основи цифрового маркетингу",
                        Description = "Як ефективно просувати бізнес онлайн",
                        Duration = TimeSpan.FromHours(2.5),
                        CategoryId = marketingCategoryId,
                        TrainerId = maksimId
                    },
                    new Workshop
                    {
                        Title = "Мистецтво фотографії",
                        Description = "Техніки та секрети професійної фотографії",
                        Duration = TimeSpan.FromHours(5),
                        CategoryId = photoCategoryId,
                        TrainerId = svitlanaId
                    }
                };

                context.Workshops.AddRange(workshops);
                await context.SaveChangesAsync();
            }

            if (!context.Sessions.Any())
            {
                var workshops = context.Workshops.ToList();

                var sessions = new[]
                {
                    new Session { WorkshopId = workshops[0].WorkshopId, StartTime = DateTime.Now.AddDays(1).AddHours(10) },
                    new Session { WorkshopId = workshops[0].WorkshopId, StartTime = DateTime.Now.AddDays(2).AddHours(10) },
                    new Session { WorkshopId = workshops[1].WorkshopId, StartTime = DateTime.Now.AddDays(3).AddHours(14) },
                    new Session { WorkshopId = workshops[2].WorkshopId, StartTime = DateTime.Now.AddDays(4).AddHours(9) },
                    new Session { WorkshopId = workshops[3].WorkshopId, StartTime = DateTime.Now.AddDays(5).AddHours(11) },
                    new Session { WorkshopId = workshops[3].WorkshopId, StartTime = DateTime.Now.AddDays(6).AddHours(11) }
                };

                context.Sessions.AddRange(sessions);
                await context.SaveChangesAsync();
            }
        }
    }
}
