using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Medicos : Window {
		private static Medico? SelectedMedico;


		public Medicos() {
			InitializeComponent();

		}


		
/*
		// Modo Gallego
		private void LLenarMedicosGallegoStyle() {
			var MiConexion = new SqlConnection(BaseDeDatosSQL.connectionString);
			MiConexion.Open();
			string query = "SELECT * FROM Medico";
			SqlCommand command = new SqlCommand(query, MiConexion);
			SqlDataAdapter adapter = new SqlDataAdapter(command);
			DataTable dt = new DataTable();
			using (adapter) {
				adapter.Fill(dt);
			}
			medicosListView.ItemsSource = dt.DefaultView;
			medicosListView.SelectedValuePath = "Id";
		}
		private void medicosListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (medicosListView.SelectedItem != null) {
				buttonModificar.IsEnabled = true;
				MessageBox.Show($"Selected Medico DNI: {medicosListView.SelectedValue}");
			}
			else {
				buttonModificar.IsEnabled = false;
			}
		}
*/
		//----------------------eventosRefresh-------------------//
		private void medicosListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (medicosListView.SelectedItem != null) {
				SelectedMedico = (Medico) medicosListView.SelectedItem;
				buttonModificar.IsEnabled = true;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			}
			else {
				buttonModificar.IsEnabled = false;
			}
		}
		//----------------------eventosRefresh-------------------//
		private void Window_Activated(object sender, EventArgs e) {
			// LLenarMedicosGallegoStyle();
			medicosListView.ItemsSource = App.BaseDeDatos.ReadMedicos(); // ahora viene desde ventana activated
		}
		//------------------botonesParaModificarDB------------------//
		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<MedicosModificar>(); 
		}
		private void ButtonModificar(object sender, RoutedEventArgs e) {
			if (SelectedMedico != null) {
				this.AbrirComoDialogo<MedicosModificar>(SelectedMedico);
			}
		}
		//---------------------botonesDeVolver-------------------//
		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.VolverAHome();
		}
		//------------------------Fin.Medicos----------------------//
	}
}
