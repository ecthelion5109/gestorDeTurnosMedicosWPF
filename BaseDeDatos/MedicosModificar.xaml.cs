using System.Windows;

namespace ClinicaMedica {
	public partial class MedicosModificar : Window {
		private static Medico? SelectedMedico;
		//---------------------public.constructors-------------------//
		public MedicosModificar() //Crear medico
		{
			InitializeComponent();
			SelectedMedico = null;
			txtDiasDeAtencion.ItemsSource = (new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" }).Select(dia => new HorarioMedico { DiaSemana = dia }).ToList();
		}

		public MedicosModificar(Medico selectedMedico) //Modificar medico
		{
			InitializeComponent();
			SelectedMedico = selectedMedico;
			SelectedMedico.MostrarseEnVentana(this);
		}
		

		//---------------------botones.GuardarCambios-------------------//
		private bool CamposCompletadosCorrectamente(){
			if (
				this.txtSueldoMinGarant.Text is null 
				|| this.txtDni.Text is null 
				|| this.txtFechaIngreso.SelectedDate is null 
				|| this.txtRealizaGuardia.IsChecked is null
			) {
				MessageBox.Show($"Error: Faltan datos obligatorios por completar.", "Faltan datos.", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			 }
					 
			if (!Int64.TryParse(this.txtDni.Text, out _)){
                MessageBox.Show($"El dni no es un numero entero valido.");
				return false;
            }
					 
			if (!Double.TryParse(this.txtSueldoMinGarant.Text, out _)) {
				MessageBox.Show("El sueldo minimo no es un número decimal válido. Use la coma (,) como separador decimal.");
				return false;
			}
			
			//List<HorarioMedico> diasFromUI = (List<HorarioMedico>)window.txtDiasDeAtencion.ItemsSource;
			// foreach (var dia in diasFromUI) {
			// if (!string.IsNullOrWhiteSpace(dia.InicioHorario) && !string.IsNullOrWhiteSpace(dia.FinHorario)) {
			// DiasDeAtencion[dia.DiaSemana] = new Horario(dia.InicioHorario, dia.FinHorario);
			// }
			// if (!TimeOnly.TryParse(this.txtDiasDeAtencion.Text, out _)) {
			// MessageBox.Show("El sueldo minimo no es un número decimal válido. Use la coma (,) como separador decimal.");
			// return false;
			// }
			// }


			return true;
		}
		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			App.PlayClickJewel();
			if (!CamposCompletadosCorrectamente()) {
				return;
			}
			
			if (SelectedMedico is null) {
				SelectedMedico = new Medico(this);
				if ( App.BaseDeDatos.CreateMedico(SelectedMedico)){
					this.Cerrar();
				}						
			}
			else {
				SelectedMedico.TomarDatosDesdeVentana(this);
				if ( App.BaseDeDatos.UpdateMedico(SelectedMedico)){
					this.Cerrar();
				}			
			}
		}


		//---------------------botones.Eliminar-------------------//
		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			App.PlayClickJewel();
			//---------Checknulls-----------//
			if (SelectedMedico is null || SelectedMedico.Dni is null) {
				MessageBox.Show($"No hay item seleccionado.");
				return;
			}
			//---------confirmacion-----------//
			if (MessageBox.Show($"¿Está seguro que desea eliminar este médico? {SelectedMedico.Name}", "Confirmar Eliminación", MessageBoxButton.OKCancel, MessageBoxImage.Warning ) != MessageBoxResult.OK) {
				return;
			}
			//---------Eliminar-----------//
			if (App.BaseDeDatos.DeleteMedico(SelectedMedico)){
				this.Cerrar(); // this.NavegarA<Medicos>();
			}
		}
		//---------------------botones.Salida-------------------//
		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.Cerrar(); // this.NavegarA<Medicos>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
		//------------------------Fin---------------------------//
	}
}
