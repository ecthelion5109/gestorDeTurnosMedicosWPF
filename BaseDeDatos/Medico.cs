using SystemTextJson = System.Text.Json;
using Newtonsoft.Json;
using System.Windows;

namespace ClinicaMedica {
	//---------------------------------Tablas.Horarios-------------------------------//
	public class HorarioMedico {
		public string ?Id { get; set; }
		public string ?MedicoId { get; set; }
		public string DiaSemana { get; set; }
		public TimeOnly ?HoraInicio { get; set; }
		public TimeOnly ?HoraFin { get; set; }
		
		public static List<HorarioMedico> DiasDeLaSemanaComoLista() {
			return new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" }.Select(dia => new HorarioMedico { DiaSemana = dia }).ToList();
		}
		
		public static Dictionary<string, HorarioMedico> DiasDeLaSemanaComoDiccionario(){
			return new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" }.ToDictionary(dia => dia, dia => new HorarioMedico { DiaSemana = dia });
		}

		public override string ToString() {
			return $"Id: {Id ?? "N/A"}, MedicoId: {MedicoId ?? "N/A"}, DiaSemana: {DiaSemana}, " +
				   $"HoraInicio: {(HoraInicio.HasValue ? HoraInicio.Value.ToString("HH:mm") : "N/A")}, " +
				   $"HoraFin: {(HoraFin.HasValue ? HoraFin.Value.ToString("HH:mm") : "N/A")}";
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
		public Dictionary<string, HorarioMedico> DiasDeAtencion { get; set; } = HorarioMedico.DiasDeLaSemanaComoDiccionario();
			
		[JsonIgnore]
		public string Displayear => $"{Id}: {Especialidad} - {Name} {LastName}";
		
	//---------------------------------Constructor.Vacio-------------------------------//
		public Medico() { }

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
					string diaSemana = dia.Value.GetProperty("DiaSemana").GetString();

					HorarioMedico horariomedico = new HorarioMedico {
						Id = dia.Value.GetProperty("Id").GetString(),
						DiaSemana = diaSemana,
						MedicoId = dia.Value.GetProperty("MedicoId").GetString(),
						HoraInicio = TimeOnly.Parse(dia.Value.GetProperty("HoraInicio").GetString()),
						HoraFin = TimeOnly.Parse(dia.Value.GetProperty("HoraFin").GetString()),
					};
					MessageBox.Show($"{horariomedico}");
					DiasDeAtencion[diaSemana] = horariomedico;
				}
			}
		}

		// Constructor de PAciente en base a una ventana
		public Medico(MedicosModificar window){
			TomarDatosDesdeVentana(window);
		}


		// Metodo para devolver una lista con los horarios medicos para la interfaz gráfica.
		// private List<HorarioMedico> GetDiasDeAtencionListForUI() {
			// var dias = new List<HorarioMedico> {
				// new() { DiaSemana = "Lunes" },
				// new() { DiaSemana = "Martes" },
				// new() { DiaSemana = "Miercoles" },
				// new() { DiaSemana = "Jueves" },
				// new() { DiaSemana = "Viernes" },
				// new() { DiaSemana = "Sabado" },
				// new() { DiaSemana = "Domingo" }
			// };
			// foreach (var dia in dias) {
				// if (DiasDeAtencion.TryGetValue(dia.DiaSemana, out var horarios)) {
					// dia.InicioHorario = horarios.Start;
					// dia.FinHorario = horarios.End;
				// }
			// }
			// return dias;
		// }
		

		// Metodo para actualizar los dias de atencion en base a la ventana
		// private void UpdateDiasDeAtencionFromUI(List<HorarioMedico> diasFromUI) {
			// DiasDeAtencion.Clear();
			// foreach (var dia in diasFromUI) {
				// if (!string.IsNullOrWhiteSpace(dia.InicioHorario) && !string.IsNullOrWhiteSpace(dia.FinHorario)) {
					// DiasDeAtencion[dia.DiaSemana] = new Horario(dia.InicioHorario, dia.FinHorario);
				// }
			// }
		// }
		
		
		

		// Metodo para aplicarle los cambios de una ventana a una instancia de medico existente.
		public void TomarDatosDesdeVentana(MedicosModificar window) {
			this.Name = window.txtNombre.Text;
			this.LastName = window.txtApellido.Text;
			this.Dni = window.txtDni.Text;
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
			// UpdateDiasDeAtencionFromUI((List<HorarioMedico>)window.txtDiasDeAtencion.ItemsSource);
		}
		
		// Metodo para mostrarse en una ventana
		public void MostrarseEnVentana(MedicosModificar ventana) {
			// ventana.txtDiasDeAtencion.ItemsSource = this.GetDiasDeAtencionListForUI();
			ventana.txtNombre.Text = this.Name;
			ventana.txtApellido.Text = this.LastName;
			ventana.txtDni.Text = this.Dni;
            ventana.txtTelefono.Text = this.Telefono;
            ventana.txtProvincia.Text = this.Provincia;
			ventana.txtDomicilio.Text = this.Domicilio;
			ventana.txtLocalidad.Text = this.Localidad;
			ventana.txtEspecialidad.Text = this.Especialidad;
			ventana.txtFechaIngreso.SelectedDate = this.FechaIngreso;
			ventana.txtSueldoMinGarant.Text = this.SueldoMinimoGarantizado.ToString();
			ventana.txtRealizaGuardia.IsChecked = this.Guardia;
			
			ventana.txtDiasDeAtencion.ItemsSource = this.DiasDeAtencion.Values.ToList();
		}
		
		
		
	}
}
