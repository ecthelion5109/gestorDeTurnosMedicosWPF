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
		
		//---------------------public.constructors-------------------//
		public MedicosModificar() //Crear medico
		{
			InitializeComponent();
			
			txtDiasDeAtencion.ItemsSource = (new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" }).Select(dia => new HorarioMedico { DiaSemana = dia }).ToList();
			
			botonMultiUso.Click += ButtonCrearMedico;
			botonMultiUso.Content = "Crear";
		}
		
		public MedicosModificar(Medico selectedMedico) //Modificar medico
		{
			InitializeComponent();
			SelectedMedico = selectedMedico;
			
			// this.txtDiasDeAtencion.ItemsSource = SelectedMedico.DiasDeAtencion;
			this.txtDiasDeAtencion.ItemsSource = SelectedMedico.GetDiasDeAtencionListForUI();
			this.txtNombre.Text = SelectedMedico.Name;
			this.txtApellido.Text = SelectedMedico.Lastname;
			this.txtDNI.Text = SelectedMedico.Dni;
			this.txtProvincia.Text = SelectedMedico.Provincia;
			this.txtDomicilio.Text = SelectedMedico.Domicilio;
			this.txtLocalidad.Text = SelectedMedico.Localidad;
			this.txtEspecialidad.Text = SelectedMedico.Specialidad;
			this.txtFechaIngreso.SelectedDate = SelectedMedico.FechaIngreso;
			this.txtSueldoMinGarant.Text = SelectedMedico.SueldoMinimoGarantizado.ToString();
			this.txtRealizaGuardia.IsChecked = SelectedMedico.Guardia;
			
			botonMultiUso.Click += ButtonModificarMedico;
			botonMultiUso.Content = "Modificar";
		}

		//---------------------private.Procedures-------------------//
		private bool TryAsignarValoresAMedico(Medico SelectedMedico) {
			// MessageBox.Show(SelectedMedico.Name);
			if (string.IsNullOrEmpty(this.txtDNI.Text)) {
				return false; 
			}

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

			//SelectedMedico.DiasDeAtencion = (List<HorarioMedico>) this.txtDiasDeAtencion.ItemsSource;
			SelectedMedico.UpdateDiasDeAtencionFromUI( (List<HorarioMedico>) this.txtDiasDeAtencion.ItemsSource);
			// foreach (var item in txtDiasDeAtencion.ItemsSource) {
				// if (item is HorarioMedico diaAtencion) {
					// var dia = diaAtencion.DiaSemana;
					// var start = diaAtencion.InicioHorario;
					// var end = diaAtencion.FinHorario;
					// if (!string.IsNullOrEmpty(dia) && !string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end)) {
						// SelectedMedico.DiasDeAtencion[dia] = new Horario(start, end);
					// }
				// }
			// }
			return true;
		}



		//---------------------botones.Crear-------------------//
		private void ButtonCrearMedico(object sender, RoutedEventArgs e) {
			SelectedMedico = new Medico();
			if (!TryAsignarValoresAMedico(SelectedMedico)){
				return;
			};
			if ( string.IsNullOrEmpty(SelectedMedico.Dni) ) {
				MessageBox.Show($"Campo DNI es mandatorio.");
				return;
			}
			else if (BaseDeDatos.Database["medicos"].ContainsKey(SelectedMedico.Dni)) {
				MessageBox.Show($"Error: Ya existe un médico con DNI: {SelectedMedico.Dni}");
				return;
			}
			else {
				BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
				MessageBox.Show($"Se ha agregado el Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}");
			}
			
			// Guardar los cambios en el archivo JSON
			BaseDeDatos.UpdateJsonFile();
		}


		//---------------------botones.Modificar-------------------//
		private void ButtonModificarMedico(object sender, RoutedEventArgs e) {
			if (SelectedMedico == null || SelectedMedico.Dni == null){
				MessageBox.Show($"Escenario imposible. No se seleccionó ningun medico.");
				return;
			}
			string originalDni = SelectedMedico.Dni;
			if (!TryAsignarValoresAMedico(SelectedMedico)){
				return;
			};
			
			if (string.IsNullOrEmpty(SelectedMedico.Dni)) {
				MessageBox.Show($"El DNI es obligatorio.");
				return;
			} 
			else if (originalDni == SelectedMedico.Dni) {
				MessageBox.Show($"Se han guardado los cambios de Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}");
			}
			else {
				BaseDeDatos.Database["medicos"].Remove(originalDni);
				BaseDeDatos.Database["medicos"][SelectedMedico.Dni] = SelectedMedico;
				MessageBox.Show($"Se ha actualizado el DNI(clave primaria) de: {SelectedMedico.Name} {SelectedMedico.Lastname}");
			}
			
			// Guardar los cambios en el archivo JSON
			BaseDeDatos.UpdateJsonFile();
		}

		//---------------------botones.VolverAtras-------------------//
		private void ButtonVolver(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

	}
}
