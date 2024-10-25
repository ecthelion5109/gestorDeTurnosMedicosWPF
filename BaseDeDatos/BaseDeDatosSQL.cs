using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace ClinicaMedica {
	public class BaseDeDatosSQL {
		public static string connectionString = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;
		
		
		//------------------------CREATE----------------------//
		public static OperationCode CreateMedico(Medico medico) {
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE Dni = @Dni";
			string insertQuery = "INSERT INTO Medico (Name, LastName, Especialidad, SueldoMinimoGarantizado, FechaIngreso, Dni) VALUES (@Name, @LastName, @Especialidad, @SueldoMinimoGarantizado, @FechaIngreso, @Dni)";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
				checkCommand.Parameters.AddWithValue("@Dni", medico.Dni);
				connection.Open();
				if ((int)checkCommand.ExecuteScalar() > 0) {
					return OperationCode.YA_EXISTE;
				}
				SqlCommand sqlComando = new SqlCommand(insertQuery, connection);
				sqlComando.Parameters.AddWithValue("@Name", medico.Name);
				sqlComando.Parameters.AddWithValue("@LastName", medico.LastName);
				sqlComando.Parameters.AddWithValue("@Especialidad", medico.Especialidad);
				sqlComando.Parameters.AddWithValue("@SueldoMinimoGarantizado", medico.SueldoMinimoGarantizado);
				sqlComando.Parameters.AddWithValue("@FechaIngreso", medico.FechaIngreso);
				sqlComando.Parameters.AddWithValue("@Dni", medico.Dni);
				// Add parameters for all other fields you want to insert
				sqlComando.ExecuteNonQuery();
			}
			return OperationCode.CREATE_SUCCESS;
		}



		//------------------------READ----------------------//
		public static List<Medico> ReadMedicos() {
			List<Medico> medicoList = new List<Medico>();
			string query = "SELECT * FROM Medico";
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
					SqlCommand command = new SqlCommand(query, connection);
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read()) {
						Medico medico = new Medico {
							Dni = reader["Dni"]?.ToString(),
							Name = reader["Name"]?.ToString(),
							LastName = reader["LastName"]?.ToString(),
							Provincia = reader["Provincia"]?.ToString(),
							Domicilio = reader["Domicilio"]?.ToString(),
							Localidad = reader["Localidad"]?.ToString(),
							Especialidad = reader["Especialidad"]?.ToString(),
							Telefono = reader["Telefono"]?.ToString(),
							Guardia = reader["Guardia"] != DBNull.Value ? Convert.ToBoolean(reader["Guardia"]) : false,
							FechaIngreso = reader["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["FechaIngreso"]) : (DateTime?)null,
							SueldoMinimoGarantizado = reader["SueldoMinimoGarantizado"] != DBNull.Value ? Convert.ToDouble(reader["SueldoMinimoGarantizado"]) : 0.0
						};
						medicoList.Add(medico);
					}
					reader.Close();
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error retrieving data: " + ex.Message);
			}
			return medicoList;
		}

        public static List<Paciente> ReadPacientes()
        {
            List<Paciente> pacienteList = new List<Paciente>();
            string query = "SELECT * FROM Paciente";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Paciente paciente = new Paciente
                        {
                            Dni = reader["Dni"]?.ToString(),
                            Name = reader["Name"]?.ToString(),
                            LastName = reader["LastName"]?.ToString(),
							FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null,
                            Domicilio = reader["Domicilio"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            Provincia = reader["Provincia"]?.ToString(),                          
                            Localidad = reader["Localidad"]?.ToString(),                            
                            Telefono = reader["Telefono"]?.ToString(),                            
                            FechaIngreso = reader["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["FechaIngreso"]) : (DateTime?)null,
                           
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
        //------------------------UPDATE----------------------//

        //public CorroborororarDNIIntegrigrty(){




        //}


        public static OperationCode UpdateMedico(Medico medico, string originalDni) {
			string updateQuery = "UPDATE Medico SET Name = @Name, LastName = @LastName, Especialidad = @Especialidad, SueldoMinimoGarantizado = @SueldoMinimoGarantizado,  FechaIngreso = @FechaIngreso, Dni = @Dni WHERE Dni = @originalDni";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				// try {
					connection.Open();

					// Proceed with the update if no conflicts are found
					using (SqlCommand sqlComando = new SqlCommand(updateQuery, connection)) {
						sqlComando.Parameters.AddWithValue("@Name", medico.Name);
						sqlComando.Parameters.AddWithValue("@LastName", medico.LastName);
						sqlComando.Parameters.AddWithValue("@Especialidad", medico.Especialidad);
						sqlComando.Parameters.AddWithValue("@SueldoMinimoGarantizado", medico.SueldoMinimoGarantizado);
						sqlComando.Parameters.AddWithValue("@FechaIngreso", medico.FechaIngreso);
						sqlComando.Parameters.AddWithValue("@Dni", medico.Dni);
						sqlComando.Parameters.AddWithValue("@originalDni", originalDni);
					sqlComando.ExecuteNonQuery();
					}
				//}
				// catch (SqlException) {
					// Handle SQL-related exceptions if needed
					// return OperationCode.ERROR;
				// }
			}

			return OperationCode.UPDATE_SUCCESS;
		}



		//------------------------DELETE----------------------//
		public static OperationCode DeleteMedico(string medicoDni) {
			string query = "DELETE FROM Medico WHERE Dni = @Dni";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@Dni", medicoDni);

				connection.Open();
				command.ExecuteNonQuery();
			}

			return OperationCode.DELETE_SUCCESS;
		}








		//------------------------Fin.BaseDeDatosSQL----------------------//
	}
}
