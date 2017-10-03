/* UPDATE 4.8.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '4.8.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

ALTER TABLE "PUERTO_DESPACHANTE" RENAME TO "Puerto_Despachante";
ALTER SEQUENCE "PUERTO_DESPACHANTE_OID_seq" RENAME TO "Puerto_Despachante_OID_seq";
ALTER TABLE "Puerto_Despachante" SET SCHEMA "0001";

SET SEARCH_PATH = "0001";

ALTER TABLE "Puerto_Despachante" ALTER COLUMN "OID" SET DEFAULT nextval(('"Puerto_Despachante_OID_seq"'::text)::regclass);
ALTER TABLE "Pago_Factura" ADD COLUMN "TIPO_PAGO" bigint DEFAULT 1;
UPDATE "Pago_Factura" SET "TIPO_PAGO" = 1;

ALTER TABLE "Pago_Factura" DROP CONSTRAINT "FK_Pago_Factura_Factura" CASCADE;


