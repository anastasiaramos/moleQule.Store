/* UPDATE 7.3.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.3.0.1' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "FacturaProveedor" ADD COLUMN "IMPUESTOS" numeric(10,2) DEFAULT 0;