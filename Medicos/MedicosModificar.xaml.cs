using System;
using System.Collections.Generic;
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
	/// <summary>
	/// Lógica de interacción para MedicosModificar.xaml
	/// </summary>

	public partial class MedicosModificar : Window {
		public static Medico SelectedMedico;
		
		public MedicosModificar() {
			InitializeComponent();
			SelectedMedico = new Medico();
			// Bind the list of HorarioMedico to the DataGrid
			this.txtDiasDeAtencion.ItemsSource = SelectedMedico.GetDiasDeAtencionList();

		}
		public MedicosModificar(Medico selectedMedico) {
			InitializeComponent();
			//Medico SelectedMedico = BaseDeDatos.LeerDesdeJson<Medico>("medico.json");
			//Medico SelectedMedico = (Medico)BaseDeDatos.DatabaseOBJ["medicos"][previousWindow.SelectedMedico.Dni];
			// Medico SelectedMedico = (Medico)BaseDeDatos.Database["medicos"][selectedMedico.Dni];
			SelectedMedico = selectedMedico;
			
			this.txtNombre.Text = SelectedMedico.Name;
			this.txtApellido.Text = SelectedMedico.Lastname;
			this.txtDNI.Text = SelectedMedico.Dni.ToString(); ;
			this.txtProvincia.Text = SelectedMedico.Provincia;
			this.txtDomicilio.Text = SelectedMedico.Domicilio;
			this.txtLocalidad.Text = SelectedMedico.Localidad;
			this.txtEspecialidad.Text = SelectedMedico.Specialidad;
			this.txtFechaIngreso.SelectedDate = SelectedMedico.FechaIngreso;
			this.txtSueldoMinGarant.Text = SelectedMedico.SueldoMinimoGarantizado.ToString();
			this.txtRealizaGuardia.IsChecked = SelectedMedico.Guardia;
			// Bind the list of HorarioMedico to the DataGrid
			this.txtDiasDeAtencion.ItemsSource = SelectedMedico.GetDiasDeAtencionList();
		}



		private static void AsignarValoresAMedico(Medico medico, MedicosModificar ventana) {
			medico.Name = ventana.txtNombre.Text;
			medico.Lastname = ventana.txtApellido.Text;
			medico.Dni = ventana.txtDNI.Text;
			medico.Provincia = ventana.txtProvincia.Text;
			medico.Domicilio = ventana.txtDomicilio.Text;
			medico.Localidad = ventana.txtLocalidad.Text;
			medico.Specialidad = ventana.txtEspecialidad.Text;
			medico.Guardia = (bool)ventana.txtRealizaGuardia.IsChecked;
			medico.FechaIngreso = (DateTime)ventana.txtFechaIngreso.SelectedDate;
			medico.SueldoMinimoGarantizado = double.Parse(ventana.txtSueldoMinGarant.Text);

			var diasDeAtencion = new Dictionary<string, Horario>();

			foreach (var item in ventana.txtDiasDeAtencion.ItemsSource) {
				var diaAtencion = item as HorarioMedico;
				if (diaAtencion != null) {
					var dia = diaAtencion.DiaSemana;
					var start = diaAtencion.InicioHorario;
					var end = diaAtencion.FinHorario;

					if (!string.IsNullOrEmpty(dia) && !string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end)) {
						diasDeAtencion[dia] = new Horario(start, end);
					}
				}
			}

			medico.DiasDeAtencion = diasDeAtencion;
		}

		private void ButtonGuardarCambios(object sender, RoutedEventArgs e) {
			// Medico medicoModificado;

			// AsignarValoresAMedico(medicoModificado, this);
			AsignarValoresAMedico(SelectedMedico, this);


			// if (BaseDeDatos.Database["medicos"].ContainsKey(this.txtDNI.Text)) {
			if (BaseDeDatos.Database["medicos"].ContainsKey(this.txtDNI.Text)) {
				// medicoModificado = (Medico) BaseDeDatos.Database["medicos"][this.txtDNI.Text];
				// MessageBox.Show($"Se han guardado los cambios de Medico: {medicoModificado.Name} {medicoModificado.Lastname}");
				MessageBox.Show($"Se han guardado los cambios de Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}");
			}
			else {
				// medicoModificado = new Medico();
				// BaseDeDatos.Database["medicos"][medicoModificado.Dni] = medicoModificado;
				BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
				// MessageBox.Show($"Se ha creado un nuevo Medico: {medicoModificado.Name} {medicoModificado.Lastname}");
				MessageBox.Show($"Se ha creado un nuevo Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}");
			}


			// Save changes to the database
			BaseDeDatos.UpdateJsonFile();
		}

		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

		private void txtDiasDeAtencion_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}
	}
}
