using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;

namespace ClinicaMedica {
	//---------------------------------Tablas-------------------------------//
	public class Paciente {
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
			
		// Factory method to convert a JsonElement to a Paciente instance
		public static Paciente FromJson(JsonElement json)
		{
			return new Paciente
			{
				Dni = json.GetProperty("Dni").GetString(), //.GetInt32(),
				Name = json.GetProperty("Name").GetString(),
				Lastname = json.GetProperty("Lastname").GetString(),
				FechaIngreso = json.GetProperty("FechaIngreso").GetDateTime(),
				Email = json.GetProperty("Email").GetString(),
				Telefono = json.GetProperty("Telefono").GetString(),
				FechaNacimiento = json.GetProperty("FechaNacimiento").GetDateTime(),
				Direccion = json.GetProperty("Direccion").GetString(),
				Localidad = json.GetProperty("Localidad").GetString(),
				Provincia = json.GetProperty("Provincia").GetString()
			};
		}
	}
	
	public class Medico {
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
		public Dictionary<string, (string start, string end)> DiasDeAtencion { get; set; } = new Dictionary<string, (string start, string end)>();
		
		// Factory method for instantiating from JSON
		public static Medico FromJson(JsonElement jsonElement)
		{
			var medico = new Medico
			{
				Name = jsonElement.GetProperty("Name").GetString(),
				Lastname = jsonElement.GetProperty("Lastname").GetString(),
				Dni = jsonElement.GetProperty("Dni").GetString(), //.GetInt32(),
				Provincia = jsonElement.GetProperty("Provincia").GetString(),
				Domicilio = jsonElement.GetProperty("Domicilio").GetString(),
				Localidad = jsonElement.GetProperty("Localidad").GetString(),
				Specialidad = jsonElement.GetProperty("Specialidad").GetString(),
				Telefono = jsonElement.GetProperty("Telefono").GetString(),
				Guardia = jsonElement.GetProperty("Guardia").GetBoolean(),
				FechaIngreso = DateTime.Parse(jsonElement.GetProperty("FechaIngreso").GetString()),
				SueldoMinimoGarantizado = jsonElement.GetProperty("SueldoMinimoGarantizado").GetDouble()
			};

			// Read days of attention
			if (jsonElement.TryGetProperty("DiasDeAtencion", out var diasDeAtencionElement))
			{
				foreach (var dia in diasDeAtencionElement.EnumerateObject())
				{
					var diaKey = dia.Name;
					var start = dia.Value.GetProperty("start").GetString();
					var end = dia.Value.GetProperty("end").GetString();
					medico.DiasDeAtencion[diaKey] = (start, end);
				}
			}

			return medico;
		}
	}
	public class Turno {
		public int MedicoPk { get; set; }
		public int PacientePk { get; set; }
		public DateTime FechaYHoraAsignada { get; set; }
		
		// Factory method for instantiating from JSON
		public static Turno FromJson(JsonElement jsonElement)
		{
			return new Turno
			{
				MedicoPk = jsonElement.GetProperty("MedicoPk").GetInt32(),
				PacientePk = jsonElement.GetProperty("PacientePk").GetInt32(),
				FechaYHoraAsignada = DateTime.Parse(jsonElement.GetProperty("FechaYHoraAsignada").GetString())
			};
		}
	}
	
}