using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Medicos : Window {
		private static Medico? SelectedMedico;
		private static Turno? SelectedTurno;
		private static Paciente? SelectedPaciente;

		public Medicos() {
			InitializeComponent();
			// this.DataContext = this;
			// turnosListView.ItemsData = SelectedMedico;
		}


	
		
		//----------------------metodos-------------------//
		
		
		
		private void LLenarStackPanelPaciente(){
			buttonModificarMedico.IsEnabled = true;
			turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnosWhereMedicoId(SelectedMedico);
			buttonModificarTurno.IsEnabled = true;
			//derivado.
			SelectedPaciente = BaseDeDatosSQL.DictPacientes[SelectedTurno.PacienteId];
			txtPacienteDni.Text = SelectedPaciente.Dni;
			txtPacienteNombre.Text = SelectedPaciente.Name;
			txtPacienteApellido.Text = SelectedPaciente.LastName;
			txtPacienteEmail.Text = SelectedPaciente.Email;
			txtPacienteTelefono.Text = SelectedPaciente.Telefono;
			buttonModificarPaciente.IsEnabled = true;
		}
		private void ClearStackPanelPaciente(){
			buttonModificarMedico.IsEnabled = false;
			SelectedTurno = null;
			SelectedPaciente = null;
			turnosListView.ItemsSource = null;
			buttonModificarTurno.IsEnabled = false;
			//derivado.
			txtPacienteDni.Text = "";
			txtPacienteNombre.Text = "";
			txtPacienteApellido.Text = "";
			txtPacienteEmail.Text = "";
			txtPacienteTelefono.Text = "";
			buttonModificarPaciente.IsEnabled = false;
		}
		
		
		private void llenarStackPanels(){
			if (SelectedMedico is null){
				ClearStackPanelPaciente();
			} else {
				LLenarStackPanelPaciente();
			}
		}

		//----------------------eventosRefresh-------------------//
		private void medicosListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedMedico = (Medico) medicosListView.SelectedItem;
			llenarStackPanels();
		}
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedTurno = (Turno) turnosListView.SelectedItem;
			llenarStackPanels();
		}
		private void Window_Activated(object sender, EventArgs e) {	
			medicosListView.ItemsSource = App.BaseDeDatos.ReadMedicos(); // ahora viene desde ventana activated
			llenarStackPanels();
		}
		//---------------------botonesDeModificar-------------------//
		private void ButtonModificarTurno(object sender, RoutedEventArgs e) {
			if (SelectedTurno != null) {
				this.AbrirComoDialogo<TurnosModificar>(SelectedTurno);
			}
		}
		private void ButtonModificarMedico(object sender, RoutedEventArgs e) {
			if (SelectedMedico != null) {
				this.AbrirComoDialogo<MedicosModificar>(SelectedMedico);
			}
		}
		private void ButtonModificarPaciente(object sender, RoutedEventArgs e) {
			if (SelectedPaciente != null) {
				this.AbrirComoDialogo<PacientesModificar>(SelectedPaciente);
			}
		}
		//------------------botonesParaCrear------------------//
		private void ButtonAgregar(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<MedicosModificar>(); 
		}
		//---------------------botonesDeVolver-------------------//
		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.VolverAHome();
		}
		//------------------------Fin.Medicos----------------------//
	}
}
