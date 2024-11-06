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

namespace ClinicaMedica
{
    /// <summary>
    /// Lógica de interacción para Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window
    {
        private static Paciente? SelectedPaciente;


        public Pacientes()
        {
            InitializeComponent();
			// generar
			// pacientesListView.ItemsSource = App.BaseDeDatos.ReadPacientes(); // ahora viene desde ventana activated

		}


		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<PacientesModificar>(); // this.NavegarA<PacientesModificar>();

		}

		private void ButtonModificarPaciente(object sender, RoutedEventArgs e) {
			if (SelectedPaciente != null) {
				this.AbrirComoDialogo<PacientesModificar>(SelectedPaciente);
			}

		}

		private void pacientesListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (pacientesListView.SelectedItem != null) {
				SelectedPaciente = (Paciente)pacientesListView.SelectedItem;
				buttonModificarPaciente.IsEnabled = true;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			}
			else {
				buttonModificarPaciente.IsEnabled = false;
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

		private void Window_Activated(object sender, EventArgs e) {

			pacientesListView.ItemsSource = App.BaseDeDatos.ReadPacientes(); // ahora viene desde ventana activated
		}
    }
}
