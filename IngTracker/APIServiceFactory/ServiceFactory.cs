using DataAccess.Context;
using DataAccess.Repositorios;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace APIServiceFactory;

public class ServiceFactory
{
    public static void AddServices(IServiceCollection serviceCollection)
    {
        // Repositorios
        serviceCollection.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
    }
    
    public static void AddConnectionString(IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<DbContext, AppDbContext>(o => o.UseSqlServer(connectionString));
    }
}