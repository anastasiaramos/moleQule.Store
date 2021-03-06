/* UPDATE 4.4.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '4.4.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "ExpedienteREA";
CREATE TABLE "ExpedienteREA" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" int8 NOT NULL,
	"CODIGO" varchar(255) UNIQUE,
	"ESTADO" bigint DEFAULT 1,
	"CODIGO_ADUANERO" varchar(255),
	"FECHA" date,
	"EXPEDIENTE_REA" varchar(255),
	"CERTIFICADO_REA" varchar(255),
	"N_DUA" varchar(255),
	"AYUDAS" decimal(10,2) DEFAULT 0,
	"OBSERVACIONES" text,
	CONSTRAINT "PK_ExpedienteREA" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "ExpedienteREA" OWNER TO moladmin;
GRANT ALL ON TABLE "ExpedienteREA" TO GROUP "MOLEQULE_ADMINISTRATOR";