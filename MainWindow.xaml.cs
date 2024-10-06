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
// using System;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using System.IO;



namespace ClinicaMedica {
	
	public class Paciente{
		int dni;
		string name;
		string lastname;
		Date fecha_ingreso;	//delimator. No puede haber ingresado hace 100 años ni haber ingresado en el futuro
		string email;
		string telefono;
		string cobertura_medica;
		DateTime fecha_nacimiento;
	}
	
	
	public class Medico{
		int dni;
		string name; //50 digitos
		string lastname; //50 digitos
		string provincia; //40 digitos
		string domicilio; //50 digitos
		string localidad; //50 digitos
		string specialidad; //20 digitos
		string telefono;
		string[] dias_de_atencion;
		bool guardia;
		DateTime fecha_ingreso;	//delimator. No puede haber ingresado hace 100 años ni haber ingresado en el futuro
		double sueldo_minimo_garantizado; //no puede tener cero ni numeros negativos
	}
	
	public class Turno{
		int medico_pk; 
		int paciente_pk;
		DateTime fecha_y_hora_asignada;
	}
	
	// Paciente instance = Paciente new(){
		// pk = 1;
		// name = "Carlos";
		// lastname = "Mendiguren";
		// fecha_ingreso = "15-06-2024"
	// }
	
	
	
	
	
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void MetodoBotonIniciarSesion(object sender, RoutedEventArgs e) {
			//MessageBox.Show($"Usuario correcta?: {label_user.Text.Equals("juanita_conchuda_123")}");
			//MessageBox.Show($"Contraseña correcta?: {label_pass.Text.Equals("123")}");

			Console.WriteLine(sender.ToString());
			//MessageBox.Show(sender.ToString());
			string usuario = ""; //"mariela";
			string contraseña = ""; // "123";


			if (  label_user.Text.Equals(usuario) && label_pass.Text.Equals(contraseña)   ) {
				//escondo la anterior
				//this.Hide();


				//creo una nueva y la vinculo a la aplicacion completa
				PantallaPrincipal inicio = new PantallaPrincipal();
				Application.Current.MainWindow = inicio;
				inicio.Show();

				//cierro la anterior.
				this.Close();
			}
			else {
				MessageBox.Show("Contraseña o usuario incorrecta");
			}
		}

        public void MetodoBotonSalir(object sender, RoutedEventArgs e)
        {
			Application.Current.Shutdown ();
        }
	}
}