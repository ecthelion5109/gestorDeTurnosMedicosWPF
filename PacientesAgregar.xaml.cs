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

namespace ClinicaMedica
{
    /// <summary>
    /// Lógica de interacción para PacientesAgregar.xaml
    /// </summary>
    public partial class PacientesAgregar : Window
    {
        public PacientesAgregar()
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
    }
}
