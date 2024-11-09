using System.Windows;

namespace ClinicaMedica {
	public partial class MedicosModificar : Window {
		private static Medico? SelectedMedico;
		//---------------------public.constructors-------------------//
		public MedicosModificar() //Crear medico
		{
			InitializeComponent();
			SelectedMedico = null;
			txtDiasDeAtencion.ItemsSource = HorarioMedico.DiasDeLaSemanaComoLista();
		}

		public MedicosModificar(Medico selectedMedico) //Modificar medico
		{
			InitializeComponent();
			SelectedMedico = selectedMedico;
			SelectedMedico.MostrarseEnVentana(this);
		}
		

		//---------------------Erroraogsfa-------------------//
		public bool CheckHorasFormato() {
			 if (this.txtDiasDeAtencion.ItemsSource is not List<HorarioMedico> diasFromUI) {
				throw new InvalidOperationException("ItemsSource is not set or is not a List of HorarioMedico.");
			}

			foreach (var dia in diasFromUI) {
				// Check if HoraInicio and HoraFin have valid TimeOnly values
				// if (!string.IsNullOrWhiteSpace(dia.DiaSemana)) {
					if ( 
						Convert.ToInt8(dia.HoraInicio == null) + Convert.ToInt8(dia.HoraFin == null) == 1
					){
						MessageBox.Show($"Error. Si se opta por inicio, .");
					} 
					
					else if (
						(dia.HoraInicio == null)
						&& (dia.HoraFin == null)
					) {
						continue;
					}
					
					
					if (dia.HoraInicio == null) {
						MessageBox.Show(
							$"Formato inválido en HoraInicio para el día {dia.DiaSemana}. Asegúrese de que es un valor de tiempo válido.",
							"Error de Formato",
							MessageBoxButton.OK,
							MessageBoxImage.Error
						);
						return false;
					}

					if (dia.HoraFin == null) {
						MessageBox.Show(
							$"Formato inválido en HoraFin para el día {dia.DiaSemana}. Asegúrese de que es un valor de tiempo válido.",
							"Error de Formato",
							MessageBoxButton.OK,
							MessageBoxImage.Error
						);
						return false;
					}
				// }
			}

			// If all times are valid, return true
			return true;
		}




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
					 
			return CheckHorasFormato();
		}
		
		
		
		//---------------------botones.GuardarCambios-------------------//
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
