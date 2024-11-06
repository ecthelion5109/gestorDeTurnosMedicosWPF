namespace ClinicaMedica {
	public interface IBaseDeDatos{

		//Dictionary<string, Medico> DictMedicos;
		//Dictionary<string, Paciente> DictPacientes;
		//List<Turno> ListTurnos;

		// Read methods
		//List<Medico> ReadMedicos();
		//List<Paciente> ReadPacientes();
		List<Turno> ReadTurnosWhereMedicoId(Medico instance);
		
		
		// List<Turno> ReadMedicosWhereTurnoId(Turno instance);
		// List<Turno> ReadPacientesWhereTurnoId(Turno instance);


		

		// Read methods
		List<Medico> ReadMedicos();
		List<Paciente> ReadPacientes();
		List<Turno> ReadTurnos();

		// Create methods
		bool CreateMedico(Medico instance);
		bool CreatePaciente(Paciente instance);
		bool CreateTurno(Turno instance);

		// Update methods
		bool UpdateMedico(Medico instance, string originalDni);	//El dni es mas que nada para jsonMode
		bool UpdatePaciente(Paciente instance, string originalDni);//El dni es mas que nada para jsonMode
		bool UpdateTurno(Turno instance);//El dni es mas que nada para jsonMode

		// Delete methods
		bool DeleteMedico(Medico instance);
		bool DeletePaciente(Paciente instance);
		bool DeleteTurno(Turno instance);
		
		
		string LoadPacienteNombreCompletoFromDatabase(string Id);
		string LoadMedicoNombreCompletoFromDatabase(string Id);
		string LoadEspecialidadFromDatabase(string Id);
	}
	
	
}