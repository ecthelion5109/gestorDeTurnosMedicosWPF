namespace ClinicaMedica {
	public interface IBaseDeDatos{

		//Dictionary<string, Medico> DictMedicos;
		//Dictionary<string, Paciente> DictPacientes;
		//List<Turno> DictTurnos;
		// Read methods
		List<Medico> ReadMedicos();
		List<Paciente> ReadPacientes();
		List<Turno> ReadTurnos();

		// Create methods
		bool CreateMedico(Medico instance);
		bool CreatePaciente(Paciente instance);
		bool CreateTurno(Turno instance);

		// Update methods
		bool UpdateMedico(Medico instance, string originalDni);
		bool UpdatePaciente(Paciente instance, string originalDni);
		bool UpdateTurno(Turno instance);

		// Delete methods
		bool DeleteMedico(string Id);
		bool DeletePaciente(string Id);
		bool DeleteTurno(string Id);
		
		
		string LoadPacienteNombreCompletoFromDatabase(string Id);
		string LoadMedicoNombreCompletoFromDatabase(string Id);
		string LoadEspecialidadFromDatabase(string Id);
	}
	
	
}