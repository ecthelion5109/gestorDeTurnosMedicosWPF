CREATE DATABASE ClinicaMedica;
USE ClinicaMedica;

CREATE TABLE [dbo].[Medico] (
    [id]                       INT           IDENTITY (1, 1) NOT NULL,
    [nombre]                   NVARCHAR (50) NULL,
    [apellido]                 NVARCHAR (50) NULL,
    [provincia]                NVARCHAR (50) NULL,
    [domicilio]                NVARCHAR (50) NULL,
    [localidad]                NVARCHAR (50) NULL,
    [especialidad]             NVARCHAR (50) NULL,
    [telefono]                 NVARCHAR (20) NULL,
    [guardia]                  BIT           NULL,
    [fecha_ingreso]            DATETIME2 (7) NULL,
    [sueldo_minimo_garantizado] FLOAT (53)    NULL,
    [dni]                      NCHAR(8)      NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [unique_dni] UNIQUE ([dni]) -- Adding the UNIQUE constraint
);

-- Inserting some sample data
INSERT INTO [dbo].[Medico] 
    ([nombre], [apellido], [provincia], [domicilio], [localidad], [especialidad], [telefono], [guardia], [fecha_ingreso], [sueldo_minimo_garantizado], [dni])
VALUES 
    ('John', 'Doe', 'Buenos Aires', 'Av. Siempre Viva 123', 'Capital Federal', 'Cardiology', '123-456-7890', 1, '2022-01-15 08:30:00', 85000.50, '12345678'),
    ('Jane', 'Smith', 'Córdoba', 'Calle Falsa 456', 'Villa Carlos Paz', 'Neurology', '234-567-8901', 0, '2021-05-20 14:00:00', 92000.00, '87654321'),
    ('Michael', 'Johnson', 'Mendoza', 'Ruta 40 Km 12', 'Godoy Cruz', 'Pediatrics', '345-678-9012', 1, '2020-09-10 10:45:00', 78000.75, '11223344'),
    ('Emily', 'Davis', 'Salta', 'Calle San Martin 100', 'Salta', 'Oncology', '456-789-0123', 0, '2023-02-05 12:30:00', 99000.25, '55667788'),
    ('William', 'Brown', 'Santa Fe', 'Boulevard Galvez 2000', 'Rosario', 'Orthopedics', '567-890-1234', 1, '2019-12-25 18:00:00', 86000.00, '99887766');

-- Creating the Paciente table
CREATE TABLE [dbo].[Paciente] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Dni]                NCHAR(8)      NOT NULL,
    [Name]               NVARCHAR(50)  NULL,
    [Lastname]           NVARCHAR(50)  NULL,
    [FechaIngreso]       DATETIME      NOT NULL,
    [Email]              NVARCHAR(50)  NULL,
    [Telefono]           NCHAR(11)     NULL,
    [FechaNacimiento]    DATETIME      NULL,
    [Direccion]          NVARCHAR(50)  NULL,
    [Localidad]          NVARCHAR(50)  NULL,
    [Provincia]          NVARCHAR(50)  NULL,
    CONSTRAINT [unique_paciente_dni] UNIQUE ([Dni]) -- Adding a unique constraint for DNI
);

-- Inserting some sample data into the Paciente table
INSERT INTO [dbo].[Paciente]
    ([Dni], [Name], [Lastname], [FechaIngreso], [Email], [Telefono], [FechaNacimiento], [Direccion], [Localidad], [Provincia])
VALUES
    ('98765432', 'Laura', 'Gomez', '2023-04-15', 'laura.gomez@mail.com', '123-456-7890', '1990-07-20', 'Calle Principal 123', 'San Miguel', 'Buenos Aires'),
    ('12349876', 'Carlos', 'Rodriguez', '2022-08-10', 'carlos.rodriguez@mail.com', '234-567-8901', '1985-11-15', 'Avenida Central 456', 'Córdoba', 'Córdoba'),
    ('56781234', 'Ana', 'Martinez', '2021-02-28', 'ana.martinez@mail.com', '345-678-9012', '1978-03-25', 'Boulevard Norte 789', 'Rosario', 'Santa Fe'),
    ('43219876', 'Javier', 'Fernandez', '2020-11-05', 'javier.fernandez@mail.com', '456-789-0123', '1982-12-05', 'Pasaje del Sol 111', 'Mendoza', 'Mendoza'),
    ('87654321', 'Sofia', 'Lopez', '2019-05-22', 'sofia.lopez@mail.com', '567-890-1234', '1995-09-30', 'Calle Sur 222', 'Salta', 'Salta');
