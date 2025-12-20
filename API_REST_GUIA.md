# API REST - IngTracker

## 🎯 Diseño siguiendo Buenas Prácticas REST

Esta API sigue los principios RESTful:
- ✅ **Recursos como sustantivos** (usuarios, carreras, materias, inscripciones)
- ✅ **Verbos HTTP apropiados** (GET, POST, PUT, PATCH, DELETE)
- ✅ **Códigos de estado HTTP correctos** (200, 201, 204, 400, 404)
- ✅ **URIs jerárquicas y predecibles**
- ✅ **Respuestas JSON consistentes**
- ✅ **Versionado mediante URL** (api/...)
- ✅ **HATEOAS** (Location header en POST)

---

## 📋 Endpoints

### 🧑 Usuarios - `/api/usuarios`

| Método | Endpoint | Descripción | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| `GET` | `/api/usuarios` | Obtiene todos los usuarios | - | `200 OK` - Array de usuarios |
| `GET` | `/api/usuarios/{id}` | Obtiene un usuario por ID | - | `200 OK` / `404 Not Found` |
| `GET` | `/api/usuarios/{id}/carrera` | Obtiene usuario con su carrera | - | `200 OK` / `404 Not Found` |
| `GET` | `/api/usuarios/{id}/materias` | Obtiene materias del usuario | - | `200 OK` / `404 Not Found` |
| `POST` | `/api/usuarios` | Crea un nuevo usuario | `CrearUsuarioRequest` | `201 Created` / `400 Bad Request` |
| `PUT` | `/api/usuarios/{id}` | Actualiza un usuario | `ActualizarUsuarioRequest` | `204 No Content` / `404 Not Found` |
| `DELETE` | `/api/usuarios/{id}` | Elimina un usuario | - | `204 No Content` / `404 Not Found` |

#### Request Examples

**POST /api/usuarios**
```json
{
  "nombre": "Juan Pérez",
  "email": "juan@mail.com",
  "carreraId": 1
}
```

**PUT /api/usuarios/1**
```json
{
  "nombre": "Juan Pérez",
  "email": "juan.perez@mail.com",
  "carreraId": 1
}
```

#### Response Example

```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "email": "juan@mail.com",
  "carreraId": 1,
  "carreraNombre": "Ingeniería en Sistemas"
}
```

---

### 🎓 Carreras - `/api/carreras`

| Método | Endpoint | Descripción | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| `GET` | `/api/carreras` | Obtiene todas las carreras | - | `200 OK` |
| `GET` | `/api/carreras/{id}` | Obtiene una carrera por ID | - | `200 OK` / `404 Not Found` |
| `GET` | `/api/carreras/{id}/detalle` | Obtiene carrera completa (con materias y títulos) | - | `200 OK` / `404 Not Found` |
| `GET` | `/api/carreras/{id}/materias` | Obtiene materias de la carrera | - | `200 OK` |
| `POST` | `/api/carreras` | Crea una nueva carrera | `CrearCarreraRequest` | `201 Created` / `400 Bad Request` |
| `PUT` | `/api/carreras/{id}` | Actualiza una carrera | `ActualizarCarreraRequest` | `204 No Content` / `404 Not Found` |
| `DELETE` | `/api/carreras/{id}` | Elimina una carrera | - | `204 No Content` / `404 Not Found` |

#### Request Examples

**POST /api/carreras**
```json
{
  "nombre": "Ingeniería en Sistemas",
  "descripcion": "Carrera de grado en Ingeniería en Sistemas de Información"
}
```

#### Response Example

```json
{
  "id": 1,
  "nombre": "Ingeniería en Sistemas",
  "descripcion": "Carrera de grado en Ingeniería en Sistemas de Información"
}
```

---

### 📚 Materias - `/api/materias`

| Método | Endpoint | Descripción | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| `GET` | `/api/materias` | Obtiene todas las materias | - | `200 OK` |
| `GET` | `/api/materias/{id}` | Obtiene una materia por ID | - | `200 OK` / `404 Not Found` |
| `GET` | `/api/materias/{id}/previas` | Obtiene materia con sus previas | - | `200 OK` / `404 Not Found` |
| `POST` | `/api/materias` | Crea una nueva materia | `CrearMateriaRequest` | `201 Created` / `400 Bad Request` |
| `PUT` | `/api/materias/{id}` | Actualiza una materia | `ActualizarMateriaRequest` | `204 No Content` / `404 Not Found` |
| `DELETE` | `/api/materias/{id}` | Elimina una materia | - | `204 No Content` / `404 Not Found` |
| `POST` | `/api/materias/{id}/previas` | Asigna una previa a la materia | `AsignarPreviaRequest` | `204 No Content` / `400 Bad Request` |
| `DELETE` | `/api/materias/{id}/previas/{previaId}` | Remueve una previa | - | `204 No Content` / `404 Not Found` |

#### Request Examples

**POST /api/materias**
```json
{
  "codigo": "PRO1",
  "nombre": "Programación 1",
  "semestre": 1,
  "carreraId": 1
}
```

**POST /api/materias/2/previas** (Asignar Programación 1 como previa de Programación 2)
```json
{
  "materiaIdPrevia": 1
}
```

#### Response Example

```json
{
  "id": 1,
  "codigo": "PRO1",
  "nombre": "Programación 1",
  "semestre": 1,
  "carreraId": 1
}
```

---

### ✅ Inscripciones - `/api/inscripciones`

| Método | Endpoint | Descripción | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| `GET` | `/api/inscripciones/usuario/{usuarioId}` | Obtiene inscripciones del usuario | - | `200 OK` |
| `GET` | `/api/inscripciones/usuario/{usuarioId}/disponibles` | Obtiene materias disponibles para inscribir | - | `200 OK` / `404 Not Found` |
| `GET` | `/api/inscripciones/usuario/{usuarioId}/puede-inscribir/{materiaId}` | Verifica si puede inscribirse | - | `200 OK` (boolean) |
| `POST` | `/api/inscripciones` | Inscribe a un usuario a una materia | `InscribirMateriaRequest` | `201 Created` / `400 Bad Request` |
| `PATCH` | `/api/inscripciones/{id}/estado` | Actualiza el estado de la inscripción | `ActualizarEstadoMateriaRequest` | `204 No Content` / `404 Not Found` |
| `POST` | `/api/inscripciones/{id}/aprobar` | Aprueba una materia con nota | `AprobarMateriaRequest` | `204 No Content` / `400 Bad Request` |
| `DELETE` | `/api/inscripciones/{id}` | Elimina una inscripción | - | `204 No Content` / `404 Not Found` |

#### Request Examples

**POST /api/inscripciones** (Inscribir usuario a materia)
```json
{
  "usuarioId": 1,
  "materiaId": 5
}
```

**POST /api/inscripciones/10/aprobar**
```json
{
  "nota": 85
}
```

**PATCH /api/inscripciones/10/estado**
```json
{
  "estado": 2,
  "nota": 75
}
```

#### Response Example

```json
{
  "id": 10,
  "usuarioId": 1,
  "materiaId": 5,
  "materiaNombre": "Programación 1",
  "materiaCodigo": "PRO1",
  "estado": 1,
  "nota": null,
  "fechaInicio": "2024-12-19T21:30:00Z",
  "fechaAprobacion": null
}
```

---

## 📊 Estados de Materias

```csharp
public enum Estado
{
    Pendiente = 0,    // Aún no cursó
    Cursando = 1,     // Actualmente cursando
    Aprobada = 2      // Materia aprobada
}
```

---

## 🔢 Códigos de Estado HTTP

### Exitosos (2xx)
- **200 OK**: Solicitud exitosa con respuesta
- **201 Created**: Recurso creado exitosamente (incluye `Location` header)
- **204 No Content**: Solicitud exitosa sin contenido de respuesta

### Errores del Cliente (4xx)
- **400 Bad Request**: Datos inválidos o violación de reglas de negocio
- **404 Not Found**: Recurso no encontrado

### Formato de Error

```json
{
  "statusCode": 404,
  "message": "No se encontró el usuario con id 999",
  "detail": null,
  "timestamp": "2024-12-19T21:30:00.000Z"
}
```

---

## 🎯 Flujos de Uso Comunes

### 1️⃣ Crear Usuario y Carrera

```bash
# 1. Crear carrera
POST /api/carreras
{
  "nombre": "Ingeniería en Sistemas",
  "descripcion": "Carrera de grado"
}
# Response: 201 Created, Location: /api/carreras/1

# 2. Crear usuario
POST /api/usuarios
{
  "nombre": "Juan Pérez",
  "email": "juan@mail.com",
  "carreraId": 1
}
# Response: 201 Created, Location: /api/usuarios/1
```

### 2️⃣ Configurar Materias con Previas

```bash
# 1. Crear Programación 1
POST /api/materias
{
  "codigo": "PRO1",
  "nombre": "Programación 1",
  "semestre": 1,
  "carreraId": 1
}
# Response: materiaId = 1

# 2. Crear Programación 2
POST /api/materias
{
  "codigo": "PRO2",
  "nombre": "Programación 2",
  "semestre": 2,
  "carreraId": 1
}
# Response: materiaId = 2

# 3. Asignar PRO1 como previa de PRO2
POST /api/materias/2/previas
{
  "materiaIdPrevia": 1
}
# Response: 204 No Content
```

### 3️⃣ Seguimiento de Materias de Usuario

```bash
# 1. Ver materias disponibles
GET /api/inscripciones/usuario/1/disponibles
# Response: Lista de materias que puede cursar

# 2. Inscribir a Programación 1
POST /api/inscripciones
{
  "usuarioId": 1,
  "materiaId": 1
}
# Response: 201 Created, inscripcionId = 10

# 3. Aprobar Programación 1
POST /api/inscripciones/10/aprobar
{
  "nota": 90
}
# Response: 204 No Content

# 4. Ver materias disponibles (ahora incluye PRO2)
GET /api/inscripciones/usuario/1/disponibles

# 5. Verificar si puede inscribirse a PRO2
GET /api/inscripciones/usuario/1/puede-inscribir/2
# Response: true

# 6. Inscribir a Programación 2
POST /api/inscripciones
{
  "usuarioId": 1,
  "materiaId": 2
}
```

### 4️⃣ Ver Progreso del Usuario

```bash
# Ver todas las materias del usuario
GET /api/inscripciones/usuario/1

# Ver usuario con su carrera
GET /api/usuarios/1/carrera

# Ver usuario con todas sus materias
GET /api/usuarios/1/materias
```

---

## 🚀 Próximos Pasos

1. **Configurar appsettings.json** con connection string
2. **Crear migraciones** de Entity Framework
3. **Agregar validaciones** con FluentValidation o DataAnnotations
4. **Implementar paginación** en endpoints GET que retornan listas
5. **Agregar autenticación** con JWT
6. **Implementar logging** con Serilog
7. **Agregar filtros globales** para manejo de excepciones

---

## 📝 Notas Técnicas

- **Records**: Se usan `record` para DTOs (inmutables)
- **Primary Constructors**: Sintaxis moderna C# 12
- **ProducesResponseType**: Documenta responses para Swagger
- **ActionResult<T>**: Tipado fuerte en responses
- **CreatedAtAction**: Retorna Location header (HATEOAS)
- **No Async**: Todo síncrono por diseño del proyecto
