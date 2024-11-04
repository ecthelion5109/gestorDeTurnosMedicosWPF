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
		private string? SelectedTurnoId;
		
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
						CONCAT(P.Name, ' ', P.LastName) AS Paciente,
						CONCAT(M.Name, ' ', M.LastName) AS Medico,
						FORMAT(T.Fecha, 'yyyy-MM-dd') AS Fecha,
						T.Hora
					FROM 
						Turno T
					JOIN 
						Paciente P ON T.PacienteID = P.Id
					JOIN 
						Medico M ON T.MedicoID = M.Id
					WHERE
						T.Fecha = @Fecha;
				";

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
		private void Window_Activated(object sender, EventArgs e) {
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
                    Paciente P ON T.PacienteID = P.Id
                JOIN 
                    Medico M ON T.MedicoID = M.Id;
            ";
			SqlDataAdapter adaptador = new SqlDataAdapter(consulta, BaseDeDatosSQL.connectionString);
			using (adaptador) {
				DataTable tablita = new DataTable();
				adaptador.Fill(tablita);

				turnosListView.ItemsSource = tablita.DefaultView;
			}
			turnosListView.SelectedValuePath = "Id";
		}
		//----------------------eventosRefresh-------------------//
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (turnosListView.SelectedValue != null) {
				SelectedTurnoId = turnosListView.SelectedValue.ToString();
				buttonModificar.IsEnabled = true;
			}
			else {
				SelectedTurnoId = null;
				buttonModificar.IsEnabled = false;
			}
		}
		//------------------botonesParaModificarDB------------------//
		private void ButtonModificar(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<TurnosModificar>(SelectedTurnoId);
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
	}
}
