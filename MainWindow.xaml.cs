using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClinicaMedica {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			//MessageBox.Show($"Usuario correcta?: {label_user.Text.Equals("juanita_conchuda_123")}");
			//MessageBox.Show($"Contraseña correcta?: {label_pass.Text.Equals("123")}");

			Console.WriteLine(sender.ToString());
			//MessageBox.Show(sender.ToString());
			string usuario = "mariela";
			string contraseña = "123";


			if (  label_user.Text.Equals(usuario) && label_pass.Text.Equals(contraseña)   ) {
				MessageBox.Show("Bienvenida, hija de puta!!");
			}
			else {
				MessageBox.Show("No tudiaste muchacho. Bochado");
			}
		}

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			Application.Current.Shutdown ();
        }

		private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) {
		}
	}
}