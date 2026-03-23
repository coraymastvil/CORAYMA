-- SQL Script for FitLife Gym System
-- Author: Antigravity AI
-- Date: 2026-03-23

-- Table: Miembro
CREATE TABLE IF NOT EXISTS Miembro (
    cedula VARCHAR(20) PRIMARY KEY,
    nombre_completo VARCHAR(100) NOT NULL,
    telefono VARCHAR(20)
);

-- Table: Clase
CREATE TABLE IF NOT EXISTS Clase (
    id_clase INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre_clase VARCHAR(50) NOT NULL,
    dia_semana VARCHAR(20) NOT NULL,
    horario VARCHAR(20) NOT NULL
);

-- Table: Inscripcion (Many-to-Many Relationship)
CREATE TABLE IF NOT EXISTS Inscripcion (
    cedula VARCHAR(20),
    id_clase INTEGER,
    PRIMARY KEY (cedula, id_clase),
    FOREIGN KEY (cedula) REFERENCES Miembro(cedula) ON DELETE CASCADE,
    FOREIGN KEY (id_clase) REFERENCES Clase(id_clase) ON DELETE CASCADE
);

-- Inserting 5 Members
INSERT INTO Miembro (cedula, nombre_completo, telefono) VALUES ('1-1234-5678', 'Juan Pérez', '8888-1111');
INSERT INTO Miembro (cedula, nombre_completo, telefono) VALUES ('2-2345-6789', 'María Rodríguez', '8888-2222');
INSERT INTO Miembro (cedula, nombre_completo, telefono) VALUES ('3-3456-7890', 'Carlos González', '8888-3333');
INSERT INTO Miembro (cedula, nombre_completo, telefono) VALUES ('4-4567-8901', 'Lucía Martínez', '8888-4444');
INSERT INTO Miembro (cedula, nombre_completo, telefono) VALUES ('5-5678-9012', 'Sofía López', '8888-5555');

-- Inserting 5 Classes
INSERT INTO Clase (nombre_clase, dia_semana, horario) VALUES ('Yoga Iniciación', 'Lunes', '08:00');
INSERT INTO Clase (nombre_clase, dia_semana, horario) VALUES ('Spinning Pro', 'Martes', '18:00');
INSERT INTO Clase (nombre_clase, dia_semana, horario) VALUES ('Zumba Latino', 'Miércoles', '19:30');
INSERT INTO Clase (nombre_clase, dia_semana, horario) VALUES ('Boxeo', 'Jueves', '17:00');
INSERT INTO Clase (nombre_clase, dia_semana, horario) VALUES ('Pilates', 'Viernes', '09:00');

-- Inserting some registrations
INSERT INTO Inscripcion (cedula, id_clase) VALUES ('1-1234-5678', 1);
INSERT INTO Inscripcion (cedula, id_clase) VALUES ('1-1234-5678', 2);
INSERT INTO Inscripcion (cedula, id_clase) VALUES ('2-2345-6789', 3);
INSERT INTO Inscripcion (cedula, id_clase) VALUES ('3-3456-7890', 4);
INSERT INTO Inscripcion (cedula, id_clase) VALUES ('4-4567-8901', 5);
