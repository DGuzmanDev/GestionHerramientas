﻿CREATE DATABASE GestionHerramientas;

USE GestionHerramientas;

CREATE LOGIN Dguzman WITH PASSWORD = 'Admin@SQLServer03101',
DEFAULT_DATABASE = GestionHerramientas;

CREATE USER Dguzman FOR LOGIN Dguzman;

GRANT CONTROL ON DATABASE::GestionHerramientas TO Dguzman;

CREATE SCHEMA GestionHerramientas AUTHORIZATION Dguzman GRANT CONTROL ON SCHEMA::GestionHerramientas TO Dguzman;

CREATE TABLE GestionHerramientas.GestionHerramientas.colaborador(
    id int IDENTITY (1, 1) NOT NULL,
    identificacion varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    nombre varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    apellidos varchar(40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    estado bit DEFAULT 1 NOT NULL,
    fecha_registro datetime2(0) DEFAULT getdate() NOT NULL,
    fecha_actualizacion datetime2(0) DEFAULT getdate() NOT NULL,
    CONSTRAINT colaborador_PK PRIMARY KEY (id),
    CONSTRAINT colaborador_identificacion_UN UNIQUE (identificacion)
);

CREATE NONCLUSTERED INDEX colaborador_identificacion_IDX ON GestionHerramientas.GestionHerramientas.colaborador(
    identificacion
);

CREATE TABLE GestionHerramientas.GestionHerramientas.herramienta(
    id int IDENTITY (1, 1) NOT NULL,
    codigo varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    nombre varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    descripcion varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    colaborador_id int NULL,
    fecha_prestamo datetime2(0) NULL,
    fecha_devolucion datetime2(0) NULL,
    fecha_registro datetime2(0) DEFAULT getdate() NOT NULL,
    fecha_actualizacion datetime2(0) DEFAULT getdate() NOT NULL,
    CONSTRAINT herramientas_PK PRIMARY KEY (id),
    CONSTRAINT herramientas_UN_CODIGO UNIQUE (codigo),
    CONSTRAINT herramientas_FK FOREIGN KEY (colaborador_id) REFERENCES GestionHerramientas.GestionHerramientas.colaborador(id)
);