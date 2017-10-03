/* UPDATE 7.3.1.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.3.1.1' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Pago_Operacion"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."Pago_Operacion_OID_seq"'::text)::regclass);	
	