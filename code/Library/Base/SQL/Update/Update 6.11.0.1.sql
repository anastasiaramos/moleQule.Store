/* UPDATE 6.11.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.11.0.1' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "FacturaProveedor" DROP COLUMN "EXPEDIENTE";
ALTER TABLE "AlbaranProveedor" DROP COLUMN "EXPEDIENTE";