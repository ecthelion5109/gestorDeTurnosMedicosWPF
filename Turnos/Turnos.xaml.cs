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

		private void LLenarTurnosGallegoStyle() {

			string consulta = @"
                SELECT 
					TurnoID,
                    CONCAT(P.Name, ' ', P.LastName) AS Paciente,
                    CONCAT(M.Name, ' ', M.LastName) AS Medico,
                    T.FechaHora
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
			//turnosListView.DisplayMemberPath = "Name";
			turnosListView.SelectedValuePath = "TurnoID";
		}

		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (turnosListView.SelectedValue != null) {
				SelectedTurnoId = turnosListView.SelectedValue.ToString();
				//MessageBox.Show($"Selected Medico DNI: {turnosListView.SelectedValue}");
				//MessageBox.Show($"Selected Medico2 DNI: {SelectedTurnoId}");
				buttonModificar.IsEnabled = true;
			}
			else {
				SelectedTurnoId = null;
				buttonModificar.IsEnabled = false;
			}
		}

		private void Window_Activated(object sender, EventArgs e) {
			LLenarTurnosGallegoStyle();
		}


		private void CalendarioTurnos_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
			DateTime? fechaSeleccionada = CalendarioTurnos.SelectedDate;

			//if (fechaSeleccionada.HasValue) {
			//	CargarTurnos(fechaSeleccionada.Value);
			//}
		}


		//---------------------botonesParaModificarDB-------------------//
		private void ButtonModificar(object sender, RoutedEventArgs e) {
			// if (SelectedTurnoId != null && turnosListView.SelectedItem != null) {
				this.AbrirComoDialogo<TurnosModificar>(SelectedTurnoId); // this.NavegarA<TurnosModificar>();
			// }
		}
		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<TurnosModificar>(); // this.NavegarA<TurnosModificar>();
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
