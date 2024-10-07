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
			this.NavegarA<Medicos>();
		}
		
		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

		private void MetodoBotonTestearJsonGuardar(object sender, RoutedEventArgs e) {

		}
	}
}
