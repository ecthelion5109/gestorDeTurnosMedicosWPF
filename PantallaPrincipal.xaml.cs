using System;
using System.Collections.Generic;
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
	/// <summary>
	/// Lógica de interacción para Window1.xaml
	/// </summary>
	public partial class PantallaPrincipal : Window {
		public PantallaPrincipal() {
			InitializeComponent();
		}

		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

        private void MetodoBotonMedicos(object sender, RoutedEventArgs e)
        {
            Medicos medicoWindow = new Medicos();
            Application.Current.MainWindow = medicoWindow;
			medicoWindow.Show();

            //cierro la anterior.
            this.Close();
		}

		private void MetodoBotonTurnos(object sender, RoutedEventArgs e) {
			Turnos turnoWindow = new Turnos();
			Application.Current.MainWindow = turnoWindow;
			turnoWindow.Show();

			//cierro la anterior.
			this.Close();
		}

		private void MetodoBotonPacientes(object sender, RoutedEventArgs e) {
			Pacientes pacienteWindow = new Pacientes();
			Application.Current.MainWindow = pacienteWindow;
			pacienteWindow.Show();

			//cierro la anterior.
			this.Close();
		}
	}
}
