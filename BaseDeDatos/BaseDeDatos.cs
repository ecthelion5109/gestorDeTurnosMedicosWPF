using System.Windows;

namespace ClinicaMedica {
	public abstract class BaseDeDatosAbstracta{
		public Dictionary<string, Turno> DictTurnos = new();
		public Dictionary<string, Medico> DictMedicos = new();
		public Dictionary<string, Paciente> DictPacientes = new();
		public virtual bool ConectadaExitosamente { get; protected set; } = false;
		
		// Read methods
		public abstract List<Medico> ReadMedicos();
		public abstract List<Paciente> ReadPacientes();
		public abstract List<Turno> ReadTurnos();

		// Create methods
		public abstract bool CreateMedico(Medico instance);
		public abstract bool CreatePaciente(Paciente instance);
		public abstract bool CreateTurno(Turno instance);

		// Update methods
		public abstract bool UpdateMedico(Medico instance);
		public abstract bool UpdatePaciente(Paciente instance);
		public abstract bool UpdateTurno(Turno instance);

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

		public List<Medico> ReadMedicosWhereEspecialidad(string especialidad) {
			return DictMedicos.Values.Where(m => m.Especialidad == especialidad).ToList();
		}
		public List<string> ReadDistinctEspecialidades() {
			return DictMedicos.Values.Select(medico => medico.Especialidad).Distinct().ToList();
		}

		//------------------------settings----------------------//
		// public abstract bool EliminarDatabaseExitosamente();

	}
	
	
}