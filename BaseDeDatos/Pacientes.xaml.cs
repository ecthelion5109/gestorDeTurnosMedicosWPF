using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
using System.Data;

namespace ClinicaMedica
{
    /// <summary>
    /// Lógica de interacción para Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window {
		private static Medico? SelectedMedico;
		private static Turno? SelectedTurno;
		private static Paciente? SelectedPaciente;


		public Pacientes()
        {
            InitializeComponent();
		}

		//----------------------ClearMetohds-------------------//
		private void ClearPacienteUI(){
			buttonModificarPaciente.IsEnabled = false;
			SelectedPaciente = null;
			turnosListView.ItemsSource = null; // Clear turnos related to the selected paciente
		}

		private void ClearTurnoUI(){
			buttonModificarTurno.IsEnabled = false;
			SelectedTurno = null;
		}

		private void ClearMedicoUI(){
			buttonModificarMedico.IsEnabled = false;
			txtMedicoDni.Text = "";
			txtMedicoNombre.Text = "";
			txtMedicoApellido.Text = "";
			txtMedicoEspecialidad.Text = "";
		}


		//----------------------LlenarMethods-------------------//
		private void UpdatePacienteUI() {
			if (SelectedPaciente != null) {
				buttonModificarPaciente.IsEnabled = true;
				turnosListView.ItemsSource = App.BaseDeDatos.ReadTurnosWherePacienteId(SelectedPaciente);
			}
			else {
				ClearPacienteUI();
			}
		}

		private void UpdateTurnoUI(){
			if (SelectedTurno != null){
				buttonModificarTurno.IsEnabled = true;
				if (App.BaseDeDatos.TryGetMedico(SelectedTurno.MedicoId, out SelectedMedico)) {
					UpdateMedicoUI();
				}
			}
			else{
				ClearTurnoUI();
				ClearMedicoUI();
			}
		}
		
		private void UpdateMedicoUI() {
			if (SelectedMedico != null) {
				txtMedicoDni.Text = SelectedMedico.Dni;
				txtMedicoNombre.Text = SelectedMedico.Name;
				txtMedicoApellido.Text = SelectedMedico.LastName;
				txtMedicoEspecialidad.Text = SelectedMedico.Especialidad;
				buttonModificarMedico.IsEnabled = true;
			}
			else {
				ClearMedicoUI();
			}
		}

		//----------------------eventosRefresh-------------------//
		private void Window_Activated(object sender, EventArgs e) {	
			App.UpdateLabelDataBaseModo(this.labelBaseDeDatosModo);
			
			pacientesListView.ItemsSource = App.BaseDeDatos.ReadPacientes();
			ClearPacienteUI();  // Clear all selections and UI elements
			ClearTurnoUI();
			ClearMedicoUI();
		}
		private void listViewTurnos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedTurno = (Turno)turnosListView.SelectedItem;
			UpdateTurnoUI();
		}
		private void pacientesListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SelectedPaciente = (Paciente)pacientesListView.SelectedItem;
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
