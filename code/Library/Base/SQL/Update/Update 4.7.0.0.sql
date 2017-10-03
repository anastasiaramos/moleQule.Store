/* UPDATE 4.7.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '4.7.0.0' WHERE "NAME" = 'STORE_DB_VERSION';

INSERT INTO "TIPOENTIDAD" ("VALOR", "USER_CREATED", "COMMON_SCHEMA") VALUES ('Empleado', FALSE, TRUE);
INSERT INTO "Entidad" ("TIPO") VALUES ('Empleado');
UPDATE "TIPOENTIDAD" SET "VALOR" = 'FacturaRecibida' WHERE "VALOR" = 'FacturaProveedor';
UPDATE "Entidad" SET "TIPO" = 'FacturaRecibida' WHERE "TIPO" = 'FacturaProveedor';

UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Almacenes' WHERE "TIPO" = 'Almacen';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Almacenes' WHERE "TIPO" = 'Despachante';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Empleados' WHERE "TIPO" = 'Empleado';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Expedientes' WHERE "TIPO" = 'Expediente';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Facturas Recibidas' WHERE "TIPO" = 'FacturaRecibida';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Navieras' WHERE "TIPO" = 'Naviera';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Pagos' WHERE "TIPO" = 'Pago';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Productos' WHERE "TIPO" = 'Producto';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Proveedores' WHERE "TIPO" = 'Proveedor';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Transportistas' WHERE "TIPO" = 'Transportista';
UPDATE "COMMON"."Entidad" SET "OBSERVACIONES" = 'Líneas de Fomento' WHERE "TIPO" = 'LineaFomento';

SET SEARCH_PATH = "0001";

ALTER TABLE "Proveedor" ALTER COLUMN "ESTADO" TYPE bigint;
ALTER TABLE "AlbaranProveedor" ADD COLUMN "ESTADO" bigint DEFAULT 1;

UPDATE "AlbaranProveedor" SET "ESTADO" = 1;

UPDATE "AlbaranProveedor" SET "ESTADO" = 6
FROM (SELECT DISTINCT "OID_ALBARAN"
     FROM "Albaran_FacturaProveedor") AS AF
WHERE "AlbaranProveedor"."OID" = AF."OID_ALBARAN";