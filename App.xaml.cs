using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class App : Application {
		public static BaseDeDatosAbstracta BaseDeDatos;
		public static bool UsuarioLogueado = false;
		// public static SoundPlayer UClick = new SoundPlayer("sonidos\\uclick.wav");
        // public static SoundPlayer UClickNoFun = new SoundPlayer("sonidos\\uclicknofun.wav");
		// public static SoundPlayer UClickJewel = new SoundPlayer("sonidos\\uclick_jewel.wav");
		public static MediaPlayer UClick = new MediaPlayer();
		public static MediaPlayer UClickNoFun = new MediaPlayer();
		public static MediaPlayer UClickJewel = new MediaPlayer();

		public void StyleButton_MouseEnter(object sender, System.Windows.RoutedEventArgs e){
			// UClickJewel.Play();
		}

		public void StyleButton_Click(object sender, System.Windows.RoutedEventArgs e) {
			UClickNoFun.Open(new Uri("sonidos\\uclick_jewel.wav", UriKind.Relative));
			// Application.Current.Dispatcher.Invoke(() => {
			UClickNoFun.Play();
			// });
		}

		static public void PlayClickJewel() {
			UClickNoFun.Open(new Uri("sonidos\\uclick_jewel.wav", UriKind.Relative));
			UClickNoFun.Play();
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
