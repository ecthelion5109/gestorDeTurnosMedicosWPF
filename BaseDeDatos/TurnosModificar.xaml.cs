using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
    public partial class TurnosModificar : Window {
		// private static Medico? SelectedMedico;
		private static Turno? SelectedTurno;
		// private static Paciente? SelectedPaciente;
		//---------------------public.constructors-------------------//
        public TurnosModificar() //Crear turno
		{
            InitializeComponent();
			LLenarComboBoxes();
			SelectedTurno = null;
		}

		public TurnosModificar(Turno selectedTurno)  //Modificar turno
		{
			InitializeComponent();
			LLenarComboBoxes();
			SelectedTurno = selectedTurno;
			SelectedTurno.MostrarseEnVentana(this);
		}

		private void LLenarComboBoxes() {
			txtEspecialidades.ItemsSource = App.BaseDeDatos.ReadDistinctEspecialidades();

			txtPacientes.ItemsSource = App.BaseDeDatos.ReadPacientes();
			txtPacientes.DisplayMemberPath = "Displayear";
			txtPacientes.SelectedValuePath = "Id";
			
			txtMedicos.ItemsSource = App.BaseDeDatos.ReadMedicos();
			txtMedicos.DisplayMemberPath = "Displayear";
			txtMedicos.SelectedValuePath = "Id";
		}

		private bool CamposCompletadosCorrectamente(){
			if (
				this.txtPacientes.SelectedValue is null 
				|| this.txtMedicos.SelectedValue is null 
				|| this.txtFecha.SelectedDate is null 
				|| string.IsNullOrEmpty(this.txtHora.Text)
			) {
				MessageBox.Show($"Error: Faltan datos obligatorios por completar", "Faltan datos.", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			} 

			if ( !App.TryParseHoraField(this.txtHora.Text) ){
				MessageBox.Show($"Error: No se reconoce la hora. \n Ingrese un string con formato valido (hh:mm)");
				return false;
			}
			return true;
		}

		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			App.PlayClickJewel();
			// ---------AsegurarInput-----------//
			if (!CamposCompletadosCorrectamente()) {
				return;
			}
			//---------Crear-----------//
			if (SelectedTurno is null) {
				var newturno = new Turno();
				newturno.LeerDesdeVentana(this);
				if ( App.BaseDeDatos.CreateTurno(newturno) ) {
					this.Cerrar();
				}
			}
			//---------Modificar-----------//
			else {
				SelectedTurno.LeerDesdeVentana(this);
				if ( App.BaseDeDatos.UpdateTurno(SelectedTurno) ) {
					this.Cerrar();
				}
			}
		}

		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			App.PlayClickJewel();
			//---------Checknulls-----------//
			if (SelectedTurno is null) {
				MessageBox.Show($"No hay item seleccionado.");
				return;
			}
			//---------confirmacion-----------//
			if (MessageBox.Show($"¿Está seguro que desea eliminar este turno?",
				"Confirmar Eliminación",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			) != MessageBoxResult.OK) {
				return;
			}
			//---------Eliminar-----------//
			if (App.BaseDeDatos.DeleteTurno(SelectedTurno)){
				this.Cerrar();
			}
		}
		//---------------------botones.Salida-------------------//
		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.Cerrar(); // this.NavegarA<Turnos>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}

		private void txtEspecialidades_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			txtMedicos.ItemsSource = App.BaseDeDatos.ReadMedicosWhereEspecialidad(txtEspecialidades.SelectedItem.ToString());
			txtMedicos.DisplayMemberPath = "Displayear";
			txtMedicos.SelectedValuePath = "Id";

        }
        //------------------------Fin---------------------------//
    }
}
