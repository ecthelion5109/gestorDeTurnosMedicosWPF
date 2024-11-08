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
		bool FaltanCamposPorCompletar(){
			return !(string.IsNullOrEmpty(this.txtSueldoMinGarant.Text) ||
					 string.IsNullOrEmpty(this.txtDni.Text) ||
					 this.txtFechaIngreso.SelectedDate is null ||
					 this.txtRealizaGuardia.IsChecked is null);
		}
		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			App.PlayClickJewel();
			//---------Crear-----------//
			if (SelectedMedico is null) {
				if (FaltanCamposPorCompletar()) {
					SelectedMedico = new Medico(this);
					if ( App.BaseDeDatos.CreateMedico(SelectedMedico)){
						this.Cerrar();
					}						
				}
				else {
					MessageBox.Show($"Error: Faltan datos obligatorios por completar.");
				}
			}
			//---------Modificar-----------//
			else {
				//string originalDni = SelectedMedico.Dni;
				if (FaltanCamposPorCompletar()) {
					SelectedMedico.TomarDatosDesdeVentana(this);
					App.BaseDeDatos.UpdateMedico(SelectedMedico);
				}
				else {
					MessageBox.Show($"Error: Faltan datos obligatorios por completar.");
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
			if (MessageBox.Show($"¿Está seguro que desea eliminar este médico? {SelectedMedico.Name}",
				"Confirmar Eliminación",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			) != MessageBoxResult.OK) {
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
