using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica {
	public partial class Turnos : Window {
		private static Turno? SelectedTurno = null;
		
		public Turnos() {
            InitializeComponent();
		}

		//----------------------ActualizarSecciones-------------------//
		private void UpdateCalendarUI() {
			txtCalendario.SelectedDate = SelectedTurno?.Fecha;
			txtCalendario.DisplayDate = SelectedTurno?.Fecha ?? DateTime.Today;
		}
		private void UpdateMedicoUI(){
			txtMedicoDni.Text = SelectedTurno?.MedicoRelacionado?.Dni;
			txtMedicoNombre.Text = SelectedTurno?.MedicoRelacionado?.Name;
			txtMedicoApellido.Text = SelectedTurno?.MedicoRelacionado?.LastName;
			txtMedicoEspecialidad.Text = SelectedTurno?.MedicoRelacionado?.Especialidad;
			buttonModificarMedico.IsEnabled = SelectedTurno?.MedicoRelacionado != null;
		}

		private void UpdateTurnoUI(){
			turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnos();
			SelectedTurno = (Turno)turnosListView.SelectedItem;
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
		
		//----------------------EeventosRefresh-------------------//
		private void Window_Activated(object sender, EventArgs e) {	
			App.UpdateLabelDataBaseModo(this.labelBaseDeDatosModo);
			UpdateTurnoUI();
			UpdateCalendarUI();
			UpdateMedicoUI();
			UpdatePacienteUI();
		}
		
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			UpdateTurnoUI();
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
			if (SelectedTurno?.MedicoRelacionado != null) {
				this.AbrirComoDialogo<MedicosModificar>(SelectedTurno?.MedicoRelacionado);
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
