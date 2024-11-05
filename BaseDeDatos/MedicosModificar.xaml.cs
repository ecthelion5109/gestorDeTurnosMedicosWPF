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
	public partial class MedicosModificar : Window {
		private static Medico? SelectedMedico;
		//---------------------public.constructors-------------------//
		public MedicosModificar() //Crear medico
		{
			InitializeComponent();
			SelectedMedico = null;
			txtDiasDeAtencion.ItemsSource = (new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" }).Select(dia => new HorarioMedico { DiaSemana = dia }).ToList();
			//botonMultiUso.Click += ButtonCrearMedico;
			//botonMultiUso.Content = "Crear";
		}

		public MedicosModificar(Medico selectedMedico) //Modificar medico
		{
			InitializeComponent();
			SelectedMedico = selectedMedico;
			this.txtDiasDeAtencion.ItemsSource = SelectedMedico.GetDiasDeAtencionListForUI();
			this.txtNombre.Text = SelectedMedico.Name;
			this.txtApellido.Text = SelectedMedico.LastName;
			this.txtDni.Text = SelectedMedico.Dni;
            this.txtTelefono.Text = SelectedMedico.Telefono;
            this.txtProvincia.Text = SelectedMedico.Provincia;
			this.txtDomicilio.Text = SelectedMedico.Domicilio;
			this.txtLocalidad.Text = SelectedMedico.Localidad;
			this.txtEspecialidad.Text = SelectedMedico.Especialidad;
			this.txtFechaIngreso.SelectedDate = SelectedMedico.FechaIngreso;
			this.txtSueldoMinGarant.Text = SelectedMedico.SueldoMinimoGarantizado.ToString();
			this.txtRealizaGuardia.IsChecked = SelectedMedico.Guardia;
			//botonMultiUso.Click += ButtonModificarMedico;
			//botonMultiUso.Content = "Modificar";
		}
		

		//---------------------botones.GuardarCambios-------------------//
		bool CorroborarUserInputEsSeguro(){
			return !(string.IsNullOrEmpty(this.txtSueldoMinGarant.Text) ||
					 string.IsNullOrEmpty(this.txtDni.Text) ||
					 this.txtFechaIngreso.SelectedDate is null ||
					 this.txtRealizaGuardia.IsChecked is null);
		}
		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			//---------Crear-----------//
			if (SelectedMedico is null) {
				if (CorroborarUserInputEsSeguro()) {
					SelectedMedico = new Medico(this);
					App.BaseDeDatos.CreateMedico(SelectedMedico);
				}
				else {
					MessageBox.Show($"Error: Faltan datos obligatorios por completar.");
				}
			}
			//---------Modificar-----------//
			else {
				string originalDni = SelectedMedico.Dni;
				if (CorroborarUserInputEsSeguro()) {
					SelectedMedico.AsignarDatosFromWindow(this);
					App.BaseDeDatos.UpdateMedico(SelectedMedico, originalDni);
				}
				else {
					MessageBox.Show($"Error: Faltan datos obligatorios por completar.");
				}
			}
		}


		//---------------------botones.Eliminar-------------------//
		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			//---------Checknulls-----------//
			if (SelectedMedico is null || SelectedMedico.Dni is null) {
				MessageBox.Show($"No hay item seleccionado.");
				return;
			}
			//---------confirmacion-----------//
			if (MessageBox.Show($"¿Está seguro que desea eliminar este médico? {SelectedMedico.Name}",
				"Confirmar Eliminación",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			) != MessageBoxResult.OK) {
				return;
			}
			//---------Eliminar-----------//
			if (App.BaseDeDatos.DeleteMedico(SelectedMedico.Dni)){
				this.Close(); // this.NavegarA<Medicos>();
			}
		}
		//---------------------botones.Salida-------------------//
		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.Close(); // this.NavegarA<Medicos>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		//------------------------Fin---------------------------//
	}
}
