using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Controls;

namespace ClinicaMedica {
	//---------------------------------Funciones-------------------------------//
	public class BaseDeDatos {
		//------------------------Properties------------------//
		public static string archivoPath = "database.json";
		private static Dictionary<string, Dictionary<string, object>> _database;
		public static Dictionary<string, Dictionary<string, object>> Database => _database ??= LeerDatabaseComoDiccionarioNewtonsoft();
		
		
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

		//------------------------Publico----------------------//
		public static void UpdateJsonFile()
		{
			string jsonString = JsonConvert.SerializeObject(Database, Formatting.Indented);
			File.WriteAllText(archivoPath, jsonString);
		}
		//---------------------------------methodsOld-------------------------------//
		public static void GuardarComoJson<T>(T objeto, string archivo) {
			var opciones = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
			string jsonString = System.Text.Json.JsonSerializer.Serialize(objeto, opciones);
			File.WriteAllText(archivo, jsonString);
		}
		public static T LeerDesdeJson<T>(string archivo) {
			string jsonString = File.ReadAllText(archivo);
			return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
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



	}
}
