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
            PantallaPrincipal pantallaPrincipalWindow = new PantallaPrincipal();
            Application.Current.MainWindow = pantallaPrincipalWindow;
            pantallaPrincipalWindow.Show();

            //cierro la anterior.
            this.Close();
        }
    }
}
