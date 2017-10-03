﻿/* UPDATE 5.0.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.0.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "PedidoProveedor" ADD COLUMN "OID_ACREEDOR" bigint DEFAULT 0;
ALTER TABLE "PedidoProveedor" ADD COLUMN "TIPO_ACREEDOR" bigint DEFAULT 1;
ALTER TABLE "PedidoProveedor" ADD COLUMN "OID_USUARIO" bigint DEFAULT 0;

UPDATE "PedidoProveedor" SET "ESTADO" = 17 WHERE "ESTADO" = 3;
UPDATE "PedidoProveedor" SET "ESTADO" = 8 WHERE "ESTADO" = 2;

--ALTER TABLE "LineaPedido" RENAME TO "LineaPedidoProveedor";
--ALTER SEQUENCE "LineaPedido_OID_seq" RENAME TO "LineaPedidoProveedor_OID_seq";
--ALTER TABLE "LineaPedidoProveedor" ALTER COLUMN "OID" SET DEFAULT nextval('"LineaPedidoProveedor_OID_seq"'::regclass);

--ALTER TABLE "LineaPedidoProveedor" ADD COLUMN "OID_PRODUCTO" bigint DEFAULT 0;

--UPDATE "LineaPedidoProveedor" SET "ESTADO" = 14 WHERE "ESTADO" = 1;
--UPDATE "LineaPedidoProveedor" SET "ESTADO" = 7 WHERE "ESTADO" = 2;
--UPDATE "LineaPedidoProveedor" SET "ESTADO" = 17 WHERE "ESTADO" = 3;

--ALTER TABLE "LineaPedidoProveedor" DROP CONSTRAINT "pk_lineapedido";

DROP TABLE  IF EXISTS "LineaPedidoProveedor" CASCADE;
CREATE TABLE "LineaPedidoProveedor" 
( 
	"OID" bigserial NOT NULL,
	"OID_PEDIDO" int8 NOT NULL,
	"OID_PROVEEDOR" int8 NOT NULL,
	"OID_PRODUCTO" int8 NOT NULL,
	"CONCEPTO" varchar(255),
	"CANTIDAD" numeric(10,2) NOT NULL DEFAULT 0,
	"ESTADO" int8,
	"OBSERVACIONES" text,
	"FECHA_RECEPCION" date,
	CONSTRAINT "PK_LineaPedidoProveedor" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "LineaPedidoProveedor" OWNER TO moladmin;
GRANT ALL ON TABLE "LineaPedidoProveedor" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "PedidoProveedor" CASCADE;
CREATE TABLE "PedidoProveedor" 
( 
	"OID" bigserial  NOT NULL,
	"OID_USUARIO" bigint DEFAULT 0,
	"OID_ACREEDOR" bigint DEFAULT 0,
	"SERIAL" int8 NOT NULL,
	"CODIGO" varchar(255) NOT NULL,
	"FECHA" timestamp,
	"ESTADO" int8,
	"TIPO_ACREEDOR" bigint DEFAULT 1,
	"OBSERVACIONES" text,
	CONSTRAINT "PK_PedidoProveedor" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "PedidoProveedor" OWNER TO moladmin;
GRANT ALL ON TABLE "PedidoProveedor" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE "LineaPedidoProveedor" ADD CONSTRAINT "FK_LineaPedidoProveedor_PedidoProveedor" FOREIGN KEY ("OID_PEDIDO") REFERENCES "PedidoProveedor" ("OID") ON UPDATE CASCADE ON DELETE CASCADE;

DROP TABLE  IF EXISTS "LineaLibroGanadero" CASCADE;
CREATE TABLE "LineaLibroGanadero" 
( 
	"OID" bigserial  NOT NULL,
	"OID_LIBRO" int8 NOT NULL,
	"OID_PARTIDA" int8 NOT NULL,
	"OID_CONCEPTO" int8 NOT NULL,
	"ESTADO" int8 DEFAULT 7,
	"SERIAL" int8 NOT NULL,
	"CODIGO" varchar(255) NOT NULL,
	"CROTAL" varchar(255),
	"FECHA" timestamp without time zone,
	"SEXO" bigint DEFAULT 1,
	"EDAD" bigint DEFAULT 1,
	"RAZA" varchar(255),
	"CAUSA" varchar(255),
	"PROCEDENCIA" varchar(255),
	"BALANCE" numeric(10,2) DEFAULT 0,
	"OBSERVACIONES" text,
	CONSTRAINT "PK_LineaLibroGanadero" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "LineaLibroGanadero" OWNER TO moladmin;
GRANT ALL ON TABLE "LineaLibroGanadero" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "LibroGanadero" CASCADE;
CREATE TABLE "LibroGanadero" 
( 
	"OID" bigserial  NOT NULL,
	"SERIAL" int8 NOT NULL,
	"CODIGO" varchar(255) NOT NULL,
	"NOMBRE" varchar(255),
	"BALANCE" numeric(10,2),
	"ESTADO" int8 DEFAULT 10,
	"OBSERVACIONES" text,
	CONSTRAINT "PK_LibroGanadero" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "LibroGanadero" OWNER TO moladmin;
GRANT ALL ON TABLE "LibroGanadero" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE "LineaLibroGanadero" ADD CONSTRAINT "FK_LineaLibroGanadero_LibroGanadero" FOREIGN KEY ("OID_LIBRO") REFERENCES "LibroGanadero" ("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
INSERT INTO "LibroGanadero"("OID", "SERIAL", "CODIGO", "NOMBRE", "OBSERVACIONES") VALUES (1, 1, '1', 'PRINCIPAL', '');

--ALTER TABLE "LineaLibroGanadero" RENAME COLUMN "OID_CABEZA" TO "OID_CONCEPTO";

INSERT INTO "LineaLibroGanadero"("SERIAL", "CODIGO", "OID_LIBRO", "OID_PARTIDA", "OID_CONCEPTO", "FECHA", "CROTAL", "RAZA", "EDAD", "SEXO", "ESTADO", "CAUSA", "PROCEDENCIA", "OBSERVACIONES") 
	SELECT 
		ROW_NUMBER() OVER (ORDER BY EX."FECHA_DESPACHO_DESTINO")
		,to_char(ROW_NUMBER() OVER (ORDER BY EX."FECHA_DESPACHO_DESTINO"), '00000') 
		,1
		,CAP."OID_PRODUCTO_EXPEDIENTE"
		,CA."OID"
		,EX."FECHA_DESPACHO_DESTINO"
		,CASE WHEN CA."IDENTIFICADOR" != '' THEN CA."IDENTIFICADOR" ELSE CA."OBSERVACIONES" END
		,CA."RAZA"
		,0
		,CASE WHEN CA."SEXO" = 'HEMBRA' THEN 2 ELSE 1 END
		,18
		,'Importación'
		,PV."PAIS"
		,CA."OBSERVACIONES"		
	FROM "ConceptoAlbaranProveedor" AS CAP
	INNER JOIN "Expediente" AS EX ON EX."OID" = CAP."OID_EXPEDIENTE"
	INNER JOIN "Cabeza" AS CA ON CA."OID_PRODUCTO_EXPEDIENTE" = CAP."OID_PRODUCTO_EXPEDIENTE"
	INNER JOIN "AlbaranProveedor" AS AP ON AP."OID" = CAP."OID_ALBARAN"
	INNER JOIN "Proveedor" AS PV ON PV."OID" = AP."OID_ACREEDOR"
	ORDER BY EX."FECHA_DESPACHO_DESTINO";
	
	INSERT INTO "LineaLibroGanadero"("SERIAL", "CODIGO", "OID_LIBRO", "OID_PARTIDA", "OID_CONCEPTO", "FECHA", "CROTAL", "RAZA", "EDAD", "SEXO", "ESTADO", "CAUSA", "PROCEDENCIA", "OBSERVACIONES") 
	SELECT 
		ROW_NUMBER() OVER (ORDER BY AC."FECHA")
		,to_char(ROW_NUMBER() OVER (ORDER BY AC."FECHA"), '00000') 
		,1
		,LL."OID_PARTIDA"
		,CAC."OID"
		,AC."FECHA"
		,LL."CROTAL"
		,LL."RAZA"
		,LL."EDAD"
		,LL."SEXO"
		,7
		,'Venta'
		,CL."NOMBRE"
		,LL."OBSERVACIONES"
	FROM "ConceptoAlbaran" AS CAC
	INNER JOIN "Expediente" AS EX ON EX."OID" = CAC."OID_EXPEDIENTE"
	INNER JOIN "LineaLibroGanadero" AS LL ON LL."OID_PARTIDA" = CAC."OID_PRODUCTO_EXPEDIENTE"
	INNER JOIN "Albaran" AS AC ON AC."OID" = CAC."OID_ALBARAN"
	INNER JOIN "Cliente" AS CL ON CL."OID" = AC."OID_CLIENTE"
	ORDER BY AC."FECHA";

UPDATE "LineaLibroGanadero" SET "FECHA" = "FECHA" + ("CODIGO" || ' seconds')::interval;

ALTER TABLE "Producto_Expediente" ALTER COLUMN "BULTOS_INICIALES" TYPE numeric(10,2);