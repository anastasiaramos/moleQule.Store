/* UPDATE 3.1.0.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

ALTER TABLE "Producto_Proveedor" RENAME COLUMN "OID_PROVEEDOR" TO "OID_ACREEDOR";
ALTER TABLE "Producto_Proveedor" ADD COLUMN "TIPO_ACREEDOR" bigint DEFAULT 0;
ALTER TABLE "Producto_Proveedor" ADD COLUMN "FACTURACION_BULTO" boolean DEFAULT FALSE;

UPDATE "Producto_Proveedor" SET "TIPO_ACREEDOR" = 1; 