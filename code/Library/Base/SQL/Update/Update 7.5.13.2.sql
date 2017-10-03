SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.13.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "LibroGanadero" RENAME TO "STLivestockBook";
ALTER SEQUENCE "LibroGanadero_OID_seq" RENAME TO "STLivestockBook_OID_seq";

ALTER TABLE "LineaLibroGanadero" RENAME TO "STLivestockBookLine";
ALTER SEQUENCE "LineaLibroGanadero_OID_seq" RENAME TO "STLivestockBookLine_OID_seq";

ALTER TABLE "STLivestockBookLine" ADD COLUMN "OID_PAIR" bigint DEFAULT 0;

UPDATE "STLivestockBookLine" SET "OID_PAIR" = LL1."OID"
FROM "STLivestockBookLine" AS LL
INNER JOIN "STLivestockBookLine" AS LL1 ON LL1."OID_PARTIDA" = LL."OID_PARTIDA" AND LL1."TIPO" = 1
WHERE LL."TIPO" = 2
	AND "STLivestockBookLine"."OID" = LL."OID";

ALTER TABLE "STLivestockBookLine" ADD COLUMN "EXPLOTACION" boolean DEFAULT TRUE;
