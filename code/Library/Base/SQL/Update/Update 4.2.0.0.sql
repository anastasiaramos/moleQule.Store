﻿/* UPDATE 4.2.0.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "LineaFomento" CASCADE;
CREATE TABLE "LineaFomento" 
( 
	"OID" bigserial NOT NULL,
	"OID_PARTIDA" bigint NOT NULL,
	"OID_EXPEDIENTE" bigint NOT NULL,
	"OID_NAVIERA" bigint NOT NULL,
	"SERIAL" bigint,
	"CODIGO" varchar(255),
	"FECHA" timestamp without time zone,
	"DESCRIPCION" text,
	"ID_SOLICITUD" varchar(255),
	"ID_ENVIO" varchar(255),
	"CONOCIMIENTO" text,
	"FECHA_CONOCIMIENTO" date,
	"DUA" character varying(255),
	"FLETE_NETO" decimal(10,2),
	"BAF" decimal(10,2),
	"TEUS20" boolean,
	"TEUS40" boolean,
	"T3_ORIGEN" decimal(10,2),
	"T3_DESTINO" decimal(10,2),
	"THC_ORIGEN" decimal(10,2),
	"THC_DESTINO" decimal(10,2),
	"ISPS" decimal(10,2),
	"TOTAL" decimal(10,2),
	"ESTADO" int8 DEFAULT 1,
	"OBSERVACIONES" text,	
	CONSTRAINT "PK_LineaFomento" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "LineaFomento" OWNER TO moladmin;
GRANT ALL ON TABLE "LineaFomento" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE "LineaFomento" ADD CONSTRAINT "FK_LineaFomento_Partida" FOREIGN KEY ("OID_PARTIDA") REFERENCES "Producto_Expediente" ("OID")ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE "LineaFomento" ADD CONSTRAINT "FK_LineaFomento_Expediente" FOREIGN KEY ("OID_EXPEDIENTE") REFERENCES "Expediente" ("OID")ON UPDATE CASCADE ON DELETE CASCADE;

INSERT INTO "LineaFomento" 
(
	"OID_EXPEDIENTE", "OID_PARTIDA", "OID_NAVIERA",
	"CONOCIMIENTO", "FECHA_CONOCIMIENTO",
	"FLETE_NETO", "BAF", "TEUS20", "TEUS40", "T3_ORIGEN", "T3_DESTINO", "THC_ORIGEN", "THC_DESTINO", "ISPS", "TOTAL"
)
(SELECT 
	EX."OID",
	PT."OID",
	EX."OID_NAVIERA",
	EX."CONOCIMIENTO",
	EX."FECHA_CONOCIMIENTO",
	"FLETE_NETO", "BAF", "TEUS20", "TEUS40", "T3_ORIGEN", "T3_DESTINO", "THC_ORIGEN", "THC_DESTINO", "ISPS", 
	EX."TOTAL_IMPUESTOS"	
FROM "Expediente" AS EX
INNER JOIN (SELECT MIN("OID") AS "OID", "OID_EXPEDIENTE"
			FROM "Producto_Expediente"
			GROUP BY "OID_EXPEDIENTE")
	AS PT ON PT."OID_EXPEDIENTE" = EX."OID");
	
UPDATE "LineaFomento" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA_CONOCIMIENTO") AS "NROW"
     FROM "LineaFomento" 
     WHERE "LineaFomento"."FECHA_CONOCIMIENTO" BETWEEN '01/01/2010' AND '12/31/2010'
     ORDER BY "FECHA_CONOCIMIENTO") AS C
WHERE "LineaFomento"."OID" = C."OID" AND "LineaFomento"."FECHA_CONOCIMIENTO" BETWEEN '01/01/2010' AND '12/31/2010';

UPDATE "LineaFomento" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA_CONOCIMIENTO") AS "NROW"
     FROM "LineaFomento" 
     WHERE "LineaFomento"."FECHA_CONOCIMIENTO" BETWEEN '01/01/2012' AND '12/31/2012'
     ORDER BY "FECHA_CONOCIMIENTO") AS C
WHERE "LineaFomento"."OID" = C."OID" AND "LineaFomento"."FECHA_CONOCIMIENTO" BETWEEN '01/01/2012' AND '12/31/2012';

UPDATE "LineaFomento" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA_CONOCIMIENTO") AS "NROW"
     FROM "LineaFomento" 
     WHERE "LineaFomento"."FECHA_CONOCIMIENTO" BETWEEN '01/01/2011' AND '12/31/2011'
     ORDER BY "FECHA_CONOCIMIENTO") AS C
WHERE "LineaFomento"."OID" = C."OID" AND "LineaFomento"."FECHA_CONOCIMIENTO" BETWEEN '01/01/2011' AND '12/31/2011';
