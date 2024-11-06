using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Data;

namespace ClinicaMedica {
	public partial class Turnos : Window {
		private Turno? SelectedTurno;
		
		public Turnos() {
            InitializeComponent();
		}

		//----------------------eventosRefresh-------------------//
		private void CalendarioTurnos_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
			using (var MiConexion = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				MiConexion.Open();

				string consulta = @"
					SELECT 
						T.Id,
						CONCAT(P.Dni, ' ', P.Name, ' ', P.LastName) AS PacienteConcat,
						CONCAT(M.Dni, ' ', M.Name, ' ', M.LastName) AS MedicoConcat,
						FORMAT(T.Fecha, 'yyyy-MM-dd') AS Fecha,
						T.Hora
					FROM 
						Turno T
					JOIN 
						Paciente P ON T.PacienteId = P.Id
					JOIN 
						Medico M ON T.MedicoId = M.Id
					WHERE
						T.Fecha = @Fecha;
				";
				// string consulta = @"
					// SELECT * FROM Turno;
				// ";

				using (var command = new SqlCommand(consulta, MiConexion)) {
					command.Parameters.AddWithValue("@Fecha", CalendarioTurnos.SelectedDate?.Date);

					using (var adaptador = new SqlDataAdapter(command)) {
						DataTable tablita = new DataTable();
						adaptador.Fill(tablita);

						turnosListView.ItemsSource = tablita.DefaultView;
					}
				}
			}

			turnosListView.SelectedValuePath = "Id";
		}
		//----------------------eventosRefresh-------------------//
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			/* //GALLEGO STYLE
				if (turnosListView.SelectedItem != null) {
					buttonModificar.IsEnabled = true;
					MessageBox.Show($"Selected Turno Id: {turnosListView.SelectedValue}");
				}
				else {
					buttonModificar.IsEnabled = false;
				}
			*/
			if (turnosListView.SelectedValue != null) {
				SelectedTurno = (Turno) turnosListView.SelectedItem;
				buttonModificar.IsEnabled = true;
				// MessageBox.Show($"Selected Turno Id: {SelectedMedico.Id}");
			}
			else {
				buttonModificar.IsEnabled = false;
			}
			
			

			using (var MiConexion = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				MiConexion.Open();

				string consulta = @"
					SELECT 
						Dni, Name, LastName, Especialidad
					FROM 
						Medico
					WHERE
						Id = @MedicoId
				";
				using (var command = new SqlCommand(consulta, MiConexion)) {
					command.Parameters.AddWithValue("@MedicoId", SelectedTurno.MedicoId);

					using (var adaptador = new SqlDataAdapter(command)) {
						DataTable tablita = new DataTable();
						adaptador.Fill(tablita);

						if (tablita.Rows.Count > 0) {
							DataRow fila = tablita.Rows[0];

							txtMedicoDni.Text = fila["Dni"].ToString();
							txtMedicoNombre.Text = fila["Name"].ToString();
							txtMedicoApellido.Text = fila["LastName"].ToString();
							txtMedicoEspecialidad.Text = fila["Especialidad"].ToString();
						}
						else {
							// Limpiar los TextBox si no se encuentra el médico
							txtMedicoDni.Text = "";
							txtMedicoNombre.Text = "";
							txtMedicoApellido.Text = "";
							txtMedicoEspecialidad.Text = "";
						}
					}
				}
			}
			// medicosListView.SelectedValuePath = "Id";
			
			
			
			
			

			
			using (var MiConexion = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				MiConexion.Open();

				string consulta = @"
					SELECT 
						Dni, Name, LastName, Email, Telefono
					FROM 
						Paciente
					WHERE
						Id = @PacienteId
				";
				using (var command = new SqlCommand(consulta, MiConexion)) {
					command.Parameters.AddWithValue("@PacienteId", SelectedTurno.PacienteId);

					using (var adaptador = new SqlDataAdapter(command)) {
						DataTable tablita = new DataTable();
						adaptador.Fill(tablita);

						if (tablita.Rows.Count > 0) {
							DataRow fila = tablita.Rows[0];

							txtPacienteDni.Text = fila["Dni"].ToString();
							txtPacienteNombre.Text = fila["Name"].ToString();
							txtPacienteApellido.Text = fila["LastName"].ToString();
							txtPacienteEmail.Text = fila["Email"].ToString();
							txtPacienteTelefono.Text = fila["Telefono"].ToString();
						}
						else {
							// Limpiar los Labels si no se encuentra el paciente
							txtPacienteDni.Text = "";
							txtPacienteNombre.Text = "";
							txtPacienteApellido.Text = "";
							txtPacienteEmail.Text = "";
							txtPacienteTelefono.Text = "";
						}
					}
				}
			}

			// pacientesListView.SelectedValuePath = "Id";
			
			
			
			
		}
		//----------------------eventosRefresh-------------------//

		private void medicosListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}

		private void pacientesListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}
		private void Window_Activated(object sender, EventArgs e) {
			/* //GALLEGO STYLE
			string consulta = @"
                SELECT 
					T.Id,
                    CONCAT(P.Name, ' ', P.LastName) AS Paciente,
                    CONCAT(M.Name, ' ', M.LastName) AS Medico,
                    FORMAT(T.Fecha, 'yyyy-MM-dd') AS Fecha,
					T.Hora
                FROM 
                    Turno T
                JOIN 
                    Paciente P ON T.PacienteId = P.Id
                JOIN 
                    Medico M ON T.MedicoId = M.Id;
            ";
			SqlDataAdapter adaptador = new SqlDataAdapter(consulta, BaseDeDatosSQL.connectionString);
			using (adaptador) {
				DataTable tablita = new DataTable();
				adaptador.Fill(tablita);

				turnosListView.ItemsSource = tablita.DefaultView;
			}
			turnosListView.SelectedValuePath = "Id";
			*/
			turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnos();
		}
		//------------------botonesParaModificarDB------------------//
		private void ButtonModificar(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<TurnosModificar>(SelectedTurno);
		}
		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<TurnosModificar>();
		}
		//---------------------botonesDeVolver-------------------//
        public void ButtonSalir(object sender, RoutedEventArgs e) {
            this.Salir();
        }
        private void ButtonHome(object sender, RoutedEventArgs e) {
			this.VolverAHome();
		}
		//------------------------Fin.Turnos----------------------//
	}
}
