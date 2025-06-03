CREATE DATABASE db_biblioteca
GO

USE db_biblioteca
GO

CREATE TABLE [Roles](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50)
);

CREATE TABLE [Administradores](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(100) NOT NULL,
	[Password] NVARCHAR(70) NOT NULL,
	[Role] INT NOT NULL REFERENCES Roles(Id)
);

CREATE TABLE [Accesos](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(100) NOT NULL,
);

CREATE TABLE Roles_tiene_Accesos(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Role] INT NOT NULL REFERENCES Roles(Id) ON DELETE CASCADE,
	[Acceso] INT NOT NULL REFERENCES Accesos(Id) ON DELETE CASCADE,
	[Acciones] TEXT NOT NULL
);

CREATE TABLE [Auditorias](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Administrador] NVARCHAR(100) NOT NULL,
	[Accion] NVARCHAR(300) NOT NULL,
	[Fecha] DATETIME DEFAULT GETDATE()
);

CREATE TABLE [Estanterias](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR (150) NOT NULL,
	[Activo] BIT NOT NULL,
	);

CREATE TABLE [Niveles](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR (150) NOT NULL,
	[Estanteria] INT REFERENCES [Estanterias]([Id])
	);

CREATE TABLE [Categorias](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR (150) NOT NULL,
	[Descripcion] NVARCHAR (300) NOT NULL,
	);

CREATE TABLE [Autores](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR (150) NOT NULL,
	[Apellido] NVARCHAR (30) NOT NULL,
	[FechaNacimiento] DATE NOT NULL
	);

CREATE TABLE [Libros](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR (150) NOT NULL,
	[Fecha] DATE NOT NULL,
	[Categoria] INT REFERENCES [Categorias]([Id]),
	[Autor] INT REFERENCES [Autores]([Id])
	);

CREATE TABLE [NumerosDeSerie](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[NumeroSerie] NVARCHAR (150) NOT NULL,
	[Libro] INT REFERENCES [Libros]([Id])
	);

CREATE TABLE [Niveles_tiene_Libros](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nivel] INT REFERENCES [Niveles]([Id]) ON DELETE CASCADE,
	[Libro] INT REFERENCES [Libros]([Id]) ON DELETE CASCADE
	);

CREATE TABLE [TiposDocumentos](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR (150) NOT NULL,
	[NombreCorto] NVARCHAR (30) NOT NULL,
	);

CREATE TABLE [Clientes](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Nombre] NVARCHAR (150) NOT NULL,
	[Apellido] NVARCHAR (150) NOT NULL,
	[Documento] NVARCHAR (150) NOT NULL,
	[Direccion] NVARCHAR (150) NOT NULL,
	[Telefono] NVARCHAR (150) NOT NULL,
	[Correo] NVARCHAR (150) NOT NULL,
	[TipoDocumento] INT REFERENCES [TiposDocumentos]([Id])
	);

CREATE TABLE [Prestamos](
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[FechaInicio] DATETIME NOT NULL,
	[FechaFinal] DATETIME NOT NULL,
	[FechaEntregado] DATETIME,
	[Cliente] INT REFERENCES [Clientes]([Id]),
	[NumeroSerie] INT REFERENCES [NumerosDeSerie]([Id])
	);


INSERT INTO [Estanterias] ([Nombre],[Activo])
VALUES ('5C',1)
INSERT INTO [Estanterias] ([Nombre],[Activo])
VALUES ('4R',1)
INSERT INTO [Estanterias] ([Nombre],[Activo])
VALUES ('3S',1)
INSERT INTO [Estanterias] ([Nombre],[Activo])
VALUES ('45F',1)
SELECT * FROM [Estanterias]

INSERT INTO [Niveles] ([Nombre],[Estanteria])
VALUES ('5',1)
INSERT INTO [Niveles] ([Nombre],[Estanteria])
VALUES ('2',2)
INSERT INTO [Niveles] ([Nombre],[Estanteria])
VALUES ('3',3)
INSERT INTO [Niveles] ([Nombre],[Estanteria])
VALUES ('6',4)
SELECT * FROM [Niveles]

INSERT INTO [Categorias] ([Nombre],[Descripcion])
VALUES ('Infantil','Libros para niños entre 5 y 12 años')
INSERT INTO [Categorias] ([Nombre],[Descripcion])
VALUES ('Educación','Libros para bachillerato y universidad')
INSERT INTO [Categorias] ([Nombre],[Descripcion])
VALUES ('Fantasia','Libros para todas las edades')
INSERT INTO [Categorias] ([Nombre],[Descripcion])
VALUES ('Novela','Libros de historias dramaticas')
SELECT * FROM [Categorias]

INSERT INTO [Autores] ([Nombre],[Apellido],[FechaNacimiento])
VALUES ('Antonie','de Saint-Exupéry','1900/6/29')
INSERT INTO [Autores] ([Nombre],[Apellido],[FechaNacimiento])
VALUES ('Aurelio','Baldor','1906/10/22')
INSERT INTO [Autores] ([Nombre],[Apellido],[FechaNacimiento])
VALUES ('J.K','Rowling','1965/7/31')
INSERT INTO [Autores] ([Nombre],[Apellido],[FechaNacimiento])
VALUES ('J.R.R','Tolkien','1982/1/3')
SELECT * FROM [Autores]

INSERT INTO [Libros] ([Nombre],[Fecha],[Categoria],[Autor])
VALUES ('El principito','1943/4/6',1,1)
INSERT INTO [Libros] ([Nombre],[Fecha],[Categoria],[Autor])
VALUES ('Algebra','1941/6/19',2,2)
INSERT INTO [Libros] ([Nombre],[Fecha],[Categoria],[Autor])
VALUES ('Harry Potter','1997/6/19',3,3)
INSERT INTO [Libros] ([Nombre],[Fecha],[Categoria],[Autor])
VALUES ('El señor de los anillos','1954/6/29',3,4)
SELECT * FROM [Libros]

INSERT INTO [NumerosDeSerie] ([NumeroSerie],[Libro])
VALUES ('P12',1)
INSERT INTO [NumerosDeSerie] ([NumeroSerie],[Libro])
VALUES ('A5',2)
INSERT INTO [NumerosDeSerie] ([NumeroSerie],[Libro])
VALUES ('H46',4)
INSERT INTO [NumerosDeSerie] ([NumeroSerie],[Libro])
VALUES ('E44',3)
SELECT * FROM [NumerosDeSerie]

INSERT INTO [Niveles_tiene_Libros] ([Nivel],[Libro])
VALUES (1,1)
INSERT INTO [Niveles_tiene_Libros] ([Nivel],[Libro])
VALUES (2,2)
INSERT INTO [Niveles_tiene_Libros] ([Nivel],[Libro])
VALUES (3,3)
INSERT INTO [Niveles_tiene_Libros] ([Nivel],[Libro])
VALUES (4,4)
SELECT * FROM [Niveles_tiene_Libros]

INSERT INTO [TiposDocumentos] ([Nombre],[NombreCorto])
VALUES ('cédula','CC')
INSERT INTO [TiposDocumentos] ([Nombre],[NombreCorto])
VALUES ('Tarjeta de indentidad','T.I')
INSERT INTO [TiposDocumentos] ([Nombre],[NombreCorto])
VALUES ('Pasaporte','P.P')
INSERT INTO [TiposDocumentos] ([Nombre],[NombreCorto])
VALUES ('cédula de extranjería','C.E')
SELECT * FROM [TiposDocumentos]

INSERT INTO [Clientes] ([Nombre],[Apellido],[Documento],[Direccion],[Correo],[Telefono],[TipoDocumento])
VALUES ('Jose','Henao','123456789','Calle 20 #40-21','correo@contacto.com','32124132',1)
INSERT INTO [Clientes] ([Nombre],[Apellido],[Documento],[Direccion],[Correo],[Telefono],[TipoDocumento])
VALUES ('Samantha','Diaz','1234256789','Carrera 19 #3-17','correo2@contacto.com','12345',1)
INSERT INTO [Clientes] ([Nombre],[Apellido],[Documento],[Direccion],[Correo],[Telefono],[TipoDocumento])
VALUES ('Miguel','Yagual','31028412','Calle 92 #20-20','correo3@contacto.com','31982137',1)
INSERT INTO [Clientes] ([Nombre],[Apellido],[Documento],[Direccion],[Correo],[Telefono],[TipoDocumento])
VALUES ('Sulay','Zapata','1593753','Calle 50 #2-10','correo4@contacto.com','11312137',1)
SELECT * FROM [Clientes]

INSERT INTO [Prestamos] ([FechaInicio],[FechaFinal],[FechaEntregado],[Cliente],[NumeroSerie])
VALUES ('2024/05/12 13:20:20','2024/05/12 13:20:20','2024/05/18 12:20:31',1,1)
INSERT INTO [Prestamos] ([FechaInicio],[FechaFinal],[FechaEntregado],[Cliente],[NumeroSerie])
VALUES ('2024/06/11 13:20:20','2024/06/11 13:20:20','2024/06/11 13:20:20',2,2)
INSERT INTO [Prestamos] ([FechaInicio],[FechaFinal],[FechaEntregado],[Cliente],[NumeroSerie])
VALUES ('2024/06/1 13:20:20','2025/08/1 9:40:00','2024/07/30 18:30:55',3,3)
INSERT INTO [Prestamos] ([FechaInicio],[FechaFinal],[FechaEntregado],[Cliente],[NumeroSerie])
VALUES ('2024/04/08 7:20:20','2024/04/08 7:20:20',NULL,4,4)
SELECT * FROM [Prestamos]

INSERT INTO [Roles] ([Nombre]) VALUES ('Jefe');

INSERT INTO [Accesos] ([Nombre]) VALUES 
('TiposDocumentos'),
('Clientes'),
('Estanterias'),
('Niveles'),
('Categorias'),
('Autores'),
('Libros'),
('Numeros de serie'),
('Prestamos'),
('Roles'),
('Accesos'),
('Administradores'),
('Auditorias'),
('Niveles_tiene_Libros'),
('Roles_tiene_Accesos');

INSERT INTO [Roles_tiene_Accesos] ([Role], [Acceso], [Acciones]) VALUES 
(1, 1, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 2, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 3, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 4, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 5, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 6, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 7, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 8, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 9, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 10, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 11, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 12, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 13, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 14, 'Listar, Por, Borrar, Crear, Guardar'),
(1, 15, 'Listar, Por, Borrar, Crear, Guardar');

INSERT INTO [Administradores] ([Nombre], [Password], [Role]) VALUES ('admin', '$2a$11$ER55nUgrwT5IYUQLYEJRYO69qLYJzaE4uZJNLD4NR2ojMVy2OfG7G', 1);

INSERT INTO Roles (Nombre) VALUES ('Invitado');

DECLARE @rolInvitado INT = (SELECT Id FROM Roles WHERE Nombre = 'Invitado');
DECLARE @accesoLibros INT = (SELECT Id FROM Accesos WHERE Nombre = 'Libros');

INSERT INTO Roles_tiene_Accesos (Role, Acceso, Acciones) VALUES (@rolInvitado, @accesoLibros, 'Listar, Por');

INSERT INTO [Administradores] ([Nombre], [Password], [Role]) VALUES ('Invitado', '$2a$12$X59nBidUOWnflcWsd8MfBOEwPTCOL0BYZO0U4X6Rz9w1A05iMkcVW', 2);