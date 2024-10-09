using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;

namespace ClinicaMedica {
	//---------------------------------Tablas-------------------------------//
	public class Paciente {
		// public int Dni { get; set; }
		public string Dni { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public DateTime FechaIngreso { get; set; }  // Corrige a DateTime
		public string Email { get; set; }
		public string Telefono { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public string Direccion { get; set; }
		public string Localidad { get; set; }
		public string Provincia { get; set; }
			
		// Factory method to convert a JsonElement to a Paciente instance
		public static Paciente FromJson(JsonElement json)
		{
			return new Paciente
			{
				Dni = json.GetProperty("Dni").GetString(), //.GetInt32(),
				Name = json.GetProperty("Name").GetString(),
				Lastname = json.GetProperty("Lastname").GetString(),
				FechaIngreso = json.GetProperty("FechaIngreso").GetDateTime(),
				Email = json.GetProperty("Email").GetString(),
				Telefono = json.GetProperty("Telefono").GetString(),
				FechaNacimiento = json.GetProperty("FechaNacimiento").GetDateTime(),
				Direccion = json.GetProperty("Direccion").GetString(),
				Localidad = json.GetProperty("Localidad").GetString(),
				Provincia = json.GetProperty("Provincia").GetString()
			};
		}
	}
	
	public class Medico {
		public string Name { get; set; }  // 50 caracteres máximo
		public string Lastname { get; set; }  // 50 caracteres máximo
		// public int Dni { get; set; }
		public string Dni { get; set; }
		public string Provincia { get; set; }  // 40 caracteres máximo
		public string Domicilio { get; set; }  // 50 caracteres máximo
		public string Localidad { get; set; }  // 50 caracteres máximo
		public string Specialidad { get; set; }  // 20 caracteres máximo
		public string Telefono { get; set; }
		public bool Guardia { get; set; }
		public DateTime FechaIngreso { get; set; }  //delimator. No puede haber ingresado hace 100 años ni haber ingresado en el futuro
		public double SueldoMinimoGarantizado { get; set; } //no puede tener cero ni numeros negativos
		public Dictionary<string, (string start, string end)> DiasDeAtencion { get; set; } = new Dictionary<string, (string start, string end)>();
		
		// Factory method for instantiating from JSON
		public static Medico FromJson(JsonElement jsonElement)
		{
			var medico = new Medico
			{
				Name = jsonElement.GetProperty("Name").GetString(),
				Lastname = jsonElement.GetProperty("Lastname").GetString(),
				Dni = jsonElement.GetProperty("Dni").GetString(), //.GetInt32(),
				Provincia = jsonElement.GetProperty("Provincia").GetString(),
				Domicilio = jsonElement.GetProperty("Domicilio").GetString(),
				Localidad = jsonElement.GetProperty("Localidad").GetString(),
				Specialidad = jsonElement.GetProperty("Specialidad").GetString(),
				Telefono = jsonElement.GetProperty("Telefono").GetString(),
				Guardia = jsonElement.GetProperty("Guardia").GetBoolean(),
				FechaIngreso = DateTime.Parse(jsonElement.GetProperty("FechaIngreso").GetString()),
				SueldoMinimoGarantizado = jsonElement.GetProperty("SueldoMinimoGarantizado").GetDouble()
			};

			// Read days of attention
			if (jsonElement.TryGetProperty("DiasDeAtencion", out var diasDeAtencionElement))
			{
				foreach (var dia in diasDeAtencionElement.EnumerateObject())
				{
					var diaKey = dia.Name;
					var start = dia.Value.GetProperty("start").GetString();
					var end = dia.Value.GetProperty("end").GetString();
					medico.DiasDeAtencion[diaKey] = (start, end);
				}
			}

			return medico;
		}
	}
	public class Turno {
		public int MedicoPk { get; set; }
		public int PacientePk { get; set; }
		public DateTime FechaYHoraAsignada { get; set; }
		
		// Factory method for instantiating from JSON
		public static Turno FromJson(JsonElement jsonElement)
		{
			return new Turno
			{
				MedicoPk = jsonElement.GetProperty("MedicoPk").GetInt32(),
				PacientePk = jsonElement.GetProperty("PacientePk").GetInt32(),
				FechaYHoraAsignada = DateTime.Parse(jsonElement.GetProperty("FechaYHoraAsignada").GetString())
			};
		}
	}
	//---------------------------------Funciones-------------------------------//
	public class BaseDeDatos {

		public static Dictionary<string, Dictionary<string, object>> LeerDatabaseComoDiccionario()
		{
			string filePath = "database.json";
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("El archivo no existe.");
			}

			string jsonString = File.ReadAllText(filePath);
			var database = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString);

			// Prepare a dictionary to hold the objects
			var result = new Dictionary<string, Dictionary<string, object>>();

			// Check and populate "pacientes"
			if (database.ContainsKey("pacientes"))
			{
				var pacientes = new Dictionary<string, object>();
				var pacientesElement = database["pacientes"];
				foreach (var pacienteElement in pacientesElement.EnumerateObject())
				{
					var paciente = Paciente.FromJson(pacienteElement.Value); // Convert JsonElement to Paciente object
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
					var medico = Medico.FromJson(medicoElement.Value); // Convert JsonElement to Medico object
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
					var turno = Turno.FromJson(turnoElement.Value); // Convert JsonElement to Turno object
					string key = turnoElement.Name; // Combine MedicoPk and PacientePk as the key
					turnos[key] = turno;
				}
				result["turnos"] = turnos;
			}

			return result;
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
			var opciones = new JsonSerializerOptions { WriteIndented = true };
			string jsonString = JsonSerializer.Serialize(objeto, opciones);
			File.WriteAllText(archivo, jsonString);
		}
		public static T LeerDesdeJson<T>(string archivo) {
			string jsonString = File.ReadAllText(archivo);
			return JsonSerializer.Deserialize<T>(jsonString);
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
