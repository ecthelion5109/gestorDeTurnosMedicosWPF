CREATE TABLE [dbo].[Medico] (
    [id]                       INT           IDENTITY (1, 1) NOT NULL,
    [nombre]                    NVARCHAR (50) NULL,
    [apellido]                  NVARCHAR (50) NULL,
    [provincia]                 NVARCHAR (50) NULL,
    [domicilio]                 NVARCHAR (50) NULL,
    [localidad]                 NVARCHAR (50) NULL,
    [especialidad]              NVARCHAR (50) NULL,
    [telefono]                  NVARCHAR (20) NULL,
    [guardia]                   BIT           NULL,
    [fecha_ingreso]             DATETIME2 (7) NULL,
    [sueldo_minimo_garantizado] FLOAT (53)    NULL,
    [dni] NCHAR(8) NULL, 
    PRIMARY KEY CLUSTERED ([id] ASC)
);


INSERT INTO [dbo].[Medico] 
    ([nombre], [apellido], [provincia], [domicilio], [localidad], [especialidad], [telefono], [guardia], [fecha_ingreso], [sueldo_minimo_garantizado], [dni])
VALUES 
    ('John', 'Doe', 'Buenos Aires', 'Av. Siempre Viva 123', 'Capital Federal', 'Cardiology', '123-456-7890', 1, '2022-01-15 08:30:00', 85000.50, '12345678'),
    ('Jane', 'Smith', 'Córdoba', 'Calle Falsa 456', 'Villa Carlos Paz', 'Neurology', '234-567-8901', 0, '2021-05-20 14:00:00', 92000.00, '87654321'),
    ('Michael', 'Johnson', 'Mendoza', 'Ruta 40 Km 12', 'Godoy Cruz', 'Pediatrics', '345-678-9012', 1, '2020-09-10 10:45:00', 78000.75, '11223344'),
    ('Emily', 'Davis', 'Salta', 'Calle San Martin 100', 'Salta', 'Oncology', '456-789-0123', 0, '2023-02-05 12:30:00', 99000.25, '55667788'),
    ('William', 'Brown', 'Santa Fe', 'Boulevard Galvez 2000', 'Rosario', 'Orthopedics', '567-890-1234', 1, '2019-12-25 18:00:00', 86000.00, '99887766');