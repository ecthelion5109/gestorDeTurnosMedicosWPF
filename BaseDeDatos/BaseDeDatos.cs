namespace ClinicaMedica {
	public interface IBaseDeDatos{
		// Read methods
		List<Medico> ReadMedicos();
		List<Paciente> ReadPacientes();
		List<Turno> ReadTurnos();

		// checkers
		bool CorroborarQueNoExistaMedico(string Id);
		bool CorroborarQueNoExistaPaciente(string Id);
		bool CorroborarQueNoExistaTurno(string Id);

		// Create methods
		void CreateMedico(Medico instance);
		void CreatePaciente(Paciente instance);
		void CreateTurno(Turno instance);

		// Update methods
		void UpdateMedico(Medico instance, string originalDni);
		void UpdatePaciente(Paciente instance, string originalDni);
		void UpdateTurno(Turno instance);

		// Delete methods
		bool DeleteMedico(string Id);
		bool DeletePaciente(string Id);
		bool DeleteTurno(string Id);
		
		
		string LoadPacienteNombreCompletoFromDatabase(string Id);
		string LoadMedicoNombreCompletoFromDatabase(string Id);
		string LoadEspecialidadFromDatabase(string Id);
	}
	
	
}