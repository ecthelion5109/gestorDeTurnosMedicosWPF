using System.Windows;
using System.IO;
using Newtonsoft.Json;


namespace ClinicaMedica {
	public class BaseDeDatosJSON {

		//------------------------public----------------------//
		public static string archivoPath = "database.json";
		public static Dictionary<string, Dictionary<string, object>> Database => _database ??= LeerDatabaseComoDiccionarioNewtonsoft();
		
		//------------------------public.CREATE----------------------//
		public static OperationCode CreateMedico(Medico medico){
			BaseDeDatosJSON.Database["medicos"][medico.Dni] = medico;
			BaseDeDatosJSON.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.CREATE_SUCCESS;
		}

		//------------------------public.READ----------------------//
		public static List<Medico> ReadMedicos() {
			return BaseDeDatosJSON.Database["medicos"].Values.Cast<Medico>().ToList();
		}
		
		//------------------------public.UPDATE----------------------//
		public static OperationCode UpdateMedico(Medico medico, string originalDni){
			if (string.IsNullOrEmpty(medico.Dni)) {
				return OperationCode.MISSING_DNI;
			} 
				
			if (originalDni != medico.Dni) {
				BaseDeDatosJSON.Database["medicos"].Remove(originalDni);
				BaseDeDatosJSON.Database["medicos"][medico.Dni] = medico;
			}
			BaseDeDatosJSON.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.UPDATE_SUCCESS;
		}
		//------------------------public.DELETE----------------------//
		
		public static OperationCode DeleteMedico(string medicoId){
			BaseDeDatosJSON.Database["medicos"].Remove(medicoId);
			BaseDeDatosJSON.GuardarJson(); // Save changes to the database
			return OperationCode.DELETE_SUCCESS;
		}
		
		
		
		//------------------------Private----------------------//
		private static Dictionary<string, Dictionary<string, object>> _database;
		private static Dictionary<string, Dictionary<string, object>> LeerDatabaseComoDiccionarioNewtonsoft(){
			// Prepara un diccionario para almacenar los objetos
			var result = new Dictionary<string, Dictionary<string, object>>();

			// Check if the file exists
			if (File.Exists(archivoPath))
			{
				string jsonString = File.ReadAllText(archivoPath);

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
		
		private static void GuardarJson(){ 
			string jsonString = JsonConvert.SerializeObject(Database, Formatting.Indented);
			File.WriteAllText(archivoPath, jsonString);
		}
		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
