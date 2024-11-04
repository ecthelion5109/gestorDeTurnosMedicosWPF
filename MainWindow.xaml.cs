using System;
using System.Windows;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaMedica{
	public partial class MainWindow : Window {
		
		public MainWindow() {
			InitializeComponent();
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
			if (App.UsuarioLogueado && App.BaseDeDatos is BaseDeDatosJSON ) {
				labelBaseDeDatosModo.Content = "Modo JSON";
				buttonVerTurnos.IsEnabled = false;
			} else {
				labelBaseDeDatosModo.Content = "Modo SQL";
				buttonVerTurnos.IsEnabled = true;
			}
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
	}
}