\# Sistema de Gestión de Recordatorios



\## Descripción



Aplicación web desarrollada en \*\*ASP.NET Core MVC (.NET 8)\*\* utilizando \*\*Supabase (PostgreSQL)\*\* como base de datos.



El sistema permite:



\* Autenticación de usuarios.

\* Manejo de sesiones mediante `HTTPContext Session`.

\* Creación de recordatorios.

\* Consulta de recordatorios almacenados en Supabase.

\* Marcado de recordatorios como completados.

\* Manejo de errores mediante \*\*Partial Views\*\*.

\* Visualización de datos mediante \*\*Partial Views\*\*.



\---



\# Tecnologías utilizadas



\* ASP.NET Core MVC (.NET 8)

\* C#

\* Supabase PostgreSQL

\* Bootstrap 5

\* HTTPContext Session

\* Partial Views

\* Visual Studio 2022

\* NuGet Supabase 1.1.1



\---



\# Funcionalidades implementadas



\## Inicio de sesión



El sistema permite autenticarse utilizando correo electrónico y contraseña almacenados en Supabase.



Durante el proceso de autenticación se validan los siguientes escenarios:



\* Usuario no existe.

\* Contraseña incorrecta.

\* Usuario bloqueado.

\* Error de comunicación con Supabase.



Cada escenario se presenta mediante una Partial View personalizada.



\---



\## Manejo de sesión



Una vez autenticado, se almacena información del usuario utilizando:



```csharp

HttpContext.Session

```



Las vistas protegidas validan la existencia de una sesión activa antes de permitir el acceso.



\---



\## Gestión de recordatorios



El usuario puede:



\* Consultar recordatorios.

\* Crear nuevos recordatorios.

\* Marcar recordatorios como completados.



Los datos se obtienen desde Supabase mediante consultas realizadas desde los controladores.



\---



\## Uso de Partial Views



Se utilizan Partial Views para:



\### Mensajes de error



\* \_UsuarioNoExiste.cshtml

\* \_PasswordIncorrecto.cshtml

\* \_UsuarioBloqueado.cshtml

\* \_ErrorSupabase.cshtml



\### Visualización de datos



\* \_TablaRecordatorios.cshtml



La tabla de recordatorios se renderiza mediante una Partial View que recibe un modelo personalizado.



\---



\# Estructura del proyecto



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

│   └── \_TablaRecordatorios.cshtml

│

├── Shared

│   ├── \_UsuarioNoExiste.cshtml

│   ├── \_PasswordIncorrecto.cshtml

│   ├── \_UsuarioBloqueado.cshtml

│   └── \_ErrorSupabase.cshtml

```



\---



\# Configuración de Supabase



\## Tabla usuarios



Ejecutar el siguiente script:



```sql

CREATE TABLE usuarios (

&#x20;   id SERIAL PRIMARY KEY,

&#x20;   nombre VARCHAR(100),

&#x20;   correo VARCHAR(100) UNIQUE,

&#x20;   password VARCHAR(100),

&#x20;   bloqueado BOOLEAN DEFAULT FALSE

);

```



\---



\## Usuario de prueba



```sql

INSERT INTO usuarios

(nombre, correo, password, bloqueado)

VALUES

(

&#x20;   'Juan',

&#x20;   'juan@gmail.com',

&#x20;   '123456',

&#x20;   false

);

```



\---



\## Tabla recordatorios



```sql

CREATE TABLE recordatorios (

&#x20;   id SERIAL PRIMARY KEY,

&#x20;   usuario\_id INT NOT NULL,

&#x20;   titulo VARCHAR(100) NOT NULL,

&#x20;   descripcion TEXT,

&#x20;   fecha\_recordatorio TIMESTAMP NOT NULL,

&#x20;   completado BOOLEAN DEFAULT FALSE,

&#x20;   creado\_at TIMESTAMP DEFAULT CURRENT\_TIMESTAMP

);

```



\---



\## Vista de recordatorios



```sql

CREATE OR REPLACE VIEW vw\_mis\_recordatorios AS

SELECT

&#x20;   r.id,

&#x20;   r.titulo,

&#x20;   r.descripcion,

&#x20;   r.fecha\_recordatorio,

&#x20;   r.completado,

&#x20;   CASE

&#x20;       WHEN r.completado = TRUE THEN 'Completado'

&#x20;       WHEN r.fecha\_recordatorio < NOW() THEN 'Vencido'

&#x20;       WHEN DATE(r.fecha\_recordatorio) = CURRENT\_DATE THEN 'Hoy'

&#x20;       ELSE 'Próximo'

&#x20;   END AS situacion

FROM recordatorios r;

```



\---



\# Configuración de conexión



Modificar los valores del archivo:



```text

Services/SupabClient.cs

```



Configurar:



```csharp

private static string url = "SUPABASE\_URL";

private static string key = "SUPABASE\_KEY";

```



con las credenciales correspondientes del proyecto Supabase.



\---



\# Ejecución del proyecto



1\. Clonar el repositorio.



```bash

git clone <url-del-repositorio>

```



2\. Abrir la solución en Visual Studio 2022.



3\. Restaurar paquetes NuGet.



4\. Configurar la base de datos en Supabase.



5\. Verificar las credenciales en:



```text

Services/SupabClient.cs

```



6\. Ejecutar la aplicación.



```text

Ctrl + F5

```



o



```text

F5

```



\---



\# Credenciales de prueba



```text

Correo:

juan@gmail.com



Contraseña:

123456

```



\---



\# Autor



Proyecto desarrollado como parte de la asignación académica de ASP.NET Core MVC utilizando Supabase, sesiones HTTP y Partial Views.



