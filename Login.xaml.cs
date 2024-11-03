using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace ClinicaMedica{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window {
        public Login(){
            InitializeComponent();
        }
		
		private void MetodoBotonIniciarSesion(object sender, RoutedEventArgs e) {

			if (!labelServidor.IsEnabled) {
				MainWindow.BaseDeDatos = new BaseDeDatosJSON();
				//MessageBox.Show("Conexión establecida con el archivo JSON.");
			} else if (  string.IsNullOrEmpty(labelServidor.Text) && string.IsNullOrEmpty(labelUsuario.Text) && string.IsNullOrEmpty(labelPassword.Text) ) {
				try {
					MainWindow.BaseDeDatos = new BaseDeDatosSQL();
				}
				catch (Exception ex) {
					MessageBox.Show($"{ex.Message}");
					return;
				}
			} else {
				try {
					BaseDeDatosSQL.connectionString = $"Server={labelServidor.Text};Database=ClinicaMedica;User ID={labelUsuario.Text};Password={labelPassword.Text};";
					MainWindow.BaseDeDatos = new BaseDeDatosSQL();
					//MessageBox.Show($"Conexion SQL establecida extiosamente");
				}
				catch (Exception ex) {
					MessageBox.Show($"{ex.Message}");
					return;
				}
			}

			App.UsuarioLogueado = true;
			App.UsuarioName = labelUsuario.Text;
			// this.NavegarA<MainWindow>();
			// this.close();
			this.Close();



		}

        public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
        }

		private void MetodoBotonCancelar(object sender, RoutedEventArgs e) {
			this.Close(); // this.NavegarA<MainWindow>();
		}

		private void ComboBoxBaseDeDatos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        }

		private void radioButtonJSONChecked(object sender, RoutedEventArgs e) {
			labelPassword.IsEnabled = false;
			//labelUsuario.IsEnabled = false;
			labelServidor.IsEnabled = false;
		}

		private void radioButtonSQLChecked(object sender, RoutedEventArgs e) {
			labelPassword.IsEnabled = true;
			//labelUsuario.IsEnabled = true;
			labelServidor.IsEnabled = true;
		}
	}
}
