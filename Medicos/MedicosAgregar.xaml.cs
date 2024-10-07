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
    /// Lógica de interacción para MedicosAgregar.xaml
    /// </summary>
    /// 

    public class HorarioMedico
    {
        public string DiaSemana { get; set; }
        public string InicioHorario { get; set; }
        public string FinHorario { get; set; }
        public bool Trabaja { get; set; }

        // Lista de horarios disponibles (ej. de 8:00 a 20:00)
        public List<string> Horarios { get; } = new List<string>
    {
        "08:00", "09:00", "10:00", "11:00", "12:00",
        "13:00", "14:00", "15:00", "16:00", "17:00",
        "18:00", "19:00", "20:00"
    };
    }
    public partial class MedicosAgregar : Window
    {
        public MedicosAgregar()
        {
            InitializeComponent();
        }
		
		public void ButtonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}
		
		public void ButtonAgregar(object sender, RoutedEventArgs e) {
		}
		
		public void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

		

		private void MetodoBotonTestearJsonGuardar(object sender, RoutedEventArgs e) {

		}

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
