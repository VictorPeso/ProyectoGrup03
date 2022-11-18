DROP DATABASE IF EXISTS T3_BaseJuego;
CREATE DATABASE T3_BaseJuego;

USE T3_BaseJuego;


CREATE TABLE Usuarios ( id INTEGER NOT NULL, Nombre VARCHAR(20) NOT NULL, Contra VARCHAR(20) NOT NULL)ENGINE=InnoDB;

CREATE TABLE Partidas( id INTEGER  NOT NULL, Numero_de_jugadores INTEGER NOT NULL, Duracion INTEGER NOT NULL)ENGINE=InnoDB;

CREATE TABLE Registro_de_partidas( idU INTEGER NOT NULL, idP INTEGER NOT NULL, Resultado_Final VARCHAR(20) NOT NULL )ENGINE=InnoDB;

INSERT INTO Usuarios (id ,Nombre, Contra) VALUES (087965,'Victor','ola');
INSERT INTO Usuarios (id ,Nombre, Contra) VALUES (093983,'Aza','oli');
INSERT INTO Usuarios (id ,Nombre, Contra) VALUES (084319,'Pedro','ole');
INSERT INTO Usuarios (id ,Nombre, Contra) VALUES (834298,'Ozuna','Corazondeseda');

INSERT INTO Partidas (id , Numero_de_jugadores, Duracion) VALUES (1, 2, 100);
INSERT INTO Partidas (id , Numero_de_jugadores, Duracion) VALUES (2, 3, 120);

INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (834298,1,'Ganador');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (084319,1,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (087965,2,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (093983,2,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (834298,2,'Ganador');



