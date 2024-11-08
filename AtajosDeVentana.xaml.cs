using System.Windows;

namespace ClinicaMedica{
    public static class AtajosDeVentana{
		public static void NavegarA<T>(this Window previousWindow) where T : Window, new(){
			App.PlayClickJewel();
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		public static void NavegarA<T>(this Window previousWindow, object optionalArg) where T : Window, new(){
			App.PlayClickJewel();
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			previousWindow.Close();  // Cerrar la ventana actual
		}
		
		public static void AbrirComoDialogo<T>(this Window previousWindow) where T : Window, new(){
			App.PlayClickJewel();
			T nuevaVentana = new();
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}

		public static void AbrirComoDialogo<T>(this Window previousWindow, object optionalArg) where T : Window{
			App.PlayClickJewel();
			T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
			Application.Current.MainWindow = nuevaVentana;
			nuevaVentana.ShowDialog();
		}
		
		public static void VolverAHome(this Window previousWindow){
			App.PlayClickJewel();
			previousWindow.NavegarA<MainWindow>();
		}
		public static void Salir(this Window previousWindow){
			App.PlayClickJewel();
			if (MessageBox.Show($"¿Está seguro que desea salir de la aplicacion?",
				"Confirmar ciere",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Question
			) != MessageBoxResult.OK) {
				return;
			}
			App.PlayClickJewel();
			//---------confirmacion-----------//
			
			Application.Current.Shutdown();  // Apagar la aplicación
		}
		public static void Cerrar(this Window previousWindow){
			App.PlayClickJewel();
			previousWindow.Close();
		}
	}
}