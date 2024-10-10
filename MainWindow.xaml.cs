using System.Windows;


namespace ClinicaMedica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public static class WindowExtensions{
		public static void NavegarA<T>(this Window previousWindow) where T : Window, new()
		{
			T nuevaVentana = new T();
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
		public MainWindow() {
			InitializeComponent();
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

		private void MetodoBotonTurnos(object sender, RoutedEventArgs e) {
			this.NavegarA<Turnos>();
		}

		private void MetodoBotonPacientes(object sender, RoutedEventArgs e) {
			this.NavegarA<Pacientes>();
		}

		public void MetodoBotonTestearJsonLeer(object sender, RoutedEventArgs e) {
			// BaseDeDatos.TestLeer1();
			BaseDeDatos.TestLeer2();
		}
		public void MetodoBotonTestearJsonGuardar(object sender, RoutedEventArgs e) {
			BaseDeDatos.TestGuardar();
		}
	}
}