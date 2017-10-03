SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.14.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE "Cabeza" CASCADE;

ALTER TABLE "Precio_Destino" RENAME TO "STDestinationPrice";
ALTER SEQUENCE "Precio_Destino_OID_seq" RENAME TO "STDestinationPrice_OID_seq";

ALTER TABLE "Precio_Origen" RENAME TO "STOriginPrice";
ALTER SEQUENCE "Precio_Origen_OID_seq" RENAME TO "STOriginPrice_OID_seq";

ALTER TABLE "Precio_Trayecto" RENAME TO "STRoutePrice";
ALTER SEQUENCE "Precio_Trayecto_OID_seq" RENAME TO "STRoutePrice_OID_seq";

ALTER TABLE "Producto_Proveedor" RENAME TO "STSupplier_Product";
ALTER SEQUENCE "Producto_Proveedor_OID_seq" RENAME TO "STSupplier_Product_OID_seq";

ALTER TABLE "Puerto_Despachante" RENAME TO "STCustomAgent_Port";
ALTER SEQUENCE "Puerto_Despachante_OID_seq" RENAME TO "STCustomAgent_Port_OID_seq";

ALTER TABLE "STPayment" ADD COLUMN "OID_LINK" bigint DEFAULT 0;

UPDATE "STPayment" SET "MEDIO_PAGO" = 5 WHERE "TIPO" = 6;