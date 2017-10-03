/* UPDATE 3.9.0.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

ALTER TABLE "Expediente" RENAME COLUMN "G_TRANS_CUENTA" TO "N_DUA";
UPDATE "Expediente" SET "N_DUA" = '';

ALTER TABLE "Expediente" DROP COLUMN "G_PROV_CUENTA";
ALTER TABLE "Expediente" DROP COLUMN "G_NAV_CUENTA";
ALTER TABLE "Expediente" DROP COLUMN "G_TRANS_DEST_CUENTA";
ALTER TABLE "Expediente" DROP COLUMN "G_DESP_CUENTA";

ALTER TABLE "Producto_Expediente" ADD COLUMN "SERIAL" bigint;
ALTER TABLE "Producto_Expediente" ADD COLUMN "CODIGO" varchar(255);

UPDATE "Producto_Expediente" SET "SERIAL" = C."NROW", "CODIGO" = trim(to_char(C."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID") AS "NROW"
     FROM "Producto_Expediente" 
     ORDER BY "OID") AS C
WHERE "Producto_Expediente"."OID" = C."OID"; 