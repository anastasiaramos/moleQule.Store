/* UPDATE 2.1.2.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

ALTER TABLE "Pago" ALTER COLUMN "FECHA" TYPE timestamp without time zone;

DROP TABLE IF EXISTS "AlbaranProveedor" CASCADE;
CREATE TABLE "AlbaranProveedor" ( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" int8,
	"OID_ACREEDOR" int8,
	"TIPO_ACREEDOR" int8,
	"SERIAL" int8 DEFAULT 0 NOT NULL UNIQUE,
	"CODIGO" varchar(255),
	"ANO" int8,
	"FECHA" date,
	"FECHA_REGISTRO" date,
	"FORMA_PAGO" bigint DEFAULT 1,
	"DIAS_PAGO" bigint DEFAULT 0,
	"MEDIO_PAGO" bigint DEFAULT 1,
	"PREVISION_PAGO" date,
	"P_IRPF" numeric(10,2),
	"P_DESCUENTO" decimal(10,2),
	"DESCUENTO" decimal(10,2) default 0,
	"BASE_IMPONIBLE" decimal(10,2),
	"IGIC" decimal(10,2),
	"TOTAL" decimal(10,2),
	"CUENTA_BANCARIA" varchar(255),
	"NOTA" boolean,
	"OBSERVACIONES" text,
	"CONTADO" boolean NOT NULL DEFAULT false,
	"RECTIFICATIVO" boolean DEFAULT false,
	CONSTRAINT "PK_AlbaranProveedor" PRIMARY KEY ("OID")	
) WITHOUT OIDS;

ALTER TABLE "AlbaranProveedor" ADD CONSTRAINT "AlbaranProveedor_SERIAL_OID_SERIE_key" UNIQUE("SERIAL", "OID_SERIE");
ALTER TABLE "AlbaranProveedor" OWNER TO moladmin;
GRANT ALL ON TABLE "AlbaranProveedor" TO GROUP "MOLEQULE_ADMINISTRATOR";

CREATE TABLE "Albaran_FacturaProveedor" 
( 
	"OID" bigserial NOT NULL,
	"OID_ALBARAN" int8 NOT NULL,
	"OID_FACTURA" int8 NOT NULL,
	"FECHA_ASIGNACION" date,
	CONSTRAINT "PK_Albaran_FacturaProveedor" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Albaran_FacturaProveedor" ADD CONSTRAINT "UQ_Albaran_Factura_OID_ALBARAN_OID_FACTURA" UNIQUE("OID_ALBARAN", "OID_FACTURA");
ALTER TABLE "Albaran_FacturaProveedor" OWNER TO moladmin;
GRANT ALL ON TABLE "Albaran_FacturaProveedor" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "ConceptoAlbaranProveedor";
CREATE TABLE "ConceptoAlbaranProveedor" ( 
	"OID" bigserial NOT NULL,
	"OID_ALBARAN" int8,
	"OID_PRODUCTO_EXPEDIENTE" int8,
	"OID_EXPEDIENTE" int8,
	"OID_PRODUCTO" int8,
	"OID_KIT" bigint NOT NULL DEFAULT 0,
	"CODIGO_EXPEDIENTE" varchar(255),
	"CONCEPTO" varchar(255),
	"FACTURACION_BULTO" boolean,
	"CANTIDAD" decimal(10,2),
	"CANTIDAD_BULTOS" numeric(10,4),
	"P_IGIC" decimal(10,2),
	"P_DESCUENTO" decimal(10,2),
	"TOTAL" decimal(10,2),
	"PRECIO" decimal(10,5),
	"SUBTOTAL" decimal(10,2),
	"GASTOS" decimal(10,5),
	CONSTRAINT "PK_ConceptoAlbaranProveedor" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "ConceptoAlbaranProveedor" OWNER TO moladmin;
GRANT ALL ON TABLE "ConceptoAlbaranProveedor" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE "AlbaranProveedor" ADD CONSTRAINT "FK_AlbaranProveedor_Serie" FOREIGN KEY ("OID_SERIE") REFERENCES "COMMON"."Serie" ("OID")ON UPDATE CASCADE ON DELETE RESTRICT ;
ALTER TABLE "Albaran_FacturaProveedor" ADD CONSTRAINT "FK_Albaran_FacturaProveedor_AlbaranProveedor" FOREIGN KEY ("OID_ALBARAN") REFERENCES "AlbaranProveedor" ("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
ALTER TABLE "Albaran_FacturaProveedor" ADD CONSTRAINT "FK_Albaran_FacturaProveedor_FacturaProveedor" FOREIGN KEY ("OID_FACTURA") REFERENCES "FacturaRecibida" ("OID") ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE "ConceptoAlbaranProveedor" ADD CONSTRAINT "FK_ConceptoAlbaranProveedor_AlbaranProveedor" FOREIGN KEY ("OID_ALBARAN") REFERENCES "AlbaranProveedor" ("OID")ON UPDATE CASCADE ON DELETE CASCADE ;
