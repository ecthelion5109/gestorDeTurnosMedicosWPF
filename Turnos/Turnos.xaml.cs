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
	/// Lógica de interacción para Turnos.xaml
	/// </summary>
	public partial class Turnos : Window {
		public Turnos() {
            InitializeComponent();
        }

        private void ButtonTurnosModificar(object sender, RoutedEventArgs e)
        {
            this.NavegarA<TurnosModificar>();
        }

		private void ButtonTurnosVer(object sender, RoutedEventArgs e)
        {
            this.NavegarA<TurnosVer>();
        }

        public void ButtonSalir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonHome(object sender, RoutedEventArgs e)
        {
            this.NavegarA<MainWindow>();

        }

		private void ButtonTurnosAgregar(object sender, RoutedEventArgs e) {

		}
	}
}
