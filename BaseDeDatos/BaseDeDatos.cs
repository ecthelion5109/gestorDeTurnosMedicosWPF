using System.Windows;
using System.IO;
using Newtonsoft.Json;

namespace ClinicaMedica {
	//---------------------------------Funciones-------------------------------//
	public class BaseDeDatos {
		public static string archivoPath = "database.json";
		
		
		
		//------------------------Properties------------------//
		private static Dictionary<string, Dictionary<string, object>> _database;
		public static Dictionary<string, Dictionary<string, object>> Database 
			=> _database ??= LeerDatabaseComoDiccionarioNewtonsoft();
		
		
		//------------------------Private----------------------//
		private static Dictionary<string, Dictionary<string, object>> LeerDatabaseComoDiccionarioNewtonsoft()
		{
			// Prepara un diccionario para almacenar los objetos
			var result = new Dictionary<string, Dictionary<string, object>>();

			// Check if the file exists
			if (File.Exists(archivoPath))
			{
				string jsonString = File.ReadAllText(archivoPath);

				// Deserialize as Dictionary of objects (the object type is the key difference)
				var database = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonString);

				// Check and convert "pacientes"
				if (database.ContainsKey("pacientes"))
				{
					var pacientes = new Dictionary<string, object>();
					foreach (var pacienteElement in database["pacientes"])
					{
						// Deserialize each entry as a Paciente object
						var paciente = JsonConvert.DeserializeObject<Paciente>(pacienteElement.Value.ToString());
						pacientes[pacienteElement.Key] = paciente;
					}
					result["pacientes"] = pacientes;
				}

				// MessageBox.Show("11, testing");
				// Check and convert "medicos"
				if (database.ContainsKey("medicos"))
				{
					var medicos = new Dictionary<string, object>();
					foreach (var medicoElement in database["medicos"])
					{
						// Use the custom constructor for Medico that takes JsonElement
						var medicoJsonElement = System.Text.Json.JsonDocument.Parse(medicoElement.Value.ToString()).RootElement;
						var medico = new Medico(medicoJsonElement);
						medicos[medicoElement.Key] = medico;
					}
					result["medicos"] = medicos;
				}

				// MessageBox.Show("22, testing");

				// Check and convert "turnos"
				if (database.ContainsKey("turnos"))
				{
					var turnos = new Dictionary<string, object>();
					foreach (var turnoElement in database["turnos"])
					{
						// Deserialize each entry as a Turno object
						var turno = JsonConvert.DeserializeObject<Turno>(turnoElement.Value.ToString());
						turnos[turnoElement.Key] = turno;
					}
					result["turnos"] = turnos;
				}
			}
			else
			{
				MessageBox.Show("Error: File not found.");
			}

			return result;
		}

		
		private static Dictionary<string, Dictionary<string, object>> LeerDatabaseComoDiccionario()
		{
			string filePath = "database.json";
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("El archivo no existe.");
			}

			string jsonString = File.ReadAllText(filePath);
			var database = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(jsonString);

			// Prepare a dictionary to hold the objects
			var result = new Dictionary<string, Dictionary<string, object>>();

			// Check and populate "pacientes"
			if (database.ContainsKey("pacientes"))
			{
				var pacientes = new Dictionary<string, object>();
				var pacientesElement = database["pacientes"];
				foreach (var pacienteElement in pacientesElement.EnumerateObject())
				{
					var paciente = new Paciente(pacienteElement.Value); // Convert JsonElement to Paciente object
																			 // int dni = int.Parse(pacienteElement.Name); // The DNI is used as the key
					string dni = pacienteElement.Name; // The DNI is used as the key
					pacientes[dni] = paciente;
				}
				result["pacientes"] = pacientes;
			}

			// Check and populate "medicos"
			if (database.ContainsKey("medicos"))
			{
				var medicos = new Dictionary<string, object>();
				var medicosElement = database["medicos"];
				foreach (var medicoElement in medicosElement.EnumerateObject())
				{
					var medico = new Medico(medicoElement.Value); // Convert JsonElement to Medico object
					// int dni = int.Parse(medicoElement.Name); // The DNI is used as the key
					string dni = medicoElement.Name; // The DNI is used as the key
					medicos[dni] = medico;
				}
				result["medicos"] = medicos;
			}

			// Check and populate "turnos"
			if (database.ContainsKey("turnos"))
			{
				var turnos = new Dictionary<string, object>();
				var turnosElement = database["turnos"];
				foreach (var turnoElement in turnosElement.EnumerateObject())
				{
					var turno = new Turno(turnoElement.Value); // Convert JsonElement to Turno object
					string key = turnoElement.Name; // Combine MedicoPk and PacientePk as the key
					turnos[key] = turno;
				}
				result["turnos"] = turnos;
			}

			return result;
		}

		
		private static void UpdateJsonFile()
		{
			string jsonString = JsonConvert.SerializeObject(Database, Formatting.Indented);
			File.WriteAllText(archivoPath, jsonString);
		}

		private static void AsignarValoresAMedico(Medico medico, MedicosModificar ventana) {
			medico.Name = ventana.txtNombre.Text;
			medico.Lastname = ventana.txtApellido.Text;
			medico.Dni = ventana.txtDNI.Text;
			medico.Provincia = ventana.txtProvincia.Text;
			medico.Domicilio = ventana.txtDomicilio.Text;
			medico.Localidad = ventana.txtLocalidad.Text;
			medico.Specialidad = ventana.txtEspecialidad.Text;
			medico.Guardia = (bool) ventana.txtRealizaGuardia.IsChecked;
			medico.FechaIngreso = (DateTime) ventana.txtFechaIngreso.SelectedDate;
			medico.SueldoMinimoGarantizado = double.Parse(ventana.txtSueldoMinGarant.Text);
		}
		
		//------------------------Publico----------------------//
		public static void AplicarYGuardarMedico(MedicosModificar ventana) {
			Medico medicoModificado;

			if (Database["medicos"].ContainsKey(ventana.txtDNI.Text)) {
				medicoModificado = (Medico) Database["medicos"][ventana.txtDNI.Text];
				MessageBox.Show($"Se han guardado los cambios de Medico: {medicoModificado.Name} {medicoModificado.Lastname}");
			} else {
				medicoModificado = new Medico();
				Database["medicos"][medicoModificado.Dni] = medicoModificado;
				MessageBox.Show($"Se ha creado un nuevo Medico: {medicoModificado.Name} {medicoModificado.Lastname}");
			}

			AsignarValoresAMedico(medicoModificado, ventana);

			// Save changes to the database
			BaseDeDatos.UpdateJsonFile();
		}
		
		public static void Guardar() {
			var database = LeerDatabaseComoDiccionario();

			if (database.ContainsKey("pacientes") && database["pacientes"].ContainsKey("40350997"))
			{
				Paciente paciente = (Paciente)database["pacientes"]["40350997"];
				MessageBox.Show($"Se ha leido a Paciente: {paciente.Name} {paciente.Lastname}");
			}

			if (database.ContainsKey("medicos") && database["medicos"].ContainsKey("87654321"))
			{
				Medico medico = (Medico)database["medicos"]["87654321"];
				MessageBox.Show($"Se ha leido a Medico: {medico.Name} {medico.Lastname}");
			}

			if (database.ContainsKey("turnos") && database["turnos"].ContainsKey("87654321_10350123"))
			{
				Turno turno = (Turno)database["turnos"]["87654321_10350123"];
				MessageBox.Show($"Turno asignado: {turno.FechaYHoraAsignada}");
			}
			
			// MessageBox.Show($"Se ha leido a Paciente: {database['pacientes'][0].Name} {database['pacientes'][0].Lastname}}");
		}
		
		
		public static void TestLeer2() {
			var database = LeerDatabaseComoDiccionario();

			if (database.ContainsKey("pacientes") && database["pacientes"].ContainsKey("40350997"))
			{
				Paciente paciente = (Paciente)database["pacientes"]["40350997"];
				MessageBox.Show($"Se ha leido a Paciente: {paciente.Name} {paciente.Lastname}");
			}

			if (database.ContainsKey("medicos") && database["medicos"].ContainsKey("87654321"))
			{
				Medico medico = (Medico)database["medicos"]["87654321"];
				MessageBox.Show($"Se ha leido a Medico: {medico.Name} {medico.Lastname}");
			}

			if (database.ContainsKey("turnos") && database["turnos"].ContainsKey("87654321_10350123"))
			{
				Turno turno = (Turno)database["turnos"]["87654321_10350123"];
				MessageBox.Show($"Turno asignado: {turno.FechaYHoraAsignada}");
			}
			
			// MessageBox.Show($"Se ha leido a Paciente: {database['pacientes'][0].Name} {database['pacientes'][0].Lastname}}");
		}
		
		
		
		
		
		
		
		
		
		
		
		
		//---------------------------------methodsOld-------------------------------//
		public static void GuardarComoJson<T>(T objeto, string archivo) {
			var opciones = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
			string jsonString = System.Text.Json.JsonSerializer.Serialize(objeto, opciones);
			File.WriteAllText(archivo, jsonString);
		}
		public static void GuardarComoJsonNewtonsoft<T>(T objeto, string archivo) {
			// Serializa el objeto usando Newtonsoft.Json y la opción de indentación
			string jsonString = JsonConvert.SerializeObject(objeto, Formatting.Indented);
			File.WriteAllText(archivo, jsonString);  // Guarda el archivo
		}
		public static T LeerDesdeJson<T>(string archivo) {
			string jsonString = File.ReadAllText(archivo);
			return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
		}
		public static void TestLeer() {
			// Leer desde JSON
			Paciente pacienteLeido = BaseDeDatos.LeerDesdeJson<Paciente>("paciente.json");
			Medico medicoLeido = BaseDeDatos.LeerDesdeJson<Medico>("medico.json");
			Turno turnoLeido = BaseDeDatos.LeerDesdeJson<Turno>("turno.json");

			MessageBox.Show($"Se ha leido a Paciente: {pacienteLeido.Name} {pacienteLeido.Lastname}\nSe ha leido a Medico: {medicoLeido.Name} {medicoLeido.Lastname}\nSe ha leido a Turno Asignado: {turnoLeido.FechaYHoraAsignada}");
		}
		public static void GuardarTurno(int dniPaciente, int dniMedico, DateTime fecha) {
			var turno = new Turno {
				MedicoPk = dniPaciente,
				PacientePk = dniMedico,
				FechaYHoraAsignada = fecha,
			};
			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(turno, "turno.json");

			MessageBox.Show($"Se ha guardado el turno para la fecha: {turno.FechaYHoraAsignada}");


			/*
			var turnos = new List<Turno>();

			// Generar 10 turnos por cada hora de 8 a 17
			for (int hora = 8; hora <= 17; hora++) {
				for (int turno = 1; turno <= 10; turno++) {
					turnos.Add(new Turno {
						Hora = $"{hora:00}:00",
						NumeroTurno = turno,
						Estado = "Disponible" // Puedes modificar esto según el estado real
					});
				}
			}

			// Asignar la lista de turnos al DataGrid
			DataGridTurnos.ItemsSource = turnos;
			*/
		}

		public static void MedicosGuardar(int dni, string name, string lastname, DateTime fechaingreso, string domicilio, string localidad, string provincia, string specialidad, bool guardia, decimal sueldominimogarantizado) {
			/*
			// Crear instancia de Medico
			var medico = new Medico {
				Name = name,
				Lastname = lastname,
				Dni = dni,
				Provincia = provincia,
				Domicilio = domicilio,
				Localidad = localidad,
				Specialidad = specialidad,
				// Telefono = telefono,
				Guardia = guardia,
				FechaIngreso = fechaingreso,
				SueldoMinimoGarantizado = (double)sueldominimogarantizado,
				//DiasDeAtencion = diasdeatencion,
			};
			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(medico, "medico.json");

			MessageBox.Show($"Se han guardado los cambios de Paciente: {medico.Name} {medico.Lastname}");
			*/
		}





		public static void PacienteGuardar(int dni, string name, string lastname, DateTime fechaingreso, string email, string telefono, DateTime fechanacimiento, string direccion, string localidad, string provincia) {
			/*
			// Crear instancias de ejemplo
			var paciente = new Paciente {
				Dni = dni,
				Name = name,
				Lastname = lastname,
				FechaIngreso = fechaingreso,
				Email = email,
				Telefono = telefono,
				FechaNacimiento = fechanacimiento,
				Direccion = direccion,
				Localidad = localidad,
				Provincia = provincia
			};
			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(paciente, "paciente.json");

			MessageBox.Show($"Se ha instanciado y guardado a Paciente: {paciente.Name} {paciente.Lastname}");
			*/
		}




		public static void TestGuardar() {
			// Crear instancias de ejemplo
			var paciente = new Paciente {
				Dni = "12345678",
				Name = "Juan",
				Lastname = "Pérez",
				FechaIngreso = DateTime.Now,
				Email = "juan.perez@example.com",
				Telefono = "123456789",
				FechaNacimiento = new DateTime(1980, 5, 15)
			};

			var medico = new Medico {
				Dni = "87654321",
				Name = "Dr. Ana",
				Lastname = "Gómez",
				Provincia = "Buenos Aires",
				Domicilio = "Calle Falsa 123",
				Localidad = "Ciudad X",
				Specialidad = "Cardiología",
				Telefono = "987654321",
				//DiasDeAtencion = new[] { "Lunes", "Miércoles", "Viernes" },
				Guardia = true,
				FechaIngreso = DateTime.Now,
				SueldoMinimoGarantizado = 150000
			};

			var turno = new Turno {
				MedicoPk = 87654321,
				PacientePk = 12345678,
				FechaYHoraAsignada = DateTime.Now.AddHours(2)
			};

			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(paciente, "paciente.json");
			BaseDeDatos.GuardarComoJson(medico, "medico.json");
			BaseDeDatos.GuardarComoJson(turno, "turno.json");

			MessageBox.Show($"Se ha instanciado y guardado a Paciente: {paciente.Name} {paciente.Lastname}\nSe ha instanciado y guardado a Medico: {medico.Name} {medico.Lastname}\nSe ha instanciado y guardado a Turno Asignado: {turno.FechaYHoraAsignada}");
		}
	}
}
