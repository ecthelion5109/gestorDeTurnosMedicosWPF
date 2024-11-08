using System.Windows;
using System.Windows.Controls;
			
		
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
