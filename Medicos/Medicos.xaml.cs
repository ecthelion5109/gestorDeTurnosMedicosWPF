using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicaMedica {
	/// <summary>
	/// Lógica de interacción para Medicos.xaml
	/// </summary>
	public partial class Medicos : Window {
		public static Medico SelectedMedico;
		public List<Medico> MedicosList { get; set; }


		public Medicos() {
			InitializeComponent();
			// Medicos = new List<Medico>{
			// new Medico { Dni = "87654321", Name = "Dr. Roxana", Lastname = "Gómez", Specialidad = "Cardiología" },
			// new Medico { Dni = "25654321", Name = "Dr. Carlos", Lastname = "Merkier", Specialidad = "Gastroenterología" }
			// };
			MedicosList = BaseDeDatos.Database["medicos"].Values.Cast<Medico>().ToList();

			// Establecer el DataContext
			DataContext = this;
		}

		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.NavegarA<MedicosModificar>();

		}

		private void MedicoListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (MedicoListView.SelectedItem != null) {
				SelectedMedico = (Medico) MedicoListView.SelectedItem;
				buttonModificar.IsEnabled = true;
				buttonEliminar.IsEnabled = true;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			} else {
				buttonModificar.IsEnabled = false;
				buttonEliminar.IsEnabled = false;
			}
		}

		private void ButtonModificar(object sender, RoutedEventArgs e) {
			//this.NavegarA<MedicosModificar>();

			MedicosModificar nuevaVentana = new MedicosModificar(SelectedMedico);
			Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
			nuevaVentana.Show();  // Mostrar la nueva ventana
			this.Close();  // Cerrar la ventana actual
		}

		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			// Muestra el MessageBox con botones de Aceptar y Cancelar
			MessageBoxResult result = MessageBox.Show(
				$"¿Está seguro que desea eliminar este médico? {SelectedMedico.Name}",   // Mensaje
				"Confirmar Eliminación",                         // Título del cuadro
				MessageBoxButton.OKCancel,                       // Botones (OK y Cancelar)
				MessageBoxImage.Warning                          // Tipo de icono (opcional)
			);

			if (result == MessageBoxResult.OK) {
				BaseDeDatos.Database["medicos"].Remove(SelectedMedico.Dni);
				//regenerate the list
				MedicosList = BaseDeDatos.Database["medicos"].Values.Cast<Medico>().ToList();
				
				// refrescar
				MedicoListView.ItemsSource = null; // Clear the existing ItemsSource
				MedicoListView.ItemsSource = MedicosList; // Reassign to refresh the ListView
				// MessageBox.Show("El médico ha sido eliminado.");
				
				// Save changes to the database
				BaseDeDatos.UpdateJsonFile();
			}
		}

		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.NavegarA<MainWindow>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
	}
}
