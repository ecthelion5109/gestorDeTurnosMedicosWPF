using System.Configuration;
using System.Data;
using System.Windows;

namespace ClinicaMedica {
	public partial class App : Application {
		public static bool UsuarioLogueado = false;
		public static string UsuarioName = "Señor Gestor";
		public static IBaseDeDatos BaseDeDatos;
		
		
	}
	

    public static class WindowExtensions{
		public static void NavegarA<T>(this Window previousWindow) where T : Window, new(){
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		public static void NavegarA<T>(this Window previousWindow, object optionalArg) where T : Window, new(){
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		
		public static void AbrirComoDialogo<T>(this Window previousWindow) where T : Window, new(){
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}

		public static void AbrirComoDialogo<T>(this Window previousWindow, object optionalArg) where T : Window{
			// Utiliza Activator para instanciar la ventana con el parámetro opcional
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}
		
		public static void VolverAHome(this Window previousWindow){
			previousWindow.NavegarA<MainWindow>();
		}
		public static void Salir(this Window previousWindow){
			Application.Current.Shutdown();  // Apagar la aplicación
		}
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

}
