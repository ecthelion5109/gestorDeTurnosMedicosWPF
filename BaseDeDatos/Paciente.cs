using SystemTextJson = System.Text.Json;
using Newtonsoft.Json;

namespace ClinicaMedica {
	//---------------------------------Tablas.Pacientes-------------------------------//
	public class Paciente {
		public string ?Id { get; set; }
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


		[JsonIgnore]
		public string Displayear => $"{Id}: {Name} {LastName}";
			
		public Paciente() { }
		
		// Constructor de PAciente para JSON
		public Paciente(SystemTextJson.JsonElement json){
			Id = json.GetProperty(nameof(Id)).GetString();
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
			LeerDesdeVentana(window);
		}
		
		
		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void LeerDesdeVentana(PacientesModificar window) {
			this.Dni = window.txtDni.Text;
			this.Name = window.txtName.Text;
			this.LastName = window.txtLastName.Text;
			this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			this.Email = window.txtEmail.Text;
			this.Telefono = window.txtTelefono.Text;
			this.FechaNacimiento = (DateTime)window.txtFechaNacimiento.SelectedDate;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			this.Provincia = window.txtProvincia.Text;
		}
		
		
		// Metodo para mostrarse en una ventana
		public void MostrarseEnVentana(PacientesModificar ventana) {
			ventana.txtDni.Text = this.Dni;
			ventana.txtName.Text = this.Name;
			ventana.txtLastName.Text = this.LastName;
			ventana.txtFechaIngreso.SelectedDate = this.FechaIngreso;
			ventana.txtEmail.Text = this.Email;
			ventana.txtTelefono.Text = this.Telefono;
			ventana.txtFechaNacimiento.SelectedDate = this.FechaNacimiento;
			ventana.txtDomicilio.Text = this.Domicilio;
			ventana.txtLocalidad.Text = this.Localidad;
			ventana.txtProvincia.Text = this.Provincia;
		}
	}
}
