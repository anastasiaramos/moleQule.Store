SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.12.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "STDeliveryLine" RENAME COLUMN "OID_PRODUCTO_EXPEDIENTE" TO "OID_BATCH";
ALTER TABLE "STStock" RENAME COLUMN "OID_PRODUCTO_EXPEDIENTE" TO "OID_BATCH";
ALTER TABLE "STInvoiceLine" RENAME COLUMN "OID_PRODUCTO_EXPEDIENTE" TO "OID_BATCH";
ALTER TABLE "Cabeza" RENAME COLUMN "OID_PRODUCTO_EXPEDIENTE" TO "OID_BATCH";
ALTER TABLE "Maquinaria" RENAME COLUMN "OID_PRODUCTO_EXPEDIENTE" TO "OID_BATCH";
ALTER TABLE "STBatch" RENAME COLUMN "OID_PRODUCTO_EXPEDIENTE" TO "OID_BATCH";
