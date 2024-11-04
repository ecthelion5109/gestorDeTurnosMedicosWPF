using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace ClinicaMedica {
	public class BaseDeDatosSQL : IBaseDeDatos{
		static public string connectionString = ConfigurationManager.ConnectionStrings["ConexionAClinicaMedica"].ConnectionString;
		private SqlConnection MiConexion;
		
		
		public BaseDeDatosSQL() {
			MiConexion = new SqlConnection(connectionString);
			MiConexion.Open();
		}


		//------------------------CHECKERS----------------------//
		public bool CorroborarQueNoExistaMedico(string key) {
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE Dni = @Dni";
			using (SqlCommand checkCommand = new SqlCommand(checkQuery, MiConexion)) {
				checkCommand.Parameters.AddWithValue("@Dni", key);
				return (int)checkCommand.ExecuteScalar() > 0;
			}
		}
		public bool CorroborarQueNoExistaPaciente(string key) {
			string checkQuery = "SELECT COUNT(1) FROM Paciente WHERE Dni = @Dni";
			using (SqlCommand checkCommand = new SqlCommand(checkQuery, MiConexion)) {
				checkCommand.Parameters.AddWithValue("@Dni", key);
				return (int)checkCommand.ExecuteScalar() > 0;
			}
		}
		public bool CorroborarQueNoExistaTurno(string key) {
			return false;
		}




		//------------------------CREATE----------------------//
		public OperationCode CreateMedico(Medico instancia) {
			string insertQuery = @"
				INSERT INTO Medico (Name, LastName, Dni, Provincia, Domicilio, Localidad, Especialidad, Telefono, Guardia, FechaIngreso, SueldoMinimoGarantizado) 
				VALUES (@Name, @LastName, @Dni, @Provincia, @Domicilio, @Localidad, @Especialidad, @Telefono, @Guardia, @FechaIngreso, @SueldoMinimoGarantizado)";

			SqlCommand sqlComando = new SqlCommand(insertQuery, MiConexion);
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
			return OperationCode.CREATE_SUCCESS;
		}
		//public bool corroborarquelaputamdreuqepatio(Paciente instancia){
		//	return !(
		//			 string.IsNullOrEmpty(instancia.Dni) ||
		//			 string.IsNullOrEmpty(instancia.Name) ||
		//			 string.IsNullOrEmpty(instancia.LastName) ||
		//			 string.IsNullOrEmpty(instancia.Email) ||
		//			 string.IsNullOrEmpty(instancia.Telefono) ||
		//			 string.IsNullOrEmpty(instancia.Domicilio) ||
		//			 string.IsNullOrEmpty(instancia.Localidad) ||
		//			 string.IsNullOrEmpty(instancia.Provincia) ||






		//			instancia.FechaIngreso is null ||
		//			instancia.FechaNacimiento is null
		//			);
		//}
		public OperationCode CreatePaciente(Paciente instancia) {





			// if (corroborarquelaputamdreuqepatio(instancia) ){
				// MessageBox.Show($@"La puta madre:
				// DatoDni:{instancia.Dni}
				// DatoName:{instancia.Name}
				// DatoLastName:{instancia.LastName}
				// DatoEmail:{instancia.Email}
				// DatoTelefono:{instancia.Telefono}
				// DatoDomicilio:{instancia.Domicilio}
				// DatoLocalidad:{instancia.Localidad}
				// DatoProvincia:{instancia.Provincia}
				// FechaIngreso:{instancia.FechaIngreso}
				// FechaNacimiento:{instancia.FechaNacimiento}


			// ");

			// } else {

			// MessageBox.Show("Good");
			// }



			string insertQuery = @"
				INSERT INTO Paciente  (Dni, Name, LastName, FechaIngreso, Email, Telefono, FechaNacimiento, Domicilio, Localidad, Provincia) 
				VALUES (@Dni, @Name, @LastName, @FechaIngreso, @Email, @Telefono, @FechaNacimiento, @Domicilio, @Localidad, @Provincia)";

			SqlCommand sqlComando = new SqlCommand(insertQuery, MiConexion);
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
			return OperationCode.CREATE_SUCCESS;
		}



		public OperationCode CreateTurno(Turno turno) {
			return OperationCode.SIN_DEFINIR;
		}


		//------------------------READ----------------------//
		public List<Medico> ReadMedicos() {
			List<Medico> medicoList = new List<Medico>();
			string query = "SELECT * FROM Medico";
			try {
				SqlCommand command = new SqlCommand(query, MiConexion);
				SqlDataReader reader = command.ExecuteReader();
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
						//DiasDeAtencion = TODAVIA NO SE
					};
					medicoList.Add(instancia);
				}
				reader.Close();
			}
			catch (Exception ex) {
				MessageBox.Show("Error retrieving data: " + ex.Message);
			}
			return medicoList;
		}

		public List<Paciente> ReadPacientes() {
			List<Paciente> pacienteList = new List<Paciente>();
			string query = "SELECT * FROM Paciente";
			try {
				SqlCommand command = new SqlCommand(query, MiConexion);
				SqlDataReader reader = command.ExecuteReader();
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
				reader.Close();
			}
			catch (Exception ex) {
				MessageBox.Show("Error retrieving data: " + ex.Message);
			}
			return pacienteList;
		}

		public List<Turno> ReadTurnos() {
			List<Turno> turnosList = new List<Turno>();
			string query = "SELECT * FROM Turno";
			try {
				SqlCommand command = new SqlCommand(query, MiConexion);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()) {
					Turno instancia = new Turno {
						Id = reader["Id"]?.ToString(),
						PacienteID = reader["PacienteId"]?.ToString(),
						MedicoID = reader["MedicoID"]?.ToString(),
						Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : (DateTime?)null,
						// Hora = reader["Hora"] != DBNull.Value ? Convert.ToDateTime(reader["Hora"]) : (DateTime?)null,
						Hora = reader["Hora"].ToString(),
					};
					turnosList.Add(instancia);
				}
				reader.Close();
			}
			catch (Exception ex) {
				MessageBox.Show("Error retrieving data: " + ex.Message);
			}
			return turnosList;
		}
		//------------------------UPDATE----------------------//
		public OperationCode UpdateMedico(Medico instancia, string originalDni) {
			string query = "UPDATE Medico SET Name = @Name, LastName = @LastName, Dni = @Dni, Provincia = @Provincia, Domicilio = @Domicilio, Localidad = @Localidad,  Especialidad = @Especialidad, Telefono = @Telefono , Guardia = @Guardia , FechaIngreso = @FechaIngreso , SueldoMinimoGarantizado = @SueldoMinimoGarantizado WHERE Dni = @originalDni";

			SqlCommand sqlComando = new SqlCommand(query, MiConexion);

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
			sqlComando.Parameters.AddWithValue("@originalDni", originalDni);
			sqlComando.ExecuteNonQuery();

			return OperationCode.UPDATE_SUCCESS;
		}
		public OperationCode UpdatePaciente(Paciente instancia, string originalDni) {
			string query = "UPDATE Paciente SET Dni = @Dni, Name = @Name, LastName = @LastName, FechaIngreso = @FechaIngreso, Email = @Email, Telefono = @Telefono, FechaNacimiento = @FechaNacimiento, Domicilio = @Domicilio, Localidad = @Localidad, Provincia = @Provincia WHERE Dni = @originalDni";
			using (SqlCommand sqlComando = new SqlCommand(query, MiConexion)) {
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

			return OperationCode.UPDATE_SUCCESS;
		}
		public OperationCode UpdateTurno(Turno instancia) {
			string query = "UPDATE Turno SET PacienteID = @PacienteID, MedicoID = @MedicoID, Fecha = @Fecha, Hora = @Hora WHERE Id = @Id";
			using (SqlCommand sqlComando = new SqlCommand(query, MiConexion)) {
				sqlComando.Parameters.AddWithValue("@PacienteID", instancia.PacienteID);
				sqlComando.Parameters.AddWithValue("@MedicoID", instancia.MedicoID);
				sqlComando.Parameters.AddWithValue("@Fecha", instancia.Fecha);
				sqlComando.Parameters.AddWithValue("@Hora", instancia.Hora);
				sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
				sqlComando.ExecuteNonQuery();
			}

			return OperationCode.UPDATE_SUCCESS;
		}



		//------------------------DELETE----------------------//
		public OperationCode DeleteMedico(string medicoDni) {
			string query = "DELETE FROM Medico WHERE Dni = @Dni";

			SqlCommand command = new SqlCommand(query, MiConexion);
			command.Parameters.AddWithValue("@Dni", medicoDni);
			command.ExecuteNonQuery();

			return OperationCode.DELETE_SUCCESS;
		}
		public OperationCode DeletePaciente(string pacienteDni) {
			string query = "DELETE FROM Paciente WHERE Dni = @Dni";

			SqlCommand command = new SqlCommand(query, MiConexion);
			command.Parameters.AddWithValue("@Dni", pacienteDni);
			command.ExecuteNonQuery();

			return OperationCode.DELETE_SUCCESS;
		}
		public OperationCode DeleteTurno(string turnoId) {
			string query = "DELETE FROM Turno WHERE Id = @Id";

			SqlCommand command = new SqlCommand(query, MiConexion);
			command.Parameters.AddWithValue("@Id", turnoId);
			command.ExecuteNonQuery();

			return OperationCode.DELETE_SUCCESS;
		}







		//------------------------Fin.BaseDeDatosSQL----------------------//
	}
}
