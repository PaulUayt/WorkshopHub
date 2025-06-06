using CorrelationId;
using CorrelationId.DependencyInjection;
using WorkshopHub.Data.Context;
using WorkshopHub.Data.Seeding;
using WorkshopHub.Web.Middleware;
using WorkshopHub.Web.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddCore(builder.Configuration);
builder.Services.AddLogging();
builder.Services.AddDefaultCorrelationId();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WorkshopHubDbContext>();
    await WorkshopSeeder.SeedAsync(dbContext);
}

app.UseCorrelationId();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/PageNotFound";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Workshops}/{action=Index}/{id?}");

app.Run();
