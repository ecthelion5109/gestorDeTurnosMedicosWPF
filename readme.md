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

### Cuenta con:
- CRUD para entidades Paciente / Medico / Turnos, donde esta ultima relaciona las dos primeras.
- Uso de Clases para las entidades de la base de datos, las cuales cuentan con metodos publicos para operaciones como llenar una ventana o leer datos de ella, constructores sobrecargados para la instanciacion desde JSON o desde SQL y metodos estaticos para instanciacion previo chequeo de integridad de datos del formulario.
- Uso de Encapsulamiento: Todas las ventanas de extension .xaml.cs no cuentan con logica de CRUD, pero si pueden utilizar los metodos publicos de "App.BaseDeDatos", y para navegar entre ventanas, llaman a la clase estatica "AtajosDeVentana".
- Uso de Templates. En AtajosDeVentana se definieron funciones aplicables para cualquier tipo de ventana, permitiendo trasladarse a otra ventana al parametrizar el nombre de la clase de ventana y usando la synthaxis "this." sobre la ventana actual.
- Uso de Poliformismo: BaseDeDatosSQL y BaseDeDatosJSON heredan de BaseDeDatosAbstracta, la cual las obliga a definir metodos publicos utilizados a lo largo de la aplicacion.
- Uso de restricciones de integridad de Registros: En la base de datos de SQL Server se definieron unique constraints, y en la clase BaseDeDatosJSON se definieron funciones para checkear integridad (imposibilidad de eliminar instancias que tengan turnos asignados, o de crear duplicados o hacer un modificacion que deje un turno sin paciente o sin medico).
- Uso de Diccionarios: para leer la base de datos (en forma de Objetos) en la memoria y evitar consultas SQL repetitivas, facilitando la referencia entre ventanas y el display de entidades según foreignk keys.
- Uso de Styles: En App.xaml se definieron stylos aplicados a Buttons, ContentControls (para crear secciones con bordes redondeados) y TextBoxs. A lo largo de la aplicacion, los archivos .xaml pueden crear objetos que apliquen esos estilos.
- Checkeo de integridad de datos ingresados: caracteres maximos coinciden con la definicion en la base de datos y no se puede guardar cambios en una ventana de agregar/modificar habiendo campos vacios (a menos que sean opcionales).
- Inicio de sesión que permite establecer una conexión a la base de datos (de microsoft SQL Server) o utilizar los archivos .json.
- Script para crear la Base de datos completa en cualquier computadora. 

### Descargas:
1. Ir a releases: https://github.com/ecthelion5109/gestorDeTurnosMedicosWPF/releases
2. Buscar la ultima entrega del proyecto. (Hubo una entrega para el TP2 y la final para el TP3)


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