using Newtonsoft.Json;
using System.Windows;
using SystemTextJson = System.Text.Json;

namespace ClinicaMedica {
	//---------------------------------Tablas.Turnos-------------------------------//
	public class Turno {
		public string ?Id { get; set; }
		public string ?PacienteId { get; set; }
		public string ?MedicoId { get; set; }
		public DateTime ?Fecha { get; set; }
		public TimeSpan ?Hora { get; set; }

		public Turno() { }
		
		// Constructor de PAciente para JSON
		public Turno(SystemTextJson.JsonElement json){
		}

		// Constructor de PAciente en base a una ventana
		public Turno(TurnosModificar window){
			TomarDatosDesdeVentana(window);
		}
		
		[JsonIgnore]
		public Medico MedicoRelacionado{
			get{
				if (App.BaseDeDatos.DictMedicos.TryGetValue(MedicoId, out Medico medicoRelacionado)){
					return medicoRelacionado;
				}
				else{
					MessageBox.Show("Error de integridad. No existe medico con esa ID", "Error de integridad", MessageBoxButton.OK, MessageBoxImage.Error);
					return null; 
				}
			}
		}
		
		[JsonIgnore]
		public Paciente PacienteRelacionado{
			get{
				if (App.BaseDeDatos.DictPacientes.TryGetValue(PacienteId, out Paciente pacienteRelacionado)){
					return pacienteRelacionado;
				}
				else{
					MessageBox.Show("Error de integridad. No existe paciente con esa ID", "Error de integridad", MessageBoxButton.OK, MessageBoxImage.Error);
					return null; 
				}
			}
		}
		
		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void TomarDatosDesdeVentana(TurnosModificar window) {
			this.Id = window.txtId.Content?.ToString() ?? this.Id;
			this.PacienteId = window.txtPacientes.SelectedValue.ToString();
			this.MedicoId = window.txtMedicos.SelectedValue.ToString();
			this.Fecha = window.txtFecha.SelectedDate.Value.Date; // Set as DateTime, keeping only the date part
			this.Hora = TimeSpan.Parse(window.txtHora.Text);
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
