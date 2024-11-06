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
using System.Media;
using System.Windows.Media;

namespace ClinicaMedica {
	public partial class Login : Window {
		public Login() {
			InitializeComponent();
		}
		
		private void MouseHoverEvento(object sender, System.Windows.Input.MouseEventArgs e) {
			MediaPlayer mediaPlayer = new MediaPlayer();
			mediaPlayer.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
			mediaPlayer.Play();
		}

		private void MetodoBotonIniciarSesion(object sender, RoutedEventArgs e) {
			MediaPlayer mediaPlayer = new MediaPlayer();
			mediaPlayer.Open(new Uri("sonidos\\uclick_jewel.wav", UriKind.Relative));
			mediaPlayer.Play();

			if (checkboxJSON.IsChecked == true) {
				App.BaseDeDatos = new BaseDeDatosJSON();
				App.UsuarioLogueado = true;
				this.Close();
				return;
			}

			if (
				!(string.IsNullOrEmpty(labelServidor.Text)
				&& string.IsNullOrEmpty(labelUsuario.Text)
				&& string.IsNullOrEmpty(labelPassword.Text))
			) {
				try {
					BaseDeDatosSQL.connectionString = $"Server={labelPassword.Text};";
					App.UsuarioName = labelUsuario.Text;
					App.BaseDeDatos = new BaseDeDatosSQL();
					App.UsuarioLogueado = true;
					this.Close();
					return;
				}
				catch (Exception ex) {
					MessageBox.Show($"{ex.Message}");
					App.UsuarioLogueado = false;
					this.Close();
					return;
				}
			}
			else {
				try {
					App.BaseDeDatos = new BaseDeDatosSQL();
					App.UsuarioLogueado = true;
					this.Close();
					return;
				}
				catch (Exception ex) {
					MessageBox.Show($"{ex.Message}");
					App.UsuarioLogueado = false;
					this.Close();
					return;
				}
			}
		}

		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}

		private void MetodoBotonCancelar(object sender, RoutedEventArgs e) {
			this.Close();
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
