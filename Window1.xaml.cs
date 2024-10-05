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
	public partial class Inicio : Window {
		public Inicio() {
			InitializeComponent();
		}

		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            médicos médicos = new médicos();
            Application.Current.MainWindow = médicos;
            médicos.Show();

            //cierro la anterior.
            this.Close();
        }
    }
}
