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
    /// Lógica de interacción para MedicosModificar.xaml
    /// </summary>
    public class HorarioMedico {
        public string DiaSemana { get; set; }
        public string InicioHorario { get; set; }
        public string FinHorario { get; set; }
        public bool Trabaja { get; set; }

        public List<string> Horarios { get; } = new List<string> {
        "08:00", "09:00", "10:00", "11:00", "12:00",
        "13:00", "14:00", "15:00", "16:00", "17:00",
        "18:00", "19:00", "20:00"
        };
    }

	public partial class MedicosModificar : Window {
		public MedicosModificar() {
			InitializeComponent();
		}
		public MedicosModificar(Medicos previousWindow) {
			InitializeComponent();
			// Medico medicoLeido = BaseDeDatos.LeerDesdeJson<Medico>("medico.json");
			//Medico medicoLeido = (Medico)BaseDeDatos.DatabaseOBJ["medicos"][previousWindow.SelectedMedicoDni];
			//this.txtNombre.Text = medicoLeido.Name;
			//this.txtApellido.Text = medicoLeido.Lastname;
			//this.txtDNI.Text = medicoLeido.Dni.ToString(); ;
			//this.txtProvincia.Text = medicoLeido.Provincia;
			//this.txtDomicilio.Text = medicoLeido.Domicilio;
			//this.txtLocalidad.Text = medicoLeido.Localidad;
			//this.txtEspecialidad.Text = medicoLeido.Specialidad;
			//this.txtFechaIngreso.SelectedDate = medicoLeido.FechaIngreso;
			//this.txtSueldoMinGarant.Text = medicoLeido.SueldoMinimoGarantizado.ToString();
			//this.txtRealizaGuardia.IsChecked = medicoLeido.Guardia;
		}

		private void ButtonGuardarCambios(object sender, RoutedEventArgs e) {
			// BaseDeDatos.MedicosGuardar(this);
			//BaseDeDatos.AplicarYGuardarMedico(this);
		}

		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

	}
}
