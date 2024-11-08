using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// using System.Media;
// using System.Windows.Media;

		
		// private void MouseHoverEvento(object sender, System.Windows.Input.MouseEventArgs e) {
			// MediaPlayer mediaPlayer = new MediaPlayer();
			// mediaPlayer.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
			// mediaPlayer.Play();
			// MediaPlayer mediaPlayer = new MediaPlayer();
			// mediaPlayer.Open(new Uri("sonidos\\uclick_jewel.wav", UriKind.Relative));
			// mediaPlayer.Play();
		// }
		
			
			//if (BaseDeDatosSQL.connectionString == null){
			//	iniciarSesionButton.Content = "Iniciar sesión con credenciales";
			//} else {
			//	iniciarSesionButton.Content = "Iniciar sesión con app.config";
			//}
			
		
namespace ClinicaMedica {
	public partial class Login : Window {
		public Login() {
			InitializeComponent();
		}
		
		private bool datos_completados(){
			return string.IsNullOrEmpty(labelServidor.Text) && string.IsNullOrEmpty(labelUsuario.Text) && string.IsNullOrEmpty(labelPassword.Text); 
		}
		
		private void MetodoBotonIniciarSesion(object sender, RoutedEventArgs e) {
			if (checkboxJSON.IsChecked == true) {
				App.BaseDeDatos = new BaseDeDatosJSON();
			} else if ( datos_completados() ) {
				App.BaseDeDatos = new BaseDeDatosSQL();
			} else {
				App.BaseDeDatos = new BaseDeDatosSQL($"Server={labelServidor.Text};Database=ClinicaMedica;User ID={labelUsuario.Text};Password={labelPassword.Text};");
			}
			App.UsuarioLogueado = App.BaseDeDatos.ConectadaExitosamente;
			if (App.UsuarioLogueado) {
				this.Cerrar();
			}
		}

		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}

		private void MetodoBotonCancelar(object sender, RoutedEventArgs e) {
			this.Cerrar();
		}

		private void ComboBoxBaseDeDatos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		}

		private void radioButtonJSONChecked(object sender, RoutedEventArgs e) {
			labelPassword.IsEnabled = false;
			labelServidor.IsEnabled = false;
			labelUsuario.IsEnabled = false;
		}

		private void radioButtonSQLChecked(object sender, RoutedEventArgs e) {
			labelPassword.IsEnabled = true;
			labelServidor.IsEnabled = true;
			labelUsuario.IsEnabled = true;
		}
	}
}
