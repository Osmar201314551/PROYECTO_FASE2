CREATE DATABASE BD_201314551;
USE BD_201314551;


CREATE TABLE Bitacora(
id_bitacora INTEGER IDENTITY NOT NULL PRIMARY KEY,
fecha DATETIME NOT NULL DEFAULT GETDATE(),
usuario VARCHAR(50) NOT NULL,
direccion_ip VARCHAR(50) NOT NULL,
accion VARCHAR(250) NOT NULL
);

CREATE TABLE Genero(
id_genero INTEGER IDENTITY NOT NULL PRIMARY KEY,
nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Editorial(
id_editorial INTEGER IDENTITY NOT NULL PRIMARY KEY,
nombre VARCHAR(50) NOT NULL,
direccion VARCHAR(50) NOT NULL,
telefono INT NOT NULL
);

CREATE TABLE Usuario(
id_usuario INTEGER IDENTITY NOT NULL PRIMARY KEY,
nombre VARCHAR(50) NOT NULL,
apellido VARCHAR(50) NOT NULL,
usuario VARCHAR(50) NOT NULL,
tipo_usr VARCHAR(50) NOT NULL,
nacimiento DATE NOT NULL,
contrasenia VARBINARY(500) NOT NULL,
imagen INTEGER NOT NULL,
);

CREATE TABLE Libro(
id_libro INTEGER IDENTITY NOT NULL PRIMARY KEY,
id_editorial INTEGER NOT NULL,
genero INTEGER NOT NULL,
titulo VARCHAR(50) NOT NULL,
autor VARCHAR(50) NOT NULL,
cantidad INTEGER NOT NULL,
FOREIGN KEY (id_editorial) REFERENCES Editorial(id_editorial),
FOREIGN KEY (genero) REFERENCES Genero(id_genero)
);

CREATE TABLE Prestamo(
id_prestamo INTEGER IDENTITY NOT NULL PRIMARY KEY,
id_usuario INTEGER NOT NULL,
id_libro INTEGER NOT NULL,
estatus BIT NOT NULL,
FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario) ON DELETE CASCADE,
FOREIGN KEY (id_libro) REFERENCES Libro(id_libro)
);

INSERT INTO Usuario (nombre, apellido, usuario, tipo_usr, nacimiento, contrasenia, imagen) VALUES ('Administrador', 'Administrador', 'superuser', 1, '1990-1-1', ENCRYPTBYPASSPHRASE('password', 'superuser'), 1);
INSERT INTO Usuario (nombre, apellido, usuario, tipo_usr, nacimiento, contrasenia, imagen) VALUES ('Juan', 'Gonzalez', 'user1', 2, '1990-6-20', ENCRYPTBYPASSPHRASE('password', 'user1'), 2);

INSERT INTO Editorial(nombre, direccion, telefono) VALUES ('Santillana S.A', 'Direccion 1', '11223344')
INSERT INTO Editorial(nombre, direccion, telefono) VALUES ('Susaeta Guatemala', 'Direccion 2', '11223344')
INSERT INTO Editorial(nombre, direccion, telefono) VALUES ('Sur S.A', 'Direccion 3', '11223344')
INSERT INTO Editorial(nombre, direccion, telefono) VALUES ('Papelera Istmeña', 'Direccion 4', '11223344')
INSERT INTO Editorial(nombre, direccion, telefono) VALUES ('Grupo Espasa', 'Direccion 5', '11223344')

INSERT INTO Genero(nombre) VALUES ('Literario')
INSERT INTO Genero(nombre) VALUES ('Aventura')

INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (1,1,'Titulo1', 'Autor1', 15)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (1,1,'Titulo2', 'Autor1', 42)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (1,1,'Titulo3', 'Autor2', 37)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (2,1,'Titulo4', 'Autor2', 60)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (2,2,'Titulo5', 'Autor3', 12)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (3,2,'Titulo6', 'Autor3', 11)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (4,2,'Titulo7', 'Autor3', 50)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (4,1,'Titulo8', 'Autor4', 24)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (4,1,'Titulo9', 'Autor5', 44)
INSERT INTO Libro (id_editorial, genero, titulo, autor, cantidad) VALUES (4,1,'Titulo10', 'Autor1', 34)

SELECT * FROM Bitacora