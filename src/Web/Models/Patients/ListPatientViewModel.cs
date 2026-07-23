namespace DemoMVC.Web.Models.Patients;

public class ListPatientViewModel
{
    public string? Id { get; set; }
    public int? MedicalRecord { get; set; }
    public string? Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Cpf { get; set; }
    public bool IsActive { get; set; }
}