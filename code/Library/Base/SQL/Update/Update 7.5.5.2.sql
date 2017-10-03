/* UPDATE 7.5.5.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.5.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "STWorkReportResource" ADD COLUMN "FROM" timestamp without time zone;
ALTER TABLE "STWorkReportResource" ADD COLUMN "TILL" timestamp without time zone;

