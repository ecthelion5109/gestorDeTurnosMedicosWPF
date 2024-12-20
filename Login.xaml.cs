﻿using System.Windows;
using System.Windows.Controls;
			
		
namespace ClinicaMedica {
	public partial class Login : Window {
		public Login() {
			InitializeComponent();
			soundCheckBox.IsChecked = App.SoundOn;
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

		private void checkboxJSON_Checked(object sender, RoutedEventArgs e)
		{
			if (labelPassword != null) labelPassword.IsEnabled = false;
			if (labelServidor != null) labelServidor.IsEnabled = false;
			if (labelUsuario != null) labelUsuario.IsEnabled = false;
		}

		private void checkboxSQL_Checked(object sender, RoutedEventArgs e)
		{
			if (labelPassword != null) labelPassword.IsEnabled = true;
			if (labelServidor != null) labelServidor.IsEnabled = true;
			if (labelUsuario != null) labelUsuario.IsEnabled = true;
		}

		private void soundCheckBox_Checked(object sender, RoutedEventArgs e) {
			if (soundCheckBox.IsChecked == true) {
				App.SoundOn = true;
				App.PlayClickJewel();
			}
			else {
				App.SoundOn = false;
			}
        }

		// private void MetodoEliminarDatabase(object sender, RoutedEventArgs e) {
			// App.BaseDeDatos.EliminarDatabaseExitosamente();
		// }
	}
}
