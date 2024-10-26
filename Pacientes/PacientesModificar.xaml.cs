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
		bool CorroborarUserInputEsSeguro(){
			return !(
					 string.IsNullOrEmpty(this.txtDni.Text)
					 );
		}
		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			OperationCode operacion;
			//---------Crear-----------//
			if (SelectedPaciente is null) {
				if (CorroborarUserInputEsSeguro()) {
					if (MainWindow.BaseDeDatos.CorroborarQueNoExistaPaciente(this.txtDni.Text)){
						operacion = OperationCode.YA_EXISTE;
					} else {
						SelectedPaciente = new Paciente(this);
						operacion = MainWindow.BaseDeDatos.CreatePaciente(SelectedPaciente);
					}
				}
				else {
					operacion = OperationCode.MISSING_FIELDS;
				}
			}
			//---------Modificar-----------//
			else {
				string originalDni = SelectedPaciente.Dni;
				if (CorroborarUserInputEsSeguro()) {
					SelectedPaciente.AsignarDatosFromWindow(this);
					operacion = MainWindow.BaseDeDatos.UpdatePaciente(SelectedPaciente, originalDni);
				}
				else {
					operacion = OperationCode.MISSING_FIELDS;
				}
			}

			//---------Mensaje-----------//
			MessageBox.Show(operacion switch {
				OperationCode.CREATE_SUCCESS => $"Exito: Se ha creado la instancia de Paciente: {SelectedPaciente.Name} {SelectedPaciente.LastName}",
				OperationCode.UPDATE_SUCCESS => $"Exito: Se han actualizado los datos de: {SelectedPaciente.Name} {SelectedPaciente.LastName}",
				OperationCode.DELETE_SUCCESS => $"Exito: Se ha eliminado a: {SelectedPaciente.Name} {SelectedPaciente.LastName} de la base de Datos",
				OperationCode.YA_EXISTE => $"Error: Ya existe un médico con DNI: {this.txtDni.Text}",
				OperationCode.MISSING_DNI => $"Error: El DNI es obligatorio.",
				OperationCode.MISSING_FIELDS => $"Error: Faltan datos obligatorios por completar.",
				_ => "Error: Sin definir"
			});
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
			OperationCode operacion = MainWindow.BaseDeDatos.DeletePaciente(SelectedPaciente.Dni);

			//---------Mensaje-----------//
			MessageBox.Show(operacion switch {
				OperationCode.DELETE_SUCCESS => $"Exito: Se han eliminado a: {SelectedPaciente.Name} {SelectedPaciente.LastName} de la base de Datos",
				_ => "Error: Sin definir"
			});
		}
		//---------------------botones.VolverAtras-------------------//
		private void ButtonVolver(object sender, RoutedEventArgs e) {
			this.NavegarA<Pacientes>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}



		//------------------------Fin----------------------//
	}
}
