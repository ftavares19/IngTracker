# Guía de Servicios - IngTracker

## 📁 Estructura Creada

### IServices (Interfaces de Servicios)
- `IUsuarioServicio` - Gestión de usuarios
- `ICarreraServicio` - Gestión de carreras
- `IMateriaServicio` - Gestión de materias y previas
- `IUsuarioMateriaServicio` - Seguimiento de materias del usuario (inscripción, aprobación, etc.)

### Services (Implementaciones)
- `UsuarioServicio`
- `CarreraServicio`
- `MateriaServicio`
- `UsuarioMateriaServicio`

### APIServiceFactory
- `ServiceFactory` - Configura DI con todos los servicios y repositorios

---

## 🎯 Funcionalidades Principales

### 1. Gestión de Usuarios (`IUsuarioServicio`)

```csharp
// Crear usuario
Usuario Crear(string nombre, string email, int carreraId);

// Obtener usuario con su carrera
Usuario ObtenerConCarrera(int id);

// Obtener usuario con sus materias inscritas
Usuario ObtenerConMaterias(int id);

// Obtener materias del usuario
IEnumerable<UsuarioMateria> ObtenerMateriasDelUsuario(int usuarioId);
```

### 2. Gestión de Carreras (`ICarreraServicio`)

```csharp
// CRUD básico
Carrera Crear(string nombre, string descripcion);
void Modificar(int id, string nombre, string descripcion);
void Eliminar(int id);

// Obtener carrera con toda su información
Carrera ObtenerCompleta(int id);  // Incluye Títulos y Materias

// Obtener materias de una carrera
IEnumerable<Materia> ObtenerMaterias(int carreraId);
```

### 3. Gestión de Materias (`IMateriaServicio`)

```csharp
// CRUD básico
Materia Crear(string codigo, string nombre, Semestre semestre, int carreraId);
void Modificar(int id, string codigo, string nombre, Semestre semestre, int carreraId);

// Obtener materias por carrera
IEnumerable<Materia> ObtenerPorCarrera(int carreraId);

// Gestionar previas
void AsignarPrevia(int materiaId, int materiaIdPrevia);
void RemoverPrevia(int materiaId, int materiaIdPrevia);
Materia ObtenerConPrevias(int id);
```

### 4. Seguimiento de Materias (`IUsuarioMateriaServicio`) ⭐

**Este es el servicio principal para el seguimiento de carrera:**

```csharp
// Inscribir usuario a una materia (valida previas automáticamente)
UsuarioMateria InscribirMateria(int usuarioId, int materiaId);

// Actualizar estado de una materia
void ActualizarEstado(int usuarioMateriaId, Estado estado, int? nota = null);

// Aprobar una materia (valida nota y actualiza fecha)
void AprobarMateria(int usuarioMateriaId, int nota);

// Eliminar inscripción
void EliminarInscripcion(int usuarioMateriaId);

// Obtener materias disponibles para inscribir (considera previas aprobadas)
IEnumerable<Materia> ObtenerMateriasDisponibles(int usuarioId);

// Verificar si puede inscribirse (valida previas)
bool PuedeInscribirMateria(int usuarioId, int materiaId);
```

---

## 🔄 Estados de Materias

```csharp
public enum Estado
{
    Pendiente,   // Aún no cursó
    Cursando,    // Actualmente cursando
    Aprobada     // Materia aprobada
}
```

---

## 📊 Flujo de Seguimiento de Carrera

### 1. Inscribir a una Materia

```csharp
// El servicio valida automáticamente:
// - Que el usuario exista
// - Que la materia exista
// - Que no esté ya inscrito
// - Que tenga las previas aprobadas
var usuarioMateria = _usuarioMateriaServicio.InscribirMateria(usuarioId: 1, materiaId: 5);
// Estado inicial: Cursando
// FechaInicio: DateTime.Now
```

### 2. Aprobar una Materia

```csharp
// Actualiza el estado, agrega la nota y la fecha de aprobación
_usuarioMateriaServicio.AprobarMateria(usuarioMateriaId: 10, nota: 85);
// Estado: Aprobada
// Nota: 85
// FechaAprobacion: DateTime.Now
```

### 3. Ver Materias Disponibles

```csharp
// Obtiene materias que puede cursar (con previas aprobadas)
var materiasDisponibles = _usuarioMateriaServicio.ObtenerMateriasDisponibles(usuarioId: 1);

// Por ejemplo, si tiene aprobadas:
// - Programación 1
// - Matemática 1
// Puede inscribirse a:
// - Programación 2 (previa: Programación 1)
// - Matemática 2 (previa: Matemática 1)
```

---

## 🎓 Lógica de Previas

El sistema valida automáticamente las materias previas:

```csharp
// Ejemplo: Asignar "Programación 1" como previa de "Programación 2"
_materiaServicio.AsignarPrevia(
    materiaId: 2,        // Programación 2
    materiaIdPrevia: 1   // Programación 1
);

// Ahora, para inscribirse a Programación 2:
bool puede = _usuarioMateriaServicio.PuedeInscribirMateria(usuarioId, materiaId: 2);
// Retorna true solo si el usuario aprobó Programación 1
```

---

## ⚙️ Configuración en Program.cs

El `ServiceFactory` ya registra todo:

```csharp
// En Program.cs
ServiceFactory.AddServices(builder.Services);

// Esto registra automáticamente:
// - DbContext (Scoped)
// - Todos los Repositorios (Scoped)
// - Todos los Servicios (Scoped)
```

---

## 🔍 Validaciones Automáticas

Los servicios incluyen validaciones:

### UsuarioServicio
- ✅ Valida que el email sea único
- ✅ Valida que la carrera exista

### MateriaServicio
- ✅ Valida que la carrera exista
- ✅ Valida que no se asigne la misma previa dos veces

### UsuarioMateriaServicio
- ✅ Valida que las previas estén aprobadas
- ✅ Valida que no se inscriba dos veces a la misma materia
- ✅ Valida rango de nota (0-100)
- ✅ Actualiza fechas automáticamente

---

## 🚀 Próximos Pasos

Para completar la API REST:

1. **Crear Controladores**
   - `UsuariosController`
   - `CarrerasController`
   - `MateriasController`
   - `UsuarioMateriasController`

2. **Crear DTOs** (Data Transfer Objects)
   - Request DTOs (para recibir datos)
   - Response DTOs (para devolver datos)

3. **Configurar Connection String**
   - En `appsettings.json`
   - Actualizar `ServiceFactory.cs` línea 17

4. **Crear Migraciones**
   ```bash
   dotnet ef migrations add InitialCreate --project DataAccess
   dotnet ef database update --project DataAccess
   ```

---

## 📖 Ejemplo de Uso Completo

```csharp
// 1. Crear carrera
var carrera = _carreraServicio.Crear("Ingeniería en Sistemas", "Carrera de 4 años");

// 2. Crear materias
var prog1 = _materiaServicio.Crear("PRO1", "Programación 1", Semestre.Semestre1, carrera.Id);
var prog2 = _materiaServicio.Crear("PRO2", "Programación 2", Semestre.Semestre2, carrera.Id);

// 3. Asignar previa
_materiaServicio.AsignarPrevia(prog2.Id, prog1.Id);

// 4. Crear usuario
var usuario = _usuarioServicio.Crear("Juan Pérez", "juan@mail.com", carrera.Id);

// 5. Inscribir a Programación 1
var inscripcion1 = _usuarioMateriaServicio.InscribirMateria(usuario.Id, prog1.Id);

// 6. Aprobar Programación 1
_usuarioMateriaServicio.AprobarMateria(inscripcion1.Id, nota: 90);

// 7. Ver materias disponibles (ahora incluye Programación 2)
var disponibles = _usuarioMateriaServicio.ObtenerMateriasDisponibles(usuario.Id);

// 8. Inscribir a Programación 2
var inscripcion2 = _usuarioMateriaServicio.InscribirMateria(usuario.Id, prog2.Id);
```

---

## ⚠️ Importante

- **Sin Async/Await**: Todo es síncrono como solicitaste
- **SaveChanges Manual**: Los servicios llaman a `_context.SaveChanges()` explícitamente
- **IEnumerable**: Consistente en todas las colecciones
- **Primary Constructors**: Sintaxis moderna de C# 12
- **Excepciones**: Usa `ExcepcionRepositorio` para errores de negocio
