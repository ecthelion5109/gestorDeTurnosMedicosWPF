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
            MedicosModificar medicoModificarWindow = new MedicosModificar();
            Application.Current.MainWindow = medicoModificarWindow;
            medicoModificarWindow.Show();
            this.Close();
        }

        private void ButtonMedicosAgregar(object sender, RoutedEventArgs e)
        {
            MedicosAgregar medicoAgregarWindow = new MedicosAgregar();
            Application.Current.MainWindow = medicoAgregarWindow;
            medicoAgregarWindow.Show();
            this.Close();
        }

        private void ButtonMedicosEliminar(object sender, RoutedEventArgs e)
        {
            MedicosEliminar medicoEliminarWindow = new MedicosEliminar();
            Application.Current.MainWindow = medicoEliminarWindow;
            medicoEliminarWindow.Show();
            this.Close();
        }

        private void ButtonMedicosVer(object sender, RoutedEventArgs e)
        {
            MedicosVer medicoVerWindow = new MedicosVer();
            Application.Current.MainWindow = medicoVerWindow;
            medicoVerWindow.Show();
		}

        private void ButtonVolver(object sender, RoutedEventArgs e)
        {
            PantallaPrincipal pantallaPrincipalWindow = new PantallaPrincipal();
            Application.Current.MainWindow = pantallaPrincipalWindow;
            pantallaPrincipalWindow.Show();
            this.Close();
        }
		
		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}
		
    }
}
