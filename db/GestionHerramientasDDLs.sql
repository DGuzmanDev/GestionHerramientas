USE GestionHerramientas;

CREATE LOGIN Dguzman WITH PASSWORD = 'Admin@SQLServer03101', DEFAULT_DATABASE = GestionHerramientas;
CREATE USER Dguzman FOR LOGIN Dguzman;  

GRANT CONTROL ON DATABASE::GestionHerramientas TO Dguzman;

CREATE SCHEMA GestionHerramientas AUTHORIZATION Dguzman   
    GRANT CONTROL ON SCHEMA::GestionHerramientas TO Dguzman; 
   
CREATE TABLE GestionHerramientas.GestionHerramientas.colaborador (
	id int IDENTITY(0,1) NOT NULL,
	identificacion varchar(10) NOT NULL,
	nombre varchar(20) NOT NULL,
	apellidos varchar(40) NOT NULL,
	estado bit DEFAULT 1 NOT NULL,
	fecha_registro datetime2(0) DEFAULT  GETDATE() NOT NULL,
	CONSTRAINT colaborador_PK PRIMARY KEY (id),
	CONSTRAINT colaborador_identificacion_UN UNIQUE (identificacion)
);
CREATE INDEX colaborador_identificacion_IDX ON GestionHerramientas.GestionHerramientas.colaborador (identificacion);
EXEC GestionHerramientas.sys.sp_addextendedproperty 'MS_Description', N'Tabla de colaboradores', 'schema', N'GestionHerramientas', 'table', N'colaborador';

CREATE TABLE GestionHerramientas.GestionHerramientas.herramienta (
	id int IDENTITY(0,1) NOT NULL,
	codigo varchar(100) NOT NULL,
	nombre varchar(50) NOT NULL,
	descripcion varchar(200) NOT NULL,
	colaborador_id int NULL,
	fecha_prestamo datetime2(0) NULL,
	fecha_devolucion datetime2(0) NULL,
	fecha_registro datetime2(0) DEFAULT GETDATE() NOT NULL,
	CONSTRAINT herramientas_PK PRIMARY KEY (id),
	CONSTRAINT herramientas_UN_CODIGO UNIQUE (codigo),
	CONSTRAINT herramientas_FK FOREIGN KEY (colaborador_id) REFERENCES GestionHerramientas.GestionHerramientas.colaborador(id)
);    
