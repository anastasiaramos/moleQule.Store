/* UPDATE 7.2.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.2.0.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Stock" DROP COLUMN "OID_CLIENTE";
ALTER TABLE "Stock" DROP COLUMN "ENTRADA";