using SystemTextJson = System.Text.Json;
using Newtonsoft.Json;

namespace ClinicaMedica {
	//---------------------------------Tablas.Horarios-------------------------------//
	public class HorarioMedico {
		public string DiaSemana { get; set; }
		public string ?HoraInicio { get; set; }
		public string ?HoraFin { get; set; }
		
		public static List<HorarioMedico> GetDiasDeLaSemanaAsList(){
			return new List<HorarioMedico> {
				new() { DiaSemana = "Lunes" },
				new() { DiaSemana = "Martes" },
				new() { DiaSemana = "Miércoles" },
				new() { DiaSemana = "Jueves" },
				new() { DiaSemana = "Viernes" },
				new() { DiaSemana = "Sábado" },
				new() { DiaSemana = "Domingo" }
			};
		}
		public static Dictionary<string, HorarioMedico> GetDiasDeLaSemanaAsDict(){
			return new Dictionary<string, HorarioMedico> {
				{ "Lunes", new HorarioMedico { DiaSemana = "Lunes" } },
				{ "Martes", new HorarioMedico { DiaSemana = "Martes" } },
				{ "Miércoles", new HorarioMedico { DiaSemana = "Miércoles" } },
				{ "Jueves", new HorarioMedico { DiaSemana = "Jueves" } },
				{ "Viernes", new HorarioMedico { DiaSemana = "Viernes" } },
				{ "Sábado", new HorarioMedico { DiaSemana = "Sábado" } },
				{ "Domingo", new HorarioMedico { DiaSemana = "Domingo" } }
			};
		}
	}

	//---------------------------------Tablas.Medicos-------------------------------//
	public class Medico {
		public string ?Id { get; set; }
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
		public Dictionary<string, HorarioMedico> DiasDeAtencion { get; set; } = HorarioMedico.GetDiasDeLaSemanaAsDict();
			
		[JsonIgnore]
		public string Displayear => $"{Id}: {Especialidad} - {Name} {LastName}";

	//---------------------------------Constructores-------------------------------//
		public Medico() { }

		// Constructor de mEDICO en base a una ventana
		public Medico(MedicosModificar window){
			TomarDatosDesdeVentana(window);
		}

		// Constructor de Medico para JSON
		public Medico(string jsonElementKey, SystemTextJson.JsonElement jsonElement) {
			Id = jsonElement.GetProperty(nameof(Id)).GetString();
			Dni = jsonElement.GetProperty(nameof(Dni)).GetString();
			Name = jsonElement.GetProperty(nameof(Name)).GetString();
			LastName = jsonElement.GetProperty(nameof(LastName)).GetString();
			Provincia = jsonElement.GetProperty(nameof(Provincia)).GetString();
			Domicilio = jsonElement.GetProperty(nameof(Domicilio)).GetString();
			Localidad = jsonElement.GetProperty(nameof(Localidad)).GetString();
			Especialidad = jsonElement.GetProperty(nameof(Especialidad)).GetString();
			Telefono = jsonElement.GetProperty(nameof(Telefono)).GetString();
			Guardia = jsonElement.GetProperty(nameof(Guardia)).GetBoolean();
			FechaIngreso = DateTime.TryParse(jsonElement.GetProperty(nameof(FechaIngreso)).GetString(), out var fecha) ? fecha : (DateTime?)null;
			SueldoMinimoGarantizado = jsonElement.GetProperty(nameof(SueldoMinimoGarantizado)).GetDouble();

			if (jsonElement.TryGetProperty(nameof(DiasDeAtencion), out SystemTextJson.JsonElement diasDeAtencionElement)) {
				foreach (var dia in diasDeAtencionElement.EnumerateObject()) {
					var diaKey = dia.Name;
					if (
						dia.Value.TryGetProperty("HoraInicio", out var startElement) 
						&& dia.Value.TryGetProperty("HoraFin", out var endElement)
					) {
						DiasDeAtencion[diaKey].HoraInicio = startElement.ToString();
						DiasDeAtencion[diaKey].HoraFin = endElement.ToString();
					}
				}
			}
		}
		

		//---------------------------------PUBLICOS-------------------------------//
		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void TomarDatosDesdeVentana(MedicosModificar window) {
			this.Name = window.txtName.Text;
			this.LastName = window.txtLastName.Text;
			this.Dni = window.txtDni.Text;
            this.Telefono = window.txtTelefono.Text;
            this.Provincia = window.txtProvincia.Text;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			this.Especialidad = window.txtEspecialidad.Text;
			this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			this.Guardia = (bool)window.txtGuardia.IsChecked;
			this.SueldoMinimoGarantizado = double.Parse(window.txtSueldoMinimoGarantizado.Text);
			//this.DiasDeAtencion = //Al haber pasado los datos como List de HorariosMedicos, los objetos originales fueron modificados in-place. Assi que aca no hay que hacer nada.
			
		}
		
		// Metodo para mostrarse en una ventana
		public void MostrarseEnVentana(MedicosModificar ventana) {
			ventana.txtName.Text = this.Name;
			ventana.txtLastName.Text = this.LastName;
			ventana.txtDni.Text = this.Dni;
            ventana.txtTelefono.Text = this.Telefono;
            ventana.txtProvincia.Text = this.Provincia;
			ventana.txtDomicilio.Text = this.Domicilio;
			ventana.txtLocalidad.Text = this.Localidad;
			ventana.txtEspecialidad.Text = this.Especialidad;
			ventana.txtFechaIngreso.SelectedDate = this.FechaIngreso;
			ventana.txtGuardia.IsChecked = this.Guardia;
			ventana.txtSueldoMinimoGarantizado.Text = this.SueldoMinimoGarantizado.ToString();
			ventana.txtDiasDeAtencion.ItemsSource = this.DiasDeAtencion.Values.ToList();
		}
		
		
		
	}
}
