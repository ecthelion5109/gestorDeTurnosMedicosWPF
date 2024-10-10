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

namespace ClinicaMedica {
	/// <summary>
	/// Lógica de interacción para MedicosModificar.xaml
	/// </summary>

	public partial class MedicosModificar : Window {
		public MedicosModificar() {
			InitializeComponent();

		}
		public MedicosModificar(string selectedMedicoDni) {
			InitializeComponent();
			//Medico medicoLeido = BaseDeDatos.LeerDesdeJson<Medico>("medico.json");
			//Medico medicoLeido = (Medico)BaseDeDatos.DatabaseOBJ["medicos"][previousWindow.SelectedMedicoDni];
			Medico medicoLeido = (Medico)BaseDeDatos.Database["medicos"][selectedMedicoDni];
			this.txtNombre.Text = medicoLeido.Name;
			this.txtApellido.Text = medicoLeido.Lastname;
			this.txtDNI.Text = medicoLeido.Dni.ToString(); ;
			this.txtProvincia.Text = medicoLeido.Provincia;
			this.txtDomicilio.Text = medicoLeido.Domicilio;
			this.txtLocalidad.Text = medicoLeido.Localidad;
			this.txtEspecialidad.Text = medicoLeido.Specialidad;
			this.txtFechaIngreso.SelectedDate = medicoLeido.FechaIngreso;
			this.txtSueldoMinGarant.Text = medicoLeido.SueldoMinimoGarantizado.ToString();
			this.txtRealizaGuardia.IsChecked = medicoLeido.Guardia;



			// Bind the list of HorarioMedico to the DataGrid
			this.txtDiasDeAtencion.ItemsSource = medicoLeido.GetDiasDeAtencionList();


		}

		private void ButtonGuardarCambios(object sender, RoutedEventArgs e) {
			// BaseDeDatos.MedicosGuardar(this);
			BaseDeDatos.AplicarYGuardarMedico(this);
		}

		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

		private void txtDiasDeAtencion_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}
	}
}
