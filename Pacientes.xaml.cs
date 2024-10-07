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
    /// Lógica de interacción para Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window {
        public Pacientes() {
            InitializeComponent();
        }

        private void ButtonPacientesVer(object sender, RoutedEventArgs e)
        {
			this.NavegarA<PacientesVer>();
        }


		private void ButtonVolver(object sender, RoutedEventArgs e) {
			this.NavegarA<PantallaPrincipal>();
		}

		private void ButtonPacientesAgregar(object sender, RoutedEventArgs e) {
			this.NavegarA<PacientesAgregar>();
		}

        private void ButtonPacientesModificar(object sender, RoutedEventArgs e)
        {
			this.NavegarA<PacientesModificar>();
        }

        private void ButtonPacientesEliminar(object sender, RoutedEventArgs e)
        {
			this.NavegarA<PacientesEliminar>();
        }

		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.NavegarA<PantallaPrincipal>();

		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();

		}
	}
}
