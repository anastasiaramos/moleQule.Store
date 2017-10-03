/* UPDATE 7.4.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.4.0.1' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

UPDATE "LineaLibroGanadero" SET "ESTADO" = LL."ESTADO"
FROM ( SELECT LL."OID", 18 AS "ESTADO" --BAJA
	FROM "LineaLibroGanadero" AS LL
	WHERE LL."TIPO" = 1
	UNION 
	SELECT LL."OID", 11 AS "ESTADO" --ALTA
	FROM "LineaLibroGanadero" AS LL
	WHERE LL."TIPO" != 1) AS LL
WHERE LL."OID" = "LineaLibroGanadero"."OID";

UPDATE "LineaLibroGanadero" SET "SERIAL" = LL."NROW", "CODIGO" = trim(to_char(LL."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID" ) AS "NROW"
     FROM "LineaLibroGanadero" 
     WHERE "FECHA" >= '2013-01-01 00:00:00') AS LL
WHERE "LineaLibroGanadero"."OID" = LL."OID";

UPDATE "LineaLibroGanadero" SET "SERIAL" = LL."NROW", "CODIGO" = trim(to_char(LL."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID" ) AS "NROW"
     FROM "LineaLibroGanadero" 
     WHERE "FECHA" between '2012-01-01 00:00:00' and '2012-12-31 23:59:59') AS LL
WHERE "LineaLibroGanadero"."OID" = LL."OID";

UPDATE "LineaLibroGanadero" SET "SERIAL" = LL."NROW", "CODIGO" = trim(to_char(LL."NROW", '00000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID" ) AS "NROW"
     FROM "LineaLibroGanadero" 
     WHERE "FECHA" between '2011-01-01 00:00:00' and '2011-12-31 23:59:59') AS LL
WHERE "LineaLibroGanadero"."OID" = LL."OID";

UPDATE "LineaLibroGanadero" SET "FECHA" = cast(cast(LL."FECHA" as date) as timestamp without time zone) + cast (LL."SERIAL" || ' seconds' as interval)
FROM (SELECT "OID", "FECHA", "SERIAL"
     FROM "LineaLibroGanadero") AS LL
WHERE "LineaLibroGanadero"."OID" = LL."OID";
	
	