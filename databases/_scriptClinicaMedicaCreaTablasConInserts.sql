-- Create HorarioMedico table
CREATE TABLE HorarioMedico (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MedicoId INT,
    DiaSemana NVARCHAR(12),
    HoraInicio TIME,
    HoraFin TIME,
    FOREIGN KEY (MedicoId) REFERENCES Medico(Id)
);



-- Create Medico table
CREATE TABLE Medico (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Dni NCHAR(8) NOT NULL UNIQUE,
    Name NVARCHAR(50),
    LastName NVARCHAR(50),
    FechaIngreso DATETIME NOT NULL,
    Domicilio NVARCHAR(50),
    Localidad NVARCHAR(50),
    Provincia NVARCHAR(50),
    Telefono NVARCHAR(20),
    Especialidad NVARCHAR(50) NOT NULL,
    Guardia BIT,
    SueldoMinimoGarantizado FLOAT(53)
);

-- Insert sample data into Medico
INSERT INTO Medico (Name, LastName, Provincia, Domicilio, Localidad, Especialidad, Telefono, Guardia, FechaIngreso, SueldoMinimoGarantizado, Dni)
VALUES 
    ('Dr. Ricardo', 'Arjona', 'Buenos Aires', 'Av. Siempre Viva 123', 'Capital Federal', 'Cardiólogo', '123-456-7890', 1, '2022-01-15', 85000.50, '12345678'),
    ('Dr. Tocando', 'Shells', 'Córdoba', 'Calle Falsa 456', 'Villa Carlos Paz', 'Ginecólogo', '234-567-8901', 0, '2021-05-20', 92000.00, '87654321'),
    ('Dr. Mario', 'Socolinsky', 'Mendoza', 'Ruta 40 Km 12', 'Godoy Cruz', 'Pediatra', '345-678-9012', 1, '2020-09-10', 78000.75, '11223344'),
    ('Dra. Roxana', 'Toledo', 'Salta', 'Calle San Martin 100', 'Salta', 'Masagista de Genitales', '456-789-0123', 0, '2023-02-05', 99000.25, '55667788'),
    ('Dra. Tete', 'Falopa', 'Santa Fe', 'Boulevard Galvez 2000', 'Rosario', 'Curadora de Empachos', '567-890-1234', 1, '2019-12-25', 86000.00, '99887766'),
    ('Dra. Debora', 'Meltrozo', 'Buenos Aires', 'Ruta 40 Km 10', 'Uruguay', 'Traficante de Estupefacientes', '123-890-9252', 1, '2024-10-05', 3564534543.00, '40350997'),
    ('Dr. Miguel', 'DedoGordo', 'Buenos Aires', 'Ruta 40 Km 13', 'Italia', 'Proctologo', '234-890-5216', 0, '2021-10-03', 543555543.00, '54355292'),
    ('Dr. Felipe', 'Estomagón', 'Chaco', 'Av. Curva Peligrosa 78', 'Resistencia', 'Gastroenterólogo', '678-123-4567', 1, '2023-07-14', 75000.00, '65432198'),
    ('Dr. Paco', 'Lespiedras', 'Misiones', 'Ruta de Tierra 99', 'Posadas', 'Osteópata', '789-234-5678', 0, '2022-11-01', 83000.75, '45678901'),
    ('Dra. Clara', 'Mentoni', 'La Pampa', 'Calle Polvorienta 101', 'Santa Rosa', 'Abortera Clandestina', '890-345-6789', 1, '2020-04-20', 70000.25, '11225588'),
    ('Dr. Tato', 'PechoFrio', 'Buenos Aires', 'Avenida Helada 404', 'Capital Federal', 'Cardiólogo', '901-456-7890', 1, '2021-03-12', 91000.00, '99884455'),
    ('Dra. Cora', 'Soncorazón', 'Entre Ríos', 'Calle Siempreviva 77', 'Paraná', 'Cardiólogo', '123-567-8901', 0, '2023-06-18', 78000.50, '33221144'),
    ('Dr. Guillermo', 'LaDroga', 'Río Negro', 'Camino Sin Retorno 22', 'Bariloche', 'Psiquiatra', '234-678-9012', 1, '2018-12-10', 86000.00, '66778899'),
    ('Dra. Fatima', 'Porro', 'Catamarca', 'Calle Tempestuosa 33', 'San Fernando', 'Psiquiatra', '345-789-0123', 0, '2024-02-11', 82500.75, '88776655');








-- Create Paciente table
CREATE TABLE Paciente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Dni NCHAR(8) NOT NULL UNIQUE,
    Name NVARCHAR(50),
    LastName NVARCHAR(50),
    FechaIngreso DATETIME NOT NULL,
    Domicilio NVARCHAR(50),
    Localidad NVARCHAR(50),
    Provincia NVARCHAR(50),
    Telefono NVARCHAR(20),
    Email NVARCHAR(50),
    FechaNacimiento DATETIME
);

-- Insert sample data into Paciente
INSERT INTO Paciente (Dni, Name, LastName, FechaIngreso, Domicilio, Localidad, Provincia, Telefono, Email, FechaNacimiento)
VALUES 
    ('87654321', 'Ana', 'Gonzalez', '2023-04-15 09:30', 'Calle Flores 123', 'Buenos Aires', 'Buenos Aires', '123-456-7890', 'ana.gonzalez@example.com', '1990-06-10'),
    ('12345678', 'Carlos', 'Pereira', '2022-11-30 14:45', 'Av. Libertad 456', 'Rosario', 'Santa Fe', '234-567-8901', 'carlos.pereira@example.com', '1985-02-18'),
    ('23456789', 'Maria', 'Lopez', '2024-01-05 08:00', 'San Martin 789', 'Córdoba', 'Córdoba', '345-678-9012', 'maria.lopez@example.com', '1992-09-25'),
    ('34567890', 'Juan', 'Martinez', '2021-08-20 10:15', 'Ruta 9 Km 15', 'Mendoza', 'Mendoza', '456-789-0123', 'juan.martinez@example.com', '1978-12-05'),
    ('45678901', 'Sofia', 'Ramirez', '2023-03-10 12:30', 'Boulevard Galvez 200', 'Salta', 'Salta', '567-890-1234', 'sofia.ramirez@example.com', '1988-04-22'),
    ('56789012', 'Lucia', 'Fernandez', '2023-05-18 16:30', 'Calle Belgrano 450', 'La Plata', 'Buenos Aires', '678-901-2345', 'lucia.fernandez@example.com', '1995-07-13'),
    ('67890123', 'Ricardo', 'Gomez', '2022-12-10 11:00', 'Avenida Mitre 1200', 'Mar del Plata', 'Buenos Aires', '789-012-3456', 'ricardo.gomez@example.com', '1983-03-22'),
    ('78901234', 'Isabel', 'Hernandez', '2023-09-15 09:45', 'Calle Avellaneda 600', 'Tucumán', 'Tucumán', '890-123-4567', 'isabel.hernandez@example.com', '1991-11-30'),
    ('89012345', 'Fernando', 'Torres', '2023-07-25 14:00', 'Calle San Juan 300', 'Rosario', 'Santa Fe', '901-234-5678', 'fernando.torres@example.com', '1980-01-10'),
    ('90123456', 'Adriana', 'Vazquez', '2024-02-20 17:30', 'Calle Figueroa 800', 'Buenos Aires', 'Buenos Aires', '012-345-6789', 'adriana.vazquez@example.com', '1994-05-03');


-- Create Turno table
CREATE TABLE Turno (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PacienteID INT NOT NULL,
    MedicoId INT NOT NULL,
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    CONSTRAINT no_disponible_medico UNIQUE (MedicoId, Fecha, Hora),
    CONSTRAINT no_disponible_paciente UNIQUE (PacienteID, Fecha, Hora),
    FOREIGN KEY (PacienteID) REFERENCES Paciente(Id),
    FOREIGN KEY (MedicoId) REFERENCES Medico(Id)
);

-- Insert sample data into Turno
-- Insert sample data into Turno
INSERT INTO Turno (PacienteID, MedicoId, Fecha, Hora)
VALUES
    (1, 2, '2024-11-01', '09:00'),
    (1, 3, '2024-11-02', '09:30'),
    (2, 4, '2024-11-03', '10:00'),
    (2, 5, '2024-11-04', '10:30'),
    (3, 6, '2024-11-05', '11:00'),
    (3, 7, '2024-11-06', '11:30'),
    (4, 8, '2024-11-07', '12:00'),
    (4, 9, '2024-11-08', '12:30'),
    (5, 10, '2024-11-09', '13:00'),
    (6, 1, '2024-11-10', '13:30'),
    (7, 2, '2024-11-11', '14:00'),
    (8, 3, '2024-11-12', '14:30'),
    (9, 4, '2024-11-13', '15:00'),
    (10, 5, '2024-11-14', '15:30'),
    (1, 6, '2024-11-15', '16:00'),
    (2, 7, '2024-11-16', '16:30'),
    (3, 8, '2024-11-17', '17:00'),
    (4, 9, '2024-11-18', '17:30'),
    (5, 10, '2024-11-19', '18:00'),
    (6, 1, '2024-11-20', '18:30'),
    (7, 2, '2024-11-21', '19:00'),
    (8, 3, '2024-11-22', '19:30'),
    (9, 4, '2024-11-23', '20:00'),
    (10, 5, '2024-11-24', '20:30'),
    (1, 6, '2024-11-25', '21:00'),
    (2, 7, '2024-11-26', '21:30'),
    (3, 8, '2024-11-27', '22:00'),
    (4, 9, '2024-11-28', '22:30'),
    (5, 10, '2024-11-29', '23:00'),
    (6, 1, '2024-11-30', '23:30'),
    (7, 2, '2024-12-01', '00:00'),
    (8, 3, '2024-12-02', '00:30'),
    (9, 4, '2024-12-03', '01:00'),
    (10, 5, '2024-12-04', '01:30'),
    (1, 6, '2024-12-05', '02:00'),
    (2, 7, '2024-12-06', '02:30'),
    (3, 8, '2024-12-07', '03:00'),
    (4, 9, '2024-12-08', '03:30'),
    (5, 10, '2024-12-09', '04:00'),
    (6, 1, '2024-12-10', '04:30'),
    (7, 2, '2024-12-11', '05:00'),
    (8, 3, '2024-12-12', '05:30'),
    (9, 4, '2024-12-13', '06:00'),
    (10, 5, '2024-12-14', '06:30'),
    (1, 6, '2024-12-15', '07:00'),
    (2, 7, '2024-12-16', '07:30'),
    (3, 8, '2024-12-17', '08:00'),
    (4, 9, '2024-12-18', '08:30'),
    (5, 10, '2024-12-19', '09:00');
