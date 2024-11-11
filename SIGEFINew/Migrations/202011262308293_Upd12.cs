namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AF_ACTAENT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ACTA = c.Int(nullable: false),
                        FECHA_ACTA = c.DateTime(nullable: false),
                        CUSTODIO = c.String(nullable: false, maxLength: 13),
                        ENTREGADO = c.String(nullable: false, maxLength: 13),
                        AUTORIZADO = c.String(nullable: false, maxLength: 13),
                        NUM_PETICION = c.String(nullable: false, maxLength: 20),
                        RESP_PETICION = c.String(nullable: false, maxLength: 13),
                        DETALLE = c.String(nullable: false, maxLength: 4000),
                        IMPRESA = c.Boolean(nullable: false),
                        ANULADA = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ACTA })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_DETACTAENTREGA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ACTA = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        NUM_ORDINAL = c.Int(nullable: false),
                        CODIGO = c.String(nullable: false, maxLength: 50),
                        APLICADO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ACTA, t.NUM_FILA })
                .ForeignKey("dbo.AF_ACTAENT", t => new { t.ID_INSTITUCION, t.NUM_ACTA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.NUM_ACTA });
            
            CreateTable(
                "dbo.AF_CUENTASCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        TIPOAF = c.Int(nullable: false),
                        CODCG_ACTIVO = c.String(maxLength: 50),
                        CODCG_CXPAGAR = c.String(maxLength: 50),
                        CODCG_DEPREACUM = c.String(maxLength: 50),
                        CODCG_DEPREGTO = c.String(maxLength: 50),
                        CODCG_COSTOS = c.String(maxLength: 50),
                        CODCG_ACTIVOPR = c.String(maxLength: 50),
                        CODCG_CXPAGARPR = c.String(maxLength: 50),
                        CODCG_DEPREACUMPR = c.String(maxLength: 50),
                        CODCG_DEPREGTOPR = c.String(maxLength: 50),
                        CODCG_COSTOSPR = c.String(maxLength: 50),
                        CODCG_ACTIVOIO = c.String(maxLength: 50),
                        CODCG_CXPAGARIO = c.String(maxLength: 50),
                        CODCG_DEPREACUMIO = c.String(maxLength: 50),
                        CODCG_DEPREGTOIO = c.String(maxLength: 50),
                        CODCG_ACTIVOIP = c.String(maxLength: 50),
                        CODCG_CXPAGARIP = c.String(maxLength: 50),
                        CODCG_DEPREACUMIP = c.String(maxLength: 50),
                        CODCG_DEPREGTOIP = c.String(maxLength: 50),
                        CODCG_CUSTDEUDORA = c.String(maxLength: 50),
                        CODCG_CUSTACREED = c.String(maxLength: 50),
                        CODCG_CMDATODEUDORA = c.String(maxLength: 50),
                        CODCG_CMDATOACREED = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_SUBTIPO, t.NUM_FILA, t.TIPOAF })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_DEVOLUCIONES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_TRANSACCION = c.Int(nullable: false),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_TRAN = c.String(nullable: false, maxLength: 4),
                        DETALLE = c.String(nullable: false, maxLength: 500),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        NUM_DOC = c.String(nullable: false, maxLength: 20),
                        FECHA_DOC = c.DateTime(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        ID_CUSTODIOENT = c.String(nullable: false, maxLength: 13),
                        ID_CUSTODIOREC = c.String(nullable: false, maxLength: 13),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_DETDEVOLUCION",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_TRANSACCION = c.Int(nullable: false),
                        SECUENCIAL = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        NUM_ORDINAL = c.Int(nullable: false),
                        CODIGO = c.String(nullable: false, maxLength: 50),
                        ASIGNADO = c.Boolean(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION, t.SECUENCIAL })
                .ForeignKey("dbo.AF_DEVOLUCIONES", t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION });
            
            CreateTable(
                "dbo.AF_MOTIVOSBAJA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        CODIGO = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                        ENVIADO = c.Int(nullable: false),
                        TIPO = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.CODIGO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_PARAMETROS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        TC_TIPO_BIEN = c.Int(nullable: false),
                        TC_STIPO_BIEN = c.Int(nullable: false),
                        TC_CLASE = c.Int(nullable: false),
                        TC_ORDINAL = c.Int(nullable: false),
                        TC_ESPECIFIC = c.Int(nullable: false),
                        NUM_ACTAENTREGA = c.Int(nullable: false),
                        NUM_TRASPASO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_INGRESO = c.Int(nullable: false),
                        PERIODO = c.Int(nullable: false),
                        CMIN_ADQUI = c.Int(nullable: false),
                        PORC_RESIDUAL = c.Int(nullable: false),
                        PRINTER_ZEBRA = c.String(maxLength: 100),
                        ANIODEP = c.Int(nullable: false),
                        MESDEP = c.Int(nullable: false),
                        CTA_ACTIVO = c.String(maxLength: 50),
                        CTA_PPAGAR = c.String(maxLength: 50),
                        CTA_DEPACU = c.String(maxLength: 50),
                        CTA_DEPGASTO = c.String(maxLength: 50),
                        CTA_COSTOS = c.String(maxLength: 50),
                        CTA_DEUDORA = c.String(maxLength: 50),
                        CTA_ACREEDORA = c.String(maxLength: 50),
                        CTA_PATRIMONIO = c.String(maxLength: 50),
                        CTA_DONAINGRE = c.String(maxLength: 50),
                        CTA_DONAEGRE = c.String(maxLength: 50),
                        CTA_DISMIPAT = c.String(maxLength: 50),
                        CTA_IVACOMPRAS = c.String(maxLength: 50),
                        PORC_IVA = c.Int(nullable: false),
                        NUM_EGRESO = c.Int(nullable: false),
                        NUM_DEVOLUCION = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_FILA })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_TABLADEPRE",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        ID_ACTIVIDAD = c.Int(nullable: false),
                        VIDA_UTIL = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO, t.ID_CLASE, t.ID_ACTIVIDAD })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_TIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        TIPOBIEN = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        ID_ESPECIFIC = c.Int(nullable: false),
                        MEF = c.Int(nullable: false),
                        ID_ESPECIFIC2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPO })
                .ForeignKey("dbo.AF_ESPECIFICACIONES", t => t.ID_ESPECIFIC, cascadeDelete: true)
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION)
                .Index(t => t.ID_ESPECIFIC);
            
            CreateTable(
                "dbo.AF_ACTIVOSFIJOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        TIPOBIEN = c.Int(nullable: false),
                        NUM_ORDINAL = c.Int(nullable: false),
                        ID_ACTIVIDAD = c.Int(nullable: false),
                        CODIGO = c.String(nullable: false, maxLength: 50),
                        NUM_INGRESO = c.Int(nullable: false),
                        FECHA_INGRESO = c.DateTime(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 1000),
                        ID_GRUPORELACION = c.Int(nullable: false),
                        NUM_SERIE = c.String(maxLength: 150),
                        DIMENSION = c.String(maxLength: 150),
                        ID_MARCA = c.Int(nullable: false),
                        ID_MODELO = c.Int(nullable: false),
                        COLOR = c.String(maxLength: 15),
                        FECHA_ADQ = c.DateTime(nullable: false),
                        VIDA_UTIL = c.Int(nullable: false),
                        PORC_DEPRE = c.Single(nullable: false),
                        CANTIDAD = c.Int(nullable: false),
                        COSTO_HISTORICO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DEPRECIACION = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_ESTCONSERVA = c.Int(nullable: false),
                        ID_ESTRUCTURA = c.Int(nullable: false),
                        ID_FONDO = c.Int(nullable: false),
                        ID_UNIDOPERATIVA = c.Int(nullable: false),
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        ORIGENCOMPRA = c.Int(nullable: false),
                        CONDICION = c.Int(nullable: false),
                        OBSERVACION = c.String(maxLength: 1000),
                        BAJA = c.Int(nullable: false),
                        DOC_BAJA = c.String(maxLength: 15),
                        FECHA_DOC_BAJA = c.DateTime(),
                        AUTORIZA_BAJA = c.String(maxLength: 20),
                        MOTIVO_BAJA = c.Int(nullable: false),
                        FECHA_BAJA = c.DateTime(),
                        DETALLE_BAJA = c.String(maxLength: 1000),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_DOCBAJA = c.String(maxLength: 20),
                        CONTABILIZADO = c.Int(nullable: false),
                        CODIGO_TT = c.Int(nullable: false),
                        PERIODO = c.Int(nullable: false),
                        FECHA_APLICACION = c.DateTime(nullable: false),
                        SUB_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DEP_ACUMULA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DEP_MENSUAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SALDO_X_DEP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_ULT_DEP = c.DateTime(),
                        VALOR_LIBROS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_RESIDUAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TIPO_TRANSAC = c.String(),
                        TIPO_ACTIVO = c.Int(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        FOTO = c.String(maxLength: 255),
                        CANTIDADORI = c.Int(nullable: false),
                        NOM_OBRA = c.String(maxLength: 100),
                        AUTOR_OBRA = c.String(maxLength: 100),
                        TIPO_OBRA = c.String(maxLength: 100),
                        DIMENSION_OBRA = c.String(maxLength: 100),
                        SUPERFICIE = c.String(maxLength: 50),
                        AREA_CONST = c.String(maxLength: 50),
                        INFRAESTRU = c.String(maxLength: 50),
                        VALORCOMER = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LINDEROS = c.String(maxLength: 300),
                        FECHAREGPR = c.DateTime(),
                        CLAVECATAS = c.String(maxLength: 50),
                        NUM_CHASIS = c.String(maxLength: 50),
                        NUM_MOTOR = c.String(maxLength: 50),
                        NUM_PLACA = c.String(maxLength: 50),
                        NUM_ASIGNADO = c.String(maxLength: 50),
                        ANIO_VEHIC = c.String(maxLength: 50),
                        MODELO_VEHIC = c.String(maxLength: 100),
                        NUM_EGRESO = c.Int(nullable: false),
                        NUM_DEVOLUCION = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO, t.ID_CLASE, t.TIPOBIEN, t.NUM_ORDINAL, t.ID_ACTIVIDAD })
                .ForeignKey("dbo.AF_ACTIVIDADES", t => t.ID_ACTIVIDAD, cascadeDelete: false)
                .ForeignKey("dbo.AF_ESTADOCONSERVA", t => t.ID_ESTCONSERVA, cascadeDelete: false)
                .ForeignKey("dbo.AF_ESTRUCTURABIEN", t => t.ID_ESTRUCTURA, cascadeDelete: false)
                .ForeignKey("dbo.AF_MARCAS", t => t.ID_MARCA, cascadeDelete: false)
                .ForeignKey("dbo.AF_MODELOS", t => new { t.ID_MARCA, t.ID_MODELO }, cascadeDelete: false)
                .ForeignKey("dbo.AF_ORIGENFONDO", t => t.ID_FONDO, cascadeDelete: false)
                .ForeignKey("dbo.AF_TIPOS", t => new { t.ID_INSTITUCION, t.ID_TIPO }, cascadeDelete: false)
                .ForeignKey("dbo.AF_TIPOSTRAN", t => new { t.ID_INSTITUCION, t.CODIGO_TT }, cascadeDelete: false)
                .ForeignKey("dbo.AF_CLASES", t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO, t.ID_CLASE }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPO })
                .Index(t => new { t.ID_INSTITUCION, t.CODIGO_TT })
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO, t.ID_CLASE })
                .Index(t => t.ID_ACTIVIDAD)
                .Index(t => t.ID_MARCA)
                .Index(t => new { t.ID_MARCA, t.ID_MODELO })
                .Index(t => t.ID_ESTCONSERVA)
                .Index(t => t.ID_ESTRUCTURA)
                .Index(t => t.ID_FONDO);
            
            CreateTable(
                "dbo.AF_ACTIVIDADES",
                c => new
                    {
                        ID_ACTIVIDAD = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_ACTIVIDAD);
            
            CreateTable(
                "dbo.AF_ESTADOCONSERVA",
                c => new
                    {
                        ID_ESTCONSERVA = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID_ESTCONSERVA);
            
            CreateTable(
                "dbo.AF_INFCOMPLEMEN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        CODIGO = c.String(nullable: false, maxLength: 50),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        NOMBRE = c.String(nullable: false, maxLength: 100),
                        DESCRIPCION = c.String(nullable: false, maxLength: 250),
                        MARCA = c.String(maxLength: 80),
                        MODELO = c.String(maxLength: 80),
                        NUM_SERIE = c.String(maxLength: 50),
                        CANTIDAD = c.Int(nullable: false),
                        P_UNIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        P_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_ESTCONSERVA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.CODIGO, t.NUM_FILA })
                .ForeignKey("dbo.AF_ESTADOCONSERVA", t => t.ID_ESTCONSERVA, cascadeDelete: true)
                .Index(t => t.ID_ESTCONSERVA);
            
            CreateTable(
                "dbo.AF_ESTRUCTURABIEN",
                c => new
                    {
                        ID_ESTRUCTURA = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_ESTRUCTURA);
            
            CreateTable(
                "dbo.AF_MARCAS",
                c => new
                    {
                        ID_MARCA = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_MARCA);
            
            CreateTable(
                "dbo.AF_MODELOS",
                c => new
                    {
                        ID_MARCA = c.Int(nullable: false),
                        ID_MODELO = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_MARCA, t.ID_MODELO })
                .ForeignKey("dbo.AF_MARCAS", t => t.ID_MARCA, cascadeDelete: true)
                .Index(t => t.ID_MARCA);
            
            CreateTable(
                "dbo.AF_ORIGENFONDO",
                c => new
                    {
                        ID_FONDO = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_FONDO);
            
            CreateTable(
                "dbo.AF_TIPOSTRAN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        CODIGO_TT = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                        TIPO = c.Int(nullable: false),
                        CONTABILIZA = c.Int(nullable: false),
                        CONIVA = c.Boolean(nullable: false),
                        ENVIADO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.CODIGO_TT })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_ESPECIFICACIONES",
                c => new
                    {
                        ID_ESPECIFIC = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID_ESPECIFIC);
            
            CreateTable(
                "dbo.AF_SUBTIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        TIPOBIEN = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        ID_ESPECIFIC = c.Int(nullable: false),
                        MEF = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO })
                .ForeignKey("dbo.AF_TIPOS", t => new { t.ID_INSTITUCION, t.ID_TIPO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPO });
            
            CreateTable(
                "dbo.AF_CLASES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        TIPOBIEN = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        DEPRECIABLE = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO, t.ID_CLASE })
                .ForeignKey("dbo.AF_SUBTIPOS", t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPO, t.ID_SUBTIPO });
            
            CreateTable(
                "dbo.AF_TIPOSMEF",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPOMEF = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOMEF })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_TRASPASOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_TRANSACCION = c.Int(nullable: false),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_TRAN = c.String(nullable: false, maxLength: 4),
                        DETALLE = c.String(nullable: false, maxLength: 500),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        NUM_DOC = c.String(nullable: false, maxLength: 20),
                        FECHA_DOC = c.DateTime(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        ID_CUSTODIOENT = c.String(nullable: false, maxLength: 13),
                        ID_CUSTODIOREC = c.String(nullable: false, maxLength: 13),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.AF_DETTRASPASO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_TRANSACCION = c.Int(nullable: false),
                        SECUENCIAL = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        NUM_ORDINAL = c.Int(nullable: false),
                        CODIGO = c.String(nullable: false, maxLength: 50),
                        ASIGNADO = c.Boolean(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION, t.SECUENCIAL })
                .ForeignKey("dbo.AF_TRASPASOS", t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.NUM_TRANSACCION });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AF_TRASPASOS", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_DETTRASPASO", new[] { "ID_INSTITUCION", "NUM_TRANSACCION" }, "dbo.AF_TRASPASOS");
            DropForeignKey("dbo.AF_TIPOSMEF", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_TIPOS", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_SUBTIPOS", new[] { "ID_INSTITUCION", "ID_TIPO" }, "dbo.AF_TIPOS");
            DropForeignKey("dbo.AF_CLASES", new[] { "ID_INSTITUCION", "ID_TIPO", "ID_SUBTIPO" }, "dbo.AF_SUBTIPOS");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", new[] { "ID_INSTITUCION", "ID_TIPO", "ID_SUBTIPO", "ID_CLASE" }, "dbo.AF_CLASES");
            DropForeignKey("dbo.AF_TIPOS", "ID_ESPECIFIC", "dbo.AF_ESPECIFICACIONES");
            DropForeignKey("dbo.AF_TIPOSTRAN", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", new[] { "ID_INSTITUCION", "CODIGO_TT" }, "dbo.AF_TIPOSTRAN");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", new[] { "ID_INSTITUCION", "ID_TIPO" }, "dbo.AF_TIPOS");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", "ID_FONDO", "dbo.AF_ORIGENFONDO");
            DropForeignKey("dbo.AF_MODELOS", "ID_MARCA", "dbo.AF_MARCAS");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", new[] { "ID_MARCA", "ID_MODELO" }, "dbo.AF_MODELOS");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", "ID_MARCA", "dbo.AF_MARCAS");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", "ID_ESTRUCTURA", "dbo.AF_ESTRUCTURABIEN");
            DropForeignKey("dbo.AF_INFCOMPLEMEN", "ID_ESTCONSERVA", "dbo.AF_ESTADOCONSERVA");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", "ID_ESTCONSERVA", "dbo.AF_ESTADOCONSERVA");
            DropForeignKey("dbo.AF_ACTIVOSFIJOS", "ID_ACTIVIDAD", "dbo.AF_ACTIVIDADES");
            DropForeignKey("dbo.AF_TABLADEPRE", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_PARAMETROS", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_MOTIVOSBAJA", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_DEVOLUCIONES", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_DETDEVOLUCION", new[] { "ID_INSTITUCION", "NUM_TRANSACCION" }, "dbo.AF_DEVOLUCIONES");
            DropForeignKey("dbo.AF_CUENTASCONT", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_ACTAENT", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.AF_DETACTAENTREGA", new[] { "ID_INSTITUCION", "NUM_ACTA" }, "dbo.AF_ACTAENT");
            DropIndex("dbo.AF_DETTRASPASO", new[] { "ID_INSTITUCION", "NUM_TRANSACCION" });
            DropIndex("dbo.AF_TRASPASOS", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_TIPOSMEF", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_CLASES", new[] { "ID_INSTITUCION", "ID_TIPO", "ID_SUBTIPO" });
            DropIndex("dbo.AF_SUBTIPOS", new[] { "ID_INSTITUCION", "ID_TIPO" });
            DropIndex("dbo.AF_TIPOSTRAN", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_MODELOS", new[] { "ID_MARCA" });
            DropIndex("dbo.AF_INFCOMPLEMEN", new[] { "ID_ESTCONSERVA" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_FONDO" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_ESTRUCTURA" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_ESTCONSERVA" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_MARCA", "ID_MODELO" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_MARCA" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_ACTIVIDAD" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_INSTITUCION", "ID_TIPO", "ID_SUBTIPO", "ID_CLASE" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_INSTITUCION", "CODIGO_TT" });
            DropIndex("dbo.AF_ACTIVOSFIJOS", new[] { "ID_INSTITUCION", "ID_TIPO" });
            DropIndex("dbo.AF_TIPOS", new[] { "ID_ESPECIFIC" });
            DropIndex("dbo.AF_TIPOS", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_TABLADEPRE", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_PARAMETROS", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_MOTIVOSBAJA", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_DETDEVOLUCION", new[] { "ID_INSTITUCION", "NUM_TRANSACCION" });
            DropIndex("dbo.AF_DEVOLUCIONES", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_CUENTASCONT", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.AF_DETACTAENTREGA", new[] { "ID_INSTITUCION", "NUM_ACTA" });
            DropIndex("dbo.AF_ACTAENT", new[] { "ID_INSTITUCION" });
            DropTable("dbo.AF_DETTRASPASO");
            DropTable("dbo.AF_TRASPASOS");
            DropTable("dbo.AF_TIPOSMEF");
            DropTable("dbo.AF_CLASES");
            DropTable("dbo.AF_SUBTIPOS");
            DropTable("dbo.AF_ESPECIFICACIONES");
            DropTable("dbo.AF_TIPOSTRAN");
            DropTable("dbo.AF_ORIGENFONDO");
            DropTable("dbo.AF_MODELOS");
            DropTable("dbo.AF_MARCAS");
            DropTable("dbo.AF_ESTRUCTURABIEN");
            DropTable("dbo.AF_INFCOMPLEMEN");
            DropTable("dbo.AF_ESTADOCONSERVA");
            DropTable("dbo.AF_ACTIVIDADES");
            DropTable("dbo.AF_ACTIVOSFIJOS");
            DropTable("dbo.AF_TIPOS");
            DropTable("dbo.AF_TABLADEPRE");
            DropTable("dbo.AF_PARAMETROS");
            DropTable("dbo.AF_MOTIVOSBAJA");
            DropTable("dbo.AF_DETDEVOLUCION");
            DropTable("dbo.AF_DEVOLUCIONES");
            DropTable("dbo.AF_CUENTASCONT");
            DropTable("dbo.AF_DETACTAENTREGA");
            DropTable("dbo.AF_ACTAENT");
        }
    }
}
