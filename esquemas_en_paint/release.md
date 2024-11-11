# Gestor De Turnos Médicos WPF

Version final del Trabajo Practico Nº3 del Grupo C de Programacion II. En esta versión, los datos de las bases de datos son inmediatamente convertidos en objetos y los botones solamente actualizan la base de datos. Los SELECT y los filtros son ejecutados por codigo C#. Las entidades relacionadas se acceden por properties en base a diccionarios de objetos con el mismo nombre que las tablas en el heap.

## Interfaz Grafica con C# WPF
- Programacion II - UTN Avellaneda - CUDI
- Grupo C, Comision 2º C, Cohorte 2024.
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

## Enlace a los videos sobre funcionalidad del codigo:
https://docs.google.com/document/d/1R1Uncca1tvTWpxoTXGpD2wEjuVOVX_QbNvkO2SDBQxQ/edit?tab=t.0

## Instrucciones para ejecución desde la aplicación compilada:
1. Descargar el comprimido "ClinicaMedicaPublish.zip" y extraer. 
2. Abrir la carpeta publish, ejecutar ClinicaMedica.exe.
3. Una vez en la aplicacion, intentar logearse con las credenciales de SQL server local. Cuando se detecte que no existe la base de datos "ClinicaMedica", se ofrecerá generarlas. Al aceptar se creara la base de datos con una docena de INSERTS por tabla.
4. Opcional: para evitar tener que escribir las credenciales en casa sesión, se puede editar el archivo el archivo `ClinicaMedica.dll.config`, y reemplazar NOMBRE_DEL_SERVIDOR por el nombre del servidor SQL Server local donde se montó la base de datos, generalmente es el nombre de la computadora.
   ``` <connectionStrings>
    <add name="ConexionAClinicaMedica"
         connectionString="Server=NOMBRE_DEL_SERVIDOR;Database=ClinicaMedica;Integrated Security=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
	```

## Instrucciones para ejecución desde el codigo fuente:
1. Descargar el comprimido "Source code(zip)" que aparece en esta entrega y extraer su contenido para obtener el codigo fuente del repositorio al momento de la entrega.
2. Abrir el proyecto WPF con Visual Studio y correr la aplicacion con el botón de iniciar. O sino, ejecutar el siguiente comando en cualquier consola o terminal:
```dotnet run```
3. Una vez en la aplicacion, intentar logearse con las credenciales de SQL server local. Cuando se detecte que no existe la base de datos "ClinicaMedica", se ofrecerá generarlas. Al aceptar se creara la base de datos con una docena de INSERTS por tabla.
4. Opcional: para evitar tener que escribir las credenciales en casa sesión, se puede editar el archivo el archivo `App.config`, y reemplazar NOMBRE_DEL_SERVIDOR por el nombre del servidor SQL Server local donde se montó la base de datos, generalmente es el nombre de la computadora.
   ``` <connectionStrings>
    <add name="ConexionAClinicaMedica"
         connectionString="Server=NOMBRE_DEL_SERVIDOR;Database=ClinicaMedica;Integrated Security=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
	```
5. Opcional: Para compilar la aplicación en un solo archivo, ejecutar el siguiente comando de consola.
```dotnet publish -c Release -r win-x64 --self-contained /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true```



### Funcionalidades:
- **CRUD completo para Paciente, Medico y Turno**: Incluye operaciones de creación, lectura, actualización y eliminación. La entidad Turno actúa como vínculo entre Paciente y Medico.

- **Modelado de entidades con clases**: Cada entidad de la base de datos (Paciente, Medico, Turno) está representada por una clase que cuenta con métodos públicos para interactuar con las ventanas (llenado de datos y lectura), constructores sobrecargados para instanciación desde JSON o SQL, y métodos estáticos que verifican la integridad de datos en formularios antes de crear instancias.

- **Encapsulamiento**: La lógica de CRUD no reside en las ventanas `.xaml.cs`, sino que estas utilizan los métodos públicos de `App.BaseDeDatos` para realizar operaciones. Para la navegación entre ventanas, se emplea la clase estática `AtajosDeVentana`.

- **Templates reutilizables**: En `AtajosDeVentana`, se definieron funciones aplicables a cualquier ventana, permitiendo una navegación flexible a través de parámetros que especifican el nombre de la clase de destino. La sintaxis `this.` facilita el uso de la ventana actual en estas transiciones.

- **Polimorfismo**: `BaseDeDatosSQL` y `BaseDeDatosJSON` heredan de `BaseDeDatosAbstracta`, una clase abstracta que obliga a ambas a implementar métodos públicos comunes, asegurando consistencia en el acceso a datos.

- **Restricciones de integridad en registros**: La base de datos SQL incluye restricciones únicas (`unique constraints`), y `BaseDeDatosJSON` define funciones para verificar integridad, previniendo, por ejemplo, la eliminación de instancias con turnos asignados o modificaciones que dejen turnos sin paciente o medico.

- **Uso de expresiones lambda y LINQ**: En lugar de realizar consultas SQL directas, la clase `BaseDeDatosAbstracta` emplea expresiones lambda y consultas LINQ para filtrar y seleccionar objetos según sus propiedades, ya sea en el sistema JSON o en SQL. Esto permite poblar `ListViews` y `ComboBoxes` de manera dinámica en respuesta a eventos dentro de una ventana, logrando eficiencia y claridad con muy poco código.

- **Uso de diccionarios**: Se emplean diccionarios para almacenar las instancias de las entidades en memoria. Las claves de estos diccionarios corresponden a las claves primarias de las tablas, lo que permite acceder rápidamente a las instancias de los objetos desde cualquier ventana sin necesidad de realizar consultas repetitivas a la base de datos. 

- **Estilos personalizados (Styles)**: `App.xaml` define estilos para `Button`, `ContentControl` (creando secciones con bordes redondeados) y `TextBox`. Los archivos `.xaml` en toda la aplicación pueden crear y aplicar estos estilos para mantener coherencia visual.

- **Validación de integridad de datos ingresados**: Las longitudes máximas de los campos coinciden con las definiciones de la base de datos, y no se permite guardar cambios en ventanas de agregar/modificar si hay campos obligatorios vacíos.

- **Inicio de sesión y conexión de base de datos**: El sistema permite al usuario iniciar sesión para establecer una conexión con Microsoft SQL Server o, alternativamente, trabajar con archivos locales en formato JSON.

- **Script de instalación de base de datos**: Se incluye un script autoejecutables (si la base de datos no existe) que permite crear la base de datos completa en cualquier equipo con una docena de Inserts predeterminados, facilitando el testeo en Modo SQL.

- **Librerias utilizadas**: Newtonsonft.Json y System.Data.SqlCliente.