namespace DemoMVC.Application.Interfaces;

public interface ISequenceGenerator
{
    Task<long> NextAsync(string sequenceName);
}