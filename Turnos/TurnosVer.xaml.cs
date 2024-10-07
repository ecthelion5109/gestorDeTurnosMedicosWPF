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
    public partial class TurnosVer : Window
    {
        public TurnosVer()
        {
            InitializeComponent();
            // Cargar los turnos al iniciar la ventana
            CargarTurnos(DateTime.Today);
        }

        // Evento que se dispara al cambiar la fecha en el calendario
        private void CalendarioTurnos_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? fechaSeleccionada = CalendarioTurnos.SelectedDate;

            if (fechaSeleccionada.HasValue)
            {
                // Llama a un método para cargar turnos de la fecha seleccionada
                CargarTurnos(fechaSeleccionada.Value);
            }
        }

        // Método para cargar los turnos del día
        private void CargarTurnos(DateTime fecha)
        {
            var turnos = new List<Turno>();

            // Generar 10 turnos por cada hora de 8 a 17
            for (int hora = 8; hora <= 17; hora++)
            {
                for (int turno = 1; turno <= 10; turno++)
                {
                    turnos.Add(new Turno
                    {
                        Hora = $"{hora:00}:00",
                        NumeroTurno = turno,
                        Estado = "Disponible" // Puedes modificar esto según el estado real
                    });
                }
            }

            // Asignar la lista de turnos al DataGrid
            DataGridTurnos.ItemsSource = turnos;
        }
    }

    // Clase Turno para representar los turnos
    public class Turno
    {
        public string Hora { get; set; }
        public int NumeroTurno { get; set; }
        public string Estado { get; set; }
    }
}