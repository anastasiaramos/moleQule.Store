/* UPDATE 7.5.4.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.4.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "RemesaNomina" RENAME TO "STPayrollBatch";
ALTER SEQUENCE "RemesaNomina_OID_seq" RENAME TO "STPayrollBatch_OID_seq";	

ALTER TABLE "Despachante" RENAME TO "STCustomAgent";
ALTER SEQUENCE "Despachante_OID_seq" RENAME TO "STCustomAgent_OID_seq";

ALTER TABLE "Naviera" RENAME TO "STShippingCompany";
ALTER SEQUENCE "Naviera_OID_seq" RENAME TO "STShippingCompany_OID_seq";
	
ALTER TABLE "Serie" RENAME TO "STSerie";
ALTER SEQUENCE "Serie_OID_seq" RENAME TO "STSerie_OID_seq";

ALTER TABLE "Serie_Familia" RENAME TO "STFamilySerie";
ALTER SEQUENCE "Serie_Familia_OID_seq" RENAME TO "STFamilySerie_OID_seq";

ALTER TABLE "TipoGasto" RENAME TO "STExpenseType";
ALTER SEQUENCE "TipoGasto_OID_seq" RENAME TO "STExpenseType_OID_seq";

ALTER TABLE "STWorkReport" ADD COLUMN "HOURS" numeric(10,2) DEFAULT 0;
ALTER TABLE "STWorkReport" ADD COLUMN "CATEGORY" bigint DEFAULT 0;
ALTER TABLE "STWorkReportResource" ADD COLUMN "HOURS" numeric(10,2) DEFAULT 0;
ALTER TABLE "STWorkReportResource" ADD COLUMN "EXTRA_COST" numeric(10,2) DEFAULT 0;

DROP TABLE IF EXISTS "STWorkReportCategory" CASCADE;
CREATE TABLE "STWorkReportCategory" 
( 
	"OID" bigserial NOT NULL,
    "NAME" text,
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkReportCategory" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkReportCategory" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkReportCategory" TO GROUP "MOLEQULE_ADMINISTRATOR";