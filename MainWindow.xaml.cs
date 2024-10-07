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


namespace ClinicaMedica {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	//---------------------------------Clases-------------------------------//
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
        public static void TestGuardar(){
			// Crear instancias de ejemplo
			var paciente = new Paciente{
				Dni = 12345678,
				Name = "Juan",
				Lastname = "Pérez",
				FechaIngreso = DateTime.Now,
				Email = "juan.perez@example.com",
				Telefono = "123456789",
				CoberturaMedica = "Cobertura X",
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
	public class Paciente{
		public int Dni { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public DateTime FechaIngreso { get; set; }  // Corrige a DateTime
		public string Email { get; set; }
		public string Telefono { get; set; }
		public string CoberturaMedica { get; set; }
		public DateTime FechaNacimiento { get; set; }
	}
	public class Medico{
		public int Dni { get; set; }
		public string Name { get; set; }  // 50 caracteres máximo
		public string Lastname { get; set; }  // 50 caracteres máximo
		public string Provincia { get; set; }  // 40 caracteres máximo
		public string Domicilio { get; set; }  // 50 caracteres máximo
		public string Localidad { get; set; }  // 50 caracteres máximo
		public string Specialidad { get; set; }  // 20 caracteres máximo
		public string Telefono { get; set; }
		public string[] DiasDeAtencion { get; set; }
		public bool Guardia { get; set; }
		public DateTime FechaIngreso { get; set; }  //delimator. No puede haber ingresado hace 100 años ni haber ingresado en el futuro
		public double SueldoMinimoGarantizado { get; set; } //no puede tener cero ni numeros negativos
	}
	public class Turno{
		public int MedicoPk { get; set; }
		public int PacientePk { get; set; }
		public DateTime FechaYHoraAsignada { get; set; }
	}
	
	public static class WindowExtensions{
		public static void NavegarA<T>(this Window ventanaActual) where T : Window, new()
		{
			T nuevaVentana = new T();
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			ventanaActual.Close();  // Cerrar la ventana actual
		}
	}
	
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}
		
		
		private void MetodoBotonIniciarSesion(object sender, RoutedEventArgs e) {
			string usuario = ""; //"mariela";
			string contraseña = ""; // "123";
			if (  label_user.Text.Equals(usuario) && label_pass.Text.Equals(contraseña)   ) {
				this.NavegarA<PantallaPrincipal>();
			}
			else {
				MessageBox.Show("Contraseña o usuario incorrecta");
			}
		}

        public void MetodoBotonSalir(object sender, RoutedEventArgs e){
			Application.Current.Shutdown ();
        }
		
        public void MetodoBotonTestearJsonLeer(object sender, RoutedEventArgs e){
			BaseDeDatos.TestLeer();
		}
        public void MetodoBotonTestearJsonGuardar(object sender, RoutedEventArgs e){
			BaseDeDatos.TestGuardar();
		}
		
		
	}
}