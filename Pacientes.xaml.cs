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
            PacientesVer pacientesVerWindow = new PacientesVer();
            Application.Current.MainWindow = pacientesVerWindow;
            pacientesVerWindow.Show();

            //cierro la anterior.
            this.Close();
        }


		private void ButtonVolver(object sender, RoutedEventArgs e) {

			PantallaPrincipal pantallaPrincipalWindow = new PantallaPrincipal();
			Application.Current.MainWindow = pantallaPrincipalWindow;
			pantallaPrincipalWindow.Show();

			//cierro la anterior.
			this.Close();
		}

		private void ButtonPacientesAgregar(object sender, RoutedEventArgs e) {

			PacientesAgregar pacientesAgregarWindow = new PacientesAgregar();
			Application.Current.MainWindow = pacientesAgregarWindow;
			pacientesAgregarWindow.Show();

			//cierro la anterior.
			this.Close();
		}
<<<<<<< HEAD

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PacientesModificar pacientesModificarWindow = new PacientesModificar();
            Application.Current.MainWindow = pacientesModificarWindow;
            pacientesModificarWindow.Show();

            //cierro la anterior.
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PacientesEliminar pacientesEliminarWindow = new PacientesEliminar();
            Application.Current.MainWindow = pacientesEliminarWindow;
            pacientesEliminarWindow.Show();

            //cierro la anterior.
            this.Close();
        }
    }
=======
	}
<<<<<<< HEAD
>>>>>>> 7f8d09c42c7564281ba5b12b29cf8c62295ea13a
>>>>>>> da5440a253a2c8906aa62e2583e527103083752e
=======
>>>>>>> 8d28eeb35fbb15d120b2680642287623c68c5f32
}
