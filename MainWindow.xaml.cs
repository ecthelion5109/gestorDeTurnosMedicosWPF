using System.Windows;
using System.Windows.Media.Imaging;

namespace ClinicaMedica{
	public partial class MainWindow : Window {
		
		public MainWindow() {
			InitializeComponent();
			soundCheckBox.IsChecked = App.SoundOn;
		}
		public void MetodoBotonLogin(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<Login>();
		}
        private void MetodoBotonMedicos(object sender, RoutedEventArgs e) {
			if (App.UsuarioLogueado) {
				this.NavegarA<Medicos>();
			} else {
				this.AbrirComoDialogo<Login>();
				if (App.UsuarioLogueado) {
					this.NavegarA<Medicos>();
				}
			}
		}

        private void MetodoBotonPacientes(object sender, RoutedEventArgs e) {
			if (App.UsuarioLogueado) {
				this.NavegarA<Pacientes>();
			}
			else {
				this.AbrirComoDialogo<Login>();
				if (App.UsuarioLogueado) {
					this.NavegarA<Pacientes>();
				}
			}
		}

		private void MetodoBotonTurnos(object sender, RoutedEventArgs e) {
			if (App.UsuarioLogueado) {
				this.NavegarA<Turnos>();
			}
			else {
				this.AbrirComoDialogo<Login>();
				if (App.UsuarioLogueado) {
					this.NavegarA<Turnos>();
				}
			}
		}
		
		private void Window_Activated(object sender, EventArgs e) {
			App.UpdateLabelDataBaseModo(this.labelBaseDeDatosModo);
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}

		private void soundCheckBox_Checked(object sender, RoutedEventArgs e) {
			if (soundCheckBox.IsChecked == true) {
				App.SoundOn = true;
				App.PlayClickJewel();
			}
			else {
				App.SoundOn = false;
			}
		}
		
		
		
		
		
	}
}