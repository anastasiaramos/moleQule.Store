SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.11.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "STEmployee" ADD COLUMN "PAYROLL_METHOD" bigint DEFAULT 1;

UPDATE "STEmployee" SET "PAYROLL_METHOD" = 1;

UPDATE "STPayrollBatch" SET "SERIAL" = date_part('month', "FECHA");
UPDATE "STPayrollBatch" SET "CODIGO" = to_char("SERIAL", '00') || '/' || to_char("FECHA", 'yy');

UPDATE "STPayroll" SET "SERIAL" = LL."NROW", "CODIGO" = to_char(LL."NROW", '0000') || '/' || to_char("FECHA", 'yy')
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID" ) AS "NROW"
     FROM "STPayroll" 
     WHERE "FECHA" between '2012-01-01 00:00:00' and '2012-12-31 23:59:59') AS LL
WHERE "STPayroll"."OID" = LL."OID";

UPDATE "STPayroll" SET "SERIAL" = LL."NROW", "CODIGO" = to_char(LL."NROW", '0000') || '/' || to_char("FECHA", 'yy')
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID" ) AS "NROW"
     FROM "STPayroll" 
     WHERE "FECHA" between '2013-01-01 00:00:00' and '2013-12-31 23:59:59') AS LL
WHERE "STPayroll"."OID" = LL."OID";

UPDATE "STPayroll" SET "SERIAL" = LL."NROW", "CODIGO" = to_char(LL."NROW", '0000') || '/' || to_char("FECHA", 'yy')
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID" ) AS "NROW"
     FROM "STPayroll" 
     WHERE "FECHA" between '2014-01-01 00:00:00' and '2014-12-31 23:59:59') AS LL
WHERE "STPayroll"."OID" = LL."OID";

UPDATE "STPayroll" SET "FECHA" = "FECHA" + "SERIAL" * INTERVAL '1 second';

