/* UPDATE 6.1.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.1.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "RemesaNomina" ADD COLUMN "BASE_IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "RemesaNomina" ADD COLUMN "DESCUENTOS" numeric(10,2) DEFAULT 0;

--ALTER TABLE "Gasto" ADD COLUMN "OID_NOMINA" int8 DEFAULT 0;
ALTER TABLE "Gasto" RENAME COLUMN "OID_NOMINA" TO "OID_REMESA_NOMINA";

ALTER INDEX "PK_Nomina" RENAME TO "PK_RemesaNomina";

DROP TABLE IF EXISTS "Nomina" CASCADE;
CREATE TABLE "Nomina" 
( 
	"OID" bigserial NOT NULL,
	"OID_USUARIO" bigint DEFAULT 1,
	"OID_REMESA" int8 DEFAULT 0,
	"OID_TIPO" int8 DEFAULT 0,
	"OID_EXPEDIENTE" int8 DEFAULT 0,
	"OID_EMPLEADO" int8,
	"SERIAL" int8,
	"CODIGO" varchar(255),
	"ESTADO" int8 DEFAULT 1,
	"FECHA" timestamp without time zone,
	"DESCRIPCION" text,
	"BRUTO" numeric(10,2),
	"BASE_IRPF" numeric(10,2),
	"NETO" numeric(10,2),
	"P_IRPF" numeric(10,2),
	"SEGURO" numeric(10,2),
	"DESCUENTOS" numeric(10,2),
	"PREVISION_PAGO" timestamp without time zone,
	"OBSERVACIONES" text,	
	CONSTRAINT "PK_Nomina" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Nomina" OWNER TO moladmin;
GRANT ALL ON TABLE "Nomina" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE "Nomina" ADD CONSTRAINT "FK_Nomina_RemesaNomina" FOREIGN KEY ("OID_REMESA") REFERENCES "RemesaNomina" ("OID") ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE "Nomina" ADD CONSTRAINT "FK_Nomina_Empleado" FOREIGN KEY ("OID_EMPLEADO") REFERENCES "Empleado" ("OID") ON UPDATE CASCADE ON DELETE RESTRICT;


UPDATE "Pago" SET "TIPO_AGENTE" = 8 WHERE "TIPO" = 3;

/*INSERT INTO "Nomina" 
(
	"OID_EMPLEADO", 
	"OID_REMESA", 
	"OID_TIPO",
	"ESTADO",
	"FECHA",
	"DESCRIPCION",
	"BRUTO",
	"NETO",
	"P_IRPF",
	"PREVISION_PAGO",
	"OBSERVACIONES"
)
(SELECT 
	GT."OID_EMPLEADO",
	GT."OID_REMESA_NOMINA",
	GT."OID_TIPO",
	GT."ESTADO",
	GT."FECHA",
	GT."DESCRIPCION",
	GT."TOTAL",
	GT."TOTAL",
	EM."P_IRPF",
	GT."PREVISION_PAGO",
	GT."OBSERVACIONES"
FROM "Gasto" AS GT
INNER JOIN "Empleado" AS EM ON EM."OID" = GT."OID_EMPLEADO"
WHERE GT."OID_TIPO" = 3 AND GT."TIPO" = 5);

UPDATE "Nomina" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA") AS "NROW"
     FROM "Nomina" 
     WHERE "Nomina"."FECHA" BETWEEN '01/01/2012' AND '12/31/2012'
     ORDER BY "FECHA") AS C
WHERE "Nomina"."OID" = C."OID" AND "Nomina"."FECHA" BETWEEN '01/01/2012' AND '12/31/2012';

UPDATE "Pago_Factura" SET "OID_FACTURA" = C."OID"
FROM (SELECT NM."OID", G."OID" AS "OID_GASTO", NM."FECHA"
     FROM "Nomina" AS NM 
	 INNER JOIN "Gasto" AS G ON G."OID_REMESA_NOMINA" = NM."OID_REMESA" AND NM."OID_EMPLEADO" = G."OID_EMPLEADO" AND NM."FECHA" = G."FECHA"
	 WHERE NM."FECHA" BETWEEN '01/01/2012' AND '12/31/2012' 
		AND G."OID_TIPO" = 3 AND G."TIPO" = 5 AND G."ESTADO" != 4
     ORDER BY NM."FECHA") AS C
WHERE C."FECHA" BETWEEN '01/01/2012' AND '12/31/2012'
	AND "Pago_Factura"."OID_FACTURA" = C."OID_GASTO" AND "Pago_Factura"."TIPO_PAGO" = 3;*/

