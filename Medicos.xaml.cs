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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MedicosModificar medicoModificarWindow = new MedicosModificar();
            Application.Current.MainWindow = medicoModificarWindow;
            medicoModificarWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MedicosAgregar medicoAgregarWindow = new MedicosAgregar();
            Application.Current.MainWindow = medicoAgregarWindow;
            medicoAgregarWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            MedicosEliminar medicoEliminarWindow = new MedicosEliminar();
            Application.Current.MainWindow = medicoEliminarWindow;
            medicoEliminarWindow.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MedicosVermas medicoVermasWindow = new MedicosVermas();
            Application.Current.MainWindow = medicoVermasWindow;
            medicoVermasWindow.Show();
=======
            PantallaPrincipal pantallaPrincipalWindow = new PantallaPrincipal();
            Application.Current.MainWindow = pantallaPrincipalWindow;
            pantallaPrincipalWindow.Show();

            //cierro la anterior.
>>>>>>> da5440a253a2c8906aa62e2583e527103083752e
            this.Close();
        }
    }
}
