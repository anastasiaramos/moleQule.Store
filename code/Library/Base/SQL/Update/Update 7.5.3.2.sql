/* UPDATE 7.5.3.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.5.3.2' WHERE "NAME" = 'STORE_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "LineaAlmacen";
DROP TABLE IF EXISTS "Alumno_Cliente";

ALTER TABLE "ConceptoAlbaranProveedor" RENAME TO "STDeliveryLine";
ALTER SEQUENCE "ConceptoAlbaranProveedor_OID_seq" RENAME TO "STDeliveryLine_OID_seq";	

ALTER TABLE "ConceptoFacturaProveedor" RENAME TO "STInvoiceLine";
ALTER SEQUENCE "ConceptoFacturaProveedor_OID_seq" RENAME TO "STInvoiceLine_OID_seq";

ALTER TABLE "Expediente" RENAME TO "STExpedient";
ALTER SEQUENCE "Expediente_OID_seq" RENAME TO "STExpedient_OID_seq";
	
ALTER TABLE "Gasto" RENAME TO "STExpense";
ALTER SEQUENCE "Gasto_OID_seq" RENAME TO "STExpense_OID_seq";

ALTER TABLE "LineaPedidoProveedor" RENAME TO "STOrderLine";
ALTER SEQUENCE "LineaPedidoProveedor_OID_seq" RENAME TO "STOrderLine_OID_seq";

ALTER TABLE "Nomina" RENAME TO "STPayroll";
ALTER SEQUENCE "Nomina_OID_seq" RENAME TO "STPayroll_OID_seq";

ALTER TABLE "Pago" RENAME TO "STPayment";
ALTER SEQUENCE "Pago_OID_seq" RENAME TO "STPayment_OID_seq";

ALTER TABLE "Pago_Operacion" RENAME TO "STTransactionPayment";
ALTER SEQUENCE "Pago_Operacion_OID_seq" RENAME TO "STTransactionPayment_OID_seq";	

ALTER TABLE "Partida" RENAME TO "STBatch";
ALTER SEQUENCE "Partida_OID_seq" RENAME TO "STBatch_OID_seq";	

ALTER TABLE "PedidoProveedor" RENAME TO "STOrder";
ALTER SEQUENCE "PedidoProveedor_OID_seq" RENAME TO "STOrder_OID_seq";	

/*CREATE TABLE "STPayroll"
(
  "OID" bigint NOT NULL DEFAULT nextval('"0001"."STPayroll_OID_seq"'::regclass),
  "OID_USUARIO" bigint DEFAULT 1,
  "OID_REMESA" bigint DEFAULT 0,
  "OID_TIPO" bigint DEFAULT 0,
  "OID_EXPEDIENTE" bigint DEFAULT 0,
  "OID_EMPLEADO" bigint,
  "SERIAL" bigint,
  "CODIGO" character varying(255),
  "ESTADO" bigint DEFAULT 1,
  "FECHA" timestamp without time zone,
  "DESCRIPCION" text,
  "BRUTO" numeric(10,2),
  "BASE_IRPF" numeric(10,2),
  "NETO" numeric(10,2),
  "P_IRPF" numeric(10,2),
  "SEGURO" numeric(10,2),
  "DESCUENTOS" numeric(10,2),
  "PREVISION_PAGO" timestamp without time zone,
  "OBSERVACIONES" text,
  CONSTRAINT "PK_Nomina" PRIMARY KEY ("OID"),
  CONSTRAINT "FK_Nomina_Empleado" FOREIGN KEY ("OID_EMPLEADO")
      REFERENCES "0001"."STEmployee" ("OID") MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT "FK_Nomina_RemesaNomina" FOREIGN KEY ("OID_REMESA")
      REFERENCES "0001"."RemesaNomina" ("OID") MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE "STPayroll" OWNER TO moladmin;
GRANT ALL ON TABLE "STPayroll" TO moladmin;
GRANT ALL ON TABLE "STPayroll" TO "MOLEQULE_ADMINISTRATOR";*/
