using System.Text.Json;
using System.ComponentModel;

namespace ClinicaMedica {
	//---------------------------------Tablas.Turnos-------------------------------//
	public class Turno : Entidad, INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName){
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	
		public string ?PacienteId { get; set; }
		public string ?MedicoId { get; set; }
		public DateTime ?Fecha { get; set; }
		public TimeSpan ?Hora { get; set; }

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
			this.Id = window.txtId.Content?.ToString() ?? this.Id;
			this.PacienteId = window.txtPacientes.SelectedValue.ToString();
			this.MedicoId = window.txtMedicos.SelectedValue.ToString();
			//this.Fecha = ((DateTime) window.txtFecha.SelectedDate).Date.ToString("yyyy-MM-dd");
			if (window.txtFecha.SelectedDate.HasValue) {
				this.Fecha = window.txtFecha.SelectedDate.Value.Date; // Set as DateTime, keeping only the date part
			}
			else {
				// Handle the case where no date is selected, if necessary
				this.Fecha = DateTime.MinValue; // Or some other default value
			}
			this.Hora = TimeSpan.Parse(window.txtHora.Text);
		}
	}
}
