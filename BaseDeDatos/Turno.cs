using System.Text.Json;
using System.ComponentModel;

namespace ClinicaMedica {
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
