using System.Windows;
using System.IO;
using Newtonsoft.Json;

namespace ClinicaMedica {
	public class BaseDeDatosJSON : IBaseDeDatos{
		public string archivoPath = "database.json";
		public Dictionary<string, Dictionary<string, object>> MiDiccionario;
		
		
		public BaseDeDatosJSON() {
			this.LLenarMiDiccionarioConNewtonsoftJson();
		}
		
		
		
		//------------------------CHECKERS----------------------//
		public bool CorroborarQueNoExistaMedico(string key){
			return this.MiDiccionario["medicos"].ContainsKey(key);
		}
		public bool CorroborarQueNoExistaPaciente(string key){
			return this.MiDiccionario["pacientes"].ContainsKey(key);
		}
		public bool CorroborarQueNoExistaTurno(string key){
			return this.MiDiccionario["turnos"].ContainsKey(key);
		}
		
		
		
		
		//------------------------CREATE----------------------//
		public OperationCode CreateMedico(Medico medico) {
			this.MiDiccionario["medicos"][medico.Dni] = medico;
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.CREATE_SUCCESS;
		}
		public OperationCode CreatePaciente(Paciente paciente){
			this.MiDiccionario["pacientes"][paciente.Dni] = paciente;
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.CREATE_SUCCESS;
		}

		public  OperationCode CreateTurno(Turno turno) {
			return OperationCode.SIN_DEFINIR;
		}

		//------------------------public.READ----------------------//
		public List<Medico> ReadMedicos() {
			return this.MiDiccionario["medicos"].Values.Cast<Medico>().ToList();
		}
        public List<Paciente> ReadPacientes() {
            return this.MiDiccionario["pacientes"].Values.Cast<Paciente>().ToList();
        }
        public List<Turno> ReadTurnos() {
            return this.MiDiccionario["turnos"].Values.Cast<Turno>().ToList();
        }
        //------------------------public.UPDATE----------------------//
        public OperationCode UpdateMedico(Medico medico, string originalDni){
			if (string.IsNullOrEmpty(medico.Dni)) {
				return OperationCode.MISSING_DNI;
			} 
				
			if (originalDni != medico.Dni) {
				this.MiDiccionario["medicos"].Remove(originalDni);
				this.MiDiccionario["medicos"][medico.Dni] = medico;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.UPDATE_SUCCESS;
		}
        public OperationCode UpdatePaciente(Paciente paciente, string originalDni){
			if (string.IsNullOrEmpty(paciente.Dni)) {
				return OperationCode.MISSING_DNI;
			} 
				
			if (originalDni != paciente.Dni) {
				this.MiDiccionario["pacientes"].Remove(originalDni);
				this.MiDiccionario["pacientes"][paciente.Dni] = paciente;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			return OperationCode.UPDATE_SUCCESS;
		}
        public OperationCode UpdateTurno(Turno turno) {
			return OperationCode.SIN_DEFINIR;
		}
		//------------------------public.DELETE----------------------//
		
		public OperationCode DeleteMedico(string medicoId){
			this.MiDiccionario["medicos"].Remove(medicoId);
			this.GuardarJson(); // Save changes to the database
			return OperationCode.DELETE_SUCCESS;
		}
		public OperationCode DeletePaciente(string pacienteId){
			this.MiDiccionario["pacientes"].Remove(pacienteId);
			this.GuardarJson(); // Save changes to the database
			return OperationCode.DELETE_SUCCESS;
		}

        public OperationCode DeleteTurno(string turnoId) {
			return OperationCode.SIN_DEFINIR;
		}
		
		
		
		//------------------------Private----------------------//
		private void LLenarMiDiccionarioConNewtonsoftJson(){
			MiDiccionario = new Dictionary<string, Dictionary<string, object>>();
			if (File.Exists(archivoPath)){
				string jsonString = File.ReadAllText(archivoPath);
				var database = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonString);
				if (database.ContainsKey("pacientes")){
					var pacientes = new Dictionary<string, object>();
					foreach (var pacienteElement in database["pacientes"])
					{
						var paciente = JsonConvert.DeserializeObject<Paciente>(pacienteElement.Value.ToString());
						pacientes[pacienteElement.Key] = paciente;
					}
					MiDiccionario["pacientes"] = pacientes;
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
					MiDiccionario["medicos"] = medicos;
				}

				// MessageBox.Show("22, testing");

				// Check and convert "turnos"
				if (database.ContainsKey("turnos")){
					var turnos = new Dictionary<string, object>();
					foreach (var turnoElement in database["turnos"])
					{
						// Deserialize each entry as a Turno object
						var turno = JsonConvert.DeserializeObject<Turno>(turnoElement.Value.ToString());
						turnos[turnoElement.Key] = turno;
					}
					MiDiccionario["turnos"] = turnos;
				}
			}
			else
			{
				MessageBox.Show("Error: File not found.");
			}
		}
		
		private void GuardarJson(){ 
			string jsonString = JsonConvert.SerializeObject(MiDiccionario, Formatting.Indented);
			File.WriteAllText(archivoPath, jsonString);
		}
		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
