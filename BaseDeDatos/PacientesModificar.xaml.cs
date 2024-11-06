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
    public partial class PacientesModificar : Window {
		private static Paciente? SelectedPaciente;
		//---------------------public.constructors-------------------//
		public PacientesModificar() //Crear medico
		{
			InitializeComponent();
			SelectedPaciente = null;
		}

		public PacientesModificar(Paciente selectedPaciente) //Modificar medico
		{
			InitializeComponent();
			SelectedPaciente = selectedPaciente;
			this.txtDni.Text = SelectedPaciente.Dni;
			this.txtNombre.Text = SelectedPaciente.Name;
			this.txtApellido.Text = SelectedPaciente.LastName;
			this.txtFechaIngreso.SelectedDate = SelectedPaciente.FechaIngreso;
			this.txtEmail.Text = SelectedPaciente.Email;
			this.txtTelefono.Text = SelectedPaciente.Telefono;
			this.txtFechaNacimiento.SelectedDate = SelectedPaciente.FechaNacimiento;
			this.txtDomicilio.Text = SelectedPaciente.Domicilio;
			this.txtLocalidad.Text = SelectedPaciente.Localidad;
			this.txtProvincia.Text = SelectedPaciente.Provincia;
		}
		


		//---------------------botones.GuardarCambios-------------------//
		bool FaltanCamposPorCompletar(){
			return (
					 string.IsNullOrEmpty(this.txtDni.Text) ||
					 //string.IsNullOrEmpty(this.txtNombre.Text) ||
					 //string.IsNullOrEmpty(this.txtApellido.Text) ||
					 //string.IsNullOrEmpty(this.txtEmail.Text) ||
					 //string.IsNullOrEmpty(this.txtTelefono.Text) ||
					 //string.IsNullOrEmpty(this.txtDomicilio.Text) ||
					 //string.IsNullOrEmpty(this.txtLocalidad.Text) ||
					 //string.IsNullOrEmpty(this.txtProvincia.Text) ||
					 
					 this.txtFechaIngreso.SelectedDate is null ||
					 this.txtFechaNacimiento.SelectedDate is null
					 );
		}
		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			// ---------AsegurarInput-----------//
			if (FaltanCamposPorCompletar()){
				MessageBox.Show($"Error: Faltan datos obligatorios por completar.");
				return;
			}
			
			//---------Crear-----------//
			if (SelectedPaciente is null) {
				var nuevpacoiente = new Paciente(this);
				if (App.BaseDeDatos.CreatePaciente(nuevpacoiente)){
					this.Close();
				}
			}
			//---------Modificar-----------//
			else {
				string originalDni = SelectedPaciente.Dni;
				SelectedPaciente.AsignarDatosFromWindow(this);
				if (App.BaseDeDatos.UpdatePaciente(SelectedPaciente, originalDni)){
					this.Close();
				}
			}
		}


		//---------------------botones.Eliminar-------------------//
		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			//---------Checknulls-----------//
			if (SelectedPaciente is null || SelectedPaciente.Dni is null) {
				MessageBox.Show($"No hay item seleccionado.");
				return;
			}
			//---------confirmacion-----------//
			if (MessageBox.Show($"¿Está seguro que desea eliminar este médico? {SelectedPaciente.Name}",
				"Confirmar Eliminación",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			) != MessageBoxResult.OK) {
				return;
			}
			//---------Eliminar-----------//
			if (App.BaseDeDatos.DeletePaciente(SelectedPaciente)) {
				this.Close(); // this.NavegarA<Medicos>();
			}
		}
		//---------------------botones.Salida-------------------//
		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.Close(); // this.NavegarA<Pacientes>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		//------------------------Fin----------------------//
	}
}
