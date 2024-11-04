-- Drop the database if it exists
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'ClinicaMedica')
BEGIN
    DROP DATABASE ClinicaMedica;
END

-- Create the database
CREATE DATABASE ClinicaMedica;
GO  -- Ensure the batch completes before moving to the next command

-- Switch to the database context
USE ClinicaMedica;

-- Drop tables if they already exist
IF OBJECT_ID('dbo.Medico', 'U') IS NOT NULL DROP TABLE dbo.Medico;
IF OBJECT_ID('dbo.Paciente', 'U') IS NOT NULL DROP TABLE dbo.Paciente;
IF OBJECT_ID('dbo.Turno', 'U') IS NOT NULL DROP TABLE dbo.Turno;

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
    Especialidad NVARCHAR(50),
    Guardia BIT,
    SueldoMinimoGarantizado FLOAT(53)
);

-- Insert sample data into Medico
INSERT INTO Medico (Name, LastName, Provincia, Domicilio, Localidad, Especialidad, Telefono, Guardia, FechaIngreso, SueldoMinimoGarantizado, Dni)
VALUES 
    ('Dr. Ricardo', 'Arjona', 'Buenos Aires', 'Av. Siempre Viva 123', 'Capital Federal', 'Cardiologo', '123-456-7890', 1, '2022-01-15', 85000.50, '12345678'),
    ('Dr. Tocando', 'Shells', 'Córdoba', 'Calle Falsa 456', 'Villa Carlos Paz', 'Ginecologo', '234-567-8901', 0, '2021-05-20', 92000.00, '87654321'),
    ('Dr. Mario', 'Socolinsky', 'Mendoza', 'Ruta 40 Km 12', 'Godoy Cruz', 'Pediatra', '345-678-9012', 1, '2020-09-10', 78000.75, '11223344'),
    ('Dra. Roxana', 'Toledo', 'Salta', 'Calle San Martin 100', 'Salta', 'Masagista de Genitales', '456-789-0123', 0, '2023-02-05', 99000.25, '55667788'),
    ('Dra. Tete', 'Falopa', 'Santa Fe', 'Boulevard Galvez 2000', 'Rosario', 'Curadora de Empachos', '567-890-1234', 1, '2019-12-25', 86000.00, '99887766'),
    ('Dra. Debora', 'Meltrozo', 'Buenos Aires', 'Ruta 40 Km 10', 'Uruguay', 'Traficante de Estupefacientes', '123-890-9252', 1, '2024-10-05', 3564534543.00, '40350997'),
    ('Dr. Miguel', 'DedoGordo', 'Buenos Aires', 'Ruta 40 Km 13', 'Italia', 'Proctologo', '234-890-5216', 0, '2021-10-03', 543555543.00, '54355292');

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
    ('45678901', 'Sofia', 'Ramirez', '2023-03-10 12:30', 'Boulevard Galvez 200', 'Salta', 'Salta', '567-890-1234', 'sofia.ramirez@example.com', '1988-04-22');

-- Create Turno table
CREATE TABLE Turno (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PacienteID INT NOT NULL,
    MedicoID INT NOT NULL,
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    CONSTRAINT no_disponible_medico UNIQUE (MedicoID, Fecha, Hora),
    CONSTRAINT no_disponible_paciente UNIQUE (PacienteID, Fecha, Hora),
    FOREIGN KEY (PacienteID) REFERENCES Paciente(Id),
    FOREIGN KEY (MedicoID) REFERENCES Medico(Id)
);

-- Insert sample data into Turno
INSERT INTO Turno (PacienteID, MedicoID, Fecha, Hora)
VALUES
    (1, 1, '2024-11-01', '09:00'),
    (2, 1, '2024-11-02', '09:30'),
    (3, 2, '2024-11-03', '10:00'),
    (4, 2, '2024-11-04', '10:30'),
    (5, 3, '2024-11-05', '11:00'),
    (1, 3, '2024-11-05', '11:30'),
    (2, 4, '2024-11-01', '09:00'),
    (3, 4, '2024-11-05', '09:30'),
    (4, 5, '2024-11-11', '10:00'),
    (5, 5, '2024-11-11', '10:30'),
    (1, 3, '2024-11-20', '10:30'),
    (5, 5, '2024-11-25', '10:30')
;
