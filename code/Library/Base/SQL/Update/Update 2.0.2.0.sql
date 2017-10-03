/* UPDATE 2.0.2.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

--ALIMENTACION
UPDATE "FacturaRecibida" SET "OID_SERIE" = 1 
FROM "Expediente", "Gasto"
WHERE "FacturaRecibida"."OID" = "Gasto"."OID_FACTURA" 
AND "Expediente"."OID" = "Gasto"."OID_EXPEDIENTE"
AND "TIPO_EXPEDIENTE" = 2;

--MAQUINARIA
UPDATE "FacturaRecibida" SET "OID_SERIE" = 2 
FROM "Expediente", "Gasto"
WHERE "FacturaRecibida"."OID" = "Gasto"."OID_FACTURA" 
AND "Expediente"."OID" = "Gasto"."OID_EXPEDIENTE"
AND "TIPO_EXPEDIENTE" = 3;

--GANADO
UPDATE "FacturaRecibida" SET "OID_SERIE" = 6 
FROM "Expediente", "Gasto"
WHERE "FacturaRecibida"."OID" = "Gasto"."OID_FACTURA" 
AND "Expediente"."OID" = "Gasto"."OID_EXPEDIENTE"
AND "TIPO_EXPEDIENTE" = 4;

ALTER TABLE "Expediente" ADD COLUMN "AYUDAS" decimal(10,2) DEFAULT 0;
ALTER TABLE "FacturaRecibida" ADD COLUMN "FECHA_REGISTRO" date;

UPDATE "Expediente" SET "TIPO_EXPEDIENTE" = 5 WHERE "TIPO_EXPEDIENTE" = 4; 
UPDATE "Expediente" SET "TIPO_EXPEDIENTE" = 4 WHERE "TIPO_EXPEDIENTE" = 3; 
UPDATE "Expediente" SET "TIPO_EXPEDIENTE" = 3 WHERE "TIPO_EXPEDIENTE" = 2; 
UPDATE "Expediente" SET "TIPO_EXPEDIENTE" = 2 WHERE "TIPO_EXPEDIENTE" = 1; 
UPDATE "Expediente" SET "TIPO_EXPEDIENTE" = 1 WHERE "TIPO_EXPEDIENTE" = 0; 