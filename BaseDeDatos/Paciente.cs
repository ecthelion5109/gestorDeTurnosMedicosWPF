using System.Text.Json;
using System.ComponentModel;

namespace ClinicaMedica {
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
