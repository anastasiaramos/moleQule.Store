/* UPDATE 4.0.0.0*/

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "Empleado";
CREATE TABLE "Empleado"
(
	"OID_IMPUESTO" int8,
	"OID" bigserial NOT NULL,
	"TIPO" bigint DEFAULT 1,
	"CODIGO" varchar(255) NOT NULL UNIQUE,
	"SERIAL" int8 NOT NULL,
	"ESTADO" int8 DEFAULT 10,
	"ID" varchar(50),
	"TIPO_ID" int8 NOT NULL DEFAULT 0,
	"NOMBRE" varchar(255),
	"APELLIDOS" varchar(255),
	"ALIAS" varchar(255) NOT NULL,
	"DIRECCION" varchar(255),
	"COD_POSTAL" varchar(255),
	"LOCALIDAD" varchar(255),
	"MUNICIPIO" varchar(255),
	"PROVINCIA" varchar(255),
	"PAIS" character varying(255),
	"TELEFONO" varchar(255),
	"EMAIL" varchar(255),
	"CUENTA_BANCARIA" varchar(255),
	"OID_CUENTA_BANCARIA_ASOCIADA" int8 DEFAULT 0,
	"CUENTA_CONTABLE" varchar(255),		
	"MEDIO_PAGO" int8 DEFAULT 1,
	"FORMA_PAGO" int8 DEFAULT 1, 
	"DIAS_PAGO" int8, 
	"CONTACTO" varchar(255),
	"OBSERVACIONES" text,
	"NIVEL_ESTUDIOS" varchar(255),
	"FOTO" varchar(255),
	"PERFIL" int8 NOT NULL,
	"ACTIVO" bool DEFAULT true,
	"INICIO_CONTRATO" date,
	"FIN_CONTRATO" date,
	"SUELDO_BRUTO" numeric(10,2) DEFAULT 0,
	"P_IRPF" numeric(10,2) DEFAULT 0,
	CONSTRAINT "EMPLEADO_PK" PRIMARY KEY ("OID")
)WITHOUT OIDS;

ALTER TABLE "Empleado" OWNER TO moladmin;
GRANT ALL ON TABLE "Empleado" TO GROUP "MOLEQULE_ADMINISTRATOR";

--AUXILIARES -> PROVEEDOR
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'AUXILIARES' AND ASI."TIPO" = 'PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'AUXILIARES' AND ASI."TIPO" = 'PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'AUXILIARES' AND ASI."TIPO" = 'PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'AUXILIARES' AND ASI."TIPO" = 'PROVEEDOR'	;

--EXPEDIENTE -> PRODUCTO
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'PRODUCTO'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'PRODUCTO'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'PRODUCTO'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'PRODUCTO'	;

--EXPEDIENTE -> FACTURA_PROVEEDOR
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'EXPEDIENTE' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;

--FACTURA_PROVEEDOR -> EXPEDIENTE
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'FACTURA_PROVEEDOR' AND ASI."TIPO" = 'EXPEDIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'FACTURA_PROVEEDOR' AND ASI."TIPO" = 'EXPEDIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'FACTURA_PROVEEDOR' AND ASI."TIPO" = 'EXPEDIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'FACTURA_PROVEEDOR' AND ASI."TIPO" = 'EXPEDIENTE'	;

--PROVEEDOR -> FACTURA_PROVEEDOR 
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PROVEEDOR' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PROVEEDOR' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PROVEEDOR' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PROVEEDOR' AND ASI."TIPO" = 'FACTURA_PROVEEDOR'	;

--PRODUCTO -> CLIENTE
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'CLIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'CLIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'CLIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'CLIENTE'	;

--PRODUCTO -> EXPEDIENTE
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'EXPEDIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'EXPEDIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'EXPEDIENTE'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'EXPEDIENTE'	;

--PRODUCTO -> PROVEEDOR
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'PROVEEDOR'	;	
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'PROVEEDOR'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."TIPO" = 'PRODUCTO' AND ASI."TIPO" = 'PROVEEDOR'	;


SET SEARCH_PATH = "0001";

ALTER TABLE "Pago" ADD COLUMN "TIPO" bigint DEFAULT 1;

UPDATE "Pago" SET "TIPO" = 1;

ALTER TABLE "Gasto" DROP CONSTRAINT "FK_Gasto_Expediente";
ALTER TABLE "Gasto" ADD COLUMN "SERIAL" bigint;
ALTER TABLE "Gasto" ADD COLUMN "CODIGO" varchar(255);
ALTER TABLE "Gasto" ADD COLUMN "OID_EMPLEADO" bigint;
ALTER TABLE "Gasto" ADD COLUMN "OID_NOMINA" bigint;
ALTER TABLE "Gasto" ADD COLUMN "ESTADO" bigint;
ALTER TABLE "Gasto" ADD COLUMN "OBSERVACIONES" text;
ALTER TABLE "Gasto" ADD COLUMN "OID_PAGO" bigint;
ALTER TABLE "Gasto" DROP COLUMN "FECHA_PAGO";
ALTER TABLE "Gasto" ADD COLUMN "FECHA" timestamp without time zone;

UPDATE "Gasto" SET "ESTADO" = 1;

UPDATE "Gasto" SET "FECHA" = F."FECHA"
FROM (SELECT "OID", "FECHA" FROM "FacturaProveedor") AS F
WHERE "Gasto"."OID_FACTURA" = F."OID";

UPDATE "Gasto" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID") AS "NROW"
     FROM "Gasto" 
	 WHERE "FECHA" BETWEEN '01/01/2010' AND '12/31/2010' 
     ORDER BY "OID", "CODIGO") AS C
WHERE "Gasto"."OID" = C."OID" AND "Gasto"."FECHA" >= '01/01/2010';

UPDATE "Gasto" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID") AS "NROW"
     FROM "Gasto" 
	 WHERE "FECHA" BETWEEN '01/01/2011' AND '12/31/2011' 
     ORDER BY "OID", "CODIGO") AS C
WHERE "Gasto"."OID" = C."OID" AND "Gasto"."FECHA" >= '01/01/2011';

UPDATE "Gasto" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID") AS "NROW"
     FROM "Gasto" 
	 WHERE "FECHA" BETWEEN '01/01/2012' AND '12/31/2012' 
     ORDER BY "OID", "CODIGO") AS C
WHERE "Gasto"."OID" = C."OID" AND "Gasto"."FECHA" >= '01/01/2012';

DROP TABLE IF EXISTS "RemesaNomina" CASCADE;
CREATE TABLE "RemesaNomina" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" bigint,
	"CODIGO" varchar(255),
	"FECHA" timestamp without time zone,
	"DESCRIPCION" text,
	"TOTAL" decimal(10,2),
	"IRPF" decimal(10,2),
	"SEGURO_EMPRESA" numeric(10,2),
	"SEGURO_PERSONAL" numeric(10,2) DEFAULT 0,
	"PREVISION_PAGO" timestamp without time zone,
	"ESTADO" int8 DEFAULT 1,
	"OBSERVACIONES" text,	
	CONSTRAINT "PK_Nomina" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "RemesaNomina" OWNER TO moladmin;
GRANT ALL ON TABLE "RemesaNomina" TO GROUP "MOLEQULE_ADMINISTRATOR";

INSERT INTO "Privilege" ("OID_USER", "OID_ITEM", "READ", "CREATE", "MODIFY", "DELETE") 
	SELECT u."OID", i."OID", FALSE, FALSE, FALSE, FALSE 
	FROM "COMMON"."User" AS u, "COMMON"."SecureItem" AS i
	WHERE (u."OID", i."OID") NOT IN (SELECT "OID_USER", "OID_ITEM" FROM "Privilege");
	
UPDATE "LineaCaja" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA") AS "NROW"
     FROM "LineaCaja" 
	 WHERE "FECHA" BETWEEN '01/01/2011' AND '12/31/2011' 
     ORDER BY "FECHA", "CODIGO") AS C
WHERE "LineaCaja"."OID" = C."OID" AND "LineaCaja"."FECHA" >= '01/01/2011';	