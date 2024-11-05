using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    public partial class TurnosModificar : Window {
		private static Turno ?SelectedTurno;
		//---------------------public.constructors-------------------//
        public TurnosModificar() //Crear turno
		{
            InitializeComponent();
			SelectedTurno = null;
			LLenarComboBoxes();
		}


		public TurnosModificar(Turno selectedTurno)  //Modificar turno
		{
			InitializeComponent();
			SelectedTurno = selectedTurno;
			LLenarComboBoxes();

			this.txtMedicos.DataContext = selectedTurno;
			// this.txtPacientes.SelectedItem = selectedTurno.PacienteConcat;
			this.txtPacientes.DataContext = selectedTurno;
			// this.txtEspecialidades.SelectedItem = selectedTurno.Especialidad;
			this.txtEspecialidades.DataContext = selectedTurno;

			this.txtId.Content = selectedTurno.Id;
			this.txtFecha.SelectedDate = selectedTurno.Fecha;
			this.txtHora.Text = selectedTurno.Hora;

		}

		private void LLenarComboBoxes() {
			using (var MiConexion = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				MiConexion.Open();

				// Query to fill txtEspecialidades ComboBox
				string consultaEspecialidades = @"SELECT DISTINCT Especialidad FROM Medico";
				using (var command = new SqlCommand(consultaEspecialidades, MiConexion)) {
					using (var reader = command.ExecuteReader()) {
						txtEspecialidades.Items.Clear();
						while (reader.Read()) {
							txtEspecialidades.Items.Add(reader["Especialidad"].ToString());
						}
					}
				}

				// Query to fill txtPacientes ComboBox
				string consultaPacientes = @"SELECT Id as PacienteId, CONCAT(Dni, ' ', Name, ' ', LastName) AS PacienteDisplay FROM Paciente";
				using (var command = new SqlCommand(consultaPacientes, MiConexion)) {
					using (var reader = command.ExecuteReader()) {
						txtPacientes.Items.Clear();
						while (reader.Read()) {
							txtPacientes.Items.Add(new { 
								PacienteId = reader["PacienteId"], 
								PacienteDisplay = reader["PacienteDisplay"]
							});
						}
					}
				}
				txtPacientes.DisplayMemberPath = "PacienteDisplay";
				txtPacientes.SelectedValuePath = "PacienteId";
				
				
				
				
				
				

				// Query to fill txtMedicos ComboBox
				string consultaMedicos = @"SELECT Id as MedicoId, CONCAT(Dni, ' ', Name, ' ', LastName) AS MedicoDisplay FROM Medico";
				using (var command = new SqlCommand(consultaMedicos, MiConexion)) {
					using (var reader = command.ExecuteReader()) {
						txtMedicos.Items.Clear();
						while (reader.Read()) {
							txtMedicos.Items.Add(new { 
								MedicoId = reader["MedicoId"], 
								MedicoDisplay = reader["MedicoDisplay"]
							});
						}
					}
				}
				txtMedicos.DisplayMemberPath = "MedicoDisplay";
				txtMedicos.SelectedValuePath = "MedicoId";
				
				
			}
		}

		public bool FaltanCamposPorCompletar(){
			return (
				//string.IsNullOrEmpty(this.txtId.Content.ToString()) ||

				this.txtPacientes.SelectedValue is null ||
				this.txtMedicos.SelectedValue is null ||

				this.txtFecha.SelectedDate is null ||
				string.IsNullOrEmpty(this.txtHora.Text)
			);
		}

		private void ButtonGuardar(object sender, RoutedEventArgs e) {









			// ---------AsegurarInput-----------//
			if (FaltanCamposPorCompletar()) {
				MessageBox.Show($"Error: Faltan datos obligatorios por completar.");
				return;
			}



			// ---------AsegurarInput-----------//
			if (true) {
				// MessageBox.Show($"txtEspecialidades.SelectedItem: {this.txtEspecialidades.SelectedItem} \ntxtHora.Text:{this.txtHora.Text}\ntxtFecha.SelectedDate:{this.txtFecha.SelectedDate}\ntxtMedicos.SelectedValue:{(this.txtMedicos.SelectedValue)}\ntxtPacientes.SelectedValue:{this.txtPacientes.SelectedValue}");
				MessageBox.Show($"txtMedicos.SelectedValue:{(this.txtMedicos.SelectedValue)}\ntxtPacientes.SelectedValue:{this.txtPacientes.SelectedValue}\ntxtHora.Text:{this.txtHora.Text}\ntxtFecha.SelectedDate:{this.txtFecha.SelectedDate}");
				//return;
			}






			//---------Crear-----------//
			if (SelectedTurno is null) {
				var newturno = new Turno();
				newturno.AsignarDatosFromWindow(this);
				if ( App.BaseDeDatos.CreateTurno(newturno) ) {
					this.Close();
				}
			}
			//---------Modificar-----------//
			else {
				SelectedTurno.AsignarDatosFromWindow(this);
				if ( App.BaseDeDatos.UpdateTurno(SelectedTurno) ) {
					this.Close();
				}
			}
		}

		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			//---------Checknulls-----------//
			if (txtId.Content is null) {
				MessageBox.Show($"No hay item seleccionado.");
				return;
			}
			//---------confirmacion-----------//
			if (MessageBox.Show($"¿Está seguro que desea eliminar este turno?",
				"Confirmar Eliminación",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			) != MessageBoxResult.OK) {
				return;
			}
			//---------Eliminar-----------//
			if (App.BaseDeDatos.DeleteTurno(txtId.Content.ToString())){
				this.Close();
			}
		}
		//---------------------botones.Salida-------------------//
		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.Close(); // this.NavegarA<Turnos>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}

		private void txtEspecialidades_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		}

		private void txtPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		}
		//------------------------Fin---------------------------//
	}
}
