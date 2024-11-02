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
    /// Lógica de interacción para TurnosModificar.xaml
    /// </summary>
    public partial class TurnosModificar : Window
    {
        public TurnosModificar()
        {
            InitializeComponent();

			// Leer la instancia desde el archivo JSON
			// Turno turnoLeido = BaseDeDatos.LeerDesdeJson<Turno>("turno.json");

			// Asignar el DNI al ComboBox
			// txtpaciente.Items.Add(turnoLeido.PacientePk);

			// Opcionalmente, puedes seleccionar automáticamente el primer valor:
			// txtpaciente.SelectedIndex = 0;

		}

        private void ButtonSalir(object sender, RoutedEventArgs e)
        {
            this.Salir();
        }

        private void ButtonCancelar(object sender, RoutedEventArgs e)
        {
            this.NavegarA<Turnos>();
        }

        private void ButtonAgregar(object sender, RoutedEventArgs e)
        {
        }
    }
}
