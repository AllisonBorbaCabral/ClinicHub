using Microsoft.AspNetCore.Mvc;
using DemoMVC.Web.Models.Customers;
using DemoMVC.Web.Interfaces.Customers;
using DemoMVC.Application.Customers.DTOs;

namespace DemoMVC.Web.Controllers.Customers;

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    public async Task<IActionResult> Index()
    {
        var dtos = await _customerService.GetAllAsync();

        var customers = dtos
            .Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                BirthDate = c.BirthDate,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive
            })
            .ToList();

        return View(customers);
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
        CustomerViewModel model
    )
    {
        if (!ModelState.IsValid)
            return View(model);

        if (string.IsNullOrEmpty(model.Name))
            return View(model);

        var request = new CreateCustomerDTO(
            Name: model.Name,
            BirthDate: model.BirthDate,
            IsActive: model.IsActive
        );

        var customer = await _customerService.Create(request);

        if (customer is null)
            return View(model);

        return RedirectToAction(nameof(Index));
    }
}