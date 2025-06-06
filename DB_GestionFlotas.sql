CREATE DATABASE GestionFlotas;
USE GestionFlotas;

-- TABLA USUARIOS
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY NOT NULL, --documento: administrador o conductor--
    PasswordHash VARBINARY (MAX) NOT NULL, 
	Rol VARCHAR(20) CHECK (Rol IN('Administrador', 'Conductor')) NOT NULL
);

-- TABLA CONDUCTORES
CREATE TABLE Conductores (
    ConductorID INT PRIMARY KEY NOT NULL, 
	Nombre VARCHAR (100) NOT NULL,
	Licencia VARCHAR(50) NULL, --aplica solo para conductores--
	Telefono VARCHAR (10) NULL, --aplica solo para conductores--
	Email VARCHAR (50) UNIQUE NOT NULL, --aplica para admins y conductores--
	PasswordHash VARBINARY (MAX) NOT NULL,
);

-- TABLA VEHICULOS
CREATE TABLE Vehiculos (
	VehiculoID INT PRIMARY KEY NOT NULL,
    Matricula VARCHAR(20)  NOT NULL ,
	Marca VARCHAR (50) NOT NULL,
	Modelo VARCHAR(4) NOT NULL, --AÑO DEL VEHÍCULO
    Tipo VARCHAR(50) NOT NULL,
    Capacidad INT NOT NULL,
	Estado VARCHAR(20) CHECK (Estado IN('Disponible','En uso', 'En mantenimiento')) DEFAULT 'Disponible'
);

-- TABLA VIAJES
CREATE TABLE Viajes (
    ViajeID INT PRIMARY KEY NOT NULL,
    VehiculoID INT,
    ConductorID INT,
    Ruta VARCHAR(255),
    FechaSalida DATETIME NOT NULL,
    FechaLlegada DATETIME NOT NULL,
	Estado VARCHAR(20) CHECK(Estado IN('Programado', 'Completado', 'Cancelado'))DEFAULT 'Programado',

    FOREIGN KEY (VehiculoID) REFERENCES Vehiculos(VehiculoID),
    FOREIGN KEY (ConductorID) REFERENCES Conductores(ConductorID)
);

-- TABLA MANTENIMIENTOS
CREATE TABLE Mantenimiento (
    MantenimientoID INT PRIMARY KEY NOT NULL,
    VehiculoID INT,
    FechaMantenimiento DATE NOT NULL,
	TipoMantenimiento VARCHAR(20) CHECK(TipoMantenimiento IN('Preventivo', 'Correctivo')),
    Descripcion TEXT,

    FOREIGN KEY (VehiculoID) REFERENCES Vehiculos(VehiculoID)
);

-- TABLA KILOMETRAJES
CREATE TABLE Kilometrajes (
    KilometrajeID INT PRIMARY KEY NOT NULL,
    VehiculoID INT,
    KilometrosRecorridos INT,
    FechaRegistro DATE,
    FOREIGN KEY (VehiculoID) REFERENCES Vehiculos(VehiculoID)
);

--TABLA DE REPORTES DE USO
CREATE TABLE ReportesUsoVehiculo (
    ReporteID INT PRIMARY KEY NOT NULL,
    ViajeID INT NOT NULL,
    Observaciones TEXT,
    EstadoVehiculoFinal VARCHAR(100),
    FechaReporte DATE NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (ViajeID) REFERENCES Viajes(ViajeID) ON DELETE CASCADE
);

INSERT INTO Usuarios (UsuarioID, PasswordHash, Rol)
VALUES ('1234598765', HASHBYTES('SHA1', 'driver123'), 'Conductor');

INSERT INTO Usuarios (UsuarioID, PasswordHash, Rol)
VALUES ('1987654321', HASHBYTES('SHA1', 'admin123'), 'Administrador');

--Procedimientos almacenados

-- 1. SP_ProgramarViaje
CREATE OR ALTER PROCEDURE SP_ProgramarViaje
    @VehiculoID INT,
    @ConductorID INT,
    @Ruta NVARCHAR(255),
    @FechaSalida DATETIME,
    @FechaLlegada DATETIME
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Vehiculos WHERE ID = @VehiculoID AND Estado = 'Disponible')
       AND EXISTS (SELECT 1 FROM Conductores WHERE ID = @ConductorID AND Disponible = 1)
    BEGIN
        INSERT INTO Viajes (VehiculoID, ConductorID, Ruta, FechaSalida, FechaLlegada)
        VALUES (@VehiculoID, @ConductorID, @Ruta, @FechaSalida, @FechaLlegada)

        UPDATE Vehiculos SET Estado = 'En Viaje' WHERE ID = @VehiculoID
        UPDATE Conductores SET Disponible = 0 WHERE ID = @ConductorID
    END
    ELSE
    BEGIN
        RAISERROR('Vehículo o conductor no disponible.', 16, 1)
    END
END
GO

-- 2. SP_RegistrarMantenimiento
CREATE OR ALTER PROCEDURE SP_RegistrarMantenimiento
    @VehiculoID INT,
    @Fecha DATETIME,
    @Tipo NVARCHAR(100),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    UPDATE Vehiculos SET Estado = 'En mantenimiento' WHERE ID = @VehiculoID

    INSERT INTO Mantenimientos (VehiculoID, Fecha, Tipo, Descripcion)
    VALUES (@VehiculoID, @Fecha, @Tipo, @Descripcion)
END
GO

-- 3. SP_ActualizarKilometraje
CREATE OR ALTER PROCEDURE SP_ActualizarKilometraje
    @VehiculoID INT,
    @Kilometros INT,
    @Fecha DATETIME
AS
BEGIN
    UPDATE Vehiculos SET Kilometraje = Kilometraje + @Kilometros WHERE ID = @VehiculoID

    DECLARE @NuevoKm INT
    SELECT @NuevoKm = Kilometraje FROM Vehiculos WHERE ID = @VehiculoID

    IF (@NuevoKm >= 10000)
    BEGIN
        SELECT '¡Atención! El vehículo requiere mantenimiento preventivo.' AS Alerta
    END
    ELSE
    BEGIN
        SELECT 'Kilometraje actualizado correctamente.' AS Alerta
    END
END
GO

-- 4. SP_ObtenerHistorialMantenimiento
CREATE OR ALTER PROCEDURE SP_ObtenerHistorialMantenimiento
    @VehiculoID INT
AS
BEGIN
    SELECT * FROM Mantenimientos WHERE VehiculoID = @VehiculoID ORDER BY Fecha DESC
END
GO

-- 5. SP_GenerarReporteUsoVehiculo
CREATE OR ALTER PROCEDURE SP_GenerarReporteUsoVehiculo
AS
BEGIN
    SELECT 
        V.ID AS VehiculoID,
        COUNT(VJ.ID) AS TotalViajes,
        SUM(DATEDIFF(HOUR, VJ.FechaSalida, VJ.FechaLlegada)) AS HorasUso,
        SUM(VJ.KilometrosRecorridos) AS KilometrosTotales
    FROM Vehiculos V
    LEFT JOIN Viajes VJ ON V.ID = VJ.VehiculoID
    GROUP BY V.ID
END
GO

SELECT * FROM Usuarios;
SELECT * FROM Mantenimiento;
SELECT * FROM Vehiculos;
SELECT * FROM Conductores;