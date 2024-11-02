using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Medicos : Window {
		private static Medico? SelectedMedico;


		public Medicos() {
			InitializeComponent();

		}
		private void Window_Activated(object sender, EventArgs e) {

			MedicoListView.ItemsSource = MainWindow.BaseDeDatos.ReadMedicos(); // ahora viene desde ventana activated
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
			this.AbrirComoDialogo<MedicosModificar>(); // this.NavegarA<MedicosModificar>();

		}

		
		//---------------------botones.Modificar-------------------//
		private void ButtonModificar(object sender, RoutedEventArgs e) {
			if (SelectedMedico != null) {
				this.AbrirComoDialogo<MedicosModificar>(SelectedMedico); //this.NavegarA<MedicosModificar>();
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
