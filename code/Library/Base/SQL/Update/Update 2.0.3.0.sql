/* UPDATE 2.0.4.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

ALTER TABLE "Producto_Expediente" ADD COLUMN "GASTO_KILO" decimal(10,5) DEFAULT 0;

ALTER TABLE "FacturaRecibida" ADD COLUMN "ESTADO" int8 DEFAULT 1;

