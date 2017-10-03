SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.16.3' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "STWorkFootPrint" CASCADE;
CREATE TABLE "STWorkFootPrint" 
( 
	"OID" bigserial NOT NULL,
	"OID_EXTERNAL" bigint DEFAULT 0,
	"CODE_EXTERNAL" bigint DEFAULT 0,
	"WORK_CODE" bigint DEFAULT 1,
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
	"HOURS" numeric(10,2) DEFAULT 0,
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkFootPrint" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkFootPrint" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkFootPrint" TO GROUP "MOLEQULE_ADMINISTRATOR";