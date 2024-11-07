using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ClinicaMedica {
	public class BaseDeDatosJSON : BaseDeDatosAbstracta{
		public string medicosPath = "jsonDatabase/medicos.json";
		public string pacientesPath = "jsonDatabase/pacientes.json";
		public string turnosPath = "jsonDatabase/turnos.json";
		
		public BaseDeDatosJSON() {
			CargarMedicos(medicosPath);
			CargarPacientes(pacientesPath);
			CargarTurnos(turnosPath);
		}
		
		
		//------------------------CREATE----------------------//
		public override bool CreateMedico(Medico instancia) {
			bool YaExiste(string key){
				return DictMedicos.ContainsKey(key);
			}
		
			if (YaExiste(instancia.Dni)) {
				return false;
			}
			DictMedicos[instancia.Dni] = instancia;
			this.UpdateMedicosJson();
			MessageBox.Show($"Exito: Se ha creado la instancia de Medico: {instancia.Name} {instancia.LastName}");
			return true;
		}
		public override bool CreatePaciente(Paciente instancia) {
			bool YaExiste(string key){
				return DictPacientes.ContainsKey(key);
			}
		
			if (YaExiste(instancia.Dni)) {
				return false;
			}
			DictPacientes[instancia.Dni] = instancia;
			this.UpdatePacientesJson();
			MessageBox.Show($"Exito: Se ha creado la instancia de Paciente: {instancia.Name} {instancia.LastName}");
			return true;
		}

		public override bool CreateTurno(Turno instancia) {
			bool YaExiste(string key){
				return DictTurnos.ContainsKey(key);
			}
			
			if (YaExiste(instancia.Id)) {
				return false;
			}
			MessageBox.Show($"No se ha implementado esto en json");
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
        //------------------------public.UPDATE----------------------//
        public override bool UpdateMedico(Medico instancia, string originalDni){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
				return false;
			} 
				
			if (originalDni != instancia.Dni) {
				DictMedicos.Remove(originalDni);
				DictMedicos[instancia.Dni] = instancia;
			}
			this.UpdateMedicosJson(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
			return true;
		}
        public override bool UpdatePaciente(Paciente instancia, string originalDni){
			if (string.IsNullOrEmpty(instancia.Dni)) {
				MessageBox.Show($"Error: El DNI es un campo obligatorio.");
				return false;
			} 
				
			if (originalDni != instancia.Dni) {
				DictPacientes.Remove(originalDni);
				DictPacientes[instancia.Dni] = instancia;
			}
			this.UpdatePacientesJson(); // Guardar los cambios en el archivo JSON
			MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
			return true;
		}
        public override bool UpdateTurno(Turno instancia) {
			MessageBox.Show($"Sin definir.");
			return false;
		}
		//------------------------public.DELETE----------------------//
		
		public override bool DeleteMedico(Medico instancia) {
			try {
				DictMedicos.Remove(instancia.Id);
				this.UpdateMedicosJson(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el medico con id: {instancia.Id} del Json");
				return true;
			} catch (Exception ex) {
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el medico", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;

			}
		}
		public override bool DeletePaciente(Paciente instancia){
			try {
				DictPacientes.Remove(instancia.Id);
				this.UpdatePacientesJson(); // Save changes to the database
				MessageBox.Show($"Exito: Se ha eliminado el paciente con id: {instancia.Id} del Json");
				return true;
			} catch (Exception ex) { 
				MessageBox.Show($"Error: {ex.Message}", "Error al querer eliminar el paciente", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}

        public override bool DeleteTurno(Turno instancia) {
			MessageBox.Show($"Sin implementar");
			return false;
		}
		
		
		
		//------------------------Private----------------------//
		private void CargarMedicos(string file_path) {
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


		private void CargarPacientes(string file_path){
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
		private void CargarTurnos(string file_path){
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
		
		private void UpdatePacientesJson(){ 
			File.WriteAllText(pacientesPath, JsonConvert.SerializeObject(DictPacientes, Formatting.Indented));
		}
		private void UpdateMedicosJson(){ 
			File.WriteAllText(medicosPath, JsonConvert.SerializeObject(DictMedicos, Formatting.Indented));
		}
		private void UpdateTurnosJson(){ 
			File.WriteAllText(turnosPath, JsonConvert.SerializeObject(DictTurnos, Formatting.Indented));
		}
		
		
		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
