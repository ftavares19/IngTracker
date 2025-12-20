using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Excepciones;
using IServices;

namespace Services;

public class UsuarioServicio(
    IUsuarioRepositorio usuarioRepo,
    ICarreraRepositorio carreraRepo,
    IUsuarioMateriaRepositorio usuarioMateriaRepo,
    AppDbContext context) : IUsuarioServicio
{
    private readonly IUsuarioRepositorio _usuarioRepo = usuarioRepo;
    private readonly ICarreraRepositorio _carreraRepo = carreraRepo;
    private readonly IUsuarioMateriaRepositorio _usuarioMateriaRepo = usuarioMateriaRepo;
    private readonly AppDbContext _context = context;

    public Usuario Obtener(int id)
    {
        return _usuarioRepo.Obtener(id);
    }

    public IEnumerable<Usuario> ObtenerTodos()
    {
        return _usuarioRepo.ObtenerTodos();
    }

    public Usuario ObtenerPorEmail(string email)
    {
        var usuario = _usuarioRepo.ObtenerPorEmail(email);
        if (usuario == null)
        {
            throw new ExcepcionRepositorio("No se encontró un usuario con ese email");
        }
        return usuario;
    }

    public Usuario Crear(string nombre, string email, int carreraId)
    {
        if (!_carreraRepo.Existe(carreraId))
        {
            throw new ExcepcionRepositorio("La carrera especificada no existe");
        }

        var usuarioExistente = _usuarioRepo.ObtenerPorEmail(email);
        if (usuarioExistente != null)
        {
            throw new ExcepcionRepositorio("Ya existe un usuario con ese email");
        }

        var usuario = new Usuario
        {
            Nombre = nombre,
            Email = email,
            CarreraId = carreraId
        };

        _usuarioRepo.Agregar(usuario);
        _context.SaveChanges();

        return usuario;
    }

    public void Modificar(int id, string nombre, string email, int carreraId)
    {
        var usuario = _usuarioRepo.Obtener(id);

        if (!_carreraRepo.Existe(carreraId))
        {
            throw new ExcepcionRepositorio("La carrera especificada no existe");
        }

        usuario.Nombre = nombre;
        usuario.Email = email;
        usuario.CarreraId = carreraId;

        _usuarioRepo.Modificar(usuario);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        _usuarioRepo.Eliminar(id);
        _context.SaveChanges();
    }

    public Usuario ObtenerConCarrera(int id)
    {
        var usuario = _usuarioRepo.ObtenerConCarrera(id);
        if (usuario == null)
        {
            throw new ExcepcionRepositorio($"No se encontró el usuario con id {id}");
        }
        return usuario;
    }

    public Usuario ObtenerConMaterias(int id)
    {
        var usuario = _usuarioRepo.ObtenerConMaterias(id);
        if (usuario == null)
        {
            throw new ExcepcionRepositorio($"No se encontró el usuario con id {id}");
        }
        return usuario;
    }

    public IEnumerable<UsuarioMateria> ObtenerMateriasDelUsuario(int usuarioId)
    {
        return _usuarioMateriaRepo.ObtenerPorUsuario(usuarioId);
    }
}
