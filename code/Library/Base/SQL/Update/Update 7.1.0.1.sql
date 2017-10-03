/* UPDATE 7.1.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.1.0.1' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "ConceptoAlbaranProveedor" ADD COLUMN "P_IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "ConceptoFacturaProveedor" ADD COLUMN "P_IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "AlbaranProveedor" ADD COLUMN "IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "FacturaProveedor" ADD COLUMN "IRPF" numeric(10,2) DEFAULT 0;

UPDATE "ConceptoAlbaranProveedor" SET "P_IRPF" = AL."P_IRPF"
FROM "AlbaranProveedor" AS AL 
WHERE AL."OID" = "OID_ALBARAN";

UPDATE "ConceptoFacturaProveedor" SET "P_IRPF" = F."P_IRPF"
FROM "FacturaProveedor" AS F 
WHERE F."OID" = "OID_FACTURA";

UPDATE "AlbaranProveedor" SET "IRPF" = "BASE_IMPONIBLE" * "P_IRPF" / 100;
UPDATE "FacturaProveedor" SET "IRPF" = "BASE_IMPONIBLE" * "P_IRPF" / 100;

