namespace ClinicaMedica {
	public abstract class BaseDeDatosAbstracta{
		
		// Public diccionarios
		public static Dictionary<string, Turno> DictTurnos = new();
		public static Dictionary<string, Medico> DictMedicos = new();
		public static Dictionary<string, Paciente> DictPacientes = new();
		
		// Read methods
		public abstract List<Medico> ReadMedicos();
		public abstract List<Paciente> ReadPacientes();
		public abstract List<Turno> ReadTurnos();

		// Create methods
		public abstract bool CreateMedico(Medico instance);
		public abstract bool CreatePaciente(Paciente instance);
		public abstract bool CreateTurno(Turno instance);

		// Update methods
		public abstract bool UpdateMedico(Medico instance, string originalDni);	//El dni es mas que nada para jsonMode
		public abstract bool UpdatePaciente(Paciente instance, string originalDni);//El dni es mas que nada para jsonMode
		public abstract bool UpdateTurno(Turno instance);//El dni es mas que nada para jsonMode

		// Delete methods
		public abstract bool DeleteMedico(Medico instance);
		public abstract bool DeletePaciente(Paciente instance);
		public abstract bool DeleteTurno(Turno instance);
		
		// Filtros
		public List<Turno> ReadTurnosWhereMedicoId(Medico instance) {
			if (instance is null){
				return null;
			}
			return DictTurnos.Values.Where(t => t.MedicoId == instance.Id).ToList();
		}
		public List<Turno> ReadTurnosWherePacienteId(Paciente instance) {
			if (instance is null){
				return null;
			}
			return DictTurnos.Values.Where(t => t.PacienteId == instance.Id).ToList();
		}
		public List<string> ReadDistinctEspecialidades() {
			return DictMedicos.Values
								.Select(medico => medico.Especialidad)
								.Distinct()
								.ToList();
		}
		
		
	}
	
	
}