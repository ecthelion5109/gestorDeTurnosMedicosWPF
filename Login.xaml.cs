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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
		
		private void MetodoBotonIniciarSesion(object sender, RoutedEventArgs e) {
			string usuario = ""; //"mariela";
			string contraseña = ""; // "123";
			if (  label_user.Text.Equals(usuario) && label_pass.Text.Equals(contraseña)   ) {
				this.NavegarA<MainWindow>();
			}
			else {
				MessageBox.Show("Contraseña o usuario incorrecta");
			}
		}

        public void MetodoBotonSalir(object sender, RoutedEventArgs e){
			Application.Current.Shutdown ();
        }

		private void MetodoBotonCancelar(object sender, RoutedEventArgs e) {

			this.NavegarA<MainWindow>();
		}
	}
}
