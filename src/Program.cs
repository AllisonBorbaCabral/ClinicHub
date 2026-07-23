using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Import DbContext
using DemoMVC.Infrastructure.Data;

// Import Infrastructure
using DemoMVC.Infrastructure.People.Repositories;
using DemoMVC.Infrastructure.Patients.Repositories;
using DemoMVC.Infrastructure.Patients.Services;

// Import Application
using DemoMVC.Application.People.Services;
using DemoMVC.Application.Patients.Services;
using DemoMVC.Application.People.Interfaces;
using DemoMVC.Application.Patients.Interfaces;

// Import Repository Interface
using DemoMVC.Domain.People.Repositories;
using DemoMVC.Domain.Patients.Repositories;

var applicationOptions = new WebApplicationOptions
{
    Args = args,
    WebRootPath = "Web/wwwroot"
};

var builder = WebApplication.CreateBuilder(applicationOptions);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonUniquenessCheckerRepository, PersonRepository>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPersonUniquenessCheckerRepository, PatientRepository>();

//Services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IMedicalRecordGenerator, MedicalRecordGenerator>();

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
