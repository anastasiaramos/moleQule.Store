/* UPDATE 6.5.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.5.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "FacturaProveedor" ADD COLUMN "EXPEDIENTE" varchar(255);

UPDATE "FacturaProveedor" SET "EXPEDIENTE" = EX."CODIGO"
	FROM "Expediente" AS EX
	WHERE "FacturaProveedor"."OID_EXPEDIENTE" = EX."OID";

ALTER TABLE "AlbaranProveedor" ADD COLUMN "EXPEDIENTE" varchar(255);

UPDATE "AlbaranProveedor" SET "EXPEDIENTE" = EX."CODIGO"
	FROM "Expediente" AS EX
	WHERE "AlbaranProveedor"."OID_EXPEDIENTE" = EX."OID";