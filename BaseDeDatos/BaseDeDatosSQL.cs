using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace ClinicaMedica {
	public class BaseDeDatosSQL {
		public static string connectionString = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;
		
		
		//------------------------CREATE----------------------//
		public static OperationCode CreateMedico(Medico medico){
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE dni = @dni";
			string insertQuery = "INSERT INTO Medico (nombre, apellido, especialidad, dni, ...) VALUES (@nombre, @apellido, @especialidad, @dni, ...)";
			using (SqlConnection connection = new SqlConnection(connectionString)){
				SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
				checkCommand.Parameters.AddWithValue("@dni", medico.Dni);
				connection.Open();
				if ((int)checkCommand.ExecuteScalar() > 0){
					return OperationCode.YA_EXISTE;
				}
				SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
				insertCommand.Parameters.AddWithValue("@nombre", medico.Name);
				insertCommand.Parameters.AddWithValue("@apellido", medico.Lastname);
				insertCommand.Parameters.AddWithValue("@especialidad", medico.Specialidad);
				insertCommand.Parameters.AddWithValue("@dni", medico.Dni);
				// Add parameters for all other fields you want to insert
				insertCommand.ExecuteNonQuery();
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


		//------------------------UPDATE----------------------//
		public static OperationCode UpdateMedico(Medico medico, string originalDni) {
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE dni = @dni AND id != @id";
			string updateQuery = "UPDATE Medico SET nombre = @nombre, apellido = @apellido, especialidad = @especialidad, ... WHERE id = @id";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				try {
					connection.Open();

					// Check if another Medico with the same DNI exists
					using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection)) {
						checkCommand.Parameters.AddWithValue("@dni", medico.Dni);
						//checkCommand.Parameters.AddWithValue("@id", medico.Id);

						int count = (int)checkCommand.ExecuteScalar();
						if (count > 0) {
							return OperationCode.MISSING_DNI;
						}
					}

					// Proceed with the update if no conflicts are found
					using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection)) {
						updateCommand.Parameters.AddWithValue("@nombre", medico.Name);
						updateCommand.Parameters.AddWithValue("@apellido", medico.Lastname);
						updateCommand.Parameters.AddWithValue("@especialidad", medico.Specialidad);
						// Add parameters for other fields you want to update

						updateCommand.ExecuteNonQuery();
					}
				}
				catch (SqlException) {
					// Handle SQL-related exceptions if needed
					return OperationCode.ERROR;
				}
			}

			return OperationCode.UPDATE_SUCCESS;
		}



		//------------------------DELETE----------------------//
		public static OperationCode DeleteMedico(string medicoId)
		{
			string query = "DELETE FROM Medico WHERE id = @id";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@id", medicoId);

				connection.Open();
				command.ExecuteNonQuery();
			}

			return OperationCode.DELETE_SUCCESS;
		}








		//------------------------Fin.BaseDeDatosSQL----------------------//
	}
}
