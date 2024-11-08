using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Turnos : Window {
		private static Medico? SelectedMedico;
		private static Turno? SelectedTurno;
		private static Paciente? SelectedPaciente;
		
		public Turnos() {
            InitializeComponent();
		}

		//----------------------ClearMetohds-------------------//
		private void ClearMedicoUI(){
			buttonModificarMedico.IsEnabled = false;
			txtMedicoDni.Text = "";
			txtMedicoNombre.Text = "";
			txtMedicoApellido.Text = "";
			txtMedicoEspecialidad.Text = "";
		}

		private void ClearTurnoUI(){
			buttonModificarTurno.IsEnabled = false;
			SelectedMedico = null;
			SelectedPaciente = null;
			turnosListView.ItemsSource = null;
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
		private void UpdateCalendarUI(){
			if (SelectedTurno != null){
				txtCalendario.SelectedDate = SelectedTurno.Fecha;
				txtCalendario.DisplayDate  = (DateTime) SelectedTurno.Fecha;
			}
			else{
				txtCalendario.DisplayDate = DateTime.MinValue;
				txtCalendario.SelectedDate = null;
			}
		}
		private void UpdateMedicoUI(){
			if (SelectedMedico != null){
				txtMedicoDni.Text = SelectedMedico.Dni;
				txtMedicoNombre.Text = SelectedMedico.Name;
				txtMedicoApellido.Text = SelectedMedico.LastName;
				txtMedicoEspecialidad.Text = SelectedMedico.Especialidad;
				buttonModificarMedico.IsEnabled = true;
			}
			else{
				ClearMedicoUI();
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
			App.UpdateLabelDataBaseModo(this.labelBaseDeDatosModo);
			turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnos();
			ClearMedicoUI(); 
			ClearPacienteUI();
		}
		
		private void CalendarioTurnos_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
		}
		
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedTurno = (Turno)turnosListView.SelectedItem;
			if (SelectedTurno != null){
				buttonModificarTurno.IsEnabled = true;
				App.BaseDeDatos.TryGetPaciente(SelectedTurno.PacienteId, out SelectedPaciente);
				App.BaseDeDatos.TryGetMedico(SelectedTurno.MedicoId, out SelectedMedico);
			}
			else{
				SelectedPaciente = null;
				SelectedMedico = null;
			}
			UpdateCalendarUI();
			UpdateMedicoUI();
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
			this.AbrirComoDialogo<TurnosModificar>(); 
		}
		//---------------------botonesDeVolver-------------------//
		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		private void ButtonHome(object sender, RoutedEventArgs e) {
			this.VolverAHome();
		}


		//------------------------Fin.Turnos----------------------//
	}
}
