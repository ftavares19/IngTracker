using DataAccess.Context;
using DataAccess.Repositorios;
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
        // Repositorios
        serviceCollection.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        serviceCollection.AddScoped<ICarreraRepositorio, CarreraRepositorio>();
        serviceCollection.AddScoped<IMateriaRepositorio, MateriaRepositorio>();
        serviceCollection.AddScoped<ITituloRepositorio, TituloRepositorio>();
        serviceCollection.AddScoped<IUsuarioMateriaRepositorio, UsuarioMateriaRepositorio>();

        // Servicios
        serviceCollection.AddScoped<IUsuarioServicio, UsuarioServicio>();
        serviceCollection.AddScoped<ICarreraServicio, CarreraServicio>();
        serviceCollection.AddScoped<IMateriaServicio, MateriaServicio>();
        serviceCollection.AddScoped<IUsuarioMateriaServicio, UsuarioMateriaServicio>();
    }
    
    public static void AddConnectionString(IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<DbContext, AppDbContext>(o => o.UseSqlServer(connectionString));
    }
}