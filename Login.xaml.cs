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
    public partial class Login : Window {
        public Login(){
            InitializeComponent();
        }
		
		private void MetodoBotonIniciarSesion(object sender, RoutedEventArgs e) {
			if (!labelServidor.IsEnabled) {
				App.BaseDeDatos = new BaseDeDatosJSON();
			} else if (  string.IsNullOrEmpty(labelServidor.Text) && string.IsNullOrEmpty(labelUsuario.Text) && string.IsNullOrEmpty(labelPassword.Text) ) {
				try {
					App.BaseDeDatos = new BaseDeDatosSQL();
				}
				catch (Exception ex) {
					MessageBox.Show($"{ex.Message}");
					return;
				}
			} else {
				try {
					BaseDeDatosSQL.connectionString = $"Server={labelServidor.Text};Database=ClinicaMedica;User ID={labelUsuario.Text};Password={labelPassword.Text};";
					App.BaseDeDatos = new BaseDeDatosSQL();
				}
				catch (Exception ex) {
					MessageBox.Show($"{ex.Message}");
					return;
				}
			}
			App.UsuarioLogueado = true;
			App.UsuarioName = labelUsuario.Text;
			this.Close();
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
		}

		private void radioButtonSQLChecked(object sender, RoutedEventArgs e) {
			labelPassword.IsEnabled = true;
			labelServidor.IsEnabled = true;
		}
	}
}
