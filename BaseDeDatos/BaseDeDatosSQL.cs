using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace ClinicaMedica {
	public class BaseDeDatosSQL : BaseDeDatosAbstracta{
		static public string connectionString = ConfigurationManager.ConnectionStrings["ConexionAClinicaMedica"].ConnectionString;
		
		public BaseDeDatosSQL() {
			CargarMedicos();
			CargarPacientes();
			CargarTurnos();
		}

		
		//------------------------Private----------------------//
		private void CargarMedicos(){
			using (var conexion = new SqlConnection(connectionString)){
				conexion.Open();
				string consulta = "SELECT * FROM Medico";
				using (var sqlComando = new SqlCommand(consulta, conexion))
				using (var reader = sqlComando.ExecuteReader()){
					while (reader.Read()){
						var medico = new Medico{
							Id = reader["Id"]?.ToString(),
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
						DictMedicos[medico.Id] = medico;
					}
				}
			}
		}

		private void CargarPacientes()
		{
			using (var conexion = new SqlConnection(connectionString)){
				conexion.Open();
				string consulta = "SELECT * FROM Paciente";
				using (var sqlComando = new SqlCommand(consulta, conexion))
				using (var reader = sqlComando.ExecuteReader())
				{
					while (reader.Read())
					{
						var paciente = new Paciente
						{
								Id = reader["Id"]?.ToString(),
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
						DictPacientes[paciente.Id] = paciente;
					}
				}
			}
		}

		private void CargarTurnos()
		{
			using (var conexion = new SqlConnection(connectionString))
			{
				conexion.Open();
				string consulta = "SELECT * FROM Turno";
				using (var sqlComando = new SqlCommand(consulta, conexion))
				using (var reader = sqlComando.ExecuteReader())
				{
					while (reader.Read())
					{
						var turno = new Turno
						{
								Id = reader["Id"]?.ToString(),
								PacienteId = reader["PacienteId"]?.ToString(),
								MedicoId = reader["MedicoId"]?.ToString(),
								Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : (DateTime?)null,
								Hora = TimeSpan.Parse(reader["Hora"].ToString()),
						};
						DictTurnos[turno.Id] = turno;
					}
				}
			}
		}

		//------------------------CREATE.Medico----------------------//
		public override bool CreateMedico(Medico instancia) {
			string insertQuery = @"
				INSERT INTO Medico (Name, LastName, Dni, Provincia, Domicilio, Localidad, Especialidad, Telefono, Guardia, FechaIngreso, SueldoMinimoGarantizado) 
				VALUES (@Name, @LastName, @Dni, @Provincia, @Domicilio, @Localidad, @Especialidad, @Telefono, @Guardia, @FechaIngreso, @SueldoMinimoGarantizado)
				SELECT SCOPE_IDENTITY();"; // DEVOLEME RAPIDAMENTE LA ID QUE ACABAS DE GENERAR
			
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
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
						instancia.Id = sqlComando.ExecuteScalar().ToString();	//ahora la instancia creada desde la ventana tiene su propia Id
					}
				}
				DictMedicos[instancia.Id] = instancia;
				MessageBox.Show($"Exito: Se ha creado la instancia de Medico: {instancia.Name} {instancia.LastName}");
				return true;
			} 
			catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation error code
			{
				MessageBox.Show("Error de constraints. Ya existe un médico con ese dni.", "Violación de Constraint", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) when (ex.Number == 547) // Foreign key violation error code
			{
				MessageBox.Show("No se puede crear el médico debido a una violación de clave foránea.", "Violación de Clave Foránea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}
		
		
		//------------------------CREATE.Paciente----------------------//
		public override bool CreatePaciente(Paciente instancia) {
			string insertQuery = @"
				INSERT INTO Paciente (Dni, Name, LastName, FechaIngreso, Email, Telefono, FechaNacimiento, Domicilio, Localidad, Provincia) 
				VALUES (@Dni, @Name, @LastName, @FechaIngreso, @Email, @Telefono, @FechaNacimiento, @Domicilio, @Localidad, @Provincia)
				SELECT SCOPE_IDENTITY();"; // DEVOLEME RAPIDAMENTE LA ID QUE ACABAS DE GENERAR
			
			try {
				using (SqlConnection connection = new SqlConnection(connectionString)) {
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
						instancia.Id = sqlComando.ExecuteScalar().ToString();	//ahora la instancia creada desde la ventana tiene su propia Id
					}
				}
				DictPacientes[instancia.Id] = instancia;
				MessageBox.Show($"Exito: Se ha creado la instancia de Paciente: {instancia.Name} {instancia.LastName}");
				return true;
			} 
			catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation error code
			{
				MessageBox.Show("Error de constraints. Ya existe un paciente con ese dni.", "Violación de Constraint", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) when (ex.Number == 547) // Foreign key violation error code
			{
				MessageBox.Show("No se puede crear el paciente debido a una violación de clave foránea.", "Violación de Clave Foránea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}


		//------------------------CREATE.Turno----------------------//
		public override bool CreateTurno(Turno instancia) {
			string insertQuery = @"
				INSERT INTO Turno (PacienteId, MedicoId, Fecha, Hora) 
				VALUES (@PacienteId, @MedicoId, @Fecha, @Hora);
				SELECT SCOPE_IDENTITY();"; // DEVOLEME RAPIDAMENTE LA ID QUE ACABAS DE GENERAR
			try {
				using (var connection = new SqlConnection(connectionString)) {
					connection.Open();
					using (var sqlComando = new SqlCommand(insertQuery, connection)) {
						sqlComando.Parameters.AddWithValue("@PacienteId", instancia.PacienteId);
						sqlComando.Parameters.AddWithValue("@MedicoId", instancia.MedicoId);
						sqlComando.Parameters.AddWithValue("@Fecha", instancia.Fecha);
						sqlComando.Parameters.AddWithValue("@Hora", instancia.Hora);
						instancia.Id = sqlComando.ExecuteScalar().ToString();	//ahora la instancia creada desde la ventana tiene su propia Id
					}
				}
				DictTurnos[instancia.Id] = instancia;
				MessageBox.Show($"Exito: Se ha creado la instancia de Turno con Id {instancia.Id} entre: {instancia.PacienteId} {instancia.MedicoId} en la fecha {instancia.Fecha}");
				return true;
			}
			catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation error code
			{
				MessageBox.Show("Error de constraints. Ya existe un turno entre este paciente y medico en esa fecha. O el medico ya tiene un turno en esa fecha con otro paciente.", "Violación de Constraint", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) when (ex.Number == 547) // Foreign key violation error code
			{
				MessageBox.Show("No se puede crear el turno debido a una violación de clave foránea.", "Violación de Clave Foránea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}


		//------------------------READ----------------------//
		public override List<Medico> ReadMedicos() {
			return DictMedicos.Values.ToList();
			// List<Medico> medicoList = new List<Medico>();
			// string query = "SELECT * FROM Medico";
			// using (var connection = new SqlConnection(connectionString)) {
				// connection.Open();
				// using (var sqlComando = new SqlCommand(query, connection)) {
					// using (var reader = sqlComando.ExecuteReader()) {
						// while (reader.Read()) {
							// Medico instancia = new Medico {
								// Name = reader["Name"]?.ToString(),
								// LastName = reader["LastName"]?.ToString(),
								// Dni = reader["Dni"]?.ToString(),
								// Provincia = reader["Provincia"]?.ToString(),
								// Domicilio = reader["Domicilio"]?.ToString(),
								// Localidad = reader["Localidad"]?.ToString(),
								// Especialidad = reader["Especialidad"]?.ToString(),
								// Telefono = reader["Telefono"]?.ToString(),
								// Guardia = reader["Guardia"] != DBNull.Value ? Convert.ToBoolean(reader["Guardia"]) : false,
								// FechaIngreso = reader["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["FechaIngreso"]) : (DateTime?)null,
								// SueldoMinimoGarantizado = reader["SueldoMinimoGarantizado"] != DBNull.Value ? Convert.ToDouble(reader["SueldoMinimoGarantizado"]) : 0.0
							// };
							// medicoList.Add(instancia);
						// }
					// }
				// }
			// }
			// return medicoList;
		}

		public override List<Paciente> ReadPacientes() {
			return DictPacientes.Values.ToList();
			// List<Paciente> pacienteList = new List<Paciente>();
			// string query = "SELECT * FROM Paciente";
			// using (var connection = new SqlConnection(connectionString)) {
				// connection.Open();
				// using (var sqlComando = new SqlCommand(query, connection)) {
					// using (var reader = sqlComando.ExecuteReader()) {
						// while (reader.Read()) {
							// Paciente instancia = new Paciente {
								// Dni = reader["Dni"]?.ToString(),
								// Name = reader["Name"]?.ToString(),
								// LastName = reader["LastName"]?.ToString(),
								// FechaIngreso = reader["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(reader["FechaIngreso"]) : (DateTime?)null,
								// Email = reader["Email"]?.ToString(),
								// Telefono = reader["Telefono"]?.ToString(),
								// FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null,
								// Domicilio = reader["Domicilio"]?.ToString(),
								// Localidad = reader["Localidad"]?.ToString(),
								// Provincia = reader["Provincia"]?.ToString()
							// };
							// pacienteList.Add(instancia);
						// }
					// }
				// }
			// }
			// return pacienteList;
		}

		public override List<Turno> ReadTurnos() {
			return DictTurnos.Values.ToList();
			// List<Turno> turnosList = new List<Turno>();
			// string query = "SELECT * FROM Turno";
			// using (var connection = new SqlConnection(connectionString)) {
				// connection.Open();
				// using (var sqlComando = new SqlCommand(query, connection)) {
					// using (var reader = sqlComando.ExecuteReader()) {
						// while (reader.Read()) {
							// Turno instancia = new Turno {
								// Id = reader["Id"]?.ToString(),
								// PacienteId = reader["PacienteId"]?.ToString(),
								// MedicoId = reader["MedicoId"]?.ToString(),
								// Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : (DateTime?)null,
								// Hora = TimeSpan.Parse(reader["Hora"].ToString()),
							// };
							// turnosList.Add(instancia);
						// }
					// }
				// }
			// }
			// return turnosList;
		}
		//------------------------UPDATE----------------------//
		public override bool UpdateMedico(Medico instancia) {
			string query = "UPDATE Medico SET Name = @Name, LastName = @LastName, Dni = @Dni, Provincia = @Provincia, Domicilio = @Domicilio, Localidad = @Localidad, Especialidad = @Especialidad, Telefono = @Telefono, Guardia = @Guardia, FechaIngreso = @FechaIngreso, SueldoMinimoGarantizado = @SueldoMinimoGarantizado WHERE Id = @Id";
			try {
				using (var connection = new SqlConnection(connectionString)) {
					connection.Open();
					using (var sqlComando = new SqlCommand(query, connection)) {
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
						sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
						sqlComando.ExecuteNonQuery();
					}
				}
				MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
				return true;
			} 
			catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation error code
			{
				MessageBox.Show("Error de constraints. Ya existe un médico con ese dni.", "Violación de Constraint", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) when (ex.Number == 547) // Foreign key violation error code
			{
				MessageBox.Show("No se puede crear el médico debido a una violación de clave foránea.", "Violación de Clave Foránea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}
		public override bool UpdatePaciente(Paciente instancia) {
			string query = "UPDATE Paciente SET Dni = @Dni, Name = @Name, LastName = @LastName, FechaIngreso = @FechaIngreso, Email = @Email, Telefono = @Telefono, FechaNacimiento = @FechaNacimiento, Domicilio = @Domicilio, Localidad = @Localidad, Provincia = @Provincia WHERE Id = @Id";
			try {
				using (var connection = new SqlConnection(connectionString)) {
					connection.Open();
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
						sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
						sqlComando.ExecuteNonQuery();
					}
				}
				MessageBox.Show($"Exito: Se han actualizado los datos de: {instancia.Name} {instancia.LastName}");
				return true;
			}
			catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation error code
			{
				MessageBox.Show("Error de constraints. Ya existe un paciente con ese dni.", "Violación de Constraint", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) when (ex.Number == 547) // Foreign key violation error code
			{
				MessageBox.Show("No se puede crear el paciente debido a una violación de clave foránea.", "Violación de Clave Foránea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}
		public override bool UpdateTurno(Turno instancia) {
			//string query = "UPDATE Turno SET PacienteId = @PacienteId, MedicoId = @MedicoId, Fecha = @Fecha, Hora = @Hora WHERE Id = @Id";
			string query = "UPDATE Turno SET PacienteId = @PacienteId, MedicoId = @MedicoId WHERE Id = @Id";
			try {
				using (var connection = new SqlConnection(connectionString)) {
				connection.Open();
				using (SqlCommand sqlComando = new SqlCommand(query, connection)) {
					sqlComando.Parameters.AddWithValue("@PacienteId", instancia.PacienteId);
					sqlComando.Parameters.AddWithValue("@MedicoId", instancia.MedicoId);
					//sqlComando.Parameters.AddWithValue("@Fecha", instancia.Fecha?.Date ?? (object)DBNull.Value);
					//sqlComando.Parameters.AddWithValue("@Fecha", instancia.Fecha?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);

					//sqlComando.Parameters.AddWithValue("@Hora", instancia.Hora);
					sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
					sqlComando.ExecuteNonQuery();
					}
				}
				MessageBox.Show($"Exito: Se han actualizado los datos del turno con id: {instancia.Id}. Ahora entre {instancia.PacienteId} {instancia.MedicoId}");
				return true;
			}
			catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation error code
			{
				MessageBox.Show("Error de constraints. Ya existe un turno entre este paciente y medico en esa fecha. O el medico ya tiene un turno en esa fecha con otro paciente.", "Violación de Constraint", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) when (ex.Number == 547) // Foreign key violation error code
			{
				MessageBox.Show("No se puede crear el turno debido a una violación de clave foránea.", "Violación de Clave Foránea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}



		//------------------------DELETE----------------------//
		public override bool DeleteMedico(Medico instancia) {
			string query = "DELETE FROM Medico WHERE Id = @Id";

			try {
				using (var connection = new SqlConnection(connectionString)) {
					connection.Open();
					using (SqlCommand sqlComando = new SqlCommand(query, connection)) {
						sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
						sqlComando.ExecuteNonQuery();
					}
				}
				DictMedicos.Remove(instancia.Id);
				MessageBox.Show($"Exito: Se ha eliminado el medico con id: {instancia.Id} de la Base de Datos SQL");
				return true;
			}
			catch (SqlException ex) when (ex.Number == 547) // SQL Server foreign key violation error code
			{
				MessageBox.Show("No se puede eliminar este medico porque tiene turnos asignados.", "Violacion de clave foranea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Data Base", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}
		public override bool DeletePaciente(Paciente instancia) {
			string query = "DELETE FROM Paciente WHERE Id = @Id";
			try {
				using (var connection = new SqlConnection(connectionString)) {
					connection.Open();
					using (SqlCommand sqlComando = new SqlCommand(query, connection)) {
						sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
						sqlComando.ExecuteNonQuery();
					}
				}
				DictPacientes.Remove(instancia.Id);
				MessageBox.Show($"Exito: Se ha eliminado el paciente con id: {instancia.Id} de la Base de Datos SQL");
				return true;
			}
			catch (SqlException ex) when (ex.Number == 547) // SQL Server foreign key violation error code
			{
				MessageBox.Show("No se puede eliminar este paciente porque tiene turnos asignados.", "Violacion de clave foranea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Data Base", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}
		public override bool DeleteTurno(Turno instancia) {
			string query = "DELETE FROM Turno WHERE Id = @Id";
			try {
				using (var connection = new SqlConnection(connectionString)) {
					connection.Open();
					using (SqlCommand sqlComando = new SqlCommand(query, connection)) {
						sqlComando.Parameters.AddWithValue("@Id", instancia.Id);
						sqlComando.ExecuteNonQuery();
					}
				}
				DictTurnos.Remove(instancia.Id);
				MessageBox.Show($"Exito: Se ha eliminado el turno con id: {instancia.Id} de la Base de Datos SQL");
				return true;
			}
			catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation error code
			{
				MessageBox.Show("Error de constraints. Ya existe un turno entre este paciente y medico en esa fecha. O el medico ya tiene un turno en esa fecha con otro paciente.", "Violación de Constraint", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) when (ex.Number == 547) // Foreign key violation error code
			{
				MessageBox.Show("No se puede modificar el turno debido a una violación de clave foránea.", "Violación de Clave Foránea", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (SqlException ex) {
				MessageBox.Show($"SQL error: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex) {
				MessageBox.Show($"Error no esperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}
		//------------------------Fin.BaseDeDatosSQL----------------------//
	}
}
