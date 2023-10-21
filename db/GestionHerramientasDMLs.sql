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

    

