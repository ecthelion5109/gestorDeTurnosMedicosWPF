using System.Windows;
using System.IO;
using Newtonsoft.Json;


namespace ClinicaMedica {


	public class BaseDeDatosJSON : IBaseDeDatos{
		//------------------------public----------------------//
		public string archivoPath = "database.json";
		public Dictionary<string, Dictionary<string, object>> Database => _database ??= LeerDatabaseComoDiccionarioNewtonsoft();
		
		
		
		
		//------------------------public.CHECKERS----------------------//
		public bool CorroborarQueNoExistaMedico(string key){
			return this.Database["medicos"].ContainsKey(key);
		}
		public bool CorroborarQueNoExistaPaciente(string key){
			return this.Database["pacientes"].ContainsKey(key);
		}
		public bool CorroborarQueNoExistaTurno(string key){
			return this.Database["turnos"].ContainsKey(key);
		}
		
		
		
		
		//------------------------public.CREATE----------------------//
		public OperationCode CreateMedico(Medico medico){
			
			
			this.Database["medicos"][medico.Dni] = medico;
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.CREATE_SUCCESS;
		}
		public OperationCode CreatePaciente(Paciente paciente){
			this.Database["pacientes"][paciente.Dni] = paciente;
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.CREATE_SUCCESS;
		}

		public  OperationCode CreateTurno(Turno turno) {
			return OperationCode.SIN_DEFINIR;
		}

		//------------------------public.READ----------------------//
		public List<Medico> ReadMedicos() {
			return this.Database["medicos"].Values.Cast<Medico>().ToList();
		}
        public List<Paciente> ReadPacientes() {
            return this.Database["pacientes"].Values.Cast<Paciente>().ToList();
        }
        public List<Turno> ReadTurnos() {
            return this.Database["turnos"].Values.Cast<Turno>().ToList();
        }
        //------------------------public.UPDATE----------------------//
        public OperationCode UpdateMedico(Medico medico, string originalDni){
			if (string.IsNullOrEmpty(medico.Dni)) {
				return OperationCode.MISSING_DNI;
			} 
				
			if (originalDni != medico.Dni) {
				this.Database["medicos"].Remove(originalDni);
				this.Database["medicos"][medico.Dni] = medico;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.UPDATE_SUCCESS;
		}
        public OperationCode UpdatePaciente(Paciente paciente, string originalDni){
			if (string.IsNullOrEmpty(paciente.Dni)) {
				return OperationCode.MISSING_DNI;
			} 
				
			if (originalDni != paciente.Dni) {
				this.Database["pacientes"].Remove(originalDni);
				this.Database["pacientes"][paciente.Dni] = paciente;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.UPDATE_SUCCESS;
		}
        public OperationCode UpdateTurno(Turno turno) {
			return OperationCode.SIN_DEFINIR;
		}
		//------------------------public.DELETE----------------------//
		
		public OperationCode DeleteMedico(string medicoId){
			this.Database["medicos"].Remove(medicoId);
			this.GuardarJson(); // Save changes to the database
			return OperationCode.DELETE_SUCCESS;
		}
		public OperationCode DeletePaciente(string pacienteId){
			this.Database["pacientes"].Remove(pacienteId);
			this.GuardarJson(); // Save changes to the database
			return OperationCode.DELETE_SUCCESS;
		}

        public OperationCode DeleteTurno(string turnoId) {
			return OperationCode.SIN_DEFINIR;
		}
		
		
		
		//------------------------Private----------------------//
		private Dictionary<string, Dictionary<string, object>> _database;
		private Dictionary<string, Dictionary<string, object>> LeerDatabaseComoDiccionarioNewtonsoft(){
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
				if (database.ContainsKey("medicos"))
				{
					var medicos = new Dictionary<string, object>();
					foreach (var medicoElement in database["medicos"])
					{
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
		
		private void GuardarJson(){ 
			string jsonString = JsonConvert.SerializeObject(Database, Formatting.Indented);
			File.WriteAllText(archivoPath, jsonString);
		}
		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
