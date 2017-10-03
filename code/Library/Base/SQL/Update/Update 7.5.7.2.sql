/* UPDATE 7.5.7.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.7.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "STSupplier" ADD COLUMN "SWIFT" character varying(255);
ALTER TABLE "STCustomAgent" ADD COLUMN "SWIFT" character varying(255);
ALTER TABLE "STShippingCompany" ADD COLUMN "SWIFT" character varying(255);
ALTER TABLE "STTransporter" ADD COLUMN "SWIFT" character varying(255);
ALTER TABLE "STEmployee" ADD COLUMN "SWIFT" character varying(255);
