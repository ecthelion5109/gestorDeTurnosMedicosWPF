using System;
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
		SIN_DEFINIR,
		UPDATE_SUCCESS,
		DELETE_SUCCESS,
		MISSING_DNI,
		MISSING_FIELDS,
		ERROR,
		DATOS_LEIDOS
	}
	
	public interface IBaseDeDatos{
		// Read methods
		List<Medico> ReadMedicos();
		List<Paciente> ReadPacientes();
		List<Turno> ReadTurnos();

		// checkers
		bool CorroborarQueNoExistaMedico(string key);
		bool CorroborarQueNoExistaPaciente(string key);
		bool CorroborarQueNoExistaTurno(string key);

		// Create methods
		OperationCode CreateMedico(Medico medico);
		OperationCode CreatePaciente(Paciente paciente);
		OperationCode CreateTurno(Turno turno);

		// Update methods
		OperationCode UpdateMedico(Medico medico, string originalDni);
		OperationCode UpdatePaciente(Paciente paciente, string originalDni);
		OperationCode UpdateTurno(Turno turno);

		// Delete methods
		OperationCode DeleteMedico(string medicoId);
		OperationCode DeletePaciente(string pacienteId);
		OperationCode DeleteTurno(string turnoId);
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
		public static IBaseDeDatos BaseDeDatos;
		
		
		public MainWindow() {
			InitializeComponent();
			
			 BaseDeDatos = new BaseDeDatosJSON();
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

        private void MetodoBotonMedicos(object sender, RoutedEventArgs e){
			this.NavegarA<Medicos>();
		}

        private void MetodoBotonPacientes(object sender, RoutedEventArgs e)
        {
            this.NavegarA<Pacientes>();
        }
    }
}