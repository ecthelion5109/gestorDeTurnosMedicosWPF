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
	/// Lógica de interacción para MedicosVer.xaml
	/// </summary>
	public partial class MedicosVer : Window {
		public List<Medico> Medicos { get; set; }
		
		
		public MedicosVer() {
			InitializeComponent();
			// Medicos = new List<Medico>{
				// new Medico { Dni = "87654321", Name = "Dr. Roxana", Lastname = "Gómez", Specialidad = "Cardiología" },
				// new Medico { Dni = "25654321", Name = "Dr. Carlos", Lastname = "Merkier", Specialidad = "Gastroenterología" }
			// };
			Medicos = BaseDeDatos.Database["medicos"]
						 .Values
						 .Cast<Medico>()   // Casting the object values to Medico
						 .ToList();


			// Establecer el DataContext
			DataContext = this;
		}

		private void ButtonAgregar(object sender, RoutedEventArgs e) {

		}

		private void ButtonModificar(object sender, RoutedEventArgs e) {

		}

		private void ButtonEliminar(object sender, RoutedEventArgs e) {

		}

		private void ButtonCancelar(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

		private void ButtonSalir(object sender, RoutedEventArgs e) {
			this.Salir();
		}
	}
}
