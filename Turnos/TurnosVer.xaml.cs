using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace ClinicaMedica
{
    public partial class TurnosVer : Window
    {
        public TurnosVer()
        {
            InitializeComponent();
            // Cargar los turnos al iniciar la ventana

            llenarturnos();







			//CargarTurnos(DateTime.Today);
		}


        private void llenarturnos() {

            

			string consulta = @"
SELECT 
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

				datagridcitoFatal.ItemsSource = tablita.DefaultView;
			}



		}






		// Evento que se dispara al cambiar la fecha en el calendario
		private void CalendarioTurnos_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? fechaSeleccionada = CalendarioTurnos.SelectedDate;

            if (fechaSeleccionada.HasValue)
            {
                // Llama a un método para cargar turnos de la fecha seleccionada
                CargarTurnos(fechaSeleccionada.Value);
            }
        }

        // Método para cargar los turnos del día
        private void CargarTurnos(DateTime fecha)
        {
        }
    }
}