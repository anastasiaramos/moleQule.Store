/* UPDATE 6.0.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.0.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Familia" ADD COLUMN "AVISAR_BENEFICIO_MINIMO" boolean DEFAULT FALSE;
ALTER TABLE "Familia" ADD COLUMN "P_BENEFICIO_MINIMO" numeric(10,2) DEFAULT 0;

ALTER TABLE "Producto" ADD COLUMN "AVISAR_BENEFICIO_MINIMO" boolean DEFAULT FALSE;
ALTER TABLE "Producto" ADD COLUMN "P_BENEFICIO_MINIMO" numeric(10,2) DEFAULT 0;
