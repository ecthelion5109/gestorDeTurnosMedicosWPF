## Interfaz Grafica con C# WPF
- Programacion II - UTN Avellaneda - CUDI
- Sistema Gestor de Turnos de una Clinica Medica.
- Base de datos local en Json y en servidor local con Microsoft SQL Server.

### Integrantes
1. Sanchez Justo Pastor
2. Seling Alexander
3. Pereira Ayelen Maria Rocio
4. Rivero Juan
5. Rojas Rocio Soledad
6. Romero Lucas Gabriel
7. Romero Laura Mercedes
8. Esperon Denise
9. Soberon Daira


### Descargas:
1. Ir a releases: https://github.com/ecthelion5109/gestorDeTurnosMedicosWPF/releases
2. Buscar la ultima entrega del proyecto. (Hubo una entrega para el TP2 y la final para el TP3)

### Funcionalidades:
- **CRUD completo para Paciente, Medico y Turno**: Incluye operaciones de creación, lectura, actualización y eliminación. La entidad Turno actúa como vínculo entre Paciente y Medico.

- **Modelado de entidades con clases**: Cada entidad de la base de datos (Paciente, Medico, Turno) está representada por una clase que cuenta con métodos públicos para interactuar con las ventanas (llenado de datos y lectura), constructores sobrecargados para instanciación desde JSON o SQL, y métodos estáticos que verifican la integridad de datos en formularios antes de crear instancias.

- **Encapsulamiento**: La lógica de CRUD no reside en las ventanas `.xaml.cs`, sino que estas utilizan los métodos públicos de `App.BaseDeDatos` para realizar operaciones. Para la navegación entre ventanas, se emplea la clase estática `AtajosDeVentana`.

- **Templates reutilizables**: En `AtajosDeVentana`, se definieron funciones aplicables a cualquier ventana, permitiendo una navegación flexible a través de parámetros que especifican el nombre de la clase de destino. La sintaxis `this.` facilita el uso de la ventana actual en estas transiciones.

- **Polimorfismo**: `BaseDeDatosSQL` y `BaseDeDatosJSON` heredan de `BaseDeDatosAbstracta`, una clase abstracta que obliga a ambas a implementar métodos públicos comunes, asegurando consistencia en el acceso a datos.

- **Restricciones de integridad en registros**: La base de datos SQL incluye restricciones únicas (`unique constraints`), y `BaseDeDatosJSON` define funciones para verificar integridad, previniendo, por ejemplo, la eliminación de instancias con turnos asignados o modificaciones que dejen turnos sin paciente o medico.

- **Uso de diccionarios**: Se emplean diccionarios para almacenar las instancias de las entidades en memoria. Las claves de estos diccionarios corresponden a las claves primarias de las tablas, lo que permite acceder rápidamente a las instancias de los objetos desde cualquier ventana sin necesidad de realizar consultas repetitivas a la base de datos. 

- **Estilos personalizados (Styles)**: `App.xaml` define estilos para `Button`, `ContentControl` (creando secciones con bordes redondeados) y `TextBox`. Los archivos `.xaml` en toda la aplicación pueden crear y aplicar estos estilos para mantener coherencia visual.

- **Validación de integridad de datos ingresados**: Las longitudes máximas de los campos coinciden con las definiciones de la base de datos, y no se permite guardar cambios en ventanas de agregar/modificar si hay campos obligatorios vacíos.

- **Inicio de sesión y conexión de base de datos**: El sistema permite al usuario iniciar sesión para establecer una conexión con Microsoft SQL Server o, alternativamente, trabajar con archivos locales en formato JSON.

- **Script de instalación de base de datos**: Se incluye un script que permite crear la base de datos completa en cualquier equipo, facilitando la instalación y configuración en diferentes entornos.


#### Comandos Git:
* `git clone "link del repo tipo HTTPS"` Para descargar el repositorio en mi computadora
* `git pull` Para tirar/descargar los commits del repositorio de GitHub.
* `git add *` Agregar/marcar todos los archivos.
* `git commit -m "mensaje"` Hacer un commit local con una descripcion de que hice.
* `git push` Empujar mi commit al repositorio de GitHub.
* `git stash`Almacenar temporalmente los cambios sin hacer commit.
* `git stash push -m "mensaje"` Stashear los cambios con un nombre específico.
* `git stash pop`  Volver a aplicar los cambios stasheados.
* `git reset --hard`  Eliminar los cambios locales y quedar igual que en el repositorio remoto.
* `git log`  Para ver el historial de commits en un formato básico (de arriba para abajo).
* `git log --reverse --oneline`  Ver el historial de commits de abajo hacia arriba en una sola línea.
* `git log --reverse --pretty=format:"%h %an - %ar: %s"` Ver el historial de commits en un formato más legible y compacto.
* `git remote set-url origin https://github.com/ecthelion5109/gestorDeTurnosMedicosWPF.git` Para cambiar el link del repo.


#### Comandos DotNet:
* `dotnet publish -c Release -r win-x64 --self-contained`
* `dotnet publish -c Release -r win-x64 --self-contained /p:PublishSingleFile=true`
* `dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:IncludeAllContentForSelfExtract=true --self-contained`
* `dotnet run`