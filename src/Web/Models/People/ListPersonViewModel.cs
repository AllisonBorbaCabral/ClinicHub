namespace DemoMVC.Web.Models.People;

public class ListPersonViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Cpf { get; set; }
    public bool IsActive { get; set; }
}