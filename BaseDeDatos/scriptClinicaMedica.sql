-- CREATE DATABASE ClinicaMedica;
USE ClinicaMedica;

CREATE TABLE [dbo].[Medico] (
    [Id]                       INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Dni]                      NCHAR(8)      NOT NULL,
    [Name]                     NVARCHAR (50) NULL,
    [LastName]                 NVARCHAR (50) NULL,
    [FechaIngreso]       	   DATETIME      NOT NULL,
    [Domicilio]                NVARCHAR (50) NULL,
    [Localidad]                NVARCHAR (50) NULL,
    [Provincia]                NVARCHAR (50) NULL,
    [Telefono]                 NVARCHAR (20) NULL,
	
    [Especialidad]             NVARCHAR (50) NULL,
    [Guardia]                  BIT           NULL,
    [SueldoMinimoGarantizado]  FLOAT (53)    NULL,
	
    CONSTRAINT [unique_dni] UNIQUE ([Dni]) -- Adding the UNIQUE constraint
);

-- Inserting some sample data
INSERT INTO [dbo].[Medico] 
    ([Name], [LastName], [Provincia], [Domicilio], [Localidad], [Especialidad], [Telefono], [Guardia], [FechaIngreso], [SueldoMinimoGarantizado], [Dni])
VALUES 
    ('Dr. Ricardo', 'Arjona', 'Buenos Aires', 'Av. Siempre Viva 123', 'Capital Federal', 'Cardiology', '123-456-7890', 1, '2022-01-15 00:00:00', 85000.50, '12345678'),
    ('Dr. Tocando', 'Conchitas', 'Córdoba', 'Calle Falsa 456', 'Villa Carlos Paz', 'Gynecology', '234-567-8901', 0, '2021-05-20 00:00:00', 92000.00, '87654321'),
    ('Dr. Mario', 'Socolinsky', 'Mendoza', 'Ruta 40 Km 12', 'Godoy Cruz', 'Pediatrics', '345-678-9012', 1, '2020-09-10 00:00:00', 78000.75, '11223344'),
    ('Dra. Roxana', 'Toledo', 'Salta', 'Calle San Martin 100', 'Salta', 'Massage Therapist', '456-789-0123', 0, '2023-02-05 00:00:00', 99000.25, '55667788'),
    ('Dra. Tete', 'Falopa', 'Santa Fe', 'Boulevard Galvez 2000', 'Rosario', 'Holistic Healer', '567-890-1234', 1, '2019-12-25 00:00:00', 86000.00, '99887766'),
    ('Dra. Debora', 'Meltrozo', 'Buenos Aires', 'Ruta 40 Km 10', 'Uruguay', 'Substance Trafficker', NULL, NULL, '2024-10-05 00:00:00', 3564534543.00, '40350997'),
    ('Dr. Miguel', 'DedoGordo', 'Buenos Aires', 'Ruta 40 Km 13', 'Italia', 'Proctologist', NULL, NULL, '2021-10-03 00:00:00', 543555543.00, '54355292');


-- Creating the Paciente table
CREATE TABLE [dbo].[Paciente] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Dni]                NCHAR(8)      NOT NULL,
    [Name]               NVARCHAR(50)  NULL,
    [LastName]           NVARCHAR(50)  NULL,
    [FechaIngreso]       DATETIME      NOT NULL,
    [Domicilio]          NVARCHAR(50)  NULL,
    [Localidad]          NVARCHAR(50)  NULL,
    [Provincia]          NVARCHAR(50)  NULL,
    [Telefono]           NVARCHAR(20) NULL,
	
    [Email]              NVARCHAR(50)  NULL,
    [FechaNacimiento]    DATETIME      NULL,
	
    CONSTRAINT [unique_paciente_dni] UNIQUE ([Dni])
);

-- Inserting some sample data into the Paciente table
INSERT INTO [dbo].[Paciente] 
    ([Dni], [Name], [LastName], [FechaIngreso], [Domicilio], [Localidad], [Provincia], [Telefono], [Email], [FechaNacimiento])
VALUES 
    ('87654321', 'Ana', 'Gonzalez', '2023-04-15 09:30:00', 'Calle Flores 123', 'Buenos Aires', 'Buenos Aires', '123-456-7890', 'ana.gonzalez@example.com', '1990-06-10'),
    ('12345678', 'Carlos', 'Pereira', '2022-11-30 14:45:00', 'Av. Libertad 456', 'Rosario', 'Santa Fe', '234-567-8901', 'carlos.pereira@example.com', '1985-02-18'),
    ('23456789', 'Maria', 'Lopez', '2024-01-05 08:00:00', 'San Martin 789', 'Córdoba', 'Córdoba', '345-678-9012', 'maria.lopez@example.com', '1992-09-25'),
    ('34567890', 'Juan', 'Martinez', '2021-08-20 10:15:00', 'Ruta 9 Km 15', 'Mendoza', 'Mendoza', '456-789-0123', 'juan.martinez@example.com', '1978-12-05'),
    ('45678901', 'Sofia', 'Ramirez', '2023-03-10 12:30:00', 'Boulevard Galvez 200', 'Salta', 'Salta', '567-890-1234', 'sofia.ramirez@example.com', '1988-04-22');
