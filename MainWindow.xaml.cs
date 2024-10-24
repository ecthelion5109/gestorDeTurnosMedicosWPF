﻿using System;
using System.Windows;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaMedica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
	public enum SqlOperationType {
		CREATE,
		READ,
		UPDATE,
		DELETE
	}
	public enum OperationCode {
		YA_EXISTE,
		SUCCESS,
		CREATE_SUCCESS,
		UPDATE_SUCCESS,
		DELETE_SUCCESS,
		MISSING_DNI,
		MISSING_FIELDS,
		ERROR,
		DATOS_LEIDOS
	}
	public enum DatabaseType {
		JSON,
		SQL
	}

    public static class WindowExtensions{
		public static void NavegarA<T>(this Window previousWindow) where T : Window, new()
		{
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		public static void Salir(this Window previousWindow)
		{
			Application.Current.Shutdown();  // Apagar la aplicación
		}
	}
	
	public partial class MainWindow : Window {
		//SQL.SqlConnection.SqlClientPermission miConexionSql;
		public static DatabaseType DB_MODO = DatabaseType.JSON;
		public MainWindow() {
			InitializeComponent();
			MainWindow.DB_MODO = DatabaseType.SQL;
			// MainWindow.DB_MODO = DatabaseType.JSON;

			//string miConexion = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;

			//miConexionSql = new SqlConnection(miConexion);









		}









		public void MetodoBotonLogin(object sender, RoutedEventArgs e) {
			this.NavegarA<Login>();
		}


		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

        private void MetodoBotonMedicos(object sender, RoutedEventArgs e){
			this.NavegarA<Medicos>();
		}

        private void MetodoBotonPacientes(object sender, RoutedEventArgs e)
        {
            this.NavegarA<Pacientes>();
        }
    }
}