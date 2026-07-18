using Microsoft.AspNetCore.Mvc;
using DemoMVC.Web.Models.Patients;
using DemoMVC.Application.Patients.DTOs;
using DemoMVC.Application.Patients.Interfaces;

namespace DemoMVC.Web.Controllers.Patients;

public class PatientController : Controller
{
    private readonly IPatientService _patientService;
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    public async Task<IActionResult> Index()
    {
        var dtos = await _patientService.GetAllAsync();

        var patients = dtos
            .Select(c => new PatientViewModel
            {
                Id = c.Id,
                Name = c.Name,
                BirthDate = c.BirthDate,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive
            })
            .ToList();

        return View(patients);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // var dto = await _customerService.GetByIdAsync(id);

        // if (dto is not null)
        // {
        //     var customer = new CustomerViewModel
        //     {
        //         Id = dto.Id,
        //         Name = dto.Name,
        //         BirthDate = dto.BirthDate,
        //         CreatedAt = dto.CreatedAt,
        //         UpdatedAt = dto.UpdatedAt,
        //         IsActive = dto.IsActive
        //     };

        //     return View(customer);
        // }

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(
        PatientViewModel model
    )
    {
        if (!ModelState.IsValid)
            return View(model);

        if (string.IsNullOrEmpty(model.Name))
            return View(model);

        var request = new CreatePatientDTO(
            Name: model.Name,
            BirthDate: model.BirthDate,
            IsActive: model.IsActive
        );

        var patient = await _patientService.Create(request);

        if (patient is null)
            return View(model);

        return RedirectToAction(nameof(Index));
    }
}