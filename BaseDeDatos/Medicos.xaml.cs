using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Medicos : Window {
		private static Medico? SelectedMedico = null;
		private static Turno? SelectedTurno = null;
		public Medicos() {
			InitializeComponent();
		}

		//----------------------ActualizarSecciones-------------------//
		private void UpdateMedicoUI() {
			medicosListView.ItemsSource = App.BaseDeDatos.ReadMedicos();
			buttonModificarMedico.IsEnabled = SelectedMedico != null;
		}
		private void UpdateTurnoUI(){
			turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnosWhereMedicoId(SelectedMedico);
			buttonModificarTurno.IsEnabled = SelectedTurno != null;
		}

		private void UpdatePacienteUI(){
			txtPacienteDni.Text = SelectedTurno?.PacienteRelacionado.Dni;
			txtPacienteNombre.Text = SelectedTurno?.PacienteRelacionado.Name;
			txtPacienteApellido.Text = SelectedTurno?.PacienteRelacionado.LastName;
			txtPacienteEmail.Text = SelectedTurno?.PacienteRelacionado.Email;
			txtPacienteTelefono.Text = SelectedTurno?.PacienteRelacionado.Telefono;
			buttonModificarPaciente.IsEnabled = SelectedTurno?.PacienteRelacionado != null;
		}

		//----------------------EventosRefresh-------------------//
		private void Window_Activated(object sender, EventArgs e) {	
			App.UpdateLabelDataBaseModo(this.labelBaseDeDatosModo);
			UpdateMedicoUI();
			UpdateTurnoUI();
			UpdatePacienteUI();
		}
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedTurno = (Turno)turnosListView.SelectedItem;
			UpdateMedicoUI();
			UpdateTurnoUI();
			UpdatePacienteUI();
		}
		private void medicosListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedMedico = (Medico)medicosListView.SelectedItem;
			UpdateMedicoUI();
			UpdateTurnoUI();
			UpdatePacienteUI();
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
			if (SelectedTurno?.PacienteRelacionado != null) {
				this.AbrirComoDialogo<PacientesModificar>(SelectedTurno?.PacienteRelacionado);
			}
		}
		//------------------botonesParaCrear------------------//
		private void ButtonAgregarMedico(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<MedicosModificar>(); 
		}
		private void ButtonAgregarPaciente(object sender, RoutedEventArgs e) {
			this.AbrirComoDialogo<PacientesModificar>();
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
