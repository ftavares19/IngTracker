using DataAccess.Context;
using DataAccess.Repositories;
using IDataAccess;
using IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace APIServiceFactory;

public class ServiceFactory
{
    public static void AddServices(IServiceCollection serviceCollection)
    {
        // Repositories
        serviceCollection.AddScoped<IDegreeRepository, DegreeRepository>();
        
        // Services
        serviceCollection.AddScoped<IDegreeService, DegreeService>();
    }
    
    public static void AddConnectionString(IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<DbContext, AppDbContext>(o => o.UseSqlServer(connectionString));
    }
}