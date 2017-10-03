-- STORE MODULE SCHEMA TABLES

DROP TABLE  IF EXISTS "STBatch" CASCADE;
CREATE TABLE "STBatch" 
( 
	"OID" bigserial NOT NULL,
	"OID_EXPEDIENTE" bigint,
    "OID_PRODUCTO" bigint,
    "OID_PROVEEDOR" bigint,
    "OID_KIT" bigint DEFAULT 0 NOT NULL,
    "OID_BATCH" bigint DEFAULT 0 NOT NULL,
    "FECHA_COMPRA" timestamp without time zone,
    "TIPO_MERCANCIA" character varying(255),
    "BULTOS_INICIALES" numeric(10,2),
    "KILOS_INICIALES" numeric(10,2),
    "PRECIO_COMPRA_KILO" numeric(10,5),
    "PRECIO_VENTA_KILO" numeric(10,5),
    "PRECIO_VENTA_BULTO" numeric(10,5),
    "BENEFICIO_KILO" numeric(10,5),
    "COSTE_KILO" numeric(10,5),
    "REA_FECHA_COBRO" date,
    "REA_COBRADA" boolean,
    "AYUDA_RECIBIDA_KILO" numeric(10,5),
    "PROPORCION" numeric(10,2) DEFAULT 0 NOT NULL,
    "UBICACION" character varying(255),
    "OBSERVACIONES" text,
    "GASTO_KILO" numeric(10,5) DEFAULT 0,
    "SERIAL" bigint,
    "CODIGO" character varying(255),
    "OID_ALMACEN" bigint DEFAULT 1,
    "AYUDA" boolean DEFAULT true,
    "TIPO" bigint DEFAULT 1,
	CONSTRAINT "PK_STBatch" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STBatch" OWNER TO moladmin;
GRANT ALL ON TABLE "STBatch" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STCustomAgent" CASCADE;
CREATE TABLE "STCustomAgent" 
( 
	"OID" bigserial NOT NULL,
	"CODIGO" character varying(255) NOT NULL,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "ID" character varying(50),
    "TIPO_ID" bigint DEFAULT 0 NOT NULL,
    "NOMBRE" character varying(255),
    "ALIAS" character varying(255),
    "MEDIO_PAGO" bigint DEFAULT 1,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint,
    "CUENTA_BANCARIA" character varying(255),
	"SWIFT" character varying(255),
    "CUENTA_ASOCIADA" character varying(255),
    "CONTACTO" character varying(255),
    "EMAIL" character varying(255),
    "DIRECCION" character varying(255),
    "LOCALIDAD" character varying(255),
    "TELEFONO" character varying(255),
    "COD_POSTAL" character varying(255),
    "PROVINCIA" character varying(255),
    "MUNICIPIO" character varying(255),
    "PAIS" character varying(255),
    "OBSERVACIONES" text,
    "OID_CUENTA_BANCARIA_ASOCIADA" bigint DEFAULT 0,
    "CUENTA_CONTABLE" text,
    "OID_IMPUESTO" bigint,
    "TIPO" bigint DEFAULT 5,
    "ESTADO" integer DEFAULT 10,	
    "OID_TARJETA_ASOCIADA" bigint DEFAULT 0,
	CONSTRAINT "PK_Despachante" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STCustomAgent" OWNER TO moladmin;
GRANT ALL ON TABLE "STCustomAgent" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STCustomAgent_Port" CASCADE;
CREATE TABLE "STCustomAgent_Port" 
( 
	"OID" bigserial NOT NULL,
	"OID_PUERTO" int8,
	"OID_DESPACHANTE" int8,
	CONSTRAINT "PK_STCustomAgent_Port" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STCustomAgent_Port" OWNER TO moladmin;
GRANT ALL ON TABLE "STCustomAgent_Port" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STDelivery" CASCADE;
CREATE TABLE "STDelivery" 
( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" bigint,
    "OID_ACREEDOR" bigint,
    "TIPO_ACREEDOR" bigint,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "ANO" bigint,
    "FECHA" timestamp without time zone,
    "FECHA_REGISTRO" timestamp without time zone,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint DEFAULT 0,
    "MEDIO_PAGO" bigint DEFAULT 1,
    "PREVISION_PAGO" date,
    "P_IRPF" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "DESCUENTO" numeric(10,2) DEFAULT 0,
    "BASE_IMPONIBLE" numeric(10,2),
    "IGIC" numeric(10,2),
    "TOTAL" numeric(10,2),
    "CUENTA_BANCARIA" character varying(255),
    "NOTA" boolean,
    "OBSERVACIONES" text,
    "CONTADO" boolean DEFAULT false NOT NULL,
    "RECTIFICATIVO" boolean DEFAULT false,
    "ESTADO" bigint DEFAULT 1,
    "OID_ALMACEN" bigint DEFAULT 0,
    "OID_EXPEDIENTE" bigint DEFAULT 0,
    "OID_USUARIO" bigint DEFAULT 0,
    "EXPEDIENTE" character varying(255),
	"IRPF" numeric(10,2),
	CONSTRAINT "PK_STDelivery" PRIMARY KEY ("OID")	
) WITHOUT OIDS;

ALTER TABLE "STDelivery" OWNER TO moladmin;
GRANT ALL ON TABLE "STDelivery" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STDeliveryLine";
CREATE TABLE "STDeliveryLine" 
( 
	"OID" bigserial NOT NULL,
	"OID_ALBARAN" bigint,
    "OID_BATCH" bigint,
    "OID_EXPEDIENTE" bigint,
    "OID_PRODUCTO" bigint,
    "OID_KIT" bigint DEFAULT 0 NOT NULL,
    "CODIGO_EXPEDIENTE" character varying(255),
    "CONCEPTO" character varying(255),
    "FACTURACION_BULTO" boolean,
    "CANTIDAD" numeric(10,2),
    "CANTIDAD_BULTOS" numeric(10,4),
    "P_IGIC" numeric(10,2),
    "P_IRPF" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "TOTAL" numeric(10,2),
    "PRECIO" numeric(10,5),
    "SUBTOTAL" numeric(10,2),
    "GASTOS" numeric(10,5),
    "OID_IMPUESTO" bigint,
    "OID_LINEA_PEDIDO" bigint DEFAULT 0,
    "OID_ALMACEN" bigint DEFAULT 0,
    "CODIGO_PRODUCTO_PROVEEDOR" character varying(255),	
	CONSTRAINT "PK_STDeliveryLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STDeliveryLine" OWNER TO moladmin;
GRANT ALL ON TABLE "STDeliveryLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STDelivery_Invoice" CASCADE;
CREATE TABLE "STDelivery_Invoice" 
( 
	"OID" bigserial NOT NULL,
	"OID_ALBARAN" bigint NOT NULL,
    "OID_FACTURA" bigint NOT NULL,
    "FECHA_ASIGNACION" date,
	CONSTRAINT "PK_STDelivery_Invoice" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STDelivery_Invoice" OWNER TO moladmin;
GRANT ALL ON TABLE "STDelivery_Invoice" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STDestinationPrice" CASCADE;
CREATE TABLE "STDestinationPrice" 
( 
	"OID" bigserial NOT NULL,
	"OID_TRANSPORTISTA" bigint,
    "OID_CLIENTE" bigint,
    "NUMERO_CLIENTE" bigint,
    "CODIGO_CLIENTE" character varying(255),
    "NOMBRE_CLIENTE" character varying(255),
    "PUERTO" character varying(255),
    "PRECIO" numeric(10,2),
	CONSTRAINT "PK_STDestinationPrice" PRIMARY KEY ("OID")	
) WITHOUT OIDS;

ALTER TABLE "STDestinationPrice" OWNER TO moladmin;
GRANT ALL ON TABLE "STDestinationPrice" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STEmployee";
CREATE TABLE "STEmployee"
(
	"OID" bigserial NOT NULL,
	"OID_IMPUESTO" bigint,
	"TIPO" bigint DEFAULT 8,
	"CODIGO" varchar(255) NOT NULL UNIQUE,
	"SERIAL" int8 NOT NULL,
	"ESTADO" bigint DEFAULT 10,
	"NOMBRE" varchar(255),
	"NOMBRE_PROPIO" varchar(255),
	"APELLIDOS" varchar(255),
	"ALIAS" character varying(255) NOT NULL,
  	"ID" varchar(50),
	"TIPO_ID" int8 NOT NULL DEFAULT 0,
	"DIRECCION" varchar(255),
	"COD_POSTAL" varchar(255),
	"LOCALIDAD" varchar(255),
	"MUNICIPIO" varchar(255),
	"PROVINCIA" varchar(255),
	"PAIS" character varying(255),
  	"TELEFONO" varchar(255),
    "EMAIL" character varying(255),
	"FOTO" varchar(255),
	"PERFIL" int8 NOT NULL,
	"ACTIVO" bool DEFAULT true,
	"INICIO_CONTRATO" date,
	"FIN_CONTRATO" date,	
	"CUENTA_BANCARIA" character varying(255),
	"SWIFT" character varying(255),
	"OID_CUENTA_BANCARIA_ASOCIADA" bigint DEFAULT 0,
	"CUENTA_CONTABLE" character varying(255),
	"MEDIO_PAGO" bigint DEFAULT 1,
	"FORMA_PAGO" bigint DEFAULT 1,
	"DIAS_PAGO" bigint,
	"CONTACTO" character varying(255),
	"NIVEL_ESTUDIOS" varchar(255),
	"OBSERVACIONES" text,
	"SUELDO_BRUTO" numeric(10,2) DEFAULT 0,
	"P_IRPF" numeric(10,2) DEFAULT 0,
	"CUENTA_ASOCIADA" character varying(255),
	"SUELDO_NETO" numeric(10,2) DEFAULT 0,
	"BASE_IRPF" numeric(10,2) DEFAULT 0, 
	"DESCUENTOS" numeric(10,2) DEFAULT 0,
	"SEGURO" numeric(10,2) DEFAULT 0,
    "OID_TARJETA_ASOCIADA" bigint DEFAULT 0,
	"PAYROLL_METHOD" bigint DEFAULT 1,
	CONSTRAINT "PK_STEmployee" PRIMARY KEY ("OID")
)WITHOUT OIDS;

ALTER TABLE "STEmployee" OWNER TO moladmin;
GRANT ALL ON TABLE "STEmployee" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STExpedient";
CREATE TABLE "STExpedient" 
( 
	"OID" bigserial NOT NULL,
	"OID_NAVIERA" bigint,
    "OID_TRANS_ORIGEN" bigint,
    "OID_TRANS_DESTINO" bigint,
    "OID_DESPACHANTE" bigint,
    "OID_FACTURA_PRO" bigint,
    "OID_FACTURA_NAV" bigint,
    "OID_FACTURA_DES" bigint,
    "OID_FACTURA_TOR" bigint,
    "OID_FACTURA_TDE" bigint,
    "TIPO_EXPEDIENTE" bigint DEFAULT 0 NOT NULL,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "PUERTO_ORIGEN" character varying(255),
    "PUERTO_DESTINO" character varying(255),
    "BUQUE" character varying(255),
    "ANO" integer DEFAULT 1,
    "FECHA_PEDIDO" date,
    "FECHA_FAC_PROVEEDOR" date,
    "FECHA_EMBARQUE" date,
    "FECHA_LLEGADA_MUELLE" date,
    "FECHA_DESPACHO_DESTINO" date,
    "FECHA_SALIDA_MUELLE" date,
    "FECHA_REGRESO_MUELLE" date,
    "OBSERVACIONES" text,
    "FLETE_NETO" numeric(10,2),
    "BAF" numeric(10,2),
    "TEUS20" boolean,
    "TEUS40" boolean,
    "T3_ORIGEN" numeric(10,2),
    "T3_DESTINO" numeric(10,2),
    "THC_ORIGEN" numeric(10,2),
    "THC_DESTINO" numeric(10,2),
    "ISPS" numeric(10,2),
    "TOTAL_IMPUESTOS" numeric(10,2),
    "G_TRANS_FAC" character varying(255),
    "G_TRANS_TOTAL" numeric(10,2),
    "N_DUA" character varying(255),
    "G_NAV_FAC" character varying(255),
    "G_NAV_TOTAL" numeric(10,2),
    "G_DESP_FAC" character varying(255),
    "G_DESP_TOTAL" numeric(10,2),
    "G_DESP_IGIC" numeric(10,2),
    "G_DESP_IGIC_SERV" numeric(10,2),
    "G_TRANS_DEST_FAC" character varying(255),
    "G_TRANS_DEST_TOTAL" numeric(10,2),
    "G_TRANS_DEST_IGIC" numeric(10,2),
    "CONTENEDOR" character varying(255),
    "OID_PROVEEDOR" bigint,
    "G_PROV_FAC" character varying(255),
    "G_PROV_TOTAL" numeric(10,2),
    "CUENTA_REA" character varying(255),
    "EXPEDIENTE_REA" character varying(255),
    "CERTIFICADO_REA" character varying(255),
    "COBRO_REA" timestamp without time zone,
    "TIPO_MERCANCIA" character varying(255),
    "NOMBRE_CLIENTE" character varying(255),
    "CODIGO_ARTICULO" character varying(255),
    "NOMBRE_TRANS_DEST" character varying(255),
    "NOMBRE_TRANS_ORIG" character varying(255),
    "AYUDAS" numeric(10,2) DEFAULT 0,
    "AYUDA" boolean DEFAULT true,
    "ESTIMAR_DESPACHANTE" boolean DEFAULT true,
    "ESTIMAR_NAVIERA" boolean DEFAULT true,
    "ESTIMAR_TORIGEN" boolean DEFAULT true,
    "ESTIMAR_TDESTINO" boolean DEFAULT true,
    "FECHA" timestamp without time zone,
	CONSTRAINT "PK_STExpedient" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STExpedient" OWNER TO moladmin;
GRANT ALL ON TABLE "STExpedient" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "ExpedienteREA";
CREATE TABLE "ExpedienteREA" 
( 
	"OID" bigserial NOT NULL,
	"OID_EXPEDIENTE" bigint NOT NULL,
    "SERIAL" bigint NOT NULL,
    "CODIGO" character varying(255),
    "ESTADO" bigint DEFAULT 1,
    "CODIGO_ADUANERO" character varying(255),
    "FECHA" timestamp without time zone,
    "EXPEDIENTE_REA" character varying(255),
    "CERTIFICADO_REA" character varying(255),
    "N_DUA" character varying(255),
    "OBSERVACIONES" text,
	CONSTRAINT "PK_ExpedienteREA" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "ExpedienteREA" OWNER TO moladmin;
GRANT ALL ON TABLE "ExpedienteREA" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STExpense" CASCADE;
CREATE TABLE "STExpense" 
( 
	"OID" bigserial NOT NULL,
	"OID_EXPEDIENTE" bigint NOT NULL,
    "OID_FACTURA" bigint,
    "DESCRIPCION" text,
    "FACTURAS" character varying(255),
    "TOTAL" numeric(10,2),
    "PREVISION_PAGO" date,
    "PAGADO" boolean,
    "TIPO" bigint DEFAULT 1,
    "SERIAL" bigint,
    "CODIGO" character varying(255),
    "OID_EMPLEADO" bigint,
    "OID_REMESA_NOMINA" bigint,
    "ESTADO" bigint,
    "OBSERVACIONES" text,
    "FECHA" timestamp without time zone,
    "OID_ALBARAN" bigint DEFAULT 0,
    "OID_CONCEPTO_FACTURA" bigint DEFAULT 0,
    "OID_CONCEPTO_ALBARAN" bigint DEFAULT 0,
    "OID_TIPO" bigint DEFAULT 0,
    "OID_USUARIO" bigint DEFAULT 1,
	CONSTRAINT "PK_STExpense" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STExpense" OWNER TO moladmin;
GRANT ALL ON TABLE "STExpense" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STExpenseType" CASCADE;
CREATE TABLE "STExpenseType" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" bigint,
    "CODIGO" character varying(255),
    "CATEGORIA" bigint,
    "NOMBRE" text,
    "MEDIO_PAGO" bigint DEFAULT 1,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint,
    "OID_CUENTA_ASOCIADA" bigint DEFAULT 0,
    "CUENTA_BANCARIA" character varying(255),
    "CUENTA_CONTABLE" character varying(255),
    "OBSERVACIONES" text,	
	CONSTRAINT "PK_STExpenseType" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STExpenseType" OWNER TO moladmin;
GRANT ALL ON TABLE "STExpenseType" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STFamily" CASCADE;
CREATE TABLE "STFamily" 
( 
	"OID" bigserial NOT NULL,
	"CODIGO" bigint NOT NULL,
    "NOMBRE" character varying(255),
    "OBSERVACIONES" text,
    "CUENTA_CONTABLE_COMPRA" text,
    "OID_IMPUESTO" bigint,
    "CUENTA_CONTABLE_VENTA" character varying(255),
    "AVISAR_BENEFICIO_MINIMO" boolean DEFAULT false,
    "P_BENEFICIO_MINIMO" numeric(10,2) DEFAULT 0,
	CONSTRAINT "PK_STFamily" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STFamily" OWNER TO moladmin;
GRANT ALL ON TABLE "STFamily" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STFamilySerie" CASCADE;
CREATE TABLE "STFamilySerie" 
( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" bigint,
    "OID_FAMILIA" bigint,
	CONSTRAINT "PK_STFamilySerie" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STFamilySerie" OWNER TO moladmin;
GRANT ALL ON TABLE "STFamilySerie" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STInvoice" CASCADE;
CREATE TABLE "STInvoice" 
( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" bigint,
    "OID_ACREEDOR" bigint,
    "TIPO_ACREEDOR" bigint,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(50),
    "N_FACTURA" character varying(50),
    "VAT_NUMBER" character varying(255),
    "EMISOR" character varying(255),
    "DIRECCION" character varying(255),
    "CODIGO_POSTAL" character varying(255),
    "PROVINCIA" character varying(255),
    "MUNICIPIO" character varying(255),
    "ANO" bigint,
    "FECHA" date,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint DEFAULT 0,
    "MEDIO_PAGO" bigint DEFAULT 1,
    "PREVISION_PAGO" date,
    "BASE_IMPONIBLE" numeric(10,2) DEFAULT 0,
    "P_IRPF" numeric(10,2) DEFAULT 0,
    "P_IGIC" numeric(10,2) DEFAULT 0,
    "P_DESCUENTO" numeric(10,2) DEFAULT 0,
    "DESCUENTO" numeric(10,2) DEFAULT 0,
    "TOTAL" numeric(10,2) DEFAULT 0,
    "CUENTA_BANCARIA" character varying(255),
    "NOTA" boolean,
    "OBSERVACIONES" text,
    "ALBARAN" boolean,
    "RECTIFICATIVA" boolean,
    "FECHA_REGISTRO" date,
    "ESTADO" bigint DEFAULT 1,
    "ALBARANES" text,
    "ID_MOV_CONTABLE" character varying(255),
    "OID_USUARIO" bigint DEFAULT 0,
    "OID_EXPEDIENTE" bigint DEFAULT 0,
    "EXPEDIENTE" character varying(255),
	"IRPF" numeric(10,2),
	"IMPUESTOS" numeric(10,2) DEFAULT 0,
	CONSTRAINT "PK_STInvoice" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STInvoice" OWNER TO moladmin;
GRANT ALL ON TABLE "STInvoice" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STInvoiceLine";
CREATE TABLE "STInvoiceLine" 
( 
	"OID" bigserial NOT NULL,
	"OID_FACTURA" bigint,
    "OID_BATCH" bigint,
    "OID_EXPEDIENTE" bigint,
    "OID_PRODUCTO" bigint,
    "OID_KIT" bigint DEFAULT 0 NOT NULL,
    "OID_CONCEPTO_ALBARAN" bigint,
    "CODIGO_EXPEDIENTE" character varying(255),
    "CONCEPTO" character varying(255),
    "FACTURACION_BULTO" boolean,
    "CANTIDAD" numeric(10,2),
    "CANTIDAD_BULTOS" numeric(10,4),
    "P_IGIC" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "P_IRPF" numeric(10,2),
    "TOTAL" numeric(10,2),
    "PRECIO" numeric(10,5),
    "SUBTOTAL" numeric(10,2),
    "OID_IMPUESTO" bigint,
    "CODIGO_PRODUCTO_PROVEEDOR" character varying(255),
	CONSTRAINT "PK_STInvoiceLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STInvoiceLine" OWNER TO moladmin;
GRANT ALL ON TABLE "STInvoiceLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "InventarioAlmacen" CASCADE;
CREATE TABLE "InventarioAlmacen" 
( 
	"OID" bigserial  NOT NULL,
	"OID_ALMACEN" bigint NOT NULL,
    "NOMBRE" character varying(255),
    "FECHA" date,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_InventarioAlmacen" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "InventarioAlmacen" OWNER TO moladmin;
GRANT ALL ON TABLE "InventarioAlmacen" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "STKit" CASCADE;
CREATE TABLE "STKit" 
( 
	"OID" bigserial NOT NULL,
	"OID_KIT" bigint,
    "OID_PRODUCT" bigint,
    "AMOUNT" numeric(10,2) DEFAULT 0,
	CONSTRAINT "PK_STKit" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STKit" OWNER TO moladmin;
GRANT ALL ON TABLE "STKit" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "STLivestockBook" CASCADE;
CREATE TABLE "STLivestockBook" 
( 
	"OID" bigserial  NOT NULL,
	"SERIAL" bigint NOT NULL,
    "CODIGO" character varying(255) NOT NULL,
    "NOMBRE" character varying(255),
    "BALANCE" numeric(10,2),
    "ESTADO" bigint DEFAULT 10,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_STLivestockBook" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STLivestockBook" OWNER TO moladmin;
GRANT ALL ON TABLE "STLivestockBook" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "LineaFomento" CASCADE;
CREATE TABLE "LineaFomento" 
( 
	"OID" bigserial NOT NULL,
	"OID_PARTIDA" bigint NOT NULL,
    "OID_EXPEDIENTE" bigint NOT NULL,
    "OID_NAVIERA" bigint NOT NULL,
    "SERIAL" bigint,
    "CODIGO" character varying(255),
    "FECHA" timestamp without time zone,
    "DESCRIPCION" text,
    "ID_SOLICITUD" character varying(255),
    "ID_ENVIO" character varying(255),
    "CONOCIMIENTO" text,
    "FECHA_CONOCIMIENTO" date,
    "DUA" character varying(255),
    "FLETE_NETO" numeric(10,2),
    "BAF" numeric(10,2),
    "TEUS20" boolean,
    "TEUS40" boolean,
    "T3_ORIGEN" numeric(10,2),
    "T3_DESTINO" numeric(10,2),
    "THC_ORIGEN" numeric(10,2),
    "THC_DESTINO" numeric(10,2),
    "ISPS" numeric(10,2),
    "TOTAL" numeric(10,2),
    "ESTADO" bigint DEFAULT 1,
    "OBSERVACIONES" text,
    "KILOS" numeric(10,2) DEFAULT 0,	
	CONSTRAINT "PK_LineaFomento" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "LineaFomento" OWNER TO moladmin;
GRANT ALL ON TABLE "LineaFomento" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "LineaInventario" CASCADE;
CREATE TABLE "LineaInventario" 
( 
	"OID" bigserial  NOT NULL,
	"OID_INVENTARIO" bigint NOT NULL,
    "OID_LINEAALMACEN" bigint NOT NULL,
    "CONCEPTO" character varying(255),
    "CANTIDAD" numeric(10,2) DEFAULT 0 NOT NULL,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_LineaInventario" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "LineaInventario" OWNER TO moladmin;
GRANT ALL ON TABLE "LineaInventario" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "STLivestockBookLine" CASCADE;
CREATE TABLE "STLivestockBookLine" 
( 
	"OID" bigserial  NOT NULL,
	"OID_LIBRO" bigint NOT NULL,
    "OID_PARTIDA" bigint NOT NULL,
    "OID_CONCEPTO" bigint NOT NULL,
    "ESTADO" bigint DEFAULT 7,
    "SERIAL" bigint NOT NULL,
    "CODIGO" character varying(255) NOT NULL,
    "CROTAL" character varying(255),
    "FECHA" timestamp without time zone,
    "SEXO" bigint DEFAULT 1,
    "EDAD" bigint DEFAULT 1,
    "RAZA" character varying(255),
    "CAUSA" character varying(255),
    "PROCEDENCIA" character varying(255),
    "BALANCE" numeric(10,2) DEFAULT 0,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_STLivestockBookLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STLivestockBookLine" OWNER TO moladmin;
GRANT ALL ON TABLE "STLivestockBookLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

CREATE TABLE "Maquinaria" 
( 
	"OID" bigserial NOT NULL,
	"OID_EXPEDIENTE" bigint,
    "OID_BATCH" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "IDENTIFICADOR" character varying(255),
    "DESCRIPCION" text,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_Maquinaria" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "Maquinaria" OWNER TO moladmin;
GRANT ALL ON TABLE "Maquinaria" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "STOrder" CASCADE;
CREATE TABLE "STOrder" 
( 
	"OID" bigserial  NOT NULL,
	"OID_USUARIO" bigint DEFAULT 0,
    "OID_ACREEDOR" bigint DEFAULT 0,
    "SERIAL" bigint NOT NULL,
    "CODIGO" character varying(255) NOT NULL,
    "FECHA" timestamp without time zone,
    "ESTADO" bigint,
    "TIPO_ACREEDOR" bigint DEFAULT 1,
    "OBSERVACIONES" text,
    "OID_SERIE" bigint DEFAULT 0,
    "P_DESCUENTO" numeric(10,2),
    "DESCUENTO" numeric(10,2) DEFAULT 0,
    "BASE_IMPONIBLE" numeric(10,2),
    "IMPUESTOS" numeric(10,2),
    "TOTAL" numeric(10,2),
    "OID_EXPEDIENTE" bigint DEFAULT 0,
    "OID_ALMACEN" bigint DEFAULT 0,
	CONSTRAINT "PK_STOrder" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STOrder" OWNER TO moladmin;
GRANT ALL ON TABLE "STOrder" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STOrderLine" CASCADE;
CREATE TABLE "STOrderLine" 
( 
	"OID" bigserial  NOT NULL,
	"OID_PEDIDO" bigint NOT NULL,
    "OID_PRODUCTO" bigint NOT NULL,
    "OID_PARTIDA" bigint DEFAULT 0,
    "OID_EXPEDIENTE" bigint DEFAULT 0,
    "OID_KIT" bigint DEFAULT 0,
    "FACTURACION_BULTOS" boolean DEFAULT false,
    "P_IMPUESTOS" numeric(10,2) DEFAULT 0,
    "P_DESCUENTO" numeric(10,2) DEFAULT 0,
    "GASTOS" numeric(10,2) DEFAULT 0,
    "ESTADO" bigint,
    "CONCEPTO" character varying(255),
    "CANTIDAD" numeric(10,2) DEFAULT 0 NOT NULL,
    "CANTIDAD_BULTOS" numeric(10,4),
    "PRECIO" numeric(10,5),
    "SUBTOTAL" numeric(10,2),
    "TOTAL" numeric(10,2),
    "OBSERVACIONES" text,
    "OID_ALMACEN" bigint DEFAULT 0,
    "OID_IMPUESTO" bigint DEFAULT 0,
    "CODIGO_PRODUCTO_PROVEEDOR" character varying(255),
	CONSTRAINT "PK_STOrderLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STOrderLine" OWNER TO moladmin;
GRANT ALL ON TABLE "STOrderLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STPayroll" CASCADE;
CREATE TABLE "STPayroll" 
( 
	"OID" bigserial NOT NULL,
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
	CONSTRAINT "PK_STPayroll" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STPayroll" OWNER TO moladmin;
GRANT ALL ON TABLE "STPayroll" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STPayment" CASCADE;
CREATE TABLE "STPayment"
(
	"OID" bigserial,
	"OID_USUARIO" integer DEFAULT 0,
	"OID_ROOT" int8 DEFAULT 0,
	"OID_LINK" bigint DEFAULT 0
	"OID_AGENTE" bigint,
    "TIPO_AGENTE" bigint NOT NULL,
    "ID_PAGO" bigint,
    "FECHA" timestamp without time zone,
    "IMPORTE" numeric(10,2) DEFAULT 0,
    "MEDIO_PAGO" bigint DEFAULT 1,
    "VENCIMIENTO" date,
    "OBSERVACIONES" text,
    "OID_CUENTA_BANCARIA" bigint DEFAULT 0,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "OID_TARJETA_CREDITO" bigint,
    "GASTOS_BANCARIOS" numeric(10,2) DEFAULT 0,
    "ESTADO" bigint DEFAULT 1,
    "ID_MOV_CONTABLE" character varying(255),
    "TIPO" bigint DEFAULT 1,
	"ESTADO_PAGO" bigint DEFAULT 7,
	CONSTRAINT "PK_STPayment" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STPayment" OWNER TO moladmin;
GRANT ALL ON TABLE "STPayment" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STShippingCompany" CASCADE;
CREATE TABLE "STShippingCompany" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255) NOT NULL,
    "ID" character varying(50),
    "TIPO_ID" bigint DEFAULT 0 NOT NULL,
    "NOMBRE" character varying(255),
    "ALIAS" character varying(255),
    "DIRECCION" character varying(255),
    "LOCALIDAD" character varying(255),
    "MUNICIPIO" character varying(255),
    "COD_POSTAL" character varying(255),
    "PROVINCIA" character varying(255),
    "PAIS" character varying(255),
    "TELEFONO" character varying(255),
    "CONTACTO" character varying(255),
    "EMAIL" character varying(255),
    "MEDIO_PAGO" bigint DEFAULT 1,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint,
    "CUENTA_BANCARIA" character varying(255),
	"SWIFT" character varying(255),
    "CUENTA_ASOCIADA" character varying(255),
    "OBSERVACIONES" text,
    "OID_CUENTA_BANCARIA_ASOCIADA" bigint DEFAULT 0,
    "CUENTA_CONTABLE" text,
    "OID_IMPUESTO" bigint,
    "TIPO" bigint DEFAULT 2,
    "ESTADO" integer DEFAULT 10,
    "OID_TARJETA_ASOCIADA" bigint DEFAULT 0,
	"P_IRPF" numeric(10,2) DEFAULT 0,
	CONSTRAINT "PK_STShippingCompany" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STShippingCompany" OWNER TO moladmin;
GRANT ALL ON TABLE "STShippingCompany" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STStoreLine" CASCADE;
CREATE TABLE "STStoreLine" 
( 
	"OID" bigserial  NOT NULL,
	"OID_ALMACEN" bigint NOT NULL,
    "CONCEPTO" character varying(255),
    "FECHA" date,
    "CANTIDAD" numeric(10,2) DEFAULT 0 NOT NULL,
    "OBSERVACIONES" text,
    "REFERENCIA" character varying(255),
	CONSTRAINT "PK_STStoreLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STStoreLine" OWNER TO moladmin;
GRANT ALL ON TABLE "STStoreLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STTransactionPayment" CASCADE;
CREATE TABLE "STTransactionPayment"
(
	"OID" bigserial,
	"OID_PAGO" bigint NOT NULL,
    "OID_OPERACION" bigint,
    "OID_EXPEDIENTE" bigint NOT NULL,
    "TIPO_AGENTE" bigint NOT NULL,
    "CANTIDAD" numeric(10,2),
    "TIPO_PAGO" bigint DEFAULT 1,
	CONSTRAINT "PK_STTransactionPayment" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STTransactionPayment" OWNER TO moladmin;
GRANT ALL ON TABLE "STTransactionPayment" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STOriginPrice" CASCADE;
CREATE TABLE "STOriginPrice" 
( 
	"OID" bigserial NOT NULL,
	"OID_TRANSPORTISTA" bigint,
    "OID_PROVEEDOR" bigint,
    "PROVEEDOR" character varying(255),
    "PUERTO" character varying(255),
    "PRECIO" numeric(10,2),
	CONSTRAINT "PK_STOriginPrice" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STOriginPrice" OWNER TO moladmin;
GRANT ALL ON TABLE "STOriginPrice" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STRoutePrice" CASCADE;
CREATE TABLE "STRoutePrice" ( 
	"OID" bigserial NOT NULL,
	"PUERTO_DESTINO" character varying(255),
    "PUERTO_ORIGEN" character varying(255),
    "PRECIO" numeric(10,2),
    "OID_NAVIERA" bigint,
	CONSTRAINT "PK_STRoutePrice" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STRoutePrice" OWNER TO moladmin;
GRANT ALL ON TABLE "STRoutePrice" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "STSupplier_Product" CASCADE;
CREATE TABLE "STSupplier_Product" 
( 
	"OID" bigserial NOT NULL,
	"OID_ACREEDOR" bigint,
    "OID_PRODUCTO" bigint,
    "PRECIO" numeric(10,5),
    "TIPO_ACREEDOR" bigint DEFAULT 0,
    "FACTURACION_BULTO" boolean DEFAULT false,
    "OID_IMPUESTO" bigint DEFAULT 0,
    "CODIGO_PRODUCTO_ACREEDOR" character varying(255),
    "P_DESCUENTO" numeric(10,2) DEFAULT 0,
    "TIPO_DESCUENTO" bigint DEFAULT 1,
    "AUTOMATICO" boolean DEFAULT false,
	CONSTRAINT "PK_STSupplier_Product" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STSupplier_Product" OWNER TO moladmin;
GRANT ALL ON TABLE "STSupplier_Product" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STPayrollBatch" CASCADE;
CREATE TABLE "STPayrollBatch" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" bigint,
    "CODIGO" character varying(255),
    "FECHA" timestamp without time zone,
    "DESCRIPCION" text,
    "TOTAL" numeric(10,2),
    "IRPF" numeric(10,2),
    "SEGURO_EMPRESA" numeric(10,2),
    "SEGURO_PERSONAL" numeric(10,2) DEFAULT 0,
    "PREVISION_PAGO" timestamp without time zone,
    "ESTADO" bigint DEFAULT 1,
    "OBSERVACIONES" text,
    "BASE_IRPF" numeric(10,2) DEFAULT 0,
    "DESCUENTOS" numeric(10,2) DEFAULT 0,	
	CONSTRAINT "PK_STPayrollBatch" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STPayrollBatch" OWNER TO moladmin;
GRANT ALL ON TABLE "STPayrollBatch" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STSerie" CASCADE;
CREATE TABLE "STSerie" 
( 
	"OID" bigserial NOT NULL,
	"NOMBRE" character varying(255),
    "IDENTIFICADOR" character varying(255),
    "TIPO_SERIE" integer DEFAULT 0,
    "CABECERA" character varying(512),
    "RESUMEN" boolean,
    "OBSERVACIONES" text,
    "OID_IMPUESTO" bigint,
    "TIPO" bigint DEFAULT 0,
	CONSTRAINT "PK_STSerie" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STSerie" OWNER TO moladmin;
GRANT ALL ON TABLE "STSerie" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "STProduct" CASCADE;
CREATE TABLE "STProduct" 
( 
	"OID" bigserial NOT NULL,
	"OID_AYUDA" bigint,
    "OID_FAMILIA" bigint,
    "SERIAL" bigint DEFAULT 0,
    "CODIGO" character varying(255) NOT NULL,
    "BULTO" boolean,
    "NOMBRE" character varying(255),
    "DESCRIPCION" character varying(255),
    "PRECIO_COMPRA" numeric(10,5),
    "PRECIO_VENTA" numeric(10,5),
    "AYUDA_KILO" numeric(10,5),
    "CODIGO_ADUANERO" character varying(255),
    "OBSERVACIONES" text,
    "OID_IMPUESTO_COMPRA" bigint,
    "OID_IMPUESTO_VENTA" bigint,
    "CUENTA_CONTABLE_COMPRA" character varying(255),
    "CUENTA_CONTABLE_VENTA" character varying(255),
    "UNITARIO" boolean DEFAULT false,
    "ESTADO" bigint DEFAULT 10,
    "KILOS_BULTO" numeric(10,2) DEFAULT 1,
    "STOCK_MINIMO" numeric(10,2) DEFAULT 0,
    "TIPO_VENTA" bigint DEFAULT 10,
    "AVISAR_STOCK" boolean DEFAULT true,
    "BENEFICIO_CERO" boolean DEFAULT false,
    "AVISAR_BENEFICIO_MINIMO" boolean DEFAULT false,
    "P_BENEFICIO_MINIMO" numeric(10,2) DEFAULT 0,
	"IS_KIT" boolean DEFAULT FALSE,
	"NO_STOCK_SALE" boolean DEFAULT FALSE,
	"EXTERNAL_CODE" character varying(255),
	CONSTRAINT "PK_STProduct" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STProduct" OWNER TO moladmin;
GRANT ALL ON TABLE "STProduct" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STStock" CASCADE;
CREATE TABLE "STStock" 
( 
	"OID" bigserial NOT NULL,
    "OID_EXPEDIENTE" bigint,
    "OID_PRODUCTO" bigint,
    "CONCEPTO" character varying(255),
    "BULTOS" numeric(10,4),
    "KILOS" numeric(10,2),
    "FECHA" timestamp without time zone,
    "OBSERVACIONES" text,
    "OID_CONCEPTO_ALBARAN" bigint DEFAULT 0,
	"OID_BATCH" bigint,
    "OID_ALBARAN" bigint DEFAULT 0,
    "OID_KIT" bigint DEFAULT 0 NOT NULL,
    "OID_STOCK" bigint DEFAULT 0 NOT NULL,
    "TIPO" bigint DEFAULT 1,
    "OID_LINEA_PEDIDO" bigint DEFAULT 0,
    "INICIAL" boolean DEFAULT false,
    "OID_ALMACEN" bigint DEFAULT 0,
    "OID_USUARIO" integer DEFAULT 0,
	"OID_ENLACE" bigint DEFAULT 0,
	CONSTRAINT "PK_Stock" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "STStock" OWNER TO moladmin;
GRANT ALL ON TABLE "STStock" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STStore" CASCADE;
CREATE TABLE "STStore" 
( 
	"OID" bigserial  NOT NULL,
	"NOMBRE" character varying(255),
    "UBICACION" character varying(255),
    "OBSERVACIONES" text,
    "SERIAL" bigint,
    "CODIGO" character varying(255),
    "ESTADO" bigint DEFAULT 10,
	CONSTRAINT "PK_Almacen" PRIMARY KEY ("OID")
) WITHOUT OIDS;
            
ALTER TABLE "STStore" OWNER TO moladmin;
GRANT ALL ON TABLE "STStore" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STSupplier" CASCADE;
CREATE TABLE "STSupplier" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255) NOT NULL,
    "ID" character varying(50),
    "TIPO_ID" bigint DEFAULT 0 NOT NULL,
    "NOMBRE" character varying(255),
    "ALIAS" character varying(255),
    "COD_POSTAL" character varying(255),
    "LOCALIDAD" character varying(255),
    "MUNICIPIO" character varying(255),
    "PROVINCIA" character varying(255),
    "TELEFONO" character varying(255),
    "PAIS" character varying(255),
    "MEDIO_PAGO" bigint DEFAULT 1,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint,
    "CUENTA_BANCARIA" character varying(255),
	"SWIFT" character varying(255),
    "CUENTA_ASOCIADA" character varying(255),
    "CONTACTO" character varying(255),
    "EMAIL" character varying(255),
    "DIRECCION" character varying(255),
    "OBSERVACIONES" text,
    "OID_CUENTA_BANCARIA_ASOCIADA" bigint DEFAULT 0,
    "CUENTA_CONTABLE" text,
    "OID_IMPUESTO" bigint,
    "TIPO" bigint DEFAULT 1,
    "ESTADO" bigint DEFAULT 10,
    "OID_TARJETA_ASOCIADA" bigint DEFAULT 0,
	"P_IRPF" numeric(10,2) DEFAULT 0,
	CONSTRAINT "PK_STSupplier" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STSupplier" OWNER TO moladmin;
GRANT ALL ON TABLE "STSupplier" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STTool" CASCADE;
CREATE TABLE "STTool" 
( 
	"OID" bigserial NOT NULL,
    "SERIAL" bigint,
    "CODE" character varying(255),
    "STATUS" bigint DEFAULT 10,
	"NAME" character varying(255),	
	"DESCRIPTION" text,	
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
	"COST" numeric(10,2),
	"LOCATION" text,	
    "COMMENTS" text,	
	CONSTRAINT "PK_STTool" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STTool" OWNER TO moladmin;
GRANT ALL ON TABLE "STTool" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STTransporter" CASCADE;
CREATE TABLE "STTransporter" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255) NOT NULL,
    "ID" character varying(50),
    "TIPO_ID" bigint DEFAULT 0 NOT NULL,
    "NOMBRE" character varying(255),
    "ALIAS" character varying(255),
    "MEDIO_PAGO" bigint DEFAULT 1,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint,
    "CUENTA_BANCARIA" character varying(255),
	"SWIFT" character varying(255),
    "CUENTA_ASOCIADA" character varying(255),
    "CONTACTO" character varying(255),
    "EMAIL" character varying(255),
    "DIRECCION" character varying(255),
    "TELEFONO" character varying(255),
    "LOCALIDAD" character varying(255),
    "COD_POSTAL" character varying(255),
    "PROVINCIA" character varying(255),
    "MUNICIPIO" character varying(255),
    "PAIS" character varying(255),
    "OBSERVACIONES" text,
    "OID_CUENTA_BANCARIA_ASOCIADA" bigint DEFAULT 0,
    "CUENTA_CONTABLE" text,
    "OID_IMPUESTO" bigint,
    "TIPO_TRANSPORTISTA" bigint DEFAULT 0,
    "TIPO" bigint DEFAULT 3,
    "ESTADO" integer DEFAULT 10,
    "OID_TARJETA_ASOCIADA" bigint DEFAULT 0,
	"P_IRPF" numeric(10,2) DEFAULT 0,
	CONSTRAINT "PK_STTransporter" PRIMARY KEY ("OID")	
) WITHOUT OIDS;

ALTER TABLE "STTransporter" OWNER TO moladmin;
GRANT ALL ON TABLE "STTransporter" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STWorkCrew" CASCADE;
CREATE TABLE "STWorkCrew" 
( 
	"OID" bigserial NOT NULL,
    "SERIAL" bigint,
    "CODE" character varying(255),
    "STATUS" bigint DEFAULT 10,
	"NAME" character varying(255),	
	"DESCRIPTION" text,	
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkCrew" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkCrew" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkCrew" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STWorkReport" CASCADE;
CREATE TABLE "STWorkReport" 
( 
	"OID" bigserial NOT NULL,
	"OID_OWNER" bigint DEFAULT 1,
	"OID_EXPEDIENT" bigint DEFAULT 1,
    "SERIAL" bigint,
    "CODE" character varying(255),
    "STATUS" bigint DEFAULT 10,
    "DATE" timestamp without time zone,
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
	"HOURS" numeric(10,2) DEFAULT 0
	"TOTAL" numeric(10,2),
	"CATEGORY" bigint DEFAULT 0,
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkReport" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkReport" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkReport" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STWorkReportCategory" CASCADE;
CREATE TABLE "STWorkReportCategory" 
( 
	"OID" bigserial NOT NULL,
    "NAME" text,
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkReportCategory" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkReportCategory" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkReportCategory" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "STWorkReportResource" CASCADE;
CREATE TABLE "STWorkReportResource" 
( 
	"OID" bigserial NOT NULL,
	"OID_WORK_REPORT" bigint DEFAULT 0,
	"OID_RESOURCE" bigint DEFAULT 0,
	"ENTITY_TYPE" bigint DEFAULT 1,
	"AMOUNT" numeric(10,2),
	"COST" numeric(10,2),
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
	"HOURS" numeric(10,2) DEFAULT 0,
	"EXTRA_COST" numeric(10,2) DEFAULT 0,
	"TOTAL" numeric(10, 2) DEFAULT 0,
    "COMMENTS" text,	
	CONSTRAINT "PK_STWorkReportResource" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "STWorkReportResource" OWNER TO moladmin;
GRANT ALL ON TABLE "STWorkReportResource" TO GROUP "MOLEQULE_ADMINISTRATOR";

-- UNIQUE KEYS

ALTER TABLE ONLY "STCustomAgent"
    ADD CONSTRAINT "STCustomAgent_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STCustomAgent"
    ADD CONSTRAINT "STCustomAgent_SERIAL_key" UNIQUE ("SERIAL");

ALTER TABLE ONLY "STDelivery_Invoice"
    ADD CONSTRAINT "UQ_STDelivery_Invoice_OID_ALBARAN_OID_FACTURA" UNIQUE ("OID_ALBARAN", "OID_FACTURA");
	
ALTER TABLE ONLY "ExpedienteREA"
    ADD CONSTRAINT "ExpedienteREA_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STExpedient"
    ADD CONSTRAINT "STExpedient_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STFamily"
    ADD CONSTRAINT "STFamily_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STFamily"
    ADD CONSTRAINT "STFamily_NAME_key" UNIQUE ("NOMBRE");

ALTER TABLE ONLY "Maquinaria"
    ADD CONSTRAINT "Maquinaria_IDENTIFIER_key" UNIQUE ("IDENTIFICADOR");

ALTER TABLE ONLY "STProduct"
    ADD CONSTRAINT "STProduct_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STSerie"
    ADD CONSTRAINT "STSerie_IDENTIFIER_key" UNIQUE ("IDENTIFICADOR");

ALTER TABLE ONLY "STShippingCompany"
    ADD CONSTRAINT "Naviera_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STShippingCompany"
    ADD CONSTRAINT "STShippingCompany_SERIAL_key" UNIQUE ("SERIAL");

ALTER TABLE ONLY "STSupplier"
    ADD CONSTRAINT "STSupplier_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STSupplier"
    ADD CONSTRAINT "STSupplier_SERIAL_key" UNIQUE ("SERIAL");

ALTER TABLE ONLY "STTransporter"
    ADD CONSTRAINT "STTransporter_CODE_key" UNIQUE ("CODIGO");

ALTER TABLE ONLY "STTransporter"
    ADD CONSTRAINT "STTransporter_SERIAL_key" UNIQUE ("SERIAL");
	
-- FOREIGN KEYS	

ALTER TABLE ONLY "STBatch"
    ADD CONSTRAINT "FK_STBatch_STProduct" FOREIGN KEY ("OID_PRODUCTO") REFERENCES "STProduct"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STBatch"
    ADD CONSTRAINT "FK_STBatch_STStore" FOREIGN KEY ("OID_ALMACEN") REFERENCES "STStore"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
	
ALTER TABLE ONLY "STCustomAgent_Port"
    ADD CONSTRAINT "FK_STCustomAgent_Port_STCustomAgent" FOREIGN KEY ("OID_DESPACHANTE") REFERENCES "STCustomAgent"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STCustomAgent_Port"
    ADD CONSTRAINT "FK_STCustomAgent_Port_STPort" FOREIGN KEY ("OID_PUERTO") REFERENCES "COMMON"."PUERTO"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STDelivery"
    ADD CONSTRAINT "FK_STDelivery_STSerie" FOREIGN KEY ("OID_SERIE") REFERENCES "STSerie"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STDelivery_Invoice"
    ADD CONSTRAINT "FK_STDelivery_Invoice_STDelivery" FOREIGN KEY ("OID_ALBARAN") REFERENCES "STDelivery"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STDelivery_Invoice"
    ADD CONSTRAINT "FK_STDelivery_Invoice_STInvoice" FOREIGN KEY ("OID_FACTURA") REFERENCES "STInvoice"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STDeliveryLine"
    ADD CONSTRAINT "FK_STDeliveryLine_STDelivery" FOREIGN KEY ("OID_ALBARAN") REFERENCES "STDelivery"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
	
ALTER TABLE ONLY "STDestinationPrice"
    ADD CONSTRAINT "FK_STDestinationPrice_STTransporter" FOREIGN KEY ("OID_TRANSPORTISTA") REFERENCES "STTransporter"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "ExpedienteREA"
    ADD CONSTRAINT "FK_ExpedienteREA_STExpedient" FOREIGN KEY ("OID_EXPEDIENTE") REFERENCES "STExpedient"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
	
ALTER TABLE ONLY "STFamilySerie"
    ADD CONSTRAINT "FK_STFamilySerie_STFamily" FOREIGN KEY ("OID_FAMILIA") REFERENCES "STFamily"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STFamilySerie"
    ADD CONSTRAINT "FK_STFamilySerie_STSerie" FOREIGN KEY ("OID_SERIE") REFERENCES "STSerie"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STInvoiceLine"
    ADD CONSTRAINT "FK_STInvoiceLine_STDeliveryLine" FOREIGN KEY ("OID_CONCEPTO_ALBARAN") REFERENCES "STDeliveryLine"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STInvoiceLine"
    ADD CONSTRAINT "FK_STInvoiceLine_STInvoice" FOREIGN KEY ("OID_FACTURA") REFERENCES "STInvoice"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
	
ALTER TABLE ONLY "STKit"
    ADD CONSTRAINT "FK_STKit_STParent" FOREIGN KEY ("OID_PARENT") REFERENCES "STProduct"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
	
ALTER TABLE ONLY "STKit"
    ADD CONSTRAINT "FK_STKit_STProduct" FOREIGN KEY ("OID_PRODUCT") REFERENCES "STProduct"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
	
ALTER TABLE ONLY "LineaInventario"
    ADD CONSTRAINT "FK_LineaInventario_InventarioAlmacen" FOREIGN KEY ("OID_INVENTARIO") REFERENCES "InventarioAlmacen"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "LineaFomento"
    ADD CONSTRAINT "FK_LineaFomento_STExpedient" FOREIGN KEY ("OID_EXPEDIENTE") REFERENCES "STExpedient"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "LineaFomento"
    ADD CONSTRAINT "FK_LineaFomento_STBatch" FOREIGN KEY ("OID_PARTIDA") REFERENCES "STBatch"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STLivestockBookLine"
    ADD CONSTRAINT "FK_STLivestockBookLine_STLivestockBook" FOREIGN KEY ("OID_LIBRO") REFERENCES "STLivestockBook"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "Maquinaria"
    ADD CONSTRAINT "FK_Maquinaria_STExpedient" FOREIGN KEY ("OID_EXPEDIENTE") REFERENCES "STExpedient"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "Maquinaria"
    ADD CONSTRAINT "FK_Maquinaria_STBatch" FOREIGN KEY ("OID_BATCH") REFERENCES "STBatch"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
	
ALTER TABLE ONLY "STOrderLine"
    ADD CONSTRAINT "FK_STOrderLine_STOrder" FOREIGN KEY ("OID_PEDIDO") REFERENCES "STOrder"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
	
ALTER TABLE ONLY "STOriginPrice"
    ADD CONSTRAINT "FK_STOriginPrice_STTransporter" FOREIGN KEY ("OID_TRANSPORTISTA") REFERENCES "STTransporter"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STPayroll"
    ADD CONSTRAINT "FK_STPayroll_STEmployee" FOREIGN KEY ("OID_EMPLEADO") REFERENCES "STEmployee"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STPayroll"
    ADD CONSTRAINT "FK_STPayroll_STPayrollBatch" FOREIGN KEY ("OID_REMESA") REFERENCES "STPayrollBatch"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STRoutePrice"
    ADD CONSTRAINT "FK_STRoutePrice_STShippingCompany" FOREIGN KEY ("OID_NAVIERA") REFERENCES "STShippingCompany"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STSupplier_Product"
    ADD CONSTRAINT "FK_STSupplier_Product_STProduct" FOREIGN KEY ("OID_PRODUCTO") REFERENCES "STProduct"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STStock"
    ADD CONSTRAINT "FK_STStock_STStore" FOREIGN KEY ("OID_ALMACEN") REFERENCES "STStore"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STStock"
    ADD CONSTRAINT "FK_STStock_STProduct" FOREIGN KEY ("OID_PRODUCTO") REFERENCES "STProduct"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STStock"
    ADD CONSTRAINT "FK_STStock_STBatch" FOREIGN KEY ("OID_BATCH") REFERENCES "STBatch"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STTransactionPayment"
    ADD CONSTRAINT "FK_STTransactionPayment_STPayment" FOREIGN KEY ("OID_PAGO") REFERENCES "STPayment"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "STWorkReport"
    ADD CONSTRAINT "FK_STWorkReport_User" FOREIGN KEY ("OID_OWNER") REFERENCES "COMMON"."User"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "STWorkReport"
    ADD CONSTRAINT "FK_STWorkReport_STExpedient" FOREIGN KEY ("OID_EXPEDIENT") REFERENCES "STExpedient"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;	
	
ALTER TABLE ONLY "STWorkReportResource"
    ADD CONSTRAINT "FK_STWorkReportResource_STWorkReport" FOREIGN KEY ("OID_WORK_REPORT") REFERENCES "STWorkReport"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;