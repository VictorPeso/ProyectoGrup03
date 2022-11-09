DROP DATABASE IF EXISTS BaseJuego;
CREATE DATABASE BaseJuego;

USE BaseJuego;


CREATE TABLE Usuarios ( id INTEGER NOT NULL, Nombre VARCHAR(20) NOT NULL, Contra VARCHAR(20) NOT NULL,  Estado VARCHAR(20) NOT NULL)ENGINE=InnoDB;

CREATE TABLE Partidas( id INTEGER  NOT NULL, Numero_de_jugadores INTEGER NOT NULL, Duracion INTEGER NOT NULL)ENGINE=InnoDB;

CREATE TABLE Registro_de_partidas( idU INTEGER NOT NULL, idP INTEGER NOT NULL, Resultado_Final VARCHAR(20) NOT NULL )ENGINE=InnoDB;

INSERT INTO Usuarios (id ,Nombre, Contra, Estado) VALUES (087965,'Victor','ola','Online');
INSERT INTO Usuarios (id ,Nombre, Contra, Estado) VALUES (093983,'Aza','Boa_H.','Offline');
INSERT INTO Usuarios (id ,Nombre, Contra, Estado) VALUES (084319,'Pedro','ole','Offline');
INSERT INTO Usuarios (id ,Nombre, Contra, Estado) VALUES (834298,'Ozuna','Corazondeseda','Online');

INSERT INTO Partidas (id , Numero_de_jugadores, Duracion) VALUES (1, 2, 100);
INSERT INTO Partidas (id , Numero_de_jugadores, Duracion) VALUES (2, 3, 120);

INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (834298,1,'Ganador');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (084319,1,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (087965,2,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (093983,2,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (834298,2,'Ganador');



