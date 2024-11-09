using Newtonsoft.Json;
using SystemTextJson = System.Text.Json;

namespace ClinicaMedica {
	//---------------------------------Tablas.Turnos-------------------------------//
	public class Turno {
		public string ?Id { get; set; }
		public string ?PacienteId { get; set; }
		public string ?MedicoId { get; set; }
		public DateTime ?Fecha { get; set; }
		public TimeOnly ?Hora { get; set; }

		public Turno() { }
		
		// Constructor de PAciente para JSON
		public Turno(SystemTextJson.JsonElement json){
		}

		// Constructor de PAciente en base a una ventana
		public Turno(TurnosModificar window){
			TomarDatosDesdeVentana(window);
		}
		
		[JsonIgnore]
		public Medico MedicoRelacionado => App.BaseDeDatos.DictMedicos[MedicoId];
		
		[JsonIgnore]
		public Paciente PacienteRelacionado => App.BaseDeDatos.DictPacientes[PacienteId];
		
		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void TomarDatosDesdeVentana(TurnosModificar window) {
			this.Id = window.txtId.Content?.ToString() ?? this.Id;
			this.PacienteId = window.txtPacientes.SelectedValue.ToString();
			this.MedicoId = window.txtMedicos.SelectedValue.ToString();
			this.Fecha = window.txtFecha.SelectedDate.Value.Date; // Set as DateTime, keeping only the date part
			this.Hora = TimeOnly.Parse(window.txtHora.Text);
		}
		
		
		// Metodo para mostrarse en una ventana
		public void MostrarseEnVentana(TurnosModificar ventana) {
			ventana.txtMedicos.SelectedValue = MedicoId;
			ventana.txtPacientes.SelectedValue = PacienteId;
			ventana.txtEspecialidades.SelectedItem = MedicoRelacionado.Especialidad;
			ventana.txtId.Content = this.Id;
			ventana.txtFecha.SelectedDate = this.Fecha;
			ventana.txtHora.Text = this.Hora.ToString();
		}
		
	}
}
