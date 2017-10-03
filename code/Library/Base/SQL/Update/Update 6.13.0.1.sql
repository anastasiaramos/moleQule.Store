/* UPDATE 6.13.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.13.0.1' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Despachante" ADD COLUMN "OID_TARJETA_ASOCIADA" bigint DEFAULT 0;
ALTER TABLE "Empleado" ADD COLUMN "OID_TARJETA_ASOCIADA" bigint DEFAULT 0;
ALTER TABLE "Naviera" ADD COLUMN "OID_TARJETA_ASOCIADA" bigint DEFAULT 0;
ALTER TABLE "Proveedor" ADD COLUMN "OID_TARJETA_ASOCIADA" bigint DEFAULT 0;
ALTER TABLE "Transportista" ADD COLUMN "OID_TARJETA_ASOCIADA" bigint DEFAULT 0;

ALTER TABLE "Despachante" ADD COLUMN "P_IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "Naviera" ADD COLUMN "P_IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "Proveedor" ADD COLUMN "P_IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "Transportista" ADD COLUMN "P_IRPF" numeric(10,2) DEFAULT 0;