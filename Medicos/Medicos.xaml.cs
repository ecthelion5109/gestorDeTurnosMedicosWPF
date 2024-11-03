using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Medicos : Window {
		string SelectMedicoId;
		private static Medico? SelectedMedico;


		public Medicos() {
			InitializeComponent();

		}


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
			//medicosListView.DisplayMemberPath = "Name";
			medicosListView.SelectedValuePath = "Id";
		}






		private void Window_Activated(object sender, EventArgs e) {
			LLenarMedicosGallegoStyle();
		}
		private void MedicoListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (medicosListView.SelectedItem != null) {
				buttonModificar.IsEnabled = true;
				MessageBox.Show($"Selected Medico DNI: {medicosListView.SelectedValue}");




				//SelectMedicoId =  ;
				//SelectedMedico = (Medico) medicosListView.SelectedItem;

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
			this.VolverAHome();
		}
		
		
		//------------------------Fin.Medicos----------------------//
	}
}
