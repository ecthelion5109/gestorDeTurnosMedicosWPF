using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;

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
		public void CreateMedico(Medico instancia) {
			this.MiDiccionario["medicos"][instancia.Dni] = instancia;
			this.GuardarJson();
			MessageBox.Show($"Exito: Se ha creado la instancia de Medico: {instancia.Name} {instancia.LastName}");
		}
		public void CreatePaciente(Paciente instancia) {
			this.MiDiccionario["pacientes"][instancia.Dni] = instancia;
			this.GuardarJson();
			MessageBox.Show($"Exito: Se ha creado la instancia de Paciente: {instancia.Name} {instancia.LastName}");
		}

		public void CreateTurno(Turno turno) {
			MessageBox.Show($"No se ha implementado esto en json");
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
        public void UpdateMedico(Medico instancia, string originalDni){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
			} 
				
			if (originalDni != instancia.Dni) {
				this.MiDiccionario["medicos"].Remove(originalDni);
				this.MiDiccionario["medicos"][instancia.Dni] = instancia;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
		}
        public void UpdatePaciente(Paciente instancia, string originalDni){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
			} 
				
			if (originalDni != instancia.Dni) {
				this.MiDiccionario["pacientes"].Remove(originalDni);
				this.MiDiccionario["pacientes"][instancia.Dni] = instancia;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
		}
        public void UpdateTurno(Turno turno) {
			MessageBox.Show($"Sin definir.");
		}
		//------------------------public.DELETE----------------------//
		
		public bool DeleteMedico(string idToRemove) {
			try {
				this.MiDiccionario["medicos"].Remove(idToRemove);
				this.GuardarJson(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el medico con id: {idToRemove} del Json");
				return true;
			} catch (Exception ex) {
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el medico", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;

			}
		}
		public bool DeletePaciente(string idToRemove){
			try {
				this.MiDiccionario["pacientes"].Remove(idToRemove);
				this.GuardarJson(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el paciente con id: {idToRemove} del Json");
				return true;
			} catch (Exception ex) { 
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el paciente", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}

        public bool DeleteTurno(string idToRemove) {
			MessageBox.Show($"Sin implementar");
			return false;
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
		
		
		
		


		//------------------------GET-PROPERTIES----------------------//
		public string LoadPacienteNombreCompletoFromDatabase(string instance_id) {
			return "LoadPacienteNombreCompletoFromDatabase";
		}

		public string LoadMedicoNombreCompletoFromDatabase(string instance_id) {
			return "LoadMedicoNombreCompletoFromDatabase";
		}


		public string LoadEspecialidadFromDatabase(string instance_id) {
			return "LoadEspecialidadFromDatabase";
		}

		
		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
