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
		private static Medico? SelectedMedico;
		private static Turno? SelectedTurno;
		private static Paciente? SelectedPaciente;
		//---------------------public.constructors-------------------//
        public TurnosModificar() //Crear turno
		{
            InitializeComponent();
			SelectedTurno = null;
			LLenarComboBoxes();
		}

		public TurnosModificar(Turno selectedTurno)  //Modificar turno
		{
			InitializeComponent();
			SelectedTurno = selectedTurno;
			LLenarComboBoxes();
			SetComboBoxSelections();
		}

		private void SetComboBoxSelections(){
			App.BaseDeDatos.TryGetPaciente(SelectedTurno.PacienteId, out SelectedPaciente);
			App.BaseDeDatos.TryGetMedico(SelectedTurno.MedicoId, out SelectedMedico);

			this.txtMedicos.SelectedValue = SelectedMedico.Id;
			this.txtPacientes.SelectedValue = SelectedPaciente.Id;
			this.txtEspecialidades.SelectedItem = SelectedMedico.Especialidad;
			this.txtId.Content = SelectedTurno.Id;
			this.txtFecha.SelectedDate = SelectedTurno.Fecha;
			this.txtHora.Text = SelectedTurno.Hora.ToString();
		}

		private void LLenarComboBoxes() {
			txtEspecialidades.ItemsSource = App.BaseDeDatos.ReadDistinctEspecialidades();

			txtPacientes.ItemsSource = App.BaseDeDatos.ReadPacientes();
			txtPacientes.DisplayMemberPath = "Displayear";
			
			txtMedicos.ItemsSource = App.BaseDeDatos.ReadMedicos();
			txtMedicos.DisplayMemberPath = "Displayear";

			txtPacientes.SelectedValuePath = "Id";
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



			// ---------DebugTest-----------//
			// if (true) {
				// MessageBox.Show($"txtMedicos.SelectedValue:{(this.txtMedicos.SelectedValue)}\ntxtPacientes.SelectedValue:{this.txtPacientes.SelectedValue}\ntxtHora.Text:{this.txtHora.Text}\ntxtFecha.SelectedDate:{this.txtFecha.SelectedDate}");
			// }

			//---------Crear-----------//
			if (SelectedTurno is null) {
				var newturno = new Turno();
				newturno.AsignarDatosFromWindow(this);
				if ( App.BaseDeDatos.CreateTurno(newturno) ) {
					this.Cerrar();
				}
			}
			//---------Modificar-----------//
			else {
				SelectedTurno.AsignarDatosFromWindow(this);
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
