using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Import Infrastructure
using DemoMVC.Infrastructure.Data;
using DemoMVC.Infrastructure.Customers.Repositories;

// Import Application
using DemoMVC.Application.Customers.Services;
using DemoMVC.Application.Customers.Interfaces;

// Import Web
using DemoMVC.Web.Interfaces.Customers;
using Microsoft.CodeAnalysis.Options;

var applicationOptions = new WebApplicationOptions
{
    Args = args,
    WebRootPath = "Web/wwwroot"
};

var builder = WebApplication.CreateBuilder(applicationOptions);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

//Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml");
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
