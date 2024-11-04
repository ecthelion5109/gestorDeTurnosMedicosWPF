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
				string consultaPacientes = @"SELECT CONCAT(Dni, ' ', Name, ' ', LastName) AS PacienteInfo FROM Paciente";
				using (var command = new SqlCommand(consultaPacientes, MiConexion)) {
					using (var reader = command.ExecuteReader()) {
						txtPacientes.Items.Clear();
						while (reader.Read()) {
							txtPacientes.Items.Add(reader["PacienteInfo"].ToString());
						}
					}
				}

				// Query to fill txtMedicos
				string consultaMedicos = @"SELECT CONCAT(Dni, ' ', Name, ' ', LastName) AS MedicoInfo FROM Medico";
				using (var command = new SqlCommand(consultaMedicos, MiConexion)) {
					using (var reader = command.ExecuteReader()) {
						txtMedicos.Items.Clear();
						while (reader.Read()) {
							txtMedicos.Items.Add(reader["MedicoInfo"].ToString());
						}
					}
				}
			}
		}



		private void LLenarTurnosGallegoStyle(string selectedTurnoId) {
			using (var MiConexion = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				MiConexion.Open();
				string consulta = @"
					SELECT 
						Id,
						PacienteID,
						MedicoID,
						Fecha,
						Hora
					FROM 
						Turno
					WHERE
						Id = @Id
				";

				using (var command = new SqlCommand(consulta, MiConexion)) {
					command.Parameters.AddWithValue("@Id", selectedTurnoId);

					using (var reader = command.ExecuteReader()) {
						if (reader.Read())  // Checks if there's at least one row
						{
							//txtTurnoId.Content = reader["Id"].ToString();
							//txtPacienteDni.Text = reader["PacienteID"].ToString();
							//txtMedicoDni.Text = reader["MedicoID"].ToString();
							txtFecha.Text = reader["Fecha"].ToString();
							txtHora.Text = reader["Hora"].ToString();
						}
						else {
							// Handle the case where no results were found
							MessageBox.Show("No data found for the specified Id.");
						}
					}
				}
			}
		}



		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			OperationCode operacion;
			//---------Crear-----------//
			if (SelectedTurno is null)
				operacion = App.BaseDeDatos.CreateTurno(SelectedTurno);
			//---------Modificar-----------//
			else {
				SelectedTurno.AsignarDatosFromWindow(this);
				operacion = App.BaseDeDatos.UpdateTurno(SelectedTurno);
			}

			//---------Mensaje-----------//
			MessageBox.Show(operacion switch {
				OperationCode.CREATE_SUCCESS => $"Exito: Se ha creado la instancia de Turno: {SelectedTurno.Id}",
				OperationCode.UPDATE_SUCCESS => $"Exito: Se han actualizado los datos de: {SelectedTurno.Id}",
				_ => "Error: Sin definir"
			});
		}

		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			//---------Checknulls-----------//
			if (txtId.Content is null) {
				MessageBox.Show($"No hay item seleccionado.");
				return;
			}
			//---------confirmacion-----------//
			if (MessageBox.Show($"¿Está seguro que desea eliminar este médico? {txtId.Content}",
				"Confirmar Eliminación",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			) != MessageBoxResult.OK) {
				return;
			}
			//---------Eliminar-----------//
			OperationCode operacion = App.BaseDeDatos.DeleteTurno(txtId.Content.ToString());
			//---------Mensaje-----------//
			MessageBox.Show(operacion switch {
				OperationCode.DELETE_SUCCESS => $"Exito: Se ha eliminado a: {txtId.Content} de la base de Datos",
				_ => "Error: Sin definir"
			});
			this.Close(); // this.NavegarA<Pacientes>();

		}
		//---------------------botones.Salida-------------------//
		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.Close(); // this.NavegarA<Turnos>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		//------------------------Fin---------------------------//
	}
}
