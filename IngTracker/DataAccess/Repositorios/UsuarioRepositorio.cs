using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorios;

public class UsuarioRepositorio(AppDbContext context) : IUsuarioRepositorio
{
    
}
