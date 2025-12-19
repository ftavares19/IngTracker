# Guía de Uso de Repositorios - IngTracker

## Estructura del Proyecto

### IDataAccess (Interfaces)
Contiene todas las interfaces de repositorios. Los servicios deben depender **SOLO** de este proyecto.

- `IRepositorio<T>` - Interfaz genérica base
- `IUsuarioRepositorio`
- `ICarreraRepositorio`
- `IMateriaRepositorio`
- `ITituloRepositorio`
- `IUsuarioMateriaRepositorio`
- `Excepciones/ExcepcionRepositorio` - Excepción para errores de repositorio

### DataAccess (Implementaciones)
Contiene las implementaciones concretas y el contexto de base de datos.

- `Context/AppDbContext` - DbContext de Entity Framework
- `Repositorios/Repositorio<T>` - Repositorio genérico base
- `Repositorios/*Repositorio` - Implementaciones específicas

---

## Configuración en Program.cs de la API

```csharp
// Registrar el DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorios como Scoped
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ICarreraRepositorio, CarreraRepositorio>();
builder.Services.AddScoped<IMateriaRepositorio, MateriaRepositorio>();
builder.Services.AddScoped<ITituloRepositorio, TituloRepositorio>();
builder.Services.AddScoped<IUsuarioMateriaRepositorio, UsuarioMateriaRepositorio>();
```

**Importante:** Todos los repositorios y el contexto se registran como `Scoped`, 
esto garantiza que compartan la misma instancia del DbContext por request HTTP.

---

## Uso en Servicios

### Inyección de Dependencias

```csharp
public class UsuarioServicio
{
    private readonly IUsuarioRepositorio _usuarioRepo;
    private readonly ICarreraRepositorio _carreraRepo;
    private readonly AppDbContext _context;

    public UsuarioServicio(
        IUsuarioRepositorio usuarioRepo,
        ICarreraRepositorio carreraRepo,
        AppDbContext context)
    {
        _usuarioRepo = usuarioRepo;
        _carreraRepo = carreraRepo;
        _context = context;
    }

    public Usuario CrearUsuario(string nombre, string email, int carreraId)
    {
        // Validar que la carrera existe
        if (!_carreraRepo.Existe(carreraId))
        {
            throw new Exception("La carrera no existe");
        }

        var usuario = new Usuario
        {
            Nombre = nombre,
            Email = email,
            CarreraId = carreraId
        };

        // Agregar (NO guarda automáticamente)
        _usuarioRepo.Agregar(usuario);
        
        // Guardar los cambios manualmente
        _context.SaveChanges();

        return usuario;
    }

    public void InscribirMateria(int usuarioId, int materiaId)
    {
        // Obtener entidades
        var usuario = _usuarioRepo.Obtener(usuarioId);
        var materia = _materiaRepo.Obtener(materiaId);

        // Crear la relación
        var usuarioMateria = new UsuarioMateria
        {
            UsuarioId = usuarioId,
            MateriaId = materiaId,
            Estado = Estado.Cursando,
            FechaInicio = DateTime.Now
        };

        // Agregar
        _usuarioMateriaRepo.Agregar(usuarioMateria);
        
        // Guardar TODO de una vez (transacción atómica)
        _context.SaveChanges();
    }
}
```

---

## Métodos Disponibles

### IRepositorio<T> (Genérico)

```csharp
T Obtener(int id);                                    // Obtiene por ID o lanza ExcepcionRepositorio
IEnumerable<T> ObtenerTodos();                        // Obtiene todos los registros
IEnumerable<T> Buscar(Expression<Func<T, bool>>);     // Busca con predicado LINQ
T Agregar(T entidad);                                 // Agrega (NO guarda)
void Modificar(T entidad);                            // Modifica (NO guarda)
void Eliminar(int id);                                // Elimina (NO guarda)
bool Existe(int id);                                  // Verifica existencia
```

### IUsuarioRepositorio (específicos)

```csharp
Usuario? ObtenerPorEmail(string email);
Usuario? ObtenerConCarrera(int id);        // Include de Carrera
Usuario? ObtenerConMaterias(int id);       // Include de UsuariosMaterias y Materias
```

### ICarreraRepositorio

```csharp
Carrera? ObtenerConTitulos(int id);        // Include de Titulos
Carrera? ObtenerConMaterias(int id);       // Include de Materias
Carrera? ObtenerCompleta(int id);          // Include de Titulos y Materias
```

### IMateriaRepositorio

```csharp
Materia? ObtenerConPrevias(int id);        // Include de Previas
IEnumerable<Materia> ObtenerPorCarrera(int carreraId);
```

### ITituloRepositorio

```csharp
IEnumerable<Titulo> ObtenerPorCarrera(int carreraId);
```

### IUsuarioMateriaRepositorio

```csharp
IEnumerable<UsuarioMateria> ObtenerPorUsuario(int usuarioId);
```

---

## Manejo de Transacciones

**Los repositorios NO guardan automáticamente.** Tienes control total:

```csharp
// ✅ Operación simple
var usuario = new Usuario { Nombre = "Juan", Email = "juan@mail.com" };
_usuarioRepo.Agregar(usuario);
_context.SaveChanges();

// ✅ Múltiples operaciones (transacción atómica)
_usuarioRepo.Agregar(usuario1);
_usuarioRepo.Agregar(usuario2);
_carreraRepo.Modificar(carrera);
_context.SaveChanges(); // Se guardan todas juntas

// ✅ Con transacción explícita
using var transaction = _context.Database.BeginTransaction();
try
{
    _usuarioRepo.Agregar(usuario);
    _materiaRepo.Modificar(materia);
    _context.SaveChanges();
    
    transaction.Commit();
}
catch
{
    transaction.Rollback();
    throw;
}
```

---

## Manejo de Errores

```csharp
try
{
    var usuario = _usuarioRepo.Obtener(999); // ID que no existe
}
catch (ExcepcionRepositorio ex)
{
    // "No se encontró la entidad con id 999"
    Console.WriteLine(ex.Message);
}
```

---

## Notas Importantes

1. **Sin Async/Await** - Todo es síncrono por diseño
2. **SaveChanges Manual** - Debes llamarlo explícitamente desde el servicio
3. **Scoped Lifetime** - Todos los repositorios y el contexto comparten el mismo ciclo de vida por request
4. **IEnumerable** - Todas las colecciones usan IEnumerable<T> para consistencia
5. **Primary Constructors** - Se usa sintaxis moderna de C# 12
