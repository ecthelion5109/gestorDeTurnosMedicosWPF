using System;
using System.Windows;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaMedica{
	public partial class MainWindow : Window {
		//SQL.SqlConnection.SqlClientPermission miConexionSql;
		public static IBaseDeDatos BaseDeDatos;
		
		public MainWindow() {
			InitializeComponent();
			 //BaseDeDatos = new BaseDeDatosJSON();
			BaseDeDatos = new BaseDeDatosSQL();
			//string miConexion = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;
			//miConexionSql = new SqlConnection(miConexion);
		}


		public void MetodoBotonLogin(object sender, RoutedEventArgs e) {
			this.NavegarA<Login>();
		}

		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

        private void MetodoBotonMedicos(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

        private void MetodoBotonPacientes(object sender, RoutedEventArgs e) {
            this.NavegarA<Pacientes>();
        }

		private void MetodoBotonTurnos(object sender, RoutedEventArgs e) {
			this.NavegarA<Turnos>();
		}
	}
}