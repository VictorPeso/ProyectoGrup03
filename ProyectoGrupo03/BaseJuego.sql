DROP DATABASE IF EXISTS Juego;
CREATE DATABASE Juego;

USE Juego;

CREATE TABLE Usuarios ( id INTEGER, Nombre TEXT, Contraseña TEXT,  Estado TEXT );
CREATE TABLE Partidas( id INTEGER, Numero_de_jugadores INTEGER, Duración INTEGER );
CREATE TABLE Registro_de_partidas( idU INTEGER, idP INTEGER, Resultado_Final TEXT);

INSERT INTO Usuarios (id ,nombre, Contraseña, Estado) VALUES (087965,'Victor','Alicante','Online');
INSERT INTO Usuarios (id ,nombre, Contraseña, Estado) VALUES (093983,'Aza','Boa_H.','Offline');
INSERT INTO Usuarios (id ,nombre, Contraseña, Estado) VALUES (084319,'Pedro','ole','Offline');
INSERT INTO Usuarios (id ,nombre, Contraseña, Estado) VALUES (834298,'Ozuna','Corazondeseda','Online');

INSERT INTO Partidas (id , Numero_de_jugadores, Duración)VALUES (1, 2, 100);
INSERT INTO Partidas (id , Numero_de_jugadores, Duración)VALUES (2, 3, 120);

INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (834298,1,'Ganador');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (084319,1,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (087965,2,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (093983,2,'Perdedor');
INSERT INTO Registro_de_partidas( idU, idP, Resultado_Final) VALUES (834298,2,'Ganador');


