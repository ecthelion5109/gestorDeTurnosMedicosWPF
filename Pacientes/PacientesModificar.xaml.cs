using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Lógica de interacción para PacientesModificar.xaml
    /// </summary>
    public partial class PacientesModificar : Window
    {
        public PacientesModificar()
        {
            InitializeComponent();
        }


        public void MetodoBotonVolverAPacientes(object sender, RoutedEventArgs e)
        {
			this.NavegarA<Pacientes>();
        }

        public void MetodoBotonPaginaPrincipal(object sender, RoutedEventArgs e)
        {
			this.NavegarA<MainWindow>();
        }

        public void MetodoBotonSalir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			/*
			// dni, string name, string lastname, DateTime fechaIngreso, string email, string telefono, string cobertura, DateTime fechaNacimiento
			BaseDeDatos.PacienteGuardar(
				dni : int.Parse(txtdni.Text),
				name : txtnombre.Text,
				lastname : txtapellido.Text,
				fechaingreso : DateTime.Now,
				email : txtemail.Text,
				telefono : txttelefono.Text,
				fechanacimiento : (DateTime) txtfechanacimiento.SelectedDate,
				direccion : txtdireccion.Text,
				localidad : txtlocalidad.Text,
				provincia : txtprovincia.Text
			);
			*/
		}
    }
}
