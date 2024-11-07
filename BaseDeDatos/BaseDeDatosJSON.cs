using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ClinicaMedica {
	public class BaseDeDatosJSON : BaseDeDatosAbstracta{
		public string medicosPath = "jsonDatabase/Medicos.json";
		public string pacientesPath = "jsonDatabase/Pacientes.json";
		public string turnosPath = "jsonDatabase/Turnos.json";
		public BaseDeDatosJSON() {
			CargarMedicos();
			CargarPacientes();
			CargarTurnos();
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
		private void CargarMedicos() {
			if (File.Exists(medicosPath)) {
				try {
					string jsonString = File.ReadAllText(medicosPath);
					DictMedicos = JsonConvert.DeserializeObject<Dictionary<string, Medico>>(jsonString);
				} 
				catch (JsonException ex) {
					MessageBox.Show($"Error parsing JSON file: {ex.Message}");
				}
			} 
			else {
				MessageBox.Show("Error: File not found.");
			}
		}
		private void CargarPacientes(){
			if (File.Exists(pacientesPath)) {
				try {
					string jsonString = File.ReadAllText(pacientesPath);
					DictPacientes = JsonConvert.DeserializeObject<Dictionary<string, Paciente>>(jsonString);
				} 
				catch (JsonException ex) {
					MessageBox.Show($"Error parsing JSON file: {ex.Message}");
				}
			} 
			else {
				MessageBox.Show("Error: File not found.");
			}
		}
		private void CargarTurnos(){
			if (File.Exists(turnosPath)) {
				try {
					string jsonString = File.ReadAllText(turnosPath);
					DictTurnos = JsonConvert.DeserializeObject<Dictionary<string, Turno>>(jsonString);
				} 
				catch (JsonException ex) {
					MessageBox.Show($"Error parsing JSON file: {ex.Message}");
				}
			} 
			else {
				MessageBox.Show("Error: File not found.");
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
