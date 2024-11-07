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
		}
		
		//----------------------ClearMetohds-------------------//
		private void ClearMedicoUI(){
			buttonModificarMedico.IsEnabled = false;
			SelectedMedico = null;
			turnosListView.ItemsSource = null;
			ClearTurnoUI();
		}

		private void ClearTurnoUI(){
			buttonModificarTurno.IsEnabled = false;
			SelectedTurno = null;
			ClearPacienteUI();
		}

		private void ClearPacienteUI(){
			buttonModificarPaciente.IsEnabled = false;
			txtPacienteDni.Text = "";
			txtPacienteNombre.Text = "";
			txtPacienteApellido.Text = "";
			txtPacienteEmail.Text = "";
			txtPacienteTelefono.Text = "";
		}

		//----------------------LlenarMethods-------------------//
		private void UpdateMedicoUI(){
			if (SelectedMedico != null){
				buttonModificarMedico.IsEnabled = true;
				turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnosWhereMedicoId(SelectedMedico);
			}
			else{
				ClearMedicoUI();
			}
		}

		private void UpdateTurnoUI(){
			if (SelectedTurno != null){
				buttonModificarTurno.IsEnabled = true;
				SelectedPaciente = App.BaseDeDatos.DictPacientes[SelectedTurno.PacienteId];
				UpdatePacienteUI();
			}
			else{
				ClearTurnoUI();
			}
		}

		private void UpdatePacienteUI(){
			if (SelectedPaciente != null){
				txtPacienteDni.Text = SelectedPaciente.Dni;
				txtPacienteNombre.Text = SelectedPaciente.Name;
				txtPacienteApellido.Text = SelectedPaciente.LastName;
				txtPacienteEmail.Text = SelectedPaciente.Email;
				txtPacienteTelefono.Text = SelectedPaciente.Telefono;
				buttonModificarPaciente.IsEnabled = true;
			}
			else{
				ClearPacienteUI();
			}
		}

		//----------------------eventosRefresh-------------------//
		private void Window_Activated(object sender, EventArgs e) {	
			medicosListView.ItemsSource = App.BaseDeDatos.ReadMedicos();
			ClearMedicoUI();  // Clear all selections and UI elements
		}
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedTurno = (Turno)turnosListView.SelectedItem;
			UpdateTurnoUI();
		}
		private void medicosListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedMedico = (Medico)medicosListView.SelectedItem;
			UpdateMedicoUI();
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
		private void ButtonAgregarMedico(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<MedicosModificar>(); 
		}
		private void ButtonAgregarPaciente(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<PacientesModificar>(); // this.NavegarA<PacientesModificar>();
		}
		private void ButtonAgregarTurno(object sender, RoutedEventArgs e) {
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
