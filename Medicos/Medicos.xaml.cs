using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Medicos : Window {
		private static Medico? SelectedMedico;


		public Medicos() {
			InitializeComponent();
			
			if (MainWindow.DB_MODO == DatabaseType.JSON) //MODO JSON
			{
				MedicoListView.ItemsSource = BaseDeDatosJSON.ReadMedicos();
			}
			else //MODO SQL
			{
				MedicoListView.ItemsSource = BaseDeDatosSQL.ReadMedicos();
			}

		}

		private void MedicoListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (MedicoListView.SelectedItem != null) {
				SelectedMedico = (Medico) MedicoListView.SelectedItem;
				buttonModificar.IsEnabled = true;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			}
			else {
				buttonModificar.IsEnabled = false;
			}
		}


		
		//---------------------botones.Agregar-------------------//
		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.NavegarA<MedicosModificar>();

		}

		
		//---------------------botones.Modificar-------------------//
		private void ButtonModificar(object sender, RoutedEventArgs e) {
			//this.NavegarA<MedicosModificar>();
			if (SelectedMedico != null) {
				MedicosModificar nuevaVentana = new(SelectedMedico);
				Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
				nuevaVentana.Show();  // Mostrar la nueva ventana
				this.Close();  // Cerrar la ventana actual
			}
		}

		
		//---------------------botones.Salir-------------------//
		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		

		//---------------------botones.VolverAHome-------------------//
		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.NavegarA<MainWindow>();
		}
		
		
		//------------------------Fin.Medicos----------------------//
	}
}
