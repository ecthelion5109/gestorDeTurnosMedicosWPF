using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClinicaMedica {
	//---------------------------------Tablas.Horarios-------------------------------//
	
	public class TablaEntidad {
	}
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
		// public bool Trabaja { get; set; }
	}

	//---------------------------------Tablas.Medicos-------------------------------//
	public class Medico : TablaEntidad, INotifyPropertyChanged {
		public event PropertyChangedEventHandler? PropertyChanged; // Implementing INotifyPropertyChanged
		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) /* Method to raise the PropertyChanged event */ {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		
		
		private string? _name;
		private string? _lastname;
		private string? _dni;
		private string? _domicilio;
		private string? _localidad;
		private string? _specialidad;
		private string? _telefono;
		private bool? _guardia;
		private DateTime? _fechaIngreso;
		private double? _sueldoMinimoGarantizado;
		private string? _provincia;
		
		
		
		public string? Name {
			get => _name;
			set {
				if (_name != value) {
					_name = value;
					OnPropertyChanged();
				}
			}
		}


		public string? LastName {
			get => _lastname;
			set {
				if (_lastname != value) {
					_lastname = value;
					OnPropertyChanged();
				}
			}
		}
		
		
		public string? Dni {
			get => _dni;
			set {
				if (_dni != value) {
					_dni = value;
					OnPropertyChanged();
				}
			}
		}

		
		public string? Provincia {
			get => _provincia;
			set {
				if (_provincia != value) {
					_provincia = value;
					OnPropertyChanged();
				}
			}
		}

		
		public string? Domicilio {
			get => _domicilio;
			set {
				if (_domicilio != value) {
					_domicilio = value;
					OnPropertyChanged();
				}
			}
		}

		
		public string? Localidad {
			get => _localidad;
			set {
				if (_localidad != value) {
					_localidad = value;
					OnPropertyChanged();
				}
			}
		}

		
		public string? Especialidad {
			get => _specialidad;
			set {
				if (_specialidad != value) {
					_specialidad = value;
					OnPropertyChanged();
				}
			}
		}
		
		
		public string? Telefono {
			get => _telefono;
			set {
				if (_telefono != value) {
					_telefono = value;
					OnPropertyChanged();
				}
			}
		}
		
		public bool? Guardia {
			get => _guardia;
			set {
				if (_guardia != value) {
					_guardia = value;
					OnPropertyChanged();
				}
			}
		}
		
		
		public DateTime? FechaIngreso {
			get => _fechaIngreso;
			set {
				if (_fechaIngreso != value) {
					_fechaIngreso = value;
					OnPropertyChanged();
				}
			}
		}
		
		
		public double? SueldoMinimoGarantizado {
			get => _sueldoMinimoGarantizado;
			set {
				if (_sueldoMinimoGarantizado != value) {
					_sueldoMinimoGarantizado = value;
					OnPropertyChanged();
				}
			}
		}

		private Dictionary<string, Horario> _diasDeAtencion = new Dictionary<string, Horario>();
		public Dictionary<string, Horario> DiasDeAtencion {
			get => _diasDeAtencion;
			set {
				if (_diasDeAtencion != value) {
					_diasDeAtencion = value;
					OnPropertyChanged();
				}
			}
		}

	//---------------------------------Constructor.Vacio-------------------------------//
		public Medico() { }

		// Constructor para jsons
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

		// Dictionario de DiasDeAtencion --> a Lista
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
		
		// Lista a --> Dictionary de DiasDeAtencion
		private void UpdateDiasDeAtencionFromUI(List<HorarioMedico> diasFromUI) {
			DiasDeAtencion.Clear();
			foreach (var dia in diasFromUI) {
				if (!string.IsNullOrWhiteSpace(dia.InicioHorario) && !string.IsNullOrWhiteSpace(dia.FinHorario)) {
					DiasDeAtencion[dia.DiaSemana] = new Horario(dia.InicioHorario, dia.FinHorario);
				}
			}
		}
		
		public Medico AsignarDatosFromWindow(MedicosModificar window){ 
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
			if (double.TryParse(window.txtSueldoMinGarant.Text, out double sueldo)) {
				this.SueldoMinimoGarantizado = sueldo;
			}
			this.UpdateDiasDeAtencionFromUI( (List<HorarioMedico>) window.txtDiasDeAtencion.ItemsSource);
			return this;
		}
		
		
		
		
	}
	
	
	
	//---------------------------------Tablas.Pacientes-------------------------------//
	public class Paciente: TablaEntidad {
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
		
		// Constructor that takes a JsonElement
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

		public Paciente(PacientesModificar window){
			this.Name = window.txtNombre.Text;
			this.LastName = window.txtApellido.Text;
			this.Dni = window.txtDNI.Text;
			this.Provincia = window.txtProvincia.Text;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			//this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			this.FechaNacimiento = (DateTime)window.txtFechaNacimiento.SelectedDate;
		}
		
		
		public Paciente AsignarDatosFromWindow(PacientesModificar window){ 
			this.Name = window.txtNombre.Text;
			this.LastName = window.txtApellido.Text;
			this.Dni = window.txtDNI.Text;
			this.Provincia = window.txtProvincia.Text;
			this.Domicilio = window.txtDomicilio.Text;
			this.Localidad = window.txtLocalidad.Text;
			//this.FechaIngreso = (DateTime)window.txtFechaIngreso.SelectedDate;
			this.FechaNacimiento = (DateTime)window.txtFechaNacimiento.SelectedDate;
			return this;
		}
	}
	
	public class Turno: TablaEntidad {
		public int ?MedicoPk { get; set; }
		public int ?PacientePk { get; set; }
		public DateTime ?FechaYHoraAsignada { get; set; }
			
		// Parameterless constructor
		public Turno() { }
	
		public Turno(JsonElement jsonElement)
		{
			MedicoPk = jsonElement.GetProperty(nameof(MedicoPk)).GetInt32();
			PacientePk = jsonElement.GetProperty(nameof(PacientePk)).GetInt32();
			FechaYHoraAsignada = DateTime.TryParse(jsonElement.GetProperty(nameof(FechaYHoraAsignada)).GetString(), out var fecha) ? fecha : (DateTime?)null;

		}
		
		
		public void AsignarDatos(TurnosModificar window){
            this.PacientePk = int.Parse( window.txtpaciente.SelectedItem?.ToString() );
			this.MedicoPk = int.Parse(window.txtmedico.Text);
			this.FechaYHoraAsignada =  (DateTime) window.txtfecha.SelectedDate;
		}
	}
}
