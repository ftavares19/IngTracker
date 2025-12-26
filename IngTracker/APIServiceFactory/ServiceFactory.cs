using DataAccess.Context;
using DataAccess.Repositories;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace APIServiceFactory;

public class ServiceFactory
{
    public static void AddServices(IServiceCollection serviceCollection)
    {
        // Repositories
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
    }
    
    public static void AddConnectionString(IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<DbContext, AppDbContext>(o => o.UseSqlServer(connectionString));
    }
}