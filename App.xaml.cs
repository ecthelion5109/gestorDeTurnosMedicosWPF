using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;


namespace ClinicaMedica {
	public partial class App : Application {
		public static BaseDeDatosAbstracta BaseDeDatos;
		public static bool UsuarioLogueado = false;
		public static string UsuarioName = "Señor Gestor";
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

}
