# Trabajo Práctico Nº2
## Interfaz Grafica con C# WPF

- Sistema Gestor de una Clinica Medica.
- Base de datos local en Json.
- Entidades para CRUD: Medicos / Pacientes / Turnos.

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

### Planeado hacer:
1. Terminar el CRUD para Pacientes y Turnos.
2. Encapsular y parametrizar base de datos Json, abriendo posibilidad de usar base SQL.
3. Implementar base de datos relacional con MySQL.
4. Usar objetos Paginas dentro de la misma ventana para consistencia en tamaño y optimizacion.
5. Agregar bindings para integridad de datos (caracteres maximos, solo numeros, ingresos obligatorios, etc)
6. Pedir iniciar sesión una sola vez para poder utilizar los botones de Crear, Modificar y Eliminar.
7. Mejorar diseño a nivel practico y estetico. Utilizar styles.

### Como correr:
1. Ir a releases: https://github.com/ecthelion5109/gestorDeTurnosMedicosWPF/releases
2. Bajar a la seccion de assets y descargar "TP2_GrupoC_2024-10-10_compiladoPara-win64.rar" para ejecutar en cualquier sistema operativo win-x64.
3. Alternativamente, "Source code(zip)" para descargar el codigo del repositorio al momento de la entrega.



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