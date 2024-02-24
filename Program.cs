using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using srsProject.Models;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<srsProjectContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("srsProjectContext")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SrsProject}/{action=Index}/{id?}");


app.Run();
