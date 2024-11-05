using System.Text.Json;
using System.ComponentModel;

namespace ClinicaMedica {
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
	
	public class Entidad {
		public string ?Id { get; set; }
	}

	//---------------------------------Tablas.Medicos-------------------------------//
	public class Medico: Entidad {
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
		public string JoinedName{
			get
			{
				return $"{Dni} - {Name} {LastName}";
			}
		}

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

		// Constructor de PAciente en base a una ventana
		public Medico(MedicosModificar window){
			AsignarDatosFromWindow(window);
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
	public class Paciente : Entidad {
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
		public string JoinedName{
			get
			{
				return $"{Dni} - {Name} {LastName}";
			}
		}
			
			
			
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
			AsignarDatosFromWindow(window);
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
	public class Turno : Entidad, INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		
		private string _pacienteJoin;
		public string PacienteJoin {
			get {
				if (_pacienteJoin == null) {
					_pacienteJoin = App.BaseDeDatos.LoadPacienteNombreCompletoFromDatabase(Id);
				}
				return _pacienteJoin;
			}
			set {
				if (_pacienteJoin != value) {
					_pacienteJoin = value;
					OnPropertyChanged(nameof(PacienteJoin));
				}
			}
		}
	
	
	
		private string ?_medicoJoin = null;
		public string MedicoJoin {
			get {
				if (_medicoJoin == null) {
					_medicoJoin = App.BaseDeDatos.LoadMedicoNombreCompletoFromDatabase(MedicoId);
				}
				return _medicoJoin;
			}
			set {
				if (_medicoJoin != value) {
					_medicoJoin = value;
					OnPropertyChanged(nameof(MedicoJoin));
				}
			}
		}
		
		
		private string ?_especialidad = null;  // Backing field for caching

		public string Especialidad {
			get {
				if (_especialidad == null) {
					_especialidad = App.BaseDeDatos.LoadEspecialidadFromDatabase(MedicoId);
				}
				return _especialidad;
			}
			set {
				if (_especialidad != value) {
					_especialidad = value;
					OnPropertyChanged(nameof(Especialidad));
				}
			}
		}
	
		public string ?PacienteId { get; set; }
		public string ?MedicoId { get; set; }
		public DateTime ?Fecha { get; set; }
		public string ?Hora { get; set; }

		public Turno() { }
		
		// Constructor de PAciente para JSON
		public Turno(JsonElement json){
		}

		// Constructor de PAciente en base a una ventana
		public Turno(TurnosModificar window){
			AsignarDatosFromWindow(window);
		}
		
		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void AsignarDatosFromWindow(TurnosModificar window) {
			this.Id = window.txtId.Content.ToString();
			this.PacienteId = ((Paciente) window.txtPacientes.DataContext).Id;
			this.MedicoId = ((Medico) window.txtMedicos.DataContext).Id;
			this.Fecha = window.txtFecha.SelectedDate;
			this.Hora = window.txtFecha.Text;
		}
	}
}
