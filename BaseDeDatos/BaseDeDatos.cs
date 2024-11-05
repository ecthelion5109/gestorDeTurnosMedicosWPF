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
		OperationCode CreateMedico(Medico instance);
		OperationCode CreatePaciente(Paciente instance);
		OperationCode CreateTurno(Turno instance);

		// Update methods
		OperationCode UpdateMedico(Medico instance, string originalDni);
		OperationCode UpdatePaciente(Paciente instance, string originalDni);
		OperationCode UpdateTurno(Turno instance);

		// Delete methods
		OperationCode DeleteMedico(string Id);
		OperationCode DeletePaciente(string Id);
		OperationCode DeleteTurno(string Id);
		
		
		string LoadPacienteNombreCompletoFromDatabase(string Id);
		string LoadMedicoNombreCompletoFromDatabase(string Id);
		string LoadEspecialidadFromDatabase(string Id);
	}
}