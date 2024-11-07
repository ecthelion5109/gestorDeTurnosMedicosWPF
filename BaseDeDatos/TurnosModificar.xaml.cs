using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

		public bool FaltanCamposPorCompletar(){
			return (
				//string.IsNullOrEmpty(this.txtId.Content.ToString()) ||

				this.txtPacientes.SelectedValue is null ||
				this.txtMedicos.SelectedValue is null ||

				this.txtFecha.SelectedDate is null ||
				string.IsNullOrEmpty(this.txtHora.Text)
			);
		}

		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			// ---------AsegurarInput-----------//
			if (FaltanCamposPorCompletar()) {
				MessageBox.Show($"Error: Faltan datos obligatorios por completar.");
				return;
			}
			//---------Crear-----------//
			if (SelectedTurno is null) {
				var newturno = new Turno();
				newturno.TomarDatosDesdeVentana(this);
				if ( App.BaseDeDatos.CreateTurno(newturno) ) {
					this.Cerrar();
				}
			}
			//---------Modificar-----------//
			else {
				SelectedTurno.TomarDatosDesdeVentana(this);
				if ( App.BaseDeDatos.UpdateTurno(SelectedTurno) ) {
					this.Cerrar();
				}
			}
		}

		private void ButtonEliminar(object sender, RoutedEventArgs e) {
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
		//------------------------Fin---------------------------//
	}
}
