using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Media;

namespace ClinicaMedica {
	public partial class App : Application {
		public static BaseDeDatosAbstracta BaseDeDatos;
		public static bool UsuarioLogueado = false;
		public static string UsuarioName = "Señor Gestor";
		// public static SoundPlayer UClick = new SoundPlayer("sonidos\\uclick.wav");
        // public static SoundPlayer UClickNoFun = new SoundPlayer("sonidos\\uclicknofun.wav");
		// public static SoundPlayer UClickJewel = new SoundPlayer("sonidos\\uclick_jewel.wav");
		public static MediaPlayer UClick = new MediaPlayer();
		public static MediaPlayer UClickNoFun = new MediaPlayer();
		public static MediaPlayer UClickJewel = new MediaPlayer();

		public void Button_MouseEnter(object sender, System.Windows.RoutedEventArgs e){
			// UClickJewel.Open(new Uri("sonidos\\uclick_jewel.wav", UriKind.Relative));
			// UClickJewel.Play();
		}

		public void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
			UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
			// Application.Current.Dispatcher.Invoke(() => {
				UClickNoFun.Play();
			// });
		}
	}
	
	
    public static class WindowExtensions{
		public static void NavegarA<T>(this Window previousWindow) where T : Window, new(){
			App.UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				App.UClickNoFun.Play();
			
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		public static void NavegarA<T>(this Window previousWindow, object optionalArg) where T : Window, new(){
			App.UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				App.UClickNoFun.Play();
				
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		
		public static void AbrirComoDialogo<T>(this Window previousWindow) where T : Window, new(){
			App.UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				App.UClickNoFun.Play();
				
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}

		public static void AbrirComoDialogo<T>(this Window previousWindow, object optionalArg) where T : Window{
			App.UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				App.UClickNoFun.Play();
				
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}
		
		public static void VolverAHome(this Window previousWindow){
			App.UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				App.UClickNoFun.Play();
				
			previousWindow.NavegarA<MainWindow>();
		}
		public static void Salir(this Window previousWindow){
			App.UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				App.UClickNoFun.Play();
			Application.Current.Shutdown();  // Apagar la aplicación
		}
		public static void Cerrar(this Window previousWindow){
			App.UClickNoFun.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				App.UClickNoFun.Play();
			previousWindow.Close();
		}
	}

}
