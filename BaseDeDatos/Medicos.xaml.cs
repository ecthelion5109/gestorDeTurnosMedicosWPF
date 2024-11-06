﻿using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Medicos : Window {
		private static Medico? SelectedMedico;


		public Medicos() {
			InitializeComponent();
			// this.DataContext = this;
			// turnosListView.ItemsData = SelectedMedico;
		}


	
		//----------------------eventosRefresh-------------------//
		private void medicosListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			/* //GALLEGO STYLE
				if (medicosListView.SelectedItem != null) {
					buttonModificar.IsEnabled = true;
					MessageBox.Show($"Selected Medico DNI: {medicosListView.SelectedValue}");
				}
				else {
					buttonModificar.IsEnabled = false;
				}
			*/
			if (medicosListView.SelectedItem != null) {
				SelectedMedico = (Medico) medicosListView.SelectedItem;
				buttonModificar.IsEnabled = true;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			}
			else {
				buttonModificar.IsEnabled = false;
			}
			
			
			
			// turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnos();
			turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnosWhereMedicoId(SelectedMedico.Id);
			
			
			
			
			
			
			
			
		}
		//----------------------eventosRefresh-------------------//
		private void Window_Activated(object sender, EventArgs e) {	
			/* //GALLEGO STYLE
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
			*/
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

		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Turno selectedTurno = (Turno) turnosListView.SelectedItem;

			if (selectedTurno is null) {
				txtPacienteDni.Text = "";
				txtPacienteNombre.Text = "";
				txtPacienteApellido.Text = "";
				txtPacienteEmail.Text = "";
				txtPacienteTelefono.Text = "";
			}
			else {
				txtPacienteDni.Text = BaseDeDatosSQL.DictPacientes[selectedTurno.PacienteId].Dni;
				txtPacienteNombre.Text = BaseDeDatosSQL.DictPacientes[selectedTurno.PacienteId].Name;
				txtPacienteApellido.Text = BaseDeDatosSQL.DictPacientes[selectedTurno.PacienteId].LastName;
				txtPacienteEmail.Text = BaseDeDatosSQL.DictPacientes[selectedTurno.PacienteId].Email;
				txtPacienteTelefono.Text = BaseDeDatosSQL.DictPacientes[selectedTurno.PacienteId].Telefono;
			}
		}
		//------------------------Fin.Medicos----------------------//
	}
}
