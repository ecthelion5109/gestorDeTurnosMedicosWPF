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
		public string Start { get; set; }
		public string End { get; set; }
	}
	
	public class HorarioMedico {
		public string DiaSemana { get; set; }
		public string InicioHorario { get; set; }
		public string FinHorario { get; set; }
		public bool Trabaja { get; set; }
	}
	
	public class Medico: TablaEntidad {
		public const string TableName = "medicos";
		public string Name { get; set; }  // 50 caracteres máximo
		public string Lastname { get; set; }  // 50 caracteres máximo
		// public int Dni { get; set; }
		public string Dni { get; set; }
		public string Provincia { get; set; }  // 40 caracteres máximo
		public string Domicilio { get; set; }  // 50 caracteres máximo
		public string Localidad { get; set; }  // 50 caracteres máximo
		public string Specialidad { get; set; }  // 20 caracteres máximo
		public string Telefono { get; set; }
		public bool Guardia { get; set; }
		public DateTime FechaIngreso { get; set; }  //delimator. No puede haber ingresado hace 100 años ni haber ingresado en el futuro
		public double SueldoMinimoGarantizado { get; set; } //no puede tener cero ni numeros negativos
		// public Dictionary<string, (string start, string end)> DiasDeAtencion { get; set; } = new Dictionary<string, (string start, string end)>();
		public Dictionary<string, Horario> DiasDeAtencion { get; set; } = new Dictionary<string, Horario>();

		// public Dictionary<string, (string start, string end)> DiasDeAtencion { get; set; }
		
		
		
		public List<HorarioMedico> GetDiasDeAtencionList()
		{
			var dias = new List<HorarioMedico>
			{
				new HorarioMedico { DiaSemana = "Lunes" },
				new HorarioMedico { DiaSemana = "Martes" },
				new HorarioMedico { DiaSemana = "Miercoles" },
				new HorarioMedico { DiaSemana = "Jueves" },
				new HorarioMedico { DiaSemana = "Viernes" },
				new HorarioMedico { DiaSemana = "Sabado" },
				new HorarioMedico { DiaSemana = "Domingo" }
			};

			foreach (var dia in dias)
			{
				if (DiasDeAtencion.TryGetValue(dia.DiaSemana, out var horarios))
				{
					dia.InicioHorario = horarios.Start;
					dia.FinHorario = horarios.End;
					dia.Trabaja = true; // Assuming the doctor works on this day if there are horarios.
				}
				else
				{
					dia.Trabaja = false; // If no horarios exist for the day, the doctor doesn't work.
				}
			}

			return dias;
		}
		
		
		
		
		
		
		
		
		public Medico() { }
		
		public Medico(JsonElement jsonElement)
		{
			Name = jsonElement.GetProperty("Name").GetString();
			Lastname = jsonElement.GetProperty("Lastname").GetString();
			Dni = jsonElement.GetProperty("Dni").GetString();
			Provincia = jsonElement.GetProperty("Provincia").GetString();
			Domicilio = jsonElement.GetProperty("Domicilio").GetString();
			Localidad = jsonElement.GetProperty("Localidad").GetString();
			Specialidad = jsonElement.GetProperty("Specialidad").GetString();
			Telefono = jsonElement.GetProperty("Telefono").GetString();
			Guardia = jsonElement.GetProperty("Guardia").GetBoolean();
			FechaIngreso = DateTime.Parse(jsonElement.GetProperty("FechaIngreso").GetString());
			SueldoMinimoGarantizado = jsonElement.GetProperty("SueldoMinimoGarantizado").GetDouble();

			// Read days of attention
			if (jsonElement.TryGetProperty("DiasDeAtencion", out var diasDeAtencionElement))
			{
				foreach (var dia in diasDeAtencionElement.EnumerateObject())
				{
					var diaKey = dia.Name;

					// Check if both "start" and "end" properties exist before accessing them
					if (dia.Value.TryGetProperty("start", out var startElement) && dia.Value.TryGetProperty("end", out var endElement))
					{
						var start = startElement.GetString();
						var end = endElement.GetString();
						DiasDeAtencion[diaKey] = Horario(start, end);
					}
				}
			}
		}

		// Constructor that takes a Dictionary<string, string>
		public Medico(Dictionary<string, string> dict)
		{
			Name = dict.GetValueOrDefault("Name");
			Lastname = dict.GetValueOrDefault("Lastname");
			Dni = dict.GetValueOrDefault("Dni");
			Provincia = dict.GetValueOrDefault("Provincia");
			Domicilio = dict.GetValueOrDefault("Domicilio");
			Localidad = dict.GetValueOrDefault("Localidad");
			Specialidad = dict.GetValueOrDefault("Specialidad");
			Telefono = dict.GetValueOrDefault("Telefono");
			Guardia = bool.Parse(dict.GetValueOrDefault("Guardia", "false"));
			FechaIngreso = DateTime.Parse(dict.GetValueOrDefault("FechaIngreso"));
			SueldoMinimoGarantizado = double.Parse(dict.GetValueOrDefault("SueldoMinimoGarantizado", "0"));

			// Days of attention (as a dictionary, you can decide how you want to structure this)
			if (dict.ContainsKey("DiasDeAtencion"))
			{
				var diasDeAtencion = dict["DiasDeAtencion"];
				// Example for handling the dictionary structure of "DiasDeAtencion" (expand as necessary)
				var diasList = diasDeAtencion.Split(';');
				foreach (var dia in diasList)
				{
					var parts = dia.Split(':'); // Assuming key-value like "Monday:09:00-17:00"
					if (parts.Length == 2)
					{
						DiasDeAtencion[parts[0]] = (parts[1].Split('-')[0], parts[1].Split('-')[1]);
					}
				}
			}
		}
	}
	
	public class Paciente: TablaEntidad {
		public const string TableName = "pacientes";
		// public int Dni { get; set; }
		public string Dni { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public DateTime FechaIngreso { get; set; }  // Corrige a DateTime
		public string Email { get; set; }
		public string Telefono { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public string Direccion { get; set; }
		public string Localidad { get; set; }
		public string Provincia { get; set; }
			


		public Paciente() { }
		
		// Constructor that takes a JsonElement
		public Paciente(JsonElement json)
		{
			Dni = json.GetProperty("Dni").GetString();
			Name = json.GetProperty("Name").GetString();
			Lastname = json.GetProperty("Lastname").GetString();
			FechaIngreso = json.GetProperty("FechaIngreso").GetDateTime();
			Email = json.GetProperty("Email").GetString();
			Telefono = json.GetProperty("Telefono").GetString();
			FechaNacimiento = json.GetProperty("FechaNacimiento").GetDateTime();
			Direccion = json.GetProperty("Direccion").GetString();
			Localidad = json.GetProperty("Localidad").GetString();
			Provincia = json.GetProperty("Provincia").GetString();
		}

		// Constructor that takes a Dictionary<string, string>
		public Paciente(Dictionary<string, string> dict)
		{
			Dni = dict.GetValueOrDefault("Dni");
			Name = dict.GetValueOrDefault("Name");
			Lastname = dict.GetValueOrDefault("Lastname");
			FechaIngreso = DateTime.Parse(dict.GetValueOrDefault("FechaIngreso"));
			Email = dict.GetValueOrDefault("Email");
			Telefono = dict.GetValueOrDefault("Telefono");
			FechaNacimiento = DateTime.Parse(dict.GetValueOrDefault("FechaNacimiento"));
			Direccion = dict.GetValueOrDefault("Direccion");
			Localidad = dict.GetValueOrDefault("Localidad");
			Provincia = dict.GetValueOrDefault("Provincia");
		}

		public void UpsertToDatabaseStr(Dictionary<string, Dictionary<string, Dictionary<string, string>>> databaseStr){
			databaseStr[TableName][Dni]["Name"] = Name;
			databaseStr[TableName][Dni]["Lastname"] = Lastname;
			databaseStr[TableName][Dni]["FechaIngreso"] = FechaIngreso.ToString();
			databaseStr[TableName][Dni]["Email"] = Email;
			databaseStr[TableName][Dni]["Telefono"] = Telefono;
			databaseStr[TableName][Dni]["FechaNacimiento"] = FechaNacimiento.ToString();
			databaseStr[TableName][Dni]["Direccion"] = Direccion;
			databaseStr[TableName][Dni]["Localidad"] = Localidad;
			databaseStr[TableName][Dni]["Provincia"] = Provincia;
		}
	}
	
	public class Turno: TablaEntidad {
		public const string TableName = "turnos";
		public int MedicoPk { get; set; }
		public int PacientePk { get; set; }
		public DateTime FechaYHoraAsignada { get; set; }
			
		// Parameterless constructor
		public Turno() { }
	
		public Turno(JsonElement jsonElement)
		{
			MedicoPk = jsonElement.GetProperty("MedicoPk").GetInt32();
			PacientePk = jsonElement.GetProperty("PacientePk").GetInt32();
			FechaYHoraAsignada = DateTime.Parse(jsonElement.GetProperty("FechaYHoraAsignada").GetString());
		}
		public Turno(Dictionary<string, string> dict)
		{
			MedicoPk = int.Parse(dict.GetValueOrDefault("MedicoPk", "0"));
			PacientePk = int.Parse(dict.GetValueOrDefault("PacientePk", "0"));
			FechaYHoraAsignada = DateTime.Parse(dict.GetValueOrDefault("FechaYHoraAsignada"));
		}
	}
}
