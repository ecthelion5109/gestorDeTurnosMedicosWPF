using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace ClinicaMedica {
	public class BaseDeDatosSQL : IBaseDeDatos{
		static public string connectionString = ConfigurationManager.ConnectionStrings["ConexionAClinicaMedica"].ConnectionString;
		
		
		public BaseDeDatosSQL() {
		}

		//------------------------CHECKERS----------------------//
		public bool CorroborarQueNoExistaMedico(string key) {
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE Dni = @Dni";
			using (SqlConnection connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection)) {
					checkCommand.Parameters.AddWithValue("@Dni", key);
					return (int)checkCommand.ExecuteScalar() > 0;
				}
			}
		}
		public bool CorroborarQueNoExistaPaciente(string key) {
			string checkQuery = "SELECT COUNT(1) FROM Paciente WHERE Dni = @Dni";
			using (SqlConnection connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection)) {
					checkCommand.Parameters.AddWithValue("@Dni", key);
					return (int)checkCommand.ExecuteScalar() > 0;
				}
			}
		}

		public bool CorroborarQueNoExistaTurno(string key) {
			return false;
		}




		//------------------------CREATE.Medico----------------------//
		public OperationCode CreateMedico(Medico instancia) {
			string insertQuery = @"
				INSERT INTO Medico (Name, LastName, Dni, Provincia, Domicilio, Localidad, Especialidad, Telefono, Guardia, FechaIngreso, SueldoMinimoGarantizado) 
				VALUES (@Name, @LastName, @Dni, @Provincia, @Domicilio, @Localidad, @Especialidad, @Telefono, @Guardia, @FechaIngreso, @SueldoMinimoGarantizado)";
			
			using (SqlConnection connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (SqlCommand sqlComando = new SqlCommand(insertQuery, connection)) {
					sqlComando.Parameters.AddWithValue("@Name", instancia.Name);
					sqlComando.Parameters.AddWithValue("@LastName", instancia.LastName);
					sqlComando.Parameters.AddWithValue("@Dni", instancia.Dni);
					sqlComando.Parameters.AddWithValue("@Provincia", instancia.Provincia);
					sqlComando.Parameters.AddWithValue("@Domicilio", instancia.Domicilio);
					sqlComando.Parameters.AddWithValue("@Localidad", instancia.Localidad);
					sqlComando.Parameters.AddWithValue("@Especialidad", instancia.Especialidad);
					sqlComando.Parameters.AddWithValue("@Telefono", instancia.Telefono);
					sqlComando.Parameters.AddWithValue("@Guardia", instancia.Guardia);
					sqlComando.Parameters.AddWithValue("@FechaIngreso", instancia.FechaIngreso);
					sqlComando.Parameters.AddWithValue("@SueldoMinimoGarantizado", instancia.SueldoMinimoGarantizado);

					sqlComando.ExecuteNonQuery();
				}
			}

			return OperationCode.CREATE_SUCCESS;
		}
		
		
		//------------------------CREATE.Paciente----------------------//
		public OperationCode CreatePaciente(Paciente instancia) {
			string insertQuery = @"
				INSERT INTO Paciente (Dni, Name, LastName, FechaIngreso, Email, Telefono, FechaNacimiento, Domicilio, Localidad, Provincia) 
				VALUES (@Dni, @Name, @LastName, @FechaIngreso, @Email, @Telefono, @FechaNacimiento, @Domicilio, @Localidad, @Provincia)";
			
			using (SqlConnection connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (SqlCommand sqlComando = new SqlCommand(insertQuery, connection)) {
					sqlComando.Parameters.AddWithValue("@Dni", instancia.Dni);
					sqlComando.Parameters.AddWithValue("@Name", instancia.Name);
					sqlComando.Parameters.AddWithValue("@LastName", instancia.LastName);
					sqlComando.Parameters.AddWithValue("@FechaIngreso", instancia.FechaIngreso);
					sqlComando.Parameters.AddWithValue("@Email", instancia.Email);
					sqlComando.Parameters.AddWithValue("@Telefono", instancia.Telefono);
					sqlComando.Parameters.AddWithValue("@FechaNacimiento", instancia.FechaNacimiento);
					sqlComando.Parameters.AddWithValue("@Domicilio", instancia.Domicilio);
					sqlComando.Parameters.AddWithValue("@Localidad", instancia.Localidad);
					sqlComando.Parameters.AddWithValue("@Provincia", instancia.Provincia);

					sqlComando.ExecuteNonQuery();
				}
			}

			return OperationCode.CREATE_SUCCESS;
		}


		//------------------------CREATE.Turno----------------------//
		public OperationCode CreateTurno(Turno instancia) {
			string insertQuery = @"
				INSERT INTO Turno (Id, PacienteId, MedicoId, Fecha, Hora) 
				VALUES (@Id, @PacienteId, @MedicoId, @Fecha, @Hora)";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (var command = new SqlCommand(insertQuery, connection)) {
					command.Parameters.AddWithValue("@Id", instancia.Id);
					command.Parameters.AddWithValue("@PacienteId", instancia.PacienteId);
					command.Parameters.AddWithValue("@MedicoId", instancia.MedicoId);
					command.Parameters.AddWithValue("@Fecha", instancia.Fecha);
					command.Parameters.AddWithValue("@Hora", instancia.Hora);
					command.ExecuteNonQuery();
				}
			}
			return OperationCode.CREATE_SUCCESS;
		}


		//------------------------READ----------------------//
		public List<Medico> ReadMedicos() {
			List<Medico> medicoList = new List<Medico>();
			string query = "SELECT * FROM Medico";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (var command = new SqlCommand(query, connection)) {
					using (var reader = command.ExecuteReader()) {
						while (reader.Read()) {
							Medico instancia = new Medico {
								Name = reader["Name"]?.ToString(),
								LastName = reader["LastName"]?.ToString(),
								Dni = reader["Dni"]?.ToString(),
								Provincia = reader["Provincia"]?.ToString(),
								Domicilio = reader["Domicilio"]?.ToString(),
								Localidad = reader["Localidad"]?.ToString(),
								Especialidad = reader["Especialidad"]?.ToString(),
								Telefono = reader["Telefono"]?.ToString(),
								Guardia = reader["Guardia"] != DBNull.Value ? Convert.ToBoolean(reader["Guardia"]) : false,
								FechaIngreso = reader["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["FechaIngreso"]) : (DateTime?)null,
								SueldoMinimoGarantizado = reader["SueldoMinimoGarantizado"] != DBNull.Value ? Convert.ToDouble(reader["SueldoMinimoGarantizado"]) : 0.0
							};
							medicoList.Add(instancia);
						}
					}
				}
			}
			return medicoList;
		}

		public List<Paciente> ReadPacientes() {
			List<Paciente> pacienteList = new List<Paciente>();
			string query = "SELECT * FROM Paciente";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (var command = new SqlCommand(query, connection)) {
					using (var reader = command.ExecuteReader()) {
						while (reader.Read()) {
							Paciente instancia = new Paciente {
								Dni = reader["Dni"]?.ToString(),
								Name = reader["Name"]?.ToString(),
								LastName = reader["LastName"]?.ToString(),
								FechaIngreso = reader["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["FechaIngreso"]) : (DateTime?)null,
								Email = reader["Email"]?.ToString(),
								Telefono = reader["Telefono"]?.ToString(),
								FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null,
								Domicilio = reader["Domicilio"]?.ToString(),
								Localidad = reader["Localidad"]?.ToString(),
								Provincia = reader["Provincia"]?.ToString()
							};
							pacienteList.Add(instancia);
						}
					}
				}
			}
			return pacienteList;
		}

		public List<Turno> ReadTurnos() {
			List<Turno> turnosList = new List<Turno>();
			string query = "SELECT * FROM Turno";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (var command = new SqlCommand(query, connection)) {
					using (var reader = command.ExecuteReader()) {
						while (reader.Read()) {
							Turno instancia = new Turno {
								Id = reader["Id"]?.ToString(),
								PacienteId = reader["PacienteId"]?.ToString(),
								MedicoId = reader["MedicoId"]?.ToString(),
								Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : (DateTime?)null,
								Hora = reader["Hora"].ToString(),
							};
							turnosList.Add(instancia);
						}
					}
				}
			}
			return turnosList;
		}
		//------------------------UPDATE----------------------//
		public OperationCode UpdateMedico(Medico instancia, string originalDni) {
			string query = "UPDATE Medico SET Name = @Name, LastName = @LastName, Dni = @Dni, Provincia = @Provincia, Domicilio = @Domicilio, Localidad = @Localidad, Especialidad = @Especialidad, Telefono = @Telefono, Guardia = @Guardia, FechaIngreso = @FechaIngreso, SueldoMinimoGarantizado = @SueldoMinimoGarantizado WHERE Dni = @originalDni";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (var command = new SqlCommand(query, connection)) {
					command.Parameters.AddWithValue("@Name", instancia.Name);
					command.Parameters.AddWithValue("@LastName", instancia.LastName);
					command.Parameters.AddWithValue("@Dni", instancia.Dni);
					command.Parameters.AddWithValue("@Provincia", instancia.Provincia);
					command.Parameters.AddWithValue("@Domicilio", instancia.Domicilio);
					command.Parameters.AddWithValue("@Localidad", instancia.Localidad);
					command.Parameters.AddWithValue("@Especialidad", instancia.Especialidad);
					command.Parameters.AddWithValue("@Telefono", instancia.Telefono);
					command.Parameters.AddWithValue("@Guardia", instancia.Guardia);
					command.Parameters.AddWithValue("@FechaIngreso", instancia.FechaIngreso);
					command.Parameters.AddWithValue("@SueldoMinimoGarantizado", instancia.SueldoMinimoGarantizado);
					command.Parameters.AddWithValue("@originalDni", originalDni);
					command.ExecuteNonQuery();
				}
			}
			return OperationCode.UPDATE_SUCCESS;
		}
		public OperationCode UpdatePaciente(Paciente instancia, string originalDni) {
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = "UPDATE Paciente SET Dni = @Dni, Name = @Name, LastName = @LastName, FechaIngreso = @FechaIngreso, Email = @Email, Telefono = @Telefono, FechaNacimiento = @FechaNacimiento, Domicilio = @Domicilio, Localidad = @Localidad, Provincia = @Provincia WHERE Dni = @originalDni";
				using (SqlCommand sqlComando = new SqlCommand(query, connection)) {
					sqlComando.Parameters.AddWithValue("@Dni", instancia.Dni);
					sqlComando.Parameters.AddWithValue("@Name", instancia.Name);
					sqlComando.Parameters.AddWithValue("@LastName", instancia.LastName);
					sqlComando.Parameters.AddWithValue("@FechaIngreso", instancia.FechaIngreso);
					sqlComando.Parameters.AddWithValue("@Email", instancia.Email);
					sqlComando.Parameters.AddWithValue("@Telefono", instancia.Telefono);
					sqlComando.Parameters.AddWithValue("@FechaNacimiento", instancia.FechaNacimiento);
					sqlComando.Parameters.AddWithValue("@Domicilio", instancia.Domicilio);
					sqlComando.Parameters.AddWithValue("@Localidad", instancia.Localidad);
					sqlComando.Parameters.AddWithValue("@Provincia", instancia.Provincia);
					sqlComando.Parameters.AddWithValue("@originalDni", originalDni);
					sqlComando.ExecuteNonQuery();
				}
			}
			return OperationCode.UPDATE_SUCCESS;
		}
		public OperationCode UpdateTurno(Turno instancia) {
			//MessageBox.Show(@$"
			//	{instancia.PacienteId}
			//	{instancia.PacienteJoin}
			//	{instancia.MedicoId}
			//	{instancia.Fecha}
			//	{instancia.Hora}
			//	{instancia.Id}
			//");


			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = "UPDATE Turno SET PacienteId = @PacienteId, MedicoId = @MedicoId, Fecha = @Fecha, Hora = @Hora WHERE Id = @Id";
				using (SqlCommand sqlComando = new SqlCommand(query, connection)) {
					sqlComando.Parameters.AddWithValue("@PacienteId", instancia.PacienteId);
					sqlComando.Parameters.AddWithValue("@MedicoId", instancia.MedicoId);
					//sqlComando.Parameters.AddWithValue("@Fecha", instancia.Fecha?.Date ?? (object)DBNull.Value);
					sqlComando.Parameters.AddWithValue("@Fecha", instancia.Fecha?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);

					sqlComando.Parameters.AddWithValue("@Hora", instancia.Hora);
					sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
					sqlComando.ExecuteNonQuery();
				}
			}
			return OperationCode.UPDATE_SUCCESS;
		}



		//------------------------DELETE----------------------//
		public OperationCode DeleteMedico(string idToDelete){
			string query = "DELETE FROM Medico WHERE Dni = @Dni";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(query, connection)){
					command.Parameters.AddWithValue("@Dni", idToDelete);
					command.ExecuteNonQuery();
				}
			}
			return OperationCode.DELETE_SUCCESS;
		}
		public OperationCode DeletePaciente(string idToDelete) {
			string query = "DELETE FROM Medico WHERE Dni = @Dni";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(query, connection)){
					command.Parameters.AddWithValue("@Dni", idToDelete);
					command.ExecuteNonQuery();
				}
			}
			return OperationCode.DELETE_SUCCESS;
		}
		public OperationCode DeleteTurno(string idToDelete) {
			string query = "DELETE FROM Turno WHERE Id = @Id";
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(query, connection)){
					command.Parameters.AddWithValue("@Id", idToDelete);
					command.ExecuteNonQuery();
				}
			}
			return OperationCode.DELETE_SUCCESS;
		}










		//------------------------GET-PROPERTIES----------------------//
		public string LoadPacienteNombreCompletoFromDatabase(string instance_id) {
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = @"
					SELECT CONCAT(Dni, ' ', Name, ' ', LastName)
					FROM Paciente
					WHERE Id = @PacienteId";
				using (var command = new SqlCommand(query, connection)) {
					command.Parameters.AddWithValue("@PacienteId", instance_id);
					var result = command.ExecuteScalar();
					return result?.ToString() ?? "Paciente no encontrado";
				}
			}
		}

		public string LoadMedicoNombreCompletoFromDatabase(string instance_id) {
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = @"
                SELECT CONCAT(Dni, ' ', Name, ' ', LastName)
                FROM Medico
                WHERE Id = @MedicoId";

				using (var command = new SqlCommand(query, connection)) {
					command.Parameters.AddWithValue("@MedicoId", instance_id);

					var result = command.ExecuteScalar();
					return result?.ToString() ?? "Medico no encontrado";
				}
			}
		}


		public string LoadEspecialidadFromDatabase(string instance_id) {
			// Replace with your actual connection string
			using (var connection = new SqlConnection(BaseDeDatosSQL.connectionString)) {
				connection.Open();
				string query = "SELECT Especialidad FROM Medico WHERE Id = @MedicoId";

				using (var command = new SqlCommand(query, connection)) {
					command.Parameters.AddWithValue("@MedicoId", instance_id);

					var result = command.ExecuteScalar();
					return result?.ToString() ?? "Especialidad no encontrada";
				}
			}
		}



		//------------------------Fin.BaseDeDatosSQL----------------------//
	}
}
