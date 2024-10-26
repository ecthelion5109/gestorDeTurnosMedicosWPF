using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace ClinicaMedica {
	public class BaseDeDatosSQL : IBaseDeDatos{
		private string connectionString = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;
		private SqlConnection MiConexion;
		
		
		public BaseDeDatosSQL() {
			MiConexion = new SqlConnection(connectionString);
			MiConexion.Open();
		}
		
		
		
		
		//------------------------CHECKERS----------------------//
		public bool CorroborarQueNoExistaMedico(string key){
			return false;
		}
		public bool CorroborarQueNoExistaPaciente(string key){
			return false;
		}
		public bool CorroborarQueNoExistaTurno(string key){
			return false;
		}
		
		//------------------------CREATE----------------------//
		public  OperationCode CreateMedico(Medico medico) {
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE Dni = @Dni";
			string insertQuery = "INSERT INTO Medico (Name, LastName, Especialidad, SueldoMinimoGarantizado, FechaIngreso, Dni) VALUES (@Name, @LastName, @Especialidad, @SueldoMinimoGarantizado, @FechaIngreso, @Dni)";
			// using (SqlConnection MiConexion = new SqlConnection(connectionString)) {
				SqlCommand checkCommand = new SqlCommand(checkQuery, MiConexion);
				checkCommand.Parameters.AddWithValue("@Dni", medico.Dni);
				// MiConexion.Open();
				if ((int)checkCommand.ExecuteScalar() > 0) {
					return OperationCode.YA_EXISTE;
				}
				SqlCommand sqlComando = new SqlCommand(insertQuery, MiConexion);
				sqlComando.Parameters.AddWithValue("@Name", medico.Name);
				sqlComando.Parameters.AddWithValue("@LastName", medico.LastName);
				sqlComando.Parameters.AddWithValue("@Especialidad", medico.Especialidad);
				sqlComando.Parameters.AddWithValue("@SueldoMinimoGarantizado", medico.SueldoMinimoGarantizado);
				sqlComando.Parameters.AddWithValue("@FechaIngreso", medico.FechaIngreso);
				sqlComando.Parameters.AddWithValue("@Dni", medico.Dni);
				sqlComando.ExecuteNonQuery();
			// }
			return OperationCode.CREATE_SUCCESS;
		}
		public  OperationCode CreatePaciente(Paciente medico) {
			string checkQuery = "SELECT COUNT(1) FROM Paciente WHERE Dni = @Dni";
			string insertQuery = "INSERT INTO Paciente (Name, LastName, FechaIngreso, Dni) VALUES (@Name, @LastName, @FechaIngreso, @Dni)";
			// using (SqlConnection MiConexion = new SqlConnection(connectionString)) {
				SqlCommand checkCommand = new SqlCommand(checkQuery, MiConexion);
				checkCommand.Parameters.AddWithValue("@Dni", medico.Dni);
				// MiConexion.Open();
				if ((int)checkCommand.ExecuteScalar() > 0) {
					return OperationCode.YA_EXISTE;
				}
				SqlCommand sqlComando = new SqlCommand(insertQuery, MiConexion);
				sqlComando.Parameters.AddWithValue("@Name", medico.Name);
				sqlComando.Parameters.AddWithValue("@LastName", medico.LastName);
				sqlComando.Parameters.AddWithValue("@FechaIngreso", medico.FechaIngreso);
				sqlComando.Parameters.AddWithValue("@Dni", medico.Dni);
				sqlComando.ExecuteNonQuery();
			// }
			return OperationCode.CREATE_SUCCESS;
		}

		public  OperationCode CreateTurno(Turno turno) {
			return OperationCode.SIN_DEFINIR;
		}


		//------------------------READ----------------------//
		public List<Medico> ReadMedicos() {
			List<Medico> medicoList = new List<Medico>();
			string query = "SELECT * FROM Medico";
			try {
				// using (SqlConnection MiConexion = new SqlConnection(connectionString)) {
					SqlCommand command = new SqlCommand(query, MiConexion);
					// MiConexion.Open();
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
				// }
			}
			catch (Exception ex) {
				MessageBox.Show("Error retrieving data: " + ex.Message);
			}
			return medicoList;
		}

        public List<Paciente> ReadPacientes() {
            List<Paciente> pacienteList = new List<Paciente>();
            string query = "SELECT * FROM Paciente";
            try
            {
                // using (SqlConnection MiConexion = new SqlConnection(connectionString)){
                    SqlCommand command = new SqlCommand(query, MiConexion);
                    // MiConexion.Open();
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
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message);
            }
            return pacienteList;
        }

        public List<Turno> ReadTurnos() {
			return new List<Turno>();
		}
        //------------------------UPDATE----------------------//
        public OperationCode UpdateMedico(Medico medico, string originalDni) {
			string updateQuery = "UPDATE Medico SET Name = @Name, LastName = @LastName, Especialidad = @Especialidad, SueldoMinimoGarantizado = @SueldoMinimoGarantizado,  FechaIngreso = @FechaIngreso, Dni = @Dni WHERE Dni = @originalDni";
			// using (SqlConnection MiConexion = new SqlConnection(connectionString)) {
				// MiConexion.Open();
				using (SqlCommand sqlComando = new SqlCommand(updateQuery, MiConexion)) {
					sqlComando.Parameters.AddWithValue("@Name", medico.Name);
					sqlComando.Parameters.AddWithValue("@LastName", medico.LastName);
					sqlComando.Parameters.AddWithValue("@Especialidad", medico.Especialidad);
					sqlComando.Parameters.AddWithValue("@SueldoMinimoGarantizado", medico.SueldoMinimoGarantizado);
					sqlComando.Parameters.AddWithValue("@FechaIngreso", medico.FechaIngreso);
					sqlComando.Parameters.AddWithValue("@Dni", medico.Dni);
					sqlComando.Parameters.AddWithValue("@originalDni", originalDni);
				sqlComando.ExecuteNonQuery();
				}
			// }

			return OperationCode.UPDATE_SUCCESS;
		}
        public OperationCode UpdatePaciente(Paciente paciente, string originalDni) {
			string updateQuery = "UPDATE Paciente SET Name = @Name, LastName = @LastName, FechaIngreso = @FechaIngreso, Dni = @Dni WHERE Dni = @originalDni";
			// using (SqlConnection MiConexion = new SqlConnection(connectionString)) {
				// MiConexion.Open();
				using (SqlCommand sqlComando = new SqlCommand(updateQuery, MiConexion)) {
					sqlComando.Parameters.AddWithValue("@Name", paciente.Name);
					sqlComando.Parameters.AddWithValue("@LastName", paciente.LastName);
					sqlComando.Parameters.AddWithValue("@FechaIngreso", paciente.FechaIngreso);
					sqlComando.Parameters.AddWithValue("@Dni", paciente.Dni);
					sqlComando.Parameters.AddWithValue("@originalDni", originalDni);
				sqlComando.ExecuteNonQuery();
				}
			// }

			return OperationCode.UPDATE_SUCCESS;
		}
        public OperationCode UpdateTurno(Turno turno) {
			return OperationCode.SIN_DEFINIR;
		}



		//------------------------DELETE----------------------//
		public OperationCode DeleteMedico(string medicoDni) {
			string query = "DELETE FROM Medico WHERE Dni = @Dni";

			// using (SqlConnection MiConexion = new SqlConnection(connectionString)) {
				SqlCommand command = new SqlCommand(query, MiConexion);
				command.Parameters.AddWithValue("@Dni", medicoDni);

				// MiConexion.Open();
				command.ExecuteNonQuery();
			// }

			return OperationCode.DELETE_SUCCESS;
		}
		public OperationCode DeletePaciente(string pacienteDni) {
			string query = "DELETE FROM Paciente WHERE Dni = @Dni";

			// using (SqlConnection MiConexion = new SqlConnection(connectionString)) {
				SqlCommand command = new SqlCommand(query, MiConexion);
				command.Parameters.AddWithValue("@Dni", pacienteDni);
				// MiConexion.Open();
				command.ExecuteNonQuery();
			// }

			return OperationCode.DELETE_SUCCESS;
		}

        public OperationCode DeleteTurno(string turnoId) {
			return OperationCode.SIN_DEFINIR;
		}







		//------------------------Fin.BaseDeDatosSQL----------------------//
	}
}
