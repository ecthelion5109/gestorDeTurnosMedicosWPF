using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	/// <summary>
	/// Lógica de interacción para Medicos.xaml
	/// </summary>
	public partial class Medicos : Window {
		private static Medico? SelectedMedico;


		public Medicos() {
			InitializeComponent();
			// generar


			if (BaseDeDatos.TIPO == DatabaseType.JSON) //MODO JSON
			{
				MedicoListView.ItemsSource = BaseDeDatos.JsonLoadMedicoData();
			}
			else //MODO SQL
			{
				MedicoListView.ItemsSource = BaseDeDatos.SQL_ReadMedicos();
			}

		}



		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.NavegarA<MedicosModificar>();

		}

		private void MedicoListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (MedicoListView.SelectedItem != null) {
				SelectedMedico = (Medico)MedicoListView.SelectedItem;
				buttonModificar.IsEnabled = true;
				buttonEliminar.IsEnabled = true;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			}
			else {
				buttonModificar.IsEnabled = false;
				buttonEliminar.IsEnabled = false;
			}
		}

		private void ButtonModificar(object sender, RoutedEventArgs e) {
			//this.NavegarA<MedicosModificar>();
			if (SelectedMedico != null) {
				MedicosModificar nuevaVentana = new(SelectedMedico);
				Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
				nuevaVentana.Show();  // Mostrar la nueva ventana
				this.Close();  // Cerrar la ventana actual
			}
		}

		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			// Muestra el MessageBox con botones de Aceptar y Cancelar
			if (SelectedMedico != null && SelectedMedico.Dni != null) {
				MessageBoxResult result = MessageBox.Show(
					$"¿Está seguro que desea eliminar este médico? {SelectedMedico.Name}",   // Mensaje
					"Confirmar Eliminación",                         // Título del cuadro
					MessageBoxButton.OKCancel,                       // Botones (OK y Cancelar)
					MessageBoxImage.Warning                          // Tipo de icono (opcional)
				);

				if (result == MessageBoxResult.OK) {
					if (BaseDeDatos.TIPO == DatabaseType.JSON) //MODO JSON
					{
						BaseDeDatos.Database["medicos"].Remove(SelectedMedico.Dni);
						MedicoListView.ItemsSource = BaseDeDatos.JsonLoadMedicoData();	//refresh shit
						BaseDeDatos.UpdateJsonFile(); // Save changes to the database
					}
					else  //MODO SQL
					{
						BaseDeDatos.SQL_DeleteMedico(SelectedMedico.Dni);
						MedicoListView.ItemsSource = BaseDeDatos.SQL_ReadMedicos();
					}
				}
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
