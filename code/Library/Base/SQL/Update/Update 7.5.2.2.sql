/* UPDATE 7.5.2.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.2.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "STWorkCrew" CASCADE;
CREATE TABLE "STWorkCrew" 
( 
	"OID" bigserial NOT NULL,
    "SERIAL" bigint,
    "CODE" character varying(255),
    "STATUS" bigint DEFAULT 10,
	"NAME" character varying(255),	
	"DESCRIPTION" text,	
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkCrew" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkCrew" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkCrew" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE "Empleado" RENAME TO "STEmployee";
ALTER TABLE "STEmployee" ADD COLUMN "OID_CREW" bigint DEFAULT 0;
ALTER SEQUENCE "Empleado_OID_seq" RENAME TO "STEmployee_OID_seq";

DROP TABLE IF EXISTS "STTool" CASCADE;
CREATE TABLE "STTool" 
( 
	"OID" bigserial NOT NULL,
    "SERIAL" bigint,
    "CODE" character varying(255),
    "STATUS" bigint DEFAULT 10,
	"NAME" character varying(255),	
	"DESCRIPTION" text,	
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
	"COST" numeric(10,2),
    "LOCATION" text,	
	"COMMENTS" text,	
	CONSTRAINT "PK_STTool" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STTool" OWNER TO moladmin;
GRANT ALL ON TABLE "STTool" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STWorkReport" CASCADE;
CREATE TABLE "STWorkReport" 
( 
	"OID" bigserial NOT NULL,
	"OID_OWNER" bigint DEFAULT 1,
	"OID_EXPEDIENT" bigint DEFAULT 1,
    "SERIAL" bigint,
    "CODE" character varying(255),
    "STATUS" bigint DEFAULT 10,
    "DATE" timestamp without time zone,
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
	"TOTAL" numeric(10,2),
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkReport" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkReport" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkReport" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STWorkReportResource" CASCADE;
CREATE TABLE "STWorkReportResource" 
( 
	"OID" bigserial NOT NULL,
	"OID_WORK_REPORT" bigint NOT NULL,
	"OID_RESOURCE" bigint DEFAULT 1,
	"ENTITY_TYPE" bigint DEFAULT 1,
	"AMOUNT" numeric(10,2),
	"COST" numeric(10,2),
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkReportResource" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkReportResource" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkReportResource" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE ONLY "STWorkReport"
    ADD CONSTRAINT "FK_STWorkReport_User" FOREIGN KEY ("OID_OWNER") REFERENCES "COMMON"."User"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STWorkReport"
    ADD CONSTRAINT "FK_STWorkReport_Expedient" FOREIGN KEY ("OID_EXPEDIENT") REFERENCES "Expediente"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;	
	
ALTER TABLE ONLY "STWorkReportResource"
    ADD CONSTRAINT "FK_STWorkReportResource_STWorkReport" FOREIGN KEY ("OID_WORK_REPORT") REFERENCES "STWorkReport"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE "Familia" RENAME TO "STFamily";
ALTER SEQUENCE "Familia_OID_seq" RENAME TO "STFamily_OID_seq";
	
ALTER TABLE "Producto" RENAME TO "STProduct";
ALTER SEQUENCE "Producto_OID_seq" RENAME TO "STProduct_OID_seq";

ALTER TABLE "Proveedor" RENAME TO "STSupplier";
ALTER SEQUENCE "Proveedor_OID_seq" RENAME TO "STSupplier_OID_seq";	

ALTER TABLE "Stock" RENAME TO "STStock";
ALTER SEQUENCE "Stock_OID_seq" RENAME TO "STStock_OID_seq";

ALTER TABLE "Transportista" RENAME TO "STTransporter";
ALTER SEQUENCE "Transportista_OID_seq" RENAME TO "STTransporter_OID_seq";

ALTER TABLE "AlbaranProveedor" RENAME TO "STDelivery";
ALTER SEQUENCE "AlbaranProveedor_OID_seq" RENAME TO "STDelivery_OID_seq";	

ALTER TABLE "Albaran_FacturaProveedor" RENAME TO "STDelivery_Invoice";
ALTER SEQUENCE "Albaran_FacturaProveedor_OID_seq" RENAME TO "STDelivery_Invoice_OID_seq";

ALTER TABLE "Almacen" RENAME TO "STStore";
ALTER SEQUENCE "Almacen_OID_seq" RENAME TO "STStore_OID_seq";	

ALTER TABLE "FacturaProveedor" RENAME TO "STInvoice";
ALTER SEQUENCE "FacturaProveedor_OID_seq" RENAME TO "STInvoice_OID_seq";