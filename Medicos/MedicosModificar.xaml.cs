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
		private static Medico ?SelectedMedico;
		
		public MedicosModificar() {
			InitializeComponent();
			txtDiasDeAtencion.ItemsSource = (new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" }).Select(dia => new HorarioMedico { DiaSemana = dia }).ToList();
			
			
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
			this.txtDiasDeAtencion.ItemsSource = SelectedMedico.GetDiasDeAtencionList();
		}

		private void AsignarValoresAMedico(Medico SelectedMedico) {
			// MessageBox.Show(SelectedMedico.Name);
			
			SelectedMedico.Name = this.txtNombre.Text;
			SelectedMedico.Lastname = this.txtApellido.Text;
			SelectedMedico.Dni = this.txtDNI.Text;
			SelectedMedico.Provincia = this.txtProvincia.Text;
			SelectedMedico.Domicilio = this.txtDomicilio.Text;
			SelectedMedico.Localidad = this.txtLocalidad.Text;
			SelectedMedico.Specialidad = this.txtEspecialidad.Text;
			if (this.txtRealizaGuardia.IsChecked != null) {
				SelectedMedico.Guardia = (bool)this.txtRealizaGuardia.IsChecked;
			}
			if (this.txtFechaIngreso.SelectedDate != null){ 
				SelectedMedico.FechaIngreso = (DateTime)this.txtFechaIngreso.SelectedDate; 
			}
			if (double.TryParse(this.txtSueldoMinGarant.Text, out double sueldo)) {
				SelectedMedico.SueldoMinimoGarantizado = sueldo;
			}

			var diasDeAtencion = new Dictionary<string, Horario>();

			foreach (var item in txtDiasDeAtencion.ItemsSource) {
				if (item is HorarioMedico diaAtencion) {
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
			if (SelectedMedico == null){
				SelectedMedico = new Medico();
			}
			string originalDni = SelectedMedico.Dni;

			AsignarValoresAMedico(SelectedMedico);
			
			if (SelectedMedico.Dni == null) {
				return;
			}

			//MessageBox.Show($"Selected medico: {SelectedMedico.Name} {SelectedMedico.Lastname} {SelectedMedico.Dni}");
			if (originalDni == SelectedMedico.Dni) {
				//MessageBox.Show("Caso de actualizar un médico sin cambiar el DNI");
				BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
				MessageBox.Show($"Se han guardado los cambios de Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}");
			}
			else {
				if (BaseDeDatos.Database["medicos"].ContainsKey(SelectedMedico.Dni)) {
					//MessageBox.Show(" Caso de cambiar el DNI");
					MessageBox.Show($"Error: Ya existe un médico con DNI: {SelectedMedico.Dni}");
					return; // Salir si el nuevo DNI ya existe
				}
				else if (originalDni != null && BaseDeDatos.Database["medicos"].ContainsKey(originalDni)) {
					//MessageBox.Show("Eliminar el médico con el DNI original y agregar el nuevo");
					BaseDeDatos.Database["medicos"].Remove(originalDni);
					BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
					MessageBox.Show($"Se ha actualizado el DNI(clave primaria) de: {SelectedMedico.Name} {SelectedMedico.Lastname}");
				}
				else {
					//MessageBox.Show("Crear un nuevo médico si no existe con el DNI original ni el nuevo");
					BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
					MessageBox.Show($"Se ha agregado el Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}");
				}
			}

			// Guardar los cambios en el archivo JSON
			BaseDeDatos.UpdateJsonFile();
		}

		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}
	}
}
