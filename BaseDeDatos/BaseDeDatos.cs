using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicaMedica {
	public enum DatabaseType {
		JSON,
		SQL
	}
	//---------------------------------Funciones-------------------------------//
	public class BaseDeDatos {
		public static DatabaseType TIPO = DatabaseType.JSON;
		public static string connectionString = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;





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
						var medico = new Medico(medicoElement.Key, medicoJsonElement);
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
		public static void UpdateJsonFile(){ 
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
		}

		public static List<Medico> JsonLoadMedicoData() {
			return BaseDeDatos.Database["medicos"].Values.Cast<Medico>().ToList();
		}


		//------------------------ModoSQL----------------------//
		public static void SQL_CreateMedico(Medico medico)
		{
			string query = "INSERT INTO Medico (nombre, apellido, especialidad, ...) VALUES (@nombre, @apellido, @especialidad, ...)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@nombre", medico.Name);
				command.Parameters.AddWithValue("@apellido", medico.Lastname);
				command.Parameters.AddWithValue("@especialidad", medico.Specialidad);
				// Add parameters for all other fields you want to insert

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		
		
		
		
		public static List<Medico> SQL_ReadMedicos() {
			// List to store Medico instances
			List<Medico> medicoList = new List<Medico>();

			// SQL query to retrieve data from Medico table
			string query = "SELECT * FROM Medico";

			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					SqlCommand command = new SqlCommand(query, connection);
					connection.Open();

					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read()) {
						Medico medico = new Medico {
							Dni = reader["dni"]?.ToString(),
							Name = reader["nombre"]?.ToString(),
							Lastname = reader["apellido"]?.ToString(),
							Provincia = reader["provincia"]?.ToString(),
							Domicilio = reader["domicilio"]?.ToString(),
							Localidad = reader["localidad"]?.ToString(),
							Specialidad = reader["especialidad"]?.ToString(),
							Telefono = reader["telefono"]?.ToString(),
							Guardia = reader["guardia"] != DBNull.Value ? Convert.ToBoolean(reader["guardia"]) : false,
							FechaIngreso = reader["fecha_ingreso"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_ingreso"]) : (DateTime?)null,
							SueldoMinimoGarantizado = reader["sueldo_minimo_garantizado"] != DBNull.Value ? Convert.ToDouble(reader["sueldo_minimo_garantizado"]) : 0.0
						};
						medicoList.Add(medico);
					}
					reader.Close();
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error retrieving data: " + ex.Message);
			}

			return medicoList;
		}


		public static void SQL_UpdateMedico(Medico medico)
		{
			string query = "UPDATE Medico SET nombre = @nombre, apellido = @apellido, especialidad = @especialidad, ... WHERE id = @id";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@nombre", medico.Name);
				command.Parameters.AddWithValue("@apellido", medico.Lastname);
				command.Parameters.AddWithValue("@especialidad", medico.Specialidad);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public static void SQL_DeleteMedico(string medicoId)
		{
			string query = "DELETE FROM Medico WHERE id = @id";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@id", medicoId);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

	}
}
