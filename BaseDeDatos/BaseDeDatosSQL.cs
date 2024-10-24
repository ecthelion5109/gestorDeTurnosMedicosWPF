using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace ClinicaMedica {
	public class BaseDeDatosSQL {
		public static string connectionString = ConfigurationManager.ConnectionStrings["ConexionClinicaMedica.Properties.Settings.ClinicaMedicaConnectionString"].ConnectionString;
		
		
		//------------------------CREATE----------------------//
		public static OperationCode CreateMedico(Medico medico) {
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE dni = @dni";
			string insertQuery = "INSERT INTO Medico (nombre, apellido, especialidad, sueldo_minimo_garantizado, fecha_ingreso, dni) VALUES (@nombre, @apellido, @especialidad, @sueldo_minimo_garantizado, @fecha_ingreso, @dni)";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
				checkCommand.Parameters.AddWithValue("@dni", medico.Dni);
				connection.Open();
				if ((int)checkCommand.ExecuteScalar() > 0) {
					return OperationCode.YA_EXISTE;
				}
				SqlCommand sqlComando = new SqlCommand(insertQuery, connection);
				sqlComando.Parameters.AddWithValue("@nombre", medico.Name);
				sqlComando.Parameters.AddWithValue("@apellido", medico.Lastname);
				sqlComando.Parameters.AddWithValue("@especialidad", medico.Specialidad);
				sqlComando.Parameters.AddWithValue("@sueldo_minimo_garantizado", medico.SueldoMinimoGarantizado);
				sqlComando.Parameters.AddWithValue("@fecha_ingreso", medico.FechaIngreso);
				sqlComando.Parameters.AddWithValue("@dni", medico.Dni);
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

		//public CorroborororarDNIIntegrigrty(){




		//}


		public static OperationCode UpdateMedico(Medico medico, string originalDni) {
			string checkQuery = "SELECT COUNT(1) FROM Medico WHERE dni = @dni";
			string updateQuery = "UPDATE Medico SET nombre = @nombre, apellido = @apellido, especialidad = @especialidad, sueldo_minimo_garantizado = @sueldo_minimo_garantizado,  fecha_ingreso = @fecha_ingreso, dni = @dni WHERE dni = @originalDni";
			using (SqlConnection connection = new SqlConnection(connectionString)) {
				// try {
					connection.Open();

					// Proceed with the update if no conflicts are found
					using (SqlCommand sqlComando = new SqlCommand(updateQuery, connection)) {
						sqlComando.Parameters.AddWithValue("@nombre", medico.Name);
						sqlComando.Parameters.AddWithValue("@apellido", medico.Lastname);
						sqlComando.Parameters.AddWithValue("@especialidad", medico.Specialidad);
						sqlComando.Parameters.AddWithValue("@sueldo_minimo_garantizado", medico.SueldoMinimoGarantizado);
						sqlComando.Parameters.AddWithValue("@fecha_ingreso", medico.FechaIngreso);
						sqlComando.Parameters.AddWithValue("@dni", medico.Dni);
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
			string query = "DELETE FROM Medico WHERE dni = @dni";

			using (SqlConnection connection = new SqlConnection(connectionString)) {
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@dni", medicoDni);

				connection.Open();
				command.ExecuteNonQuery();
			}

			return OperationCode.DELETE_SUCCESS;
		}








		//------------------------Fin.BaseDeDatosSQL----------------------//
	}
}
