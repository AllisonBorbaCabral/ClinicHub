using Microsoft.AspNetCore.Mvc;
using DemoMVC.Web.Models.People;
using DemoMVC.Application.People.Interfaces;

namespace DemoMVC.Web.Controllers.People;

public class PersonController : Controller
{
    private readonly IPersonService _service;
    public PersonController(IPersonService service)
    {
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        var dtos = await _service.GetAllAsync();

        return View(dtos.Data?
            .Select(p => new ListPersonViewModel
            {
                Id = p.Id,
                Name = p.Name,
                BirthDate = p.BirthDate,
                Cpf = p.Cpf,
                IsActive = p.IsActive
            })
            .ToList());
    }
}