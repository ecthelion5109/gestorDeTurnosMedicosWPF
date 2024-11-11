using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class App : Application {
		public static bool SoundOn = false; // DEFAULT MODE // true;
		public static BaseDeDatosAbstracta BaseDeDatos;
		public static bool UsuarioLogueado = false;
		public static MediaPlayer Sonidito = new MediaPlayer();

		// public void StyleButton_Click(object sender, System.Windows.RoutedEventArgs e) {
			// Sonidito.Open(new Uri("sonidos\\uclick_jewel.wav", UriKind.Relative));
			// Sonidito.Play();
		// }

		public void StyleButton_MouseEnter(object sender, System.Windows.RoutedEventArgs e){
			//Es muy molesto.
			// Sonidito.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
			if (SoundOn) {
				// Sonidito.Open(new Uri("sonidos\\PIU!.wav", UriKind.Relative));
				Sonidito.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
				Sonidito.Play();
			}
		}

		static public void PlayClickJewel() {
			if (SoundOn) {
				// Sonidito.Open(new Uri("sonidos\\ULTRAPEEOOU!.wav", UriKind.Relative));
				Sonidito.Open(new Uri("sonidos\\uclick_jewel.wav", UriKind.Relative));
				Sonidito.Play();
			}
		}
		
		
		
		public static bool TryParseHoraField(string campo){
			if (TimeOnly.TryParse(campo, out _)){
				return true;
			} else {
				return false;
            }
		}
			
		
		public static void UpdateLabelDataBaseModo(Label label) {
			if (App.BaseDeDatos is BaseDeDatosJSON ) {
				label.Content = "Modo JSON";
			} else if (App.BaseDeDatos is BaseDeDatosSQL) {
				label.Content = "Modo SQL";
			} else {
				label.Content = "Elegir DB Modo";
			}
		}
	}
}
