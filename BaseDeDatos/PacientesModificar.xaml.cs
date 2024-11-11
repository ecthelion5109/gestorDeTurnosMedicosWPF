using System.Windows;

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
			SelectedPaciente.MostrarseEnVentana(this);
			
		}
		


		//---------------------botones.GuardarCambios-------------------//
		private bool CamposCompletadosCorrectamente(){
			if (
				 string.IsNullOrEmpty(this.txtDni.Text) ||
				 string.IsNullOrEmpty(this.txtName.Text) ||
				 string.IsNullOrEmpty(this.txtLastName.Text) ||
				 string.IsNullOrEmpty(this.txtEmail.Text) ||
				 string.IsNullOrEmpty(this.txtTelefono.Text) ||
				 string.IsNullOrEmpty(this.txtDomicilio.Text) ||
				 string.IsNullOrEmpty(this.txtLocalidad.Text) ||
				 string.IsNullOrEmpty(this.txtProvincia.Text) ||
				 this.txtFechaIngreso.SelectedDate is null ||
				 this.txtFechaNacimiento.SelectedDate is null
			) {
				MessageBox.Show($"Error: Faltan datos obligatorios por completar.", "Error de ingreso", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			 }
					 
			if (!Int64.TryParse(this.txtDni.Text, out _)){
                MessageBox.Show($"Error: El dni no es un numero entero valido.", "Error de ingreso", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
            }
			return true;
		}
		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			App.PlayClickJewel();
			// ---------AsegurarInput-----------//
			if (!CamposCompletadosCorrectamente()){
				return;
			}
			
			//---------Crear-----------//
			if (SelectedPaciente is null) {
				var nuevpacoiente = new Paciente(this);
				if (App.BaseDeDatos.CreatePaciente(nuevpacoiente)){
					this.Cerrar();
				}
			}
			//---------Modificar-----------//
			else {
				SelectedPaciente.TomarDatosDesdeVentana(this);
				if (App.BaseDeDatos.UpdatePaciente(SelectedPaciente)){
					this.Cerrar();
				}
			}
		}


		//---------------------botones.Eliminar-------------------//
		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			App.PlayClickJewel();
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
				this.Cerrar(); // this.NavegarA<Medicos>();
			}
		}
		//---------------------botones.Salida-------------------//
		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.Cerrar(); // this.NavegarA<Pacientes>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		//------------------------Fin----------------------//
	}
}
