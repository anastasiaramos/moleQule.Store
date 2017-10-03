/* UPDATE 7.2.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.2.0.1' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER SEQUENCE "Producto_Expediente_OID_seq" RENAME TO "Partida_OID_seq";

ALTER TABLE "Partida"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"Partida_OID_seq"'::text)::regclass);

