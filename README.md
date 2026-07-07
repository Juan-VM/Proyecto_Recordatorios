# Sistema de Gestión de Recordatorios

## Descripción

Aplicación web desarrollada en **ASP.NET Core MVC (.NET 8)** utilizando **Supabase (PostgreSQL)** como base de datos.

El sistema permite:

* Autenticación de usuarios.
* Manejo de sesiones mediante `HTTPContext Session`.
* Creación de recordatorios.
* Consulta de recordatorios almacenados en Supabase.
* Marcado de recordatorios como completados.
* Manejo de errores mediante **Partial Views**.
* Visualización de datos mediante **Partial Views**.

---

# Tecnologías utilizadas

* ASP.NET Core MVC (.NET 8)
* C#
* Supabase PostgreSQL
* Bootstrap 5
* HTTPContext Session
* Partial Views
* Visual Studio 2022
* NuGet Supabase 1.1.1

---

# Funcionalidades implementadas

## Inicio de sesión

El sistema permite autenticarse utilizando correo electrónico y contraseña almacenados en Supabase.

Durante el proceso de autenticación se validan los siguientes escenarios:

* Usuario no existe.
* Contraseña incorrecta.
* Usuario bloqueado.
* Error de comunicación con Supabase.

Cada escenario se presenta mediante una Partial View personalizada.

---

## Manejo de sesión

Una vez autenticado, se almacena información del usuario utilizando:

```csharp
HttpContext.Session
```

Las vistas protegidas validan la existencia de una sesión activa antes de permitir el acceso.

---

## Gestión de recordatorios

El usuario puede:

* Consultar recordatorios.
* Crear nuevos recordatorios.
* Marcar recordatorios como completados.

Los datos se obtienen desde Supabase mediante consultas realizadas desde los controladores.

---

## Uso de Partial Views

Se utilizan Partial Views para:

### Mensajes de error

* _UsuarioNoExiste.cshtml
* _PasswordIncorrecto.cshtml
* _UsuarioBloqueado.cshtml
* _ErrorSupabase.cshtml

### Visualización de datos

* _TablaRecordatorios.cshtml

La tabla de recordatorios se renderiza mediante una Partial View que recibe un modelo personalizado.

---

# Estructura del proyecto

```text
Controllers
│
├── AuthController.cs
├── RecordatorioController.cs

Models
│
├── Usuario.cs
├── LoginViewModel.cs
├── Recordatorio.cs
├── RecordatorioVista.cs

Services
│
├── SupabClient.cs

Views
│
├── Auth
│   └── Login.cshtml
│
├── Recordatorio
│   ├── Create.cshtml
│   ├── Index.cshtml
│   └── _TablaRecordatorios.cshtml
│
├── Shared
│   ├── _UsuarioNoExiste.cshtml
│   ├── _PasswordIncorrecto.cshtml
│   ├── _UsuarioBloqueado.cshtml
│   └── _ErrorSupabase.cshtml
```

---

# Configuración de Supabase

## Tabla usuarios

Ejecutar el siguiente script:

```sql
CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100),
    correo VARCHAR(100) UNIQUE,
    password VARCHAR(100),
    bloqueado BOOLEAN DEFAULT FALSE
);
```

---

## Usuario de prueba

```sql
INSERT INTO usuarios
(nombre, correo, password, bloqueado)
VALUES
(
    'Juan',
    'juan@gmail.com',
    '123456',
    false
);
```

---

## Tabla recordatorios

```sql
CREATE TABLE recordatorios (
    id SERIAL PRIMARY KEY,
    usuario_id INT NOT NULL,
    titulo VARCHAR(100) NOT NULL,
    descripcion TEXT,
    fecha_recordatorio TIMESTAMP NOT NULL,
    completado BOOLEAN DEFAULT FALSE,
    creado_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

---

## Vista de recordatorios

```sql
CREATE OR REPLACE VIEW vw_mis_recordatorios AS
SELECT
    r.id,
    r.titulo,
    r.descripcion,
    r.fecha_recordatorio,
    r.completado,
    CASE
        WHEN r.completado = TRUE THEN 'Completado'
        WHEN r.fecha_recordatorio < NOW() THEN 'Vencido'
        WHEN DATE(r.fecha_recordatorio) = CURRENT_DATE THEN 'Hoy'
        ELSE 'Próximo'
    END AS situacion
FROM recordatorios r;
```

---

# Configuración de conexión

Modificar los valores del archivo:

```text
Services/SupabClient.cs
```

Configurar:

```csharp
private static string url = "SUPABASE_URL";
private static string key = "SUPABASE_KEY";
```

con las credenciales correspondientes del proyecto Supabase.

---

# Ejecución del proyecto

1. Clonar el repositorio.

```bash
git clone <url-del-repositorio>
```

2. Abrir la solución en Visual Studio 2022.

3. Restaurar paquetes NuGet.

4. Configurar la base de datos en Supabase.

5. Verificar las credenciales en:

```text
Services/SupabClient.cs
```

6. Ejecutar la aplicación.

```text
Ctrl + F5
```

o

```text
F5
```

---

# Credenciales de prueba

```text
Correo:
juan@gmail.com

Contraseña:
123456
```

---

# Autor

Proyecto desarrollado como parte de la asignación académica de ASP.NET Core MVC utilizando Supabase, sesiones HTTP y Partial Views.
