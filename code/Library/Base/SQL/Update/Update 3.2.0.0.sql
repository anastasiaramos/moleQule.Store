/* UPDATE 3.2.0.0*/

SET SEARCH_PATH = "COMMON";

ALTER TABLE "Transportista" ADD COLUMN "TIPO_TRANSPORTISTA" bigint DEFAULT 0;

UPDATE "Transportista" SET "TIPO_TRANSPORTISTA" = 1 WHERE "DESTINO" = FALSE; 
UPDATE "Transportista" SET "TIPO_TRANSPORTISTA" = 2 WHERE "DESTINO" = TRUE; 

ALTER TABLE "Despachante" DROP COLUMN "IGIC";

ALTER TABLE "Transportista" DROP COLUMN "DESTINO";

ALTER TABLE "Serie" DROP CONSTRAINT "Serie_TIPO_SERIE_key";

SET SEARCH_PATH = "0001";

ALTER TABLE "Expediente" ADD COLUMN "AYUDA" boolean DEFAULT TRUE;

UPDATE "Expediente" SET "AYUDA" = TRUE; 

ALTER TABLE "Stock" ADD COLUMN "TIPO" bigint DEFAULT 1;
ALTER TABLE "Stock" ADD COLUMN "INICIAL" boolean DEFAULT FALSE;
ALTER TABLE "Stock" DROP COLUMN "N_FACTURA";

UPDATE "Stock" SET "INICIAL" = TRUE WHERE "CONCEPTO" = 'Entrada inicial';

UPDATE "Stock" SET "TIPO" = 1 WHERE "KILOS" > 0 AND "OID_ALBARAN" > 0;
UPDATE "Stock" SET "TIPO" = 2 WHERE "KILOS" < 0 AND "OID_ALBARAN" > 0;
UPDATE "Stock" SET "TIPO" = 3 WHERE "KILOS" > 0 AND "OID_ALBARAN" = 0;
UPDATE "Stock" SET "TIPO" = 4 WHERE "KILOS" < 0 AND "OID_ALBARAN" = 0;

ALTER TABLE "Cabeza" ADD CONSTRAINT "FK_Cabeza_Producto_Expediente" FOREIGN KEY ("OID_PRODUCTO_EXPEDIENTE") REFERENCES "Producto_Expediente" ("OID") ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE "Maquinaria" ADD CONSTRAINT "FK_Maquinaria_Producto_Expediente" FOREIGN KEY ("OID_PRODUCTO_EXPEDIENTE") REFERENCES "Producto_Expediente" ("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE "Stock" DROP CONSTRAINT "FK_Stock_Producto_Expediente";
ALTER TABLE "Stock" ADD CONSTRAINT "FK_Stock_Producto_Expediente" FOREIGN KEY ("OID_PRODUCTO_EXPEDIENTE") REFERENCES "Producto_Expediente" ("OID")ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE "Gasto" ADD COLUMN "TIPO" bigint DEFAULT 1;
ALTER TABLE "Producto_Proveedor" ADD COLUMN "OID_IMPUESTO" bigint DEFAULT 0;

ALTER TABLE "Producto_Proveedor" DROP CONSTRAINT "FK_Proveedor_Producto_Proveedor";

ALTER TABLE "Producto" ADD COLUMN "UNITARIO" boolean DEFAULT FALSE;

ALTER TABLE "Producto_Expediente" ADD COLUMN "TIPO" bigint DEFAULT 1;
UPDATE "Producto_Expediente" SET "TIPO" = 1;