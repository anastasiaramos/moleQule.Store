/* UPDATE 5.3.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.3.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Gasto" ADD COLUMN "OID_USUARIO" bigint DEFAULT 1;
UPDATE "Gasto" SET "OID_USUARIO" = 1 WHERE "OID_USUARIO" = 0;

