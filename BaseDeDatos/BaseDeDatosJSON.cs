using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ClinicaMedica {
	public class BaseDeDatosJSON : BaseDeDatosAbstracta{
		static string medicosPath = "jsonDatabase/medicos.json";
		static string pacientesPath = "jsonDatabase/pacientes.json";
		static string turnosPath = "jsonDatabase/turnos.json";
		
		public BaseDeDatosJSON() {
			JsonCargarMedicos(medicosPath);
			JsonCargarPacientes(pacientesPath);
			JsonCargarTurnos(turnosPath);
		}
		
		
		//------------------------public.CREATE.Medico----------------------//
		public override bool CreateMedico(Medico instancia) {
			if (!DictMedicos.Values.Any(i => i.Dni == instancia.Dni)){
				MessageBox.Show($"Error: Ya hay un medico con ese Dni.");
				return false;
			}
			instancia.Id = GenerateNextId( DictMedicos );
			DictMedicos[instancia.Id] = instancia;
			this.JsonUpdateMedicos();
			MessageBox.Show($"Exito: Se ha creado la instancia de Medico: {instancia.Name} {instancia.LastName}");
			return true;
		}
		//------------------------public.CREATE.Paciente----------------------//
		public override bool CreatePaciente(Paciente instancia) {
			if (!DictPacientes.Values.Any(i => i.Dni == instancia.Dni)){
				MessageBox.Show($"Error: Ya hay un paciente con ese Dni.");
				return false;
			}
			instancia.Id = GenerateNextId(DictPacientes);
			DictPacientes[instancia.Id] = instancia;
			this.JsonUpdatePacientes();
			MessageBox.Show($"Exito: Se ha creado la instancia de Paciente: {instancia.Name} {instancia.LastName}");
			return true;
		}
		//------------------------public.CREATE.Turno----------------------//
		public override bool CreateTurno(Turno instancia) {
			if (ErroresDeConstraintDeTurnos(instancia)){
				return false;
			}
			
			instancia.Id = GenerateNextId(DictTurnos);
			DictTurnos[instancia.Id] = instancia;
			this.JsonUpdateTurnos();
			MessageBox.Show($"Exito: Se ha creado la instancia de Turno con Id: {instancia.Id}");
			return true;
		}
		
		
		
		
		
		
		
		
		//------------------------public.READ----------------------//
		public override List<Medico> ReadMedicos() {
			return DictMedicos.Values.Cast<Medico>().ToList();
		}
        public override List<Paciente> ReadPacientes() {
            return DictPacientes.Values.Cast<Paciente>().ToList();
        }
        public override List<Turno> ReadTurnos() {
            return DictTurnos.Values.Cast<Turno>().ToList();
        }
		
		
		
		
		
		
		
		
		
		
		//------------------------public.UPDATE.Medico----------------------//
        public override bool UpdateMedico(Medico instancia){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
				return false;
			} 
			if (ErroresDeConstraintDeMedicos(instancia)){
				return false;
			}
			this.JsonUpdateMedicos(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
			return true;
		}
		//------------------------public.UPDATE.Paciente----------------------//
        public override bool UpdatePaciente(Paciente instancia){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
				return false;
			} 
			if (ErroresDeConstraintDePacientes(instancia)){
				return false;
			}
			this.JsonUpdatePacientes(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
			return true;
		}
		//------------------------public.UPDATE.Turno----------------------//
        public override bool UpdateTurno(Turno instancia) {
			this.JsonUpdateTurnos(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos del turno Id: {instancia.Id}");
			return true;
		}











		//------------------------public.DELETE.Medico----------------------//
		public override bool DeleteMedico(Medico instancia) {
			if (ErroresDeConstraintDeMedicos(instancia)){
				return false;
			}
			
			try {
				DictMedicos.Remove(instancia.Id);
				this.JsonUpdateMedicos(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el medico con id: {instancia.Id} del Json");
				return true;
			} catch (Exception ex) {
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el medico", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;

			}
		}
		//------------------------public.DELETE.Paciente----------------------//
		public override bool DeletePaciente(Paciente instancia){
			if (ErroresDeConstraintDePacientes(instancia)){
				return false;
			}
			
			try {
				DictPacientes.Remove(instancia.Id);
				this.JsonUpdatePacientes(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el paciente con id: {instancia.Id} del Json");
				return true;
			} catch (Exception ex) { 
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el paciente", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}
		//------------------------public.DELETE.Turno----------------------//
        public override bool DeleteTurno(Turno instancia) {
			try {
				DictTurnos.Remove(instancia.Id);
				this.JsonUpdateTurnos(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el turno con id: {instancia.Id} del Json");
				return true;
			} catch (Exception ex) { 
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el turno", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}








		//------------------------private.LOAD.Medicos----------------------//
		private void JsonCargarMedicos(string file_path) {
			if (File.Exists(file_path)) {
				try {
					// Read JSON file
					string jsonString = File.ReadAllText(file_path);

					// Deserialize to a dictionary of string to raw JSON objects
					var rawMedicosData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
					
					// Initialize the dictionary to store Medico instances
					var medicos = new Dictionary<string, Medico>();

					foreach (var medicoEntry in rawMedicosData) {
						// Parse each JSON element and create a Medico instance with the key and parsed data
						var medicoJsonElement = System.Text.Json.JsonDocument.Parse(medicoEntry.Value.ToString()).RootElement;
						var medicoInstance = new Medico(medicoEntry.Key, medicoJsonElement);
						
						medicos[medicoEntry.Key] = medicoInstance;
					}

					// Assign to DictMedicos
					DictMedicos = medicos;
				} 
				catch (JsonException ex) {
					MessageBox.Show($"Error parseando el archivo Json {file_path}: {ex.Message}");
				}
			} 
			else {
				MessageBox.Show($"Error: {file_path} no se encontró.");
			}
		}
		//------------------------private.LOAD.Pacientes----------------------//
		private void JsonCargarPacientes(string file_path){
			if (File.Exists(pacientesPath)) {
				try {
					string jsonString = File.ReadAllText(pacientesPath);
					DictPacientes = JsonConvert.DeserializeObject<Dictionary<string, Paciente>>(jsonString);
				} 
				catch (JsonException ex) {
					MessageBox.Show($"Error parseando el archivo Json {file_path}: {ex.Message}");
				}
			} 
			else {
				MessageBox.Show($"Error: {file_path} no se encontró.");
			}
		}
		//------------------------private.LOAD.Turnos----------------------//
		private void JsonCargarTurnos(string file_path){
			if (File.Exists(turnosPath)) {
				try {
					string jsonString = File.ReadAllText(turnosPath);
					DictTurnos = JsonConvert.DeserializeObject<Dictionary<string, Turno>>(jsonString);
				} 
				catch (JsonException ex) {
					MessageBox.Show($"Error parseando el archivo Json {file_path}: {ex.Message}");
				}
			} 
			else {
				MessageBox.Show($"Error: {file_path} no se encontró.");
			}
		}
		
		
		
		//------------------------private.SAVE---------------------------//
		private void JsonUpdatePacientes(){ 
			File.WriteAllText(pacientesPath, JsonConvert.SerializeObject(DictPacientes, Formatting.Indented));
		}
		private void JsonUpdateMedicos(){ 
			File.WriteAllText(medicosPath, JsonConvert.SerializeObject(DictMedicos, Formatting.Indented));
		}
		private void JsonUpdateTurnos(){ 
			File.WriteAllText(turnosPath, JsonConvert.SerializeObject(DictTurnos, Formatting.Indented));
		}
		//------------------------private.CONSTRAINTS----------------------//
		private string GenerateNextId<T>(Dictionary<string, T> dictionary) {
			int maxId = 0;

			foreach (var key in dictionary.Keys) {
				if (int.TryParse(key, out int numericId)) {
					maxId = Math.Max(maxId, numericId);
				}
			}
			return (maxId + 1).ToString();
		}
		//------------------------private.CONSTRAINTS.Turnos----------------------//
		private bool ErroresDeConstraintDeTurnos(Turno instancia) {
			if (
				DictTurnos.Values.Any(i => i.PacienteId == instancia.PacienteId)
				|| DictTurnos.Values.Any(i => i.MedicoId == instancia.MedicoId)
				|| DictTurnos.Values.Any(i => i.Fecha == instancia.Fecha)
			) {
				MessageBox.Show($"Error de integridad: Ya hay un turno entre ese paciente y ese medico en esa fecha.");
				return true;
			}

			if (
				DictTurnos.Values.Any(i => i.MedicoId == instancia.MedicoId)
				|| DictTurnos.Values.Any(i => i.Fecha == instancia.Fecha)
				|| DictTurnos.Values.Any(i => i.Hora == instancia.Hora)
			) {
				MessageBox.Show($"Error de integridad: El medico ya tiene un turno ese dia a esa hora.");
				return true;
			}
			return false;
		}
		//------------------------private.CONSTRAINTS.Paciente----------------------//
		private bool ErroresDeConstraintDePacientes(Paciente instancia) {
			if (DictTurnos.Values.Any(i => i.PacienteId == instancia.Id)) {
				MessageBox.Show($"Error de integridad: El paciente tiene turnos asignados.");
				return true;
			}
			return false;
		}
		//------------------------private.CONSTRAINTS.Medicos----------------------//
		private bool ErroresDeConstraintDeMedicos(Medico instancia) {
			if (DictTurnos.Values.Any(i => i.MedicoId == instancia.Id)) {
				MessageBox.Show($"Error de integridad: El medico tiene turnos asignados.");
				return true;
			}
			return false;
		}
		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
