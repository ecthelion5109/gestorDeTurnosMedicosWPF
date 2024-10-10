using System;
using System.Collections.Generic;
using System.Linq;
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
			 MedicosList = BaseDeDatos.Database["medicos"]
						  .Values
						  .Cast<Medico>()   // Casting the object values to Medico
						  .ToList();

			// Establecer el DataContext
			DataContext = this;
		}

		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.NavegarA<MedicosModificar>();

		}

		private void MedicoListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (MedicoListView.SelectedItem != null) {
				SelectedMedico = (Medico) MedicoListView.SelectedItem;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			}
		}

		private void ButtonModificar(object sender, RoutedEventArgs e) {
			//this.NavegarA<MedicosModificar>();

			MedicosModificar nuevaVentana = new MedicosModificar(SelectedMedico.Dni);
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

			// Verifica qué botón fue presionado
			if (result == MessageBoxResult.OK) {
				// Eliminar el médico o realizar la acción deseada
				MessageBox.Show("El médico ha sido eliminado.");
			}
			//else {
				// Cancelar la acción
				//MessageBox.Show("Operación cancelada.");
			//}
		}

		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.NavegarA<MainWindow>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
	}
}
