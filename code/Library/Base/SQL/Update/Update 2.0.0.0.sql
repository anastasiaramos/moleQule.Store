/* UPDATE 2.0.0.0*/

SET SEARCH_PATH = "COMMON";

ALTER TABLE "Proveedor" ADD COLUMN "ID" varchar(50);
ALTER TABLE "Proveedor" ADD COLUMN "TIPO_ID" int8 NOT NULL DEFAULT 0;
ALTER TABLE "Proveedor" ADD COLUMN "ALIAS" varchar(255);

ALTER TABLE "Despachante" ADD COLUMN "ALIAS" character varying(255);
ALTER TABLE "Naviera" ADD COLUMN "ALIAS" character varying(255);
ALTER TABLE "Transportista" ADD COLUMN "ALIAS" character varying(255);

ALTER TABLE "Despachante" ALTER COLUMN "FORMA_PAGO" SET DEFAULT 1;
ALTER TABLE "Naviera" ALTER COLUMN "FORMA_PAGO" SET DEFAULT 1;
ALTER TABLE "Transportista" ALTER COLUMN "FORMA_PAGO" SET DEFAULT 1;

ALTER TABLE "Serie" ADD COLUMN "VENTA" boolean NOT NULL DEFAULT TRUE;
UPDATE "Serie" SET "VENTA" = TRUE;

ALTER TABLE "Despachante" ADD COLUMN "ID" varchar(50);
ALTER TABLE "Naviera" ADD COLUMN "ID" varchar(50);
ALTER TABLE "Transportista" ADD COLUMN "ID" varchar(50);

ALTER TABLE "Despachante" ADD COLUMN "TIPO_ID" int8 NOT NULL DEFAULT 0;
ALTER TABLE "Naviera" ADD COLUMN "TIPO_ID" int8 NOT NULL DEFAULT 0;
ALTER TABLE "Transportista" ADD COLUMN "TIPO_ID" int8 NOT NULL DEFAULT 0;

SET SEARCH_PATH = "0001";

ALTER TABLE "Concepto_FacturaProveedor" ADD COLUMN "OID_EXPEDIENTE" bigint DEFAULT 0;
ALTER TABLE "Concepto_FacturaProveedor" ADD COLUMN "OID_PRODUCTO_EXPEDIENTE" bigint DEFAULT 0;
ALTER TABLE "Concepto_FacturaProveedor" ADD COLUMN "OID_PRODUCTO" bigint DEFAULT 0;
ALTER TABLE "Concepto_FacturaProveedor" ADD COLUMN "OID_KIT" int8 NOT NULL DEFAULT 0;
ALTER TABLE "Concepto_FacturaProveedor" ADD COLUMN "OID_CONCEPTO_ALBARAN" int8;
ALTER TABLE "Concepto_FacturaProveedor" ADD COLUMN "CODIGO_EXPEDIENTE" varchar(255);
ALTER TABLE "FacturaRecibida" ADD COLUMN "FECHA_REGISTRO" date;
ALTER TABLE "FacturaRecibida" ADD COLUMN "VAT_NUMBER" character varying(255);
ALTER TABLE "FacturaRecibida" ADD COLUMN "EMISOR" character varying(255);
ALTER TABLE "FacturaRecibida" ADD COLUMN "DIRECCION" character varying(255);
ALTER TABLE "FacturaRecibida" ADD COLUMN "CODIGO_POSTAL" character varying(255);
ALTER TABLE "FacturaRecibida" ADD COLUMN "PROVINCIA" character varying(255);
ALTER TABLE "FacturaRecibida" ADD COLUMN "MUNICIPIO" character varying(255);	
ALTER TABLE "FacturaRecibida" ADD COLUMN "P_IRPF" numeric(10,2) DEFAULT 0;
ALTER TABLE "FacturaRecibida" ADD COLUMN "RECTIFICATIVA" boolean;
ALTER TABLE "CobroREA" ADD COLUMN "FECHA_ASIGNACION" date;

UPDATE "FacturaRecibida" SET "FECHA_REGISTRO" = "FECHA";

ALTER TABLE "Pago" ADD COLUMN "MEDIO_PAGO" int8;
UPDATE "Pago" SET "MEDIO_PAGO" = 1 WHERE "TIPO" = 'Efectivo';
UPDATE "Pago" SET "MEDIO_PAGO" = 2 WHERE "TIPO" = 'Ingreso';
UPDATE "Pago" SET "MEDIO_PAGO" = 3 WHERE "TIPO" = 'Cheque';
UPDATE "Pago" SET "MEDIO_PAGO" = 4 WHERE "TIPO" = 'Pagare';
UPDATE "Pago" SET "MEDIO_PAGO" = 5 WHERE "TIPO" = 'Giro';
UPDATE "Pago" SET "MEDIO_PAGO" = 6 WHERE "TIPO" = 'Transferencia';
UPDATE "Pago" SET "MEDIO_PAGO" = 7 WHERE "TIPO" = 'CompensacionFactura';
UPDATE "Pago" SET "MEDIO_PAGO" = 8 WHERE "TIPO" = 'Tarjeta';
ALTER TABLE "Pago" DROP COLUMN "TIPO";

ALTER TABLE "Concepto_FacturaProveedor" RENAME TO "ConceptoFacturaRecibida";
ALTER TABLE "Concepto_Proforma" RENAME TO "ConceptoProforma";

ALTER SEQUENCE "Concepto_FacturaProveedor_OID_seq" RENAME TO "ConceptoFacturaProveedor_OID_seq";
ALTER SEQUENCE "Concepto_Proforma_OID_seq" RENAME TO "ConceptoProforma_OID_seq";

ALTER TABLE "ConceptoFacturaRecibida" ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."ConceptoFacturaProveedor_OID_seq"'::text)::regclass);
ALTER TABLE "ConceptoProforma" ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."ConceptoProforma_OID_seq"'::text)::regclass);

UPDATE "FacturaRecibida" SET "TIPO_ACREEDOR" = 5 WHERE "TIPO_ACREEDOR" = 4; 
UPDATE "FacturaRecibida" SET "TIPO_ACREEDOR" = 4 WHERE "TIPO_ACREEDOR" = 3; 
UPDATE "FacturaRecibida" SET "TIPO_ACREEDOR" = 3 WHERE "TIPO_ACREEDOR" = 2; 
UPDATE "FacturaRecibida" SET "TIPO_ACREEDOR" = 2 WHERE "TIPO_ACREEDOR" = 1; 
UPDATE "FacturaRecibida" SET "TIPO_ACREEDOR" = 1 WHERE "TIPO_ACREEDOR" = 0; 

UPDATE "Pago" SET "TIPO_AGENTE" = 5 WHERE "TIPO_AGENTE" = 4; 
UPDATE "Pago" SET "TIPO_AGENTE" = 4 WHERE "TIPO_AGENTE" = 3; 
UPDATE "Pago" SET "TIPO_AGENTE" = 3 WHERE "TIPO_AGENTE" = 2; 
UPDATE "Pago" SET "TIPO_AGENTE" = 2 WHERE "TIPO_AGENTE" = 1; 
UPDATE "Pago" SET "TIPO_AGENTE" = 1 WHERE "TIPO_AGENTE" = 0; 

UPDATE "Pago_Factura" SET "TIPO_AGENTE" = 5 WHERE "TIPO_AGENTE" = 4; 
UPDATE "Pago_Factura" SET "TIPO_AGENTE" = 4 WHERE "TIPO_AGENTE" = 3; 
UPDATE "Pago_Factura" SET "TIPO_AGENTE" = 3 WHERE "TIPO_AGENTE" = 2; 
UPDATE "Pago_Factura" SET "TIPO_AGENTE" = 2 WHERE "TIPO_AGENTE" = 1; 
UPDATE "Pago_Factura" SET "TIPO_AGENTE" = 1 WHERE "TIPO_AGENTE" = 0; 