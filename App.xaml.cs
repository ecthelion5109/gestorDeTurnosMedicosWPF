using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;


namespace ClinicaMedica {
	public partial class App : Application {
		public static bool UsuarioLogueado = false;
		public static string UsuarioName = "Señor Gestor";
		public static IBaseDeDatos BaseDeDatos;
		
		
	}
	

    public static class WindowExtensions{
		public static void NavegarA<T>(this Window previousWindow) where T : Window, new(){
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		public static void NavegarA<T>(this Window previousWindow, object optionalArg) where T : Window, new(){
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		
		public static void AbrirComoDialogo<T>(this Window previousWindow) where T : Window, new(){
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}

		public static void AbrirComoDialogo<T>(this Window previousWindow, object optionalArg) where T : Window{
			// Utiliza Activator para instanciar la ventana con el parámetro opcional
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}
		
		public static void VolverAHome(this Window previousWindow){
			previousWindow.NavegarA<MainWindow>();
		}
		public static void Salir(this Window previousWindow){
			Application.Current.Shutdown();  // Apagar la aplicación
		}
	}
	
	public enum OperationCode {
		YA_EXISTE,
		SUCCESS,
		CREATE_SUCCESS,
		SIN_DEFINIR,
		UPDATE_SUCCESS,
		DELETE_SUCCESS,
		MISSING_DNI,
		MISSING_FIELDS,
		ERROR,
		DATOS_LEIDOS
	}
	
	public interface IBaseDeDatos{
		// Read methods
		List<Medico> ReadMedicos();
		List<Paciente> ReadPacientes();
		List<Turno> ReadTurnos();

		// checkers
		bool CorroborarQueNoExistaMedico(string key);
		bool CorroborarQueNoExistaPaciente(string key);
		bool CorroborarQueNoExistaTurno(string key);

		// Create methods
		OperationCode CreateMedico(Medico medico);
		OperationCode CreatePaciente(Paciente paciente);
		OperationCode CreateTurno(Turno turno);

		// Update methods
		OperationCode UpdateMedico(Medico medico, string originalDni);
		OperationCode UpdatePaciente(Paciente paciente, string originalDni);
		OperationCode UpdateTurno(Turno turno);

		// Delete methods
		OperationCode DeleteMedico(string medicoId);
		OperationCode DeletePaciente(string pacienteId);
		OperationCode DeleteTurno(string turnoId);
	}
	
	
	
	//---------------------------------Tablas.Horarios-------------------------------//
	public class Horario{
		public string ?Start { get; set; }
		public string ?End { get; set; }
		public Horario(string Start, string End) {
			this.Start = Start;
			this.End = End;
		}
		public Horario(JsonElement Start, JsonElement End) {
			this.Start = Start.GetString();
			this.End = End.GetString();
		}
	}
	
	public class HorarioMedico {
		public required string DiaSemana { get; set; }
		public string ?InicioHorario { get; set; }
		public string ?FinHorario { get; set; }
	}

	//---------------------------------Tablas.Medicos-------------------------------//
	public class Medico {
		public string? Name { get; set; }
		public string? LastName { get; set; }
		public string? Dni { get; set; }
		public string? Provincia { get; set; }
		public string? Domicilio { get; set; }
		public string? Localidad { get; set; }
		public string? Especialidad { get; set; }
		public string? Telefono { get; set; }
		public bool? Guardia { get; set; }
		public DateTime? FechaIngreso { get; set; }
		public double? SueldoMinimoGarantizado { get; set; }
		public Dictionary<string, Horario> DiasDeAtencion { get; set; } = new Dictionary<string, Horario>();

	//---------------------------------Constructor.Vacio-------------------------------//
		public Medico() { }

		// Constructor de Medico para JSON
		public Medico(string jsonElementKey, JsonElement jsonElement) {
			Name = jsonElement.GetProperty(nameof(Name)).GetString();
			LastName = jsonElement.GetProperty(nameof(LastName)).GetString();
			Dni = jsonElementKey;
			Provincia = jsonElement.GetProperty(nameof(Provincia)).GetString();
			Domicilio = jsonElement.GetProperty(nameof(Domicilio)).GetString();
			Localidad = jsonElement.GetProperty(nameof(Localidad)).GetString();
			Especialidad = jsonElement.GetProperty(nameof(Especialidad)).GetString();
			Telefono = jsonElement.GetProperty(nameof(Telefono)).GetString();
			Guardia = jsonElement.GetProperty(nameof(Guardia)).GetBoolean();
			FechaIngreso = DateTime.TryParse(jsonElement.GetProperty(nameof(FechaIngreso)).GetString(), out var fecha) ? fecha : (DateTime?)null;
			SueldoMinimoGarantizado = jsonElement.GetProperty(nameof(SueldoMinimoGarantizado)).GetDouble();

			if (jsonElement.TryGetProperty(nameof(DiasDeAtencion), out JsonElement diasDeAtencionElement)) {
				foreach (var dia in diasDeAtencionElement.EnumerateObject()) {
					var diaKey = dia.Name;
					if (dia.Value.TryGetProperty("Start", out JsonElement startElement) && dia.Value.TryGetProperty("End", out var endElement)) {
						DiasDeAtencion[diaKey] = new Horario(startElement, endElement);
					}
				}
			}
		}

		// Constructor de Medico en base a una ventana
		public Medico(MedicosModificar window){
			this.Name = window.txtNombre.Text;
			this.LastName = window.txtApellido.Text;
			this.Dni = window.txtDNI.Text;
            this.Telefono = window.txtTelefono.Text;
            this.Provincia = window.txtProvincia.Text;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			this.Especialidad = window.txtEspecialidad.Text;
			this.Guardia = (bool)window.txtRealizaGuardia.IsChecked;
			this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			if (double.TryParse(window.txtSueldoMinGarant.Text, out double sueldo)){
				this.SueldoMinimoGarantizado = sueldo;
			} else {
				this.SueldoMinimoGarantizado = 0; // Set a default value if parsing fails
			}
			UpdateDiasDeAtencionFromUI((List<HorarioMedico>)window.txtDiasDeAtencion.ItemsSource);
		}

		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void AsignarDatosFromWindow(MedicosModificar window) {
			this.Name = window.txtNombre.Text;
			this.LastName = window.txtApellido.Text;
			this.Dni = window.txtDNI.Text;
            this.Telefono = window.txtTelefono.Text;
            this.Provincia = window.txtProvincia.Text;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			this.Especialidad = window.txtEspecialidad.Text;
			this.Guardia = (bool)window.txtRealizaGuardia.IsChecked;
			this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			if (double.TryParse(window.txtSueldoMinGarant.Text, out double sueldo)){
				this.SueldoMinimoGarantizado = sueldo;
			} else {
				this.SueldoMinimoGarantizado = 0; // Set a default value if parsing fails
			}
			UpdateDiasDeAtencionFromUI((List<HorarioMedico>)window.txtDiasDeAtencion.ItemsSource);
		}


		// Metodo para devolver una lista con los horarios medicos para la interfaz gráfica.
		public List<HorarioMedico> GetDiasDeAtencionListForUI() {
			var dias = new List<HorarioMedico> {
				new() { DiaSemana = "Lunes" },
				new() { DiaSemana = "Martes" },
				new() { DiaSemana = "Miercoles" },
				new() { DiaSemana = "Jueves" },
				new() { DiaSemana = "Viernes" },
				new() { DiaSemana = "Sabado" },
				new() { DiaSemana = "Domingo" }
			};
			foreach (var dia in dias) {
				if (DiasDeAtencion.TryGetValue(dia.DiaSemana, out var horarios)) {
					dia.InicioHorario = horarios.Start;
					dia.FinHorario = horarios.End;
				}
			}
			return dias;
		}
		

		// Metodo para actualizar los dias de atencion en base a la ventana
		private void UpdateDiasDeAtencionFromUI(List<HorarioMedico> diasFromUI) {
			DiasDeAtencion.Clear();
			foreach (var dia in diasFromUI) {
				if (!string.IsNullOrWhiteSpace(dia.InicioHorario) && !string.IsNullOrWhiteSpace(dia.FinHorario)) {
					DiasDeAtencion[dia.DiaSemana] = new Horario(dia.InicioHorario, dia.FinHorario);
				}
			}
		}
		
	}
	
	
	
	//---------------------------------Tablas.Pacientes-------------------------------//
	public class Paciente {
		// public int Dni { get; set; }
		public string ?Dni { get; set; }
		public string ?Name { get; set; }
		public string ?LastName { get; set; }
		public DateTime ?FechaIngreso { get; set; }  // Corrige a DateTime
		public string ?Email { get; set; }
		public string ?Telefono { get; set; }
		public DateTime ?FechaNacimiento { get; set; }
		public string ?Domicilio { get; set; }
		public string ?Localidad { get; set; }
		public string ?Provincia { get; set; }
			
		public Paciente() { }
		
		// Constructor de PAciente para JSON
		public Paciente(JsonElement json){
			Dni = json.GetProperty(nameof(Dni)).GetString();
			Name = json.GetProperty(nameof(Name)).GetString();
			LastName = json.GetProperty(nameof(LastName)).GetString();
			FechaIngreso = json.GetProperty(nameof(FechaIngreso)).GetDateTime();
			Email = json.GetProperty(nameof(Email)).GetString();
			Telefono = json.GetProperty(nameof(Telefono)).GetString();
			FechaNacimiento = json.GetProperty(nameof(FechaNacimiento)).GetDateTime();
			Domicilio = json.GetProperty(nameof(Domicilio)).GetString();
			Localidad = json.GetProperty(nameof(Localidad)).GetString();
			Provincia = json.GetProperty(nameof(Provincia)).GetString();
		}

		// Constructor de PAciente en base a una ventana
		public Paciente(PacientesModificar window){
			this.Dni = window.txtDni.Text;
			this.Name = window.txtNombre.Text;
			this.LastName = window.txtApellido.Text;
			this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			this.Email = window.txtEmail.Text;
			this.Telefono = window.txtTelefono.Text;
			this.FechaNacimiento = (DateTime)window.txtFechaNacimiento.SelectedDate;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			this.Provincia = window.txtProvincia.Text;
		}
		
		
		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void AsignarDatosFromWindow(PacientesModificar window) {
			this.Dni = window.txtDni.Text;
			this.Name = window.txtNombre.Text;
			this.LastName = window.txtApellido.Text;
			this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			this.Email = window.txtEmail.Text;
			this.Telefono = window.txtTelefono.Text;
			this.FechaNacimiento = (DateTime)window.txtFechaNacimiento.SelectedDate;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			this.Provincia = window.txtProvincia.Text;
		}
	}
	
	
	
	//---------------------------------Tablas.Turnos-------------------------------//
	public class Turno {
		public string ?Id { get; set; }
		public string ?PacienteID { get; set; }
		public string ?MedicoID { get; set; }
		public DateTime ?Fecha { get; set; }
		public string ?Hora { get; set; }


		// Propiedad para obtener "DNI + Nombre + Apellido" del Paciente
		private string _pacienteJoin;
		public string PacienteJoin {
			get {
				if (_pacienteJoin is null) {
					_pacienteJoin = LoadPacienteNombreCompletoFromDatabase();
				}
				return _pacienteJoin;
			}
		}

		// Propiedad para obtener "DNI + Nombre + Apellido" del Medico
		private string ?_medicoJoin = null;
		public string MedicoJoin {
			get {
				if (_medicoJoin is null) {
					_medicoJoin = LoadMedicoNombreCompletoFromDatabase();
				}
				return _medicoJoin;
			}
		}


		private string LoadPacienteNombreCompletoFromDatabase() {
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = @"
                SELECT CONCAT(Dni, ' ', Name, ' ', LastName)
                FROM Paciente
                WHERE Id = @PacienteID";

				using (var command = new SqlCommand(query, connection)) {
					command.Parameters.AddWithValue("@PacienteID", PacienteID);

					var result = command.ExecuteScalar();
					return result?.ToString() ?? "Paciente no encontrado";
				}
			}
		}

		private string LoadMedicoNombreCompletoFromDatabase() {
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = @"
                SELECT CONCAT(Dni, ' ', Name, ' ', LastName)
                FROM Medico
                WHERE Id = @MedicoID";

				using (var command = new SqlCommand(query, connection)) {
					command.Parameters.AddWithValue("@MedicoID", MedicoID);

					var result = command.ExecuteScalar();
					return result?.ToString() ?? "Medico no encontrado";
				}
			}
		}










		private string ?_especialidad = null;  // Backing field for caching

		public string Especialidad {
			get {
				if (_especialidad is null){
					_especialidad = LoadEspecialidadFromDatabase();
				}
				return _especialidad;
			}
		}

		private string LoadEspecialidadFromDatabase() {
			// Replace with your actual connection string
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = "SELECT Especialidad FROM Medico WHERE Id = @MedicoID";

				using (var command = new SqlCommand(query, connection)) {
					command.Parameters.AddWithValue("@MedicoID", MedicoID);

					var result = command.ExecuteScalar();
					return result?.ToString() ?? "Especialidad no encontrada";
				}
			}
		}

		public Turno() { }
		
		// Constructor de PAciente para JSON
		public Turno(JsonElement json){
		}


		// Constructor de Turno en base a una ventana
		public Turno(TurnosModificar window){
			this.Id = window.txtId.Content.ToString();
			this.PacienteID = window.txtPacientes.Text;
			this.MedicoID = window.txtMedicos.Text;
			this.Fecha = window.txtFecha.SelectedDate;
			this.Hora = window.txtFecha.Text;
		}
		
		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void AsignarDatosFromWindow(TurnosModificar window) {
			this.Id = window.txtId.Content.ToString();
			this.PacienteID = window.txtPacientes.Text;
			this.MedicoID = window.txtMedicos.Text;
			this.Fecha = window.txtFecha.SelectedDate;
			this.Hora = window.txtFecha.Text;
			//DateTime parsedTime;

			// Parse the time string and set the DateTime if it's valid
			//if (DateTime.TryParseExact(window.txtHora.Text, "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out parsedTime)) {
				// Set this.Hora to the parsed time, ignoring the date part
				//this.Hora = parsedTime;
			//}
			//else {
				// Handle the parsing error (e.g., show a message to the user)
				//MessageBox.Show("Invalid time format. Please use HH:mm:ss.");
			//}
		}
	}

}
