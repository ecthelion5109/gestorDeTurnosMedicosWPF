using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
    /// Lógica de interacción para Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window
    {
        private static Paciente? SelectedPaciente;


        public Pacientes()
        {
            InitializeComponent();
            // generar


            if (BaseDeDatos.TIPO == DatabaseType.JSON)
            {
                PacienteListView.ItemsSource = BaseDeDatos.Database["pacientes"].Values.Cast<Paciente>().ToList();
            }
            else
            {
                //averiguar como mierda conectar esto. "comomireda se chupa la columnas de paciente"
                PacienteListView.ItemsSource = LoadPacienteData();
            }

        }



<<<<<<< Updated upstream
		private void ButtonPacientesModificar(object sender, RoutedEventArgs e) {
			this.NavegarA<PacientesModificar>();
		}
=======
>>>>>>> Stashed changes

        private List<Paciente> LoadPacienteData()
        {
            // Get the connection string from the config file
            string miConexion = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;

            // List to store Medico instances
            List<Paciente> pacienteList = new List<Paciente>();

            // SQL query to retrieve data from Paciente table
            string query = "SELECT * FROM Paciente";

<<<<<<< Updated upstream
		}

		private void ButtonPacientesAgregar(object sender, RoutedEventArgs e) {

        }
=======
            try
            {
                using (SqlConnection connection = new SqlConnection(miConexion))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Paciente paciente = new Paciente
                        {
                            Dni = reader["dni"]?.ToString(),
                            Name = reader["nombre"]?.ToString(),
                            Lastname = reader["apellido"]?.ToString(),
                            Provincia = reader["provincia"]?.ToString(),
                            Domicilio = reader["domicilio"]?.ToString(),
                            Localidad = reader["localidad"]?.ToString(),
                            Specialidad = reader["especialidad"]?.ToString(),
                            Telefono = reader["telefono"]?.ToString(),
                            Guardia = reader["guardia"] != DBNull.Value ? Convert.ToBoolean(reader["guardia"]) : false,
                            FechaIngreso = reader["fecha_ingreso"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_ingreso"]) : (DateTime?)null,
                            SueldoMinimoGarantizado = reader["sueldo_minimo_garantizado"] != DBNull.Value ? Convert.ToDouble(reader["sueldo_minimo_garantizado"]) : 0.0
                        };
                        pacienteList.Add(paciente);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message);
            }

            return pacienteList;
        }






        private void ButtonAgregar(object sender, RoutedEventArgs e)
        {
            this.NavegarA<PacientesModificar>();

        }

        private void PacienteListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PacienteListView.SelectedItem != null)
            {
                SelectedPaciente = (Paciente)PacienteListView.SelectedItem;
                buttonModificar.IsEnabled = true;
                buttonEliminar.IsEnabled = true;
                //MessageBox.Show($"Selected Paciente DNI: {SelectedPaciente.Dni}");
            }
            else
            {
                buttonModificar.IsEnabled = false;
                buttonEliminar.IsEnabled = false;
            }
        }

        private void ButtonModificar(object sender, RoutedEventArgs e)
        {
            //this.NavegarA<PacientesModificar>();
            if (SelectedPaciente != null)
            {
                PacientesModificar nuevaVentana = new(SelectedPaciente);
                Application.Current.MainWindow = nuevaVentana;  // Establecer la nueva ventana como la principal
                nuevaVentana.Show();  // Mostrar la nueva ventana
                this.Close();  // Cerrar la ventana actual
            }
        }

        private void ButtonEliminar(object sender, RoutedEventArgs e)
        {
            // Muestra el MessageBox con botones de Aceptar y Cancelar
            if (SelectedPaciente != null && SelectedPaciente.Dni != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"¿Está seguro que desea eliminar este paciente? {SelectedPaciente.Name}",   // Mensaje
                    "Confirmar Eliminación",                         // Título del cuadro
                    MessageBoxButton.OKCancel,                       // Botones (OK y Cancelar)
                    MessageBoxImage.Warning                          // Tipo de icono (opcional)
                );

                if (result == MessageBoxResult.OK)
                {
                    BaseDeDatos.Database["pacientes"].Remove(SelectedPaciente.Dni);

                    // regenerar
                    PacienteListView.ItemsSource = BaseDeDatos.Database["pacientes"].Values.Cast<Paciente>().ToList(); // Reassign to refresh the ListView
                                                                                                                       // MessageBox.Show("El paciente ha sido eliminado.");

                    // Save changes to the database
                    BaseDeDatos.UpdateJsonFile();
                }
            }
        }

        private void ButtonHome(object sender, RoutedEventArgs e)
        {
            this.NavegarA<MainWindow>();
        }

        private void ButtonSalir(object sender, RoutedEventArgs e)
        {
            this.Salir();
        }
>>>>>>> Stashed changes
    }
}
