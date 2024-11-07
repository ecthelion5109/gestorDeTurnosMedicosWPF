using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows;
using System.Text.Json.Serialization;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Media;
using System;
using System.Windows.Media;


namespace ClinicaMedica
{
    public partial class App : Application
    {
        public static bool UsuarioLogueado = false;
        public static string UsuarioName = "Señor Gestor";
        public static IBaseDeDatos BaseDeDatos;

        // Agregamos las instancias de SoundPlayer para que se carguen una vez al iniciar la app
        public static  SoundPlayer? HoverSound;
        public static  SoundPlayer? ClickSound;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Cargamos los sonidos
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("sonidos\\uclicknofun.wav", UriKind.Relative));
            mediaPlayer.Play();
        }
    }

    public static class WindowExtensions
    {
        public static void NavegarA<T>(this Window previousWindow) where T : Window, new()
        {
            T nuevaVentana = new();
            Application.Current.MainWindow = nuevaVentana;
            nuevaVentana.Show();
            previousWindow.Close();
        }

        public static void NavegarA<T>(this Window previousWindow, object optionalArg) where T : Window, new()
        {
            T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
            Application.Current.MainWindow = nuevaVentana;
            nuevaVentana.Show();
            previousWindow.Close();
        }

        public static void AbrirComoDialogo<T>(this Window previousWindow) where T : Window, new()
        {
            T nuevaVentana = new();
            Application.Current.MainWindow = nuevaVentana;
            nuevaVentana.ShowDialog();
        }

        public static void AbrirComoDialogo<T>(this Window previousWindow, object optionalArg) where T : Window
        {
            T nuevaVentana = (T)Activator.CreateInstance(typeof(T), optionalArg);
            Application.Current.MainWindow = nuevaVentana;
            nuevaVentana.ShowDialog();
        }

        public static void VolverAHome(this Window previousWindow)
        {
            previousWindow.NavegarA<MainWindow>();
        }

        public static void Salir(this Window previousWindow)
        {
            Application.Current.Shutdown();
        }

        public static void Button_MouseEnter(object sender, System.Windows.RoutedEventArgs e)
        {
            App.HoverSound?.Play();
        }

        public static void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.ClickSound?.Play();
        }
    }
}
