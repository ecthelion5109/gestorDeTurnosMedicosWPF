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
			if (labelUsuario.IsEnabled) //osea, si modo SQL
			{
				string hostName = labelUsuario.Text;  // Usuario de SQL Server
				string hostPass = labelPassword.Text;  // Contraseña de SQL Server
				string serverName = labelServidor.Text;  // Nombre del servidor (si es configurable por el usuario)
				//BaseDeDatosSQL.connectionString = $"Server={hostName};Database=ClinicaMedica;Integrated Security=True
				//static public string connectionString = ConfigurationManager.ConnectionStrings["ConexionAClinicaMedica"].ConnectionString;
				try {
					BaseDeDatosSQL.connectionString = $"Server={serverName};Database=ClinicaMedica;User ID={hostName};Password={hostPass};";
					MainWindow.BaseDeDatos = new BaseDeDatosSQL();
					MessageBox.Show($"Conexion SQL establecida extiosamente");
				}
				catch (Exception ex) {
					MessageBox.Show($"{ex.Message}");
					return;
				}
			} else {
				MainWindow.BaseDeDatos = new BaseDeDatosSQL();
				MessageBox.Show("Conexión establecida con el archivo JSON.");
				
			}

			App.Logueado = true;
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
			labelUsuario.IsEnabled = false;
			labelServidor.IsEnabled = false;
		}

		private void radioButtonSQLChecked(object sender, RoutedEventArgs e) {
			labelPassword.IsEnabled = true;
			labelUsuario.IsEnabled = true;
			labelServidor.IsEnabled = true;
		}
	}
}
