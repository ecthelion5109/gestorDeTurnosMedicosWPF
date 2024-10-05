﻿using System;
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

namespace ClinicaMedica {
    /// <summary>
    /// Lógica de interacción para Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window {
        public Pacientes() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientesVer pacientesVerWindow = new PacientesVer();
            Application.Current.MainWindow = pacientesVerWindow;
            pacientesVerWindow.Show();

            //cierro la anterior.
            this.Close();
        }

        private void BotonVolver(object sender, RoutedEventArgs e)
        {
        }

		private void Button_Click_1(object sender, RoutedEventArgs e) {

			PantallaPrincipal pantallaPrincipalWindow = new PantallaPrincipal();
			Application.Current.MainWindow = pantallaPrincipalWindow;
			pantallaPrincipalWindow.Show();

			//cierro la anterior.
			this.Close();
		}

		private void ButtonPacienteAgregar(object sender, RoutedEventArgs e) {

			PacientesAgregar pacientesAgregarWindow = new PacientesAgregar();
			Application.Current.MainWindow = pacientesAgregarWindow;
			pacientesAgregarWindow.Show();

			//cierro la anterior.
			this.Close();
		}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PacientesModificar pacientesModificarWindow = new PacientesModificar();
            Application.Current.MainWindow = pacientesModificarWindow;
            pacientesModificarWindow.Show();

            //cierro la anterior.
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PacientesEliminar pacientesEliminarWindow = new PacientesEliminar();
            Application.Current.MainWindow = pacientesEliminarWindow;
            pacientesEliminarWindow.Show();

            //cierro la anterior.
            this.Close();
        }
    }
}
