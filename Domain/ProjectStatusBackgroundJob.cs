using Domain.Abstract;
using Domain.DomainModels.Enums;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Domain;

public class ProjectStatusBackgroundJob : IJob {
    private readonly IApplicationDbContext _context;
    public ProjectStatusBackgroundJob(IApplicationDbContext context)     {
        _context = context;     }
    public async Task Execute(IJobExecutionContext context)
    {         var projectsList = await _context.Projects.ToListAsync();
        foreach (var project in projectsList)         {
            if(project.LastDay < DateTime.UtcNow || project.LastDay < DateTime.Now.AddDays(1))             {
                project.Status = Status.Expired;             }
        }         await _context.SaveChanges();
        Console.WriteLine("It's alive");     }
}