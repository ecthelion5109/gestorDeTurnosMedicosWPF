using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;

namespace ClinicaMedica {
	//---------------------------------Tablas-------------------------------//
	
	public class TablaEntidad {
	}
	public class Horario
	{
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

	public class Medico : TablaEntidad {
		public const string TableName = "medicos";
		public string ?Name { get; set; }  // 50 caracteres máximo
		public string ?Lastname { get; set; }  // 50 caracteres máximo
		// public int Dni { get; set; }
		public string ?Dni { get; set; }
		public string ?Provincia { get; set; }  // 40 caracteres máximo
		public string ?Domicilio { get; set; }  // 50 caracteres máximo
		public string ?Localidad { get; set; }  // 50 caracteres máximo
		public string ?Specialidad { get; set; }  // 20 caracteres máximo
		public string ?Telefono { get; set; }
		public bool ?Guardia { get; set; }
		public DateTime ?FechaIngreso { get; set; }  //delimator. No puede haber ingresado hace 100 años ni haber ingresado en el futuro
		public double ?SueldoMinimoGarantizado { get; set; } //no puede tener cero ni numeros negativos
		// public Dictionary<string, (string start, string end)> DiasDeAtencion { get; set; } = new Dictionary<string, (string start, string end)>();
		public Dictionary<string, Horario> DiasDeAtencion { get; set; } = [];

		// public Dictionary<string, (string start, string end)> DiasDeAtencion { get; set; }
		
		
		
		public List<HorarioMedico> GetDiasDeAtencionList()
		{
			var dias = new List<HorarioMedico>
			{
				new() { DiaSemana = "Lunes" },
				new() { DiaSemana = "Martes" },
				new() { DiaSemana = "Miercoles" },
				new() { DiaSemana = "Jueves" },
				new() { DiaSemana = "Viernes" },
				new() { DiaSemana = "Sabado" },
				new() { DiaSemana = "Domingo" }
			};

			foreach (var dia in dias)
			{
				if (DiasDeAtencion.TryGetValue(dia.DiaSemana, out var horarios))
				{
					dia.InicioHorario = horarios.Start;
					dia.FinHorario = horarios.End;
					// dia.Trabaja = true; // Assuming the doctor works on this day if there are horarios.
				}
				// else
				// {
					// dia.Trabaja = false; // If no horarios exist for the day, the doctor doesn't work.
				// }
			}

			return dias;
		}
		
		
		
		
		
		
		
		
		public Medico() { }
		
		public Medico(string jsonElementKey, JsonElement jsonElement)
		{
	
			Name = jsonElement.GetProperty(nameof(Name)).GetString();
			Lastname = jsonElement.GetProperty(nameof(Lastname)).GetString();
			// Dni = jsonElement.GetProperty("Dni").GetString();
			Dni = jsonElementKey;
			Provincia = jsonElement.GetProperty(nameof(Provincia)).GetString();
			Domicilio = jsonElement.GetProperty(nameof(Domicilio)).GetString();
			Localidad = jsonElement.GetProperty(nameof(Localidad)).GetString();
			Specialidad = jsonElement.GetProperty(nameof(Specialidad)).GetString();
			Telefono = jsonElement.GetProperty(nameof(Telefono)).GetString();
			Guardia = jsonElement.GetProperty(nameof(Guardia)).GetBoolean();
			//FechaIngreso = DateTime.Parse(jsonElement.GetProperty(nameof(FechaIngreso)).GetString());
			FechaIngreso = DateTime.TryParse(jsonElement.GetProperty(nameof(FechaIngreso)).GetString(), out var fecha) ? fecha : (DateTime?)null;

			SueldoMinimoGarantizado = jsonElement.GetProperty(nameof(SueldoMinimoGarantizado)).GetDouble();

			// Read days of attention
			if (jsonElement.TryGetProperty(nameof(DiasDeAtencion), out JsonElement diasDeAtencionElement))
			{
				foreach (var dia in diasDeAtencionElement.EnumerateObject())
				{
					var diaKey = dia.Name;

					if (dia.Value.TryGetProperty("Start", out JsonElement startElement) && dia.Value.TryGetProperty("End", out var endElement))
					{
						DiasDeAtencion[diaKey] = new Horario(startElement, endElement);
					}
				}
			}
		}
	}
	
	public class Paciente: TablaEntidad {
		// public int Dni { get; set; }
		public string ?Dni { get; set; }
		public string ?Name { get; set; }
		public string ?Lastname { get; set; }
		public DateTime ?FechaIngreso { get; set; }  // Corrige a DateTime
		public string ?Email { get; set; }
		public string ?Telefono { get; set; }
		public DateTime ?FechaNacimiento { get; set; }
		public string ?Direccion { get; set; }
		public string ?Localidad { get; set; }
		public string ?Provincia { get; set; }
			


		public Paciente() { }
		
		// Constructor that takes a JsonElement
		public Paciente(JsonElement json)
		{
			Dni = json.GetProperty(nameof(Dni)).GetString();
			Name = json.GetProperty(nameof(Name)).GetString();
			Lastname = json.GetProperty(nameof(Lastname)).GetString();
			FechaIngreso = json.GetProperty(nameof(FechaIngreso)).GetDateTime();
			Email = json.GetProperty(nameof(Email)).GetString();
			Telefono = json.GetProperty(nameof(Telefono)).GetString();
			FechaNacimiento = json.GetProperty(nameof(FechaNacimiento)).GetDateTime();
			Direccion = json.GetProperty(nameof(Direccion)).GetString();
			Localidad = json.GetProperty(nameof(Localidad)).GetString();
			Provincia = json.GetProperty(nameof(Provincia)).GetString();
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
	}
}
