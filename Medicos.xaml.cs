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
	/// Lógica de interacción para Medicos.xaml
	/// </summary>
	public partial class Medicos : Window {
		public Medicos() {
			InitializeComponent();
		}

        private void ButtonMedicosModificar(object sender, RoutedEventArgs e)
        {
			this.NavegarA<MedicosModificar>();
        }

        private void ButtonMedicosAgregar(object sender, RoutedEventArgs e)
        {
			this.NavegarA<MedicosAgregar>();
        }

        private void ButtonMedicosEliminar(object sender, RoutedEventArgs e)
        {
			this.NavegarA<MedicosEliminar>();
        }

        private void ButtonMedicosVer(object sender, RoutedEventArgs e) {
			this.NavegarA<MedicosVer>();
		}
		
		public void ButtonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.NavegarA<PantallaPrincipal>();

		}
	}
}
