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



		private void AsignarValoresAMedico() {
			SelectedMedico.Name = this.txtNombre.Text;
			SelectedMedico.Lastname = this.txtApellido.Text;
			SelectedMedico.Dni = this.txtDNI.Text;
			SelectedMedico.Provincia = this.txtProvincia.Text;
			SelectedMedico.Domicilio = this.txtDomicilio.Text;
			SelectedMedico.Localidad = this.txtLocalidad.Text;
			SelectedMedico.Specialidad = this.txtEspecialidad.Text;
			SelectedMedico.Guardia = (bool)this.txtRealizaGuardia.IsChecked;
			SelectedMedico.FechaIngreso = (DateTime)this.txtFechaIngreso.SelectedDate;
			SelectedMedico.SueldoMinimoGarantizado = double.Parse(this.txtSueldoMinGarant.Text);

			var diasDeAtencion = new Dictionary<string, Horario>();

			foreach (var item in txtDiasDeAtencion.ItemsSource) {
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

			SelectedMedico.DiasDeAtencion = diasDeAtencion;
		}

		private void ButtonGuardarCambios(object sender, RoutedEventArgs e) {
			string originalDni = SelectedMedico.Dni;

			AsignarValoresAMedico();

			//MessageBox.Show($"Selected medico: {SelectedMedico.Name} {SelectedMedico.Lastname} {SelectedMedico.Dni}");
			if (originalDni == SelectedMedico.Dni) {
				// Caso de actualizar un médico sin cambiar el DNI
				//MessageBox.Show("Caso de actualizar un médico sin cambiar el DNI");
				BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
				MessageBox.Show($"Se han guardado los cambios de Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}");
			}
			else {
				// Caso de cambiar el DNI
				if (BaseDeDatos.Database["medicos"].ContainsKey(SelectedMedico.Dni)) {
					//MessageBox.Show(" Caso de cambiar el DNI");
					MessageBox.Show($"Error: Ya existe un médico con DNI: {SelectedMedico.Dni}");
					return; // Salir si el nuevo DNI ya existe
				}
				else if (originalDni != null && BaseDeDatos.Database["medicos"].ContainsKey(originalDni)) {
					// Eliminar el médico con el DNI original y agregar el nuevo
					//MessageBox.Show("Eliminar el médico con el DNI original y agregar el nuevo");
					BaseDeDatos.Database["medicos"].Remove(originalDni);
					BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
					MessageBox.Show($"Se ha actualizado el DNI(clave primaria) del Medico a: {SelectedMedico.Name} {SelectedMedico.Lastname}");
				}
				else {
					// Crear un nuevo médico si no existe con el DNI original ni el nuevo
					//MessageBox.Show("Crear un nuevo médico si no existe con el DNI original ni el nuevo");
					BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
					MessageBox.Show($"Se ha agregado el nuevo Medico a: {SelectedMedico.Name} {SelectedMedico.Lastname}");
				}
			}

			// Guardar los cambios en el archivo JSON
			BaseDeDatos.UpdateJsonFile();
		}


		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

		private void txtDiasDeAtencion_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}
	}
}
