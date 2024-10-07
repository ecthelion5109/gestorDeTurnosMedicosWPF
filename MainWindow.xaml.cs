using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Net;
using System.Xml.Linq;


namespace ClinicaMedica {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	//---------------------------------Clases-------------------------------//
	
	public class Paciente{
		public int Dni { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public DateTime FechaIngreso { get; set; }  // Corrige a DateTime
		public string Email { get; set; }
		public string Telefono { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public string Direccion { get; set; }
		public string Localidad { get; set; }
		public string Provincia { get; set; }
	}
	public class Medico{
		public string Name { get; set; }  // 50 caracteres máximo
		public string Lastname { get; set; }  // 50 caracteres máximo
		public int Dni { get; set; }
		public string Provincia { get; set; }  // 40 caracteres máximo
		public string Domicilio { get; set; }  // 50 caracteres máximo
		public string Localidad { get; set; }  // 50 caracteres máximo
		public string Specialidad { get; set; }  // 20 caracteres máximo
		public string Telefono { get; set; }
		public bool Guardia { get; set; }
		public DateTime FechaIngreso { get; set; }  //delimator. No puede haber ingresado hace 100 años ni haber ingresado en el futuro
		public double SueldoMinimoGarantizado { get; set; } //no puede tener cero ni numeros negativos
		public string[] DiasDeAtencion { get; set; }

        //public Dictionary<string, List<string>> DiasDeAtencion { get; set; }
    }
	public class Turno{
		public int MedicoPk { get; set; }
		public int PacientePk { get; set; }
		public DateTime FechaYHoraAsignada { get; set; }
	}
	public class BaseDeDatos{
		public static void GuardarComoJson<T>(T objeto, string archivo){
			var opciones = new JsonSerializerOptions { WriteIndented = true };
			string jsonString = JsonSerializer.Serialize(objeto, opciones);
			File.WriteAllText(archivo, jsonString);
		}
		public static T LeerDesdeJson<T>(string archivo){
			string jsonString = File.ReadAllText(archivo);
			return JsonSerializer.Deserialize<T>(jsonString);
		}
        public static void TestLeer(){
			// Leer desde JSON
			Paciente pacienteLeido = BaseDeDatos.LeerDesdeJson<Paciente>("paciente.json");
			Medico medicoLeido = BaseDeDatos.LeerDesdeJson<Medico>("medico.json");
			Turno turnoLeido = BaseDeDatos.LeerDesdeJson<Turno>("turno.json");

			MessageBox.Show($"Se ha leido a Paciente: {pacienteLeido.Name} {pacienteLeido.Lastname}\nSe ha leido a Medico: {medicoLeido.Name} {medicoLeido.Lastname}\nSe ha leido a Turno Asignado: {turnoLeido.FechaYHoraAsignada}");
        }
        public static void GuardarTurno(int dniPaciente, int dniMedico, DateTime fecha) {
			var turno = new Turno {
				MedicoPk = dniPaciente,
				PacientePk = dniMedico,
				FechaYHoraAsignada = fecha,
			};
			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(turno, "turno.json");

			MessageBox.Show($"Se ha guardado el turno para la fecha: {turno.FechaYHoraAsignada}");


			/*
			var turnos = new List<Turno>();

			// Generar 10 turnos por cada hora de 8 a 17
			for (int hora = 8; hora <= 17; hora++) {
				for (int turno = 1; turno <= 10; turno++) {
					turnos.Add(new Turno {
						Hora = $"{hora:00}:00",
						NumeroTurno = turno,
						Estado = "Disponible" // Puedes modificar esto según el estado real
					});
				}
			}

			// Asignar la lista de turnos al DataGrid
			DataGridTurnos.ItemsSource = turnos;
			*/
		}

		// public static void MedicosGuardar(int dni, string name, string lastname, DateTime fechaingreso, string email, string telefono, DateTime fechanacimiento, string domicilio, string localidad, string provincia, string specialidad, bool guardia, decimal sueldominimogarantizado) {
		public static void MedicosGuardar(int dni, string name, string lastname, DateTime fechaingreso, string domicilio, string localidad, string provincia, string specialidad, bool guardia, decimal sueldominimogarantizado) {
			// Crear instancia de Medico
			var medico = new Medico {
				Name = name,
				Lastname = lastname,
				Dni = dni,
				Provincia = provincia,
				Domicilio = domicilio,
				Localidad = localidad,
				Specialidad = specialidad,
				// Telefono = telefono,
				Guardia = guardia,
				FechaIngreso = fechaingreso,
				SueldoMinimoGarantizado = (double) sueldominimogarantizado,
				//DiasDeAtencion = diasdeatencion,
			};
			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(medico, "medico.json");

			MessageBox.Show($"Se han guardado los cambios de Paciente: {medico.Name} {medico.Lastname}");
		}





		public static void PacienteGuardar(int dni, string name, string lastname, DateTime fechaingreso, string email, string telefono, DateTime fechanacimiento, string direccion, string localidad, string provincia){
			// Crear instancias de ejemplo
			var paciente = new Paciente{
				Dni = dni,
				Name = name,
				Lastname = lastname,
				FechaIngreso = fechaingreso,
				Email = email,
				Telefono = telefono,
				FechaNacimiento = fechanacimiento,
				Direccion = direccion,
				Localidad = localidad,
				Provincia = provincia
			};
			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(paciente, "paciente.json");

			MessageBox.Show($"Se ha instanciado y guardado a Paciente: {paciente.Name} {paciente.Lastname}");
		}
		
		
		
		
        public static void TestGuardar(){
			// Crear instancias de ejemplo
			var paciente = new Paciente{
				Dni = 12345678,
				Name = "Juan",
				Lastname = "Pérez",
				FechaIngreso = DateTime.Now,
				Email = "juan.perez@example.com",
				Telefono = "123456789",
				FechaNacimiento = new DateTime(1980, 5, 15)
			};

			var medico = new Medico{
				Dni = 87654321,
				Name = "Dr. Ana",
				Lastname = "Gómez",
				Provincia = "Buenos Aires",
				Domicilio = "Calle Falsa 123",
				Localidad = "Ciudad X",
				Specialidad = "Cardiología",
				Telefono = "987654321",
				DiasDeAtencion = new[] { "Lunes", "Miércoles", "Viernes" },
				Guardia = true,
				FechaIngreso = DateTime.Now,
				SueldoMinimoGarantizado = 150000
			};

			var turno = new Turno{
				MedicoPk = 87654321,
				PacientePk = 12345678,
				FechaYHoraAsignada = DateTime.Now.AddHours(2)
			};

			// Guardar como JSON
			BaseDeDatos.GuardarComoJson(paciente, "paciente.json");
			BaseDeDatos.GuardarComoJson(medico, "medico.json");
			BaseDeDatos.GuardarComoJson(turno, "turno.json");

			MessageBox.Show($"Se ha instanciado y guardado a Paciente: {paciente.Name} {paciente.Lastname}\nSe ha instanciado y guardado a Medico: {medico.Name} {medico.Lastname}\nSe ha instanciado y guardado a Turno Asignado: {turno.FechaYHoraAsignada}");
		}
	}
	
	public static class WindowExtensions{
		public static void NavegarA<T>(this Window ventanaActual) where T : Window, new()
		{
			T nuevaVentana = new T();
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			ventanaActual.Close();  // Cerrar la ventana actual
		}
		public static void Salir(this Window ventanaActual)
		{
			Application.Current.Shutdown();  // Apagar la aplicación
		}
	}
	
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}
		public void MetodoBotonLogin(object sender, RoutedEventArgs e) {
			this.NavegarA<Login>();
		}


		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

        private void MetodoBotonMedicos(object sender, RoutedEventArgs e){
			this.NavegarA<Medicos>();
		}

		private void MetodoBotonTurnos(object sender, RoutedEventArgs e) {
			this.NavegarA<Turnos>();
		}

		private void MetodoBotonPacientes(object sender, RoutedEventArgs e) {
			this.NavegarA<Pacientes>();
		}

		public void MetodoBotonTestearJsonLeer(object sender, RoutedEventArgs e) {
			BaseDeDatos.TestLeer();
		}
		public void MetodoBotonTestearJsonGuardar(object sender, RoutedEventArgs e) {
			BaseDeDatos.TestGuardar();
		}
	}
}