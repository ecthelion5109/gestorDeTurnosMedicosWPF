using System.Windows;
using System.Windows.Controls;

namespace ClinicaMedica{
    public partial class Pacientes : Window {
		private static Turno? SelectedTurno;
		private static Paciente? SelectedPaciente;
		public Pacientes(){
            InitializeComponent();
		}
		
		//----------------------ActualizarSecciones-------------------//
		private void UpdatePacienteUI() {
			pacientesListView.ItemsSource = App.BaseDeDatos.ReadPacientes();
			buttonModificarPaciente.IsEnabled = SelectedPaciente != null;
		}
		private void UpdateTurnoUI(){
			turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnosWherePacienteId(SelectedPaciente);
			buttonModificarTurno.IsEnabled = SelectedTurno != null;
		}
		private void UpdateMedicoUI() {
			txtMedicoDni.Text = SelectedTurno?.MedicoRelacionado.Dni;
			txtMedicoNombre.Text = SelectedTurno?.MedicoRelacionado.Name;
			txtMedicoApellido.Text = SelectedTurno?.MedicoRelacionado.LastName;
			txtMedicoEspecialidad.Text = SelectedTurno?.MedicoRelacionado.Especialidad;
			buttonModificarMedico.IsEnabled = SelectedTurno?.MedicoRelacionado != null;
		}




		//----------------------EventosRefresh-------------------//
		private void Window_Activated(object sender, EventArgs e) {	
			App.UpdateLabelDataBaseModo(this.labelBaseDeDatosModo);
			UpdatePacienteUI();
			UpdateTurnoUI();
			UpdateMedicoUI();
		}
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedTurno = (Turno)turnosListView.SelectedItem;
			UpdatePacienteUI();
			UpdateTurnoUI();
			UpdateMedicoUI();
		}
		private void pacientesListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedPaciente = (Paciente)pacientesListView.SelectedItem;
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
			if (SelectedTurno?.MedicoRelacionado != null) {
				this.AbrirComoDialogo<MedicosModificar>(SelectedTurno?.MedicoRelacionado!);
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
