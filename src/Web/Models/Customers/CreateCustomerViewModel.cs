using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Web.Models.Customers;

public class CreateCustomerViewModel
{
    [Required]
    public string Name { get; set; } = String.Empty;
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly BirthDate { get; set; }
    [Required]
    public bool IsActive { get; set; }
}