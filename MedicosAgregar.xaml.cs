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

namespace ClinicaMedica
{
    /// <summary>
    /// Lógica de interacción para MedicosAgregar.xaml
    /// </summary>
    public partial class MedicosAgregar : Window
    {
        public MedicosAgregar()
        {
            InitializeComponent();
        }
		
		public void MetodoBotonVolverAMedicos(object sender, RoutedEventArgs e) {
            Medicos medicosWindow = new Medicos();
            Application.Current.MainWindow = medicosWindow;
            medicosWindow.Show();
            this.Close();
		}
		
		public void MetodoBotonPaginaPrincipal(object sender, RoutedEventArgs e) {
            PantallaPrincipal pantallaPrincipalWindow = new PantallaPrincipal();
            Application.Current.MainWindow = pantallaPrincipalWindow;
            pantallaPrincipalWindow.Show();
            this.Close();
		}
		
		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

		private void MetodoBotonTestearJsonGuardar(object sender, RoutedEventArgs e) {

		}
	}
}
