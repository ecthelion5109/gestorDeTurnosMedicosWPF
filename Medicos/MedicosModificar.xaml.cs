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
    /// Lógica de interacción para MedicosModificar.xaml
    /// </summary>
    public partial class MedicosModificar : Window
    {
        public MedicosModificar()
        {
            InitializeComponent();
        }
		
		public void MetodoBotonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

        private void ButtonAgregar(object sender, RoutedEventArgs e)
        {
            
            Medico medicoLeido = BaseDeDatos.LeerDesdeJson<Medico>("medico.json");
            txtNombre.Text = medicoLeido.Name;
            
                txtApellido.Text = medicoLeido.Lastname;
                txtDNI.Text = medicoLeido.Dni.ToString(); ;
            txtProvincia.Text = medicoLeido.Provincia;
            txtDomicilio.Text = medicoLeido.Domicilio;
            txtLocalidad.Text = medicoLeido.Localidad;
            txtEspecialidad.Text = medicoLeido.Specialidad;
            txtFechaIngreso.SelectedDate= medicoLeido.FechaIngreso;
            txtSueldoMinGarant.Text = medicoLeido.SueldoMinimoGarantizado.ToString();
            txtRealizaGuardia.IsChecked = medicoLeido.Guardia;
           

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonCancelar(object sender, RoutedEventArgs e)
        {
            this.NavegarA<Medicos>();
        }

        private void ButtonSalir(object sender, RoutedEventArgs e)
        {
            this.Salir();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
