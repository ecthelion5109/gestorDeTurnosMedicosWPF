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
			PacientesListView.ItemsSource = MainWindow.BaseDeDatos.ReadPacientes();

		}


		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.NavegarA<PacientesModificar>();

		}

		private void ButtonModificar(object sender, RoutedEventArgs e) {
			this.NavegarA<PacientesModificar>();

		}

		private void PacienteListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (PacientesListView.SelectedItem != null) {
				SelectedPaciente = (Paciente)PacientesListView.SelectedItem;
				buttonModificar.IsEnabled = true;
				//MessageBox.Show($"Selected Medico DNI: {SelectedMedico.Dni}");
			}
			else {
				buttonModificar.IsEnabled = false;
			}

		}

		//---------------------botones.Salir-------------------//
		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}


		//---------------------botones.VolverAHome-------------------//
		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.NavegarA<MainWindow>();
		}

	}
}
