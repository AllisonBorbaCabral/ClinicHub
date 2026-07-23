using Microsoft.EntityFrameworkCore;
using DemoMVC.Application.Interfaces;

namespace DemoMVC.Infrastructure.Data;

public sealed class SequenceGenerator : ISequenceGenerator
{
    private readonly ApplicationDbContext _context;

    public SequenceGenerator(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> NextAsync(
        string sequenceName)
    {
        return await _context.Database
            .SqlQuery<long>(
                $"SELECT nextval('{sequenceName}')")
            .SingleAsync();
    }
}