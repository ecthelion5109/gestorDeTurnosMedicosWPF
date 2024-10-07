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

namespace ClinicaMedica
{
    /// <summary>
    /// Lógica de interacción para MedicosAgregar.xaml
    /// </summary>
	
    public partial class MedicosAgregar : Window
    {
		private List<DateTime> diasAtencion = new List<DateTime>();
		
        public MedicosAgregar()
        {
            InitializeComponent();
        }
		
		public void ButtonSalir(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}
		
		public void ButtonAgregar(object sender, RoutedEventArgs e) {

		}
		
		public void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

		

		private void MetodoBotonTestearJsonGuardar(object sender, RoutedEventArgs e) {

		}


		// Método para agregar una fecha seleccionada a la lista
		private void AgregarDiaAtencion_Click(object sender, RoutedEventArgs e)
		{
			if (datePickerDiasAtencion.SelectedDate.HasValue)
			{
				DateTime fechaSeleccionada = datePickerDiasAtencion.SelectedDate.Value;

				// Verificar que la fecha no esté ya en la lista
				if (!diasAtencion.Contains(fechaSeleccionada))
				{
					diasAtencion.Add(fechaSeleccionada);
					listBoxDiasAtencion.Items.Add(fechaSeleccionada.ToShortDateString()); // Mostrar en el ListBox
				}
				else
				{
					MessageBox.Show("Este día ya ha sido agregado.");
				}
			}
			else
			{
				MessageBox.Show("Por favor, seleccione una fecha.");
			}
		}

		// Método para eliminar una fecha seleccionada de la lista
		private void EliminarDiaAtencion_Click(object sender, RoutedEventArgs e)
		{
			if (listBoxDiasAtencion.SelectedItem != null)
			{
				string fechaSeleccionadaStr = listBoxDiasAtencion.SelectedItem.ToString();
				DateTime fechaSeleccionada = DateTime.Parse(fechaSeleccionadaStr);

				// Eliminar la fecha de la lista
				diasAtencion.Remove(fechaSeleccionada);
				listBoxDiasAtencion.Items.Remove(fechaSeleccionadaStr);
			}
			else
			{
				MessageBox.Show("Por favor, seleccione un día para eliminar.");
			}
		}

		// Obtener la lista de fechas de días de atención
		public List<DateTime> ObtenerDiasAtencion()
		{
			return diasAtencion;
		}
	}
}
