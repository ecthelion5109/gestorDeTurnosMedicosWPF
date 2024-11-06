using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ClinicaMedica {
	public class BaseDeDatosJSON : IBaseDeDatos{
		public string archivoPath = "database.json";
		public Dictionary<string, Dictionary<string, object>> MiDiccionario;
		public static Dictionary<string, Medico> DictMedicos { get; private set; } = new ();
		public static Dictionary<string, Paciente> DictPacientes { get; private set; } = new ();
		public static List<Turno> ListTurnos { get; private set; } = new ();
		
		
		public BaseDeDatosJSON() {
			this.LLenarMiDiccionarioConNewtonsoftJson();
		}
		
		//------------------------CREATE----------------------//
		public bool CreateMedico(Medico instancia) {
			bool YaExiste(string key){
				return this.MiDiccionario["medicos"].ContainsKey(key);
			}
		
			if (YaExiste(instancia.Dni)) {
				return false;
			}
			this.MiDiccionario["medicos"][instancia.Dni] = instancia;
			this.GuardarJson();
			MessageBox.Show($"Exito: Se ha creado la instancia de Medico: {instancia.Name} {instancia.LastName}");
			return true;
		}
		public bool CreatePaciente(Paciente instancia) {
			bool YaExiste(string key){
				return this.MiDiccionario["pacientes"].ContainsKey(key);
			}
		
			if (YaExiste(instancia.Dni)) {
				return false;
			}
			this.MiDiccionario["pacientes"][instancia.Dni] = instancia;
			this.GuardarJson();
			MessageBox.Show($"Exito: Se ha creado la instancia de Paciente: {instancia.Name} {instancia.LastName}");
			return true;
		}

		public bool CreateTurno(Turno instancia) {
			bool YaExiste(string key){
				return this.MiDiccionario["turnos"].ContainsKey(key);
			}
			
			if (YaExiste(instancia.Id)) {
				return false;
			}
			MessageBox.Show($"No se ha implementado esto en json");
			return true;
		}

		//------------------------public.READ----------------------//
		public List<Turno> ReadTurnosWhereMedicoId(Medico instance) {
			if (instance is null){
				return null;
			}
			return ListTurnos.Where(t => t.MedicoId == instance.Id).ToList();
		}
		public List<Turno> ReadTurnosWherePacienteId(Paciente instance) {
			if (instance is null){
				return null;
			}
			return ListTurnos.Where(t => t.PacienteId == instance.Id).ToList();
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
        public bool UpdateMedico(Medico instancia, string originalDni){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
				return false;
			} 
				
			if (originalDni != instancia.Dni) {
				this.MiDiccionario["medicos"].Remove(originalDni);
				this.MiDiccionario["medicos"][instancia.Dni] = instancia;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
			return true;
		}
        public bool UpdatePaciente(Paciente instancia, string originalDni){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
				return false;
			} 
				
			if (originalDni != instancia.Dni) {
				this.MiDiccionario["pacientes"].Remove(originalDni);
				this.MiDiccionario["pacientes"][instancia.Dni] = instancia;
			}
			this.GuardarJson(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
			return true;
		}
        public bool UpdateTurno(Turno instancia) {
			MessageBox.Show($"Sin definir.");
			return false;
		}
		//------------------------public.DELETE----------------------//
		
		public bool DeleteMedico(Medico instancia) {
			try {
				this.MiDiccionario["medicos"].Remove(instancia.Id);
				this.GuardarJson(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el medico con id: {instancia.Id} del Json");
				return true;
			} catch (Exception ex) {
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el medico", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;

			}
		}
		public bool DeletePaciente(Paciente instancia){
			try {
				this.MiDiccionario["pacientes"].Remove(instancia.Id);
				this.GuardarJson(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el paciente con id: {instancia.Id} del Json");
				return true;
			} catch (Exception ex) { 
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el paciente", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}

        public bool DeleteTurno(Turno instancia) {
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
						var instancia = JsonConvert.DeserializeObject<Paciente>(pacienteElement.Value.ToString());
						pacientes[pacienteElement.Key] = instancia;
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
						var instancia = new Medico(medicoElement.Key, medicoJsonElement);
						medicos[medicoElement.Key] = instancia;
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
						var instancia = JsonConvert.DeserializeObject<Turno>(turnoElement.Value.ToString());
						turnos[turnoElement.Key] = instancia;
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
			return "LoadPacienteConcatNotImplemented";
		}

		public string LoadMedicoNombreCompletoFromDatabase(string instance_id) {
			return "LoadMedicoConcatNotImplemented";
		}


		public string LoadEspecialidadFromDatabase(string instance_id) {
			return "LoadEspecialidadConcatNotImplemented";
		}

		
		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
