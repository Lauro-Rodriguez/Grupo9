namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actividads",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .ForeignKey("dbo.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO });
            
            CreateTable(
                "dbo.Proyectoes",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROY_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO })
                .ForeignKey("dbo.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO });
            
            CreateTable(
                "dbo.SubProgramas",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 230),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO })
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO });
            
            CreateTable(
                "dbo.Programas",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PROG_CODIGO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.Instituciones",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false, identity: true),
                        TIPO_PRESUP = c.Int(nullable: false),
                        COD_INSTITUCION = c.Int(nullable: false),
                        NOM_INSTITUCION = c.String(nullable: false, maxLength: 100),
                        RUC_INSTITUCION = c.String(nullable: false, maxLength: 13),
                        EMAIL = c.String(maxLength: 100),
                        TIPO_DOCREPLEGAL = c.String(nullable: false, maxLength: 15),
                        NUMDOC_REPLEGAL = c.String(nullable: false, maxLength: 13),
                        NOM_REPLEGAL = c.String(nullable: false, maxLength: 100),
                        RUC_CONTADOR = c.String(maxLength: 13),
                        NOM_CONTADOR = c.String(maxLength: 100),
                        DIRECCION = c.String(nullable: false, maxLength: 255),
                        TELEFONO = c.String(nullable: false, maxLength: 9),
                        FAX = c.String(maxLength: 9),
                        ID_BASE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SERVER = c.String(nullable: false, maxLength: 30),
                        CATALOGO = c.String(nullable: false, maxLength: 30),
                        TIPOAMB = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OBLIGACONTAB = c.String(nullable: false, maxLength: 2),
                        NUMRESOL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EMAILPASSWORD = c.String(maxLength: 150),
                        SMTPHOST = c.String(maxLength: 150),
                        SMTPPORT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DIRFIRMADIGITAL = c.String(maxLength: 250),
                        PASSWFIRMADIGITAL = c.String(maxLength: 100),
                        DIRSRI = c.String(maxLength: 250),
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_CANTON = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_INICIO = c.DateTime(nullable: false, storeType: "date"),
                        CONTRIB_ESP = c.Boolean(nullable: false),
                        DATO_SEGURO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.Periodos",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false, identity: true),
                        PERI_DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        ACTIVO = c.Boolean(nullable: false),
                        FECHA_CREA = c.DateTime(storeType: "date"),
                        CERRADO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.CO_ASIENTOSCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ASIENTO_NUM = c.Int(nullable: false),
                        ASIENTO_FECHA = c.DateTime(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        TIPO_COMPROB = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROB = c.Int(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        CEDU_RUC = c.String(nullable: false, maxLength: 13),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        ASIENTO_BENEF = c.String(nullable: false, maxLength: 150),
                        ASIENTO_TIPO_DOC_BANCO = c.String(maxLength: 2),
                        ASIENTO_NUM_DOC_BANCO = c.Int(nullable: false),
                        ASIENTO_DETALLE = c.String(nullable: false, maxLength: 1000),
                        CODIGO_BANCO = c.Int(nullable: false),
                        ESTADO = c.Int(nullable: false),
                        CON_BANCO = c.Int(nullable: false),
                        INCLUIR_BANCOS = c.Int(nullable: false),
                        CON_SPI = c.Int(nullable: false),
                        FECHA_ANUL = c.DateTime(nullable: false),
                        ANULADO = c.Int(nullable: false),
                        CERRADO = c.Int(nullable: false),
                        NUMEXTERNO = c.String(maxLength: 25),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TIPO_COMPROB, t.NUM_COMPROB }, unique: true);
            
            CreateTable(
                "dbo.CO_DETALLEASIENTOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ASIENTO_NUM = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        DEBE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HABER = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ANULADO = c.Int(nullable: false),
                        CERRADO = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM, t.NUM_FILA })
                .ForeignKey("dbo.CO_ASIENTOSCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM }, cascadeDelete: false)
                .ForeignKey("dbo.CO_CUENTASCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG });
            
            CreateTable(
                "dbo.CO_CUENTASCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        NOMBRE_CG = c.String(nullable: false, maxLength: 110),
                        NIVEL_CG = c.Int(nullable: false),
                        TIPO_CG = c.String(nullable: false, maxLength: 1),
                        PPCREDITO = c.String(maxLength: 50),
                        PPDEBITO = c.String(maxLength: 50),
                        SIGEF = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.CO_RELACION",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        EJERCICIO = c.Int(nullable: false),
                        PPDEBITO = c.String(maxLength: 10),
                        PPCREDITO = c.String(maxLength: 10),
                        CUENTA_COBRO = c.String(maxLength: 10),
                        CUENTA_PAGO = c.String(maxLength: 10),
                        ENVIADO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG, t.EJERCICIO })
                .ForeignKey("dbo.CO_CUENTASCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG }, cascadeDelete: false)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG });
            
            CreateTable(
                "dbo.CO_RELARETCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_RET = c.String(nullable: false, maxLength: 20),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET, t.CODIGO_CG })
                .ForeignKey("dbo.CO_CLASIFRETEN", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET }, cascadeDelete: false)
                .ForeignKey("dbo.CO_CUENTASCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG });
            
            CreateTable(
                "dbo.CO_CLASIFRETEN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_RET = c.String(nullable: false, maxLength: 20),
                        DESCRIP_RET = c.String(nullable: false, maxLength: 100),
                        TIPO_RET = c.Int(nullable: false),
                        PORCENTAJE = c.Int(nullable: false),
                        CTA_DEBITO = c.String(maxLength: 50),
                        CTA_CREDITO = c.String(maxLength: 50),
                        COMP100 = c.String(maxLength: 20),
                        TIPO_GASTO = c.Int(nullable: false),
                        TIPO_CONTRIB = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.CO_DETALLEBANCOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ASIENTO_NUM = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        NUM_TRANSAC = c.Int(nullable: false),
                        TIPO_DOCUMENTO_TESORERIA = c.String(nullable: false, maxLength: 2),
                        NUM_DOCUMENTO_TESORERIA = c.String(nullable: false, maxLength: 12),
                        BENEFICIARIO_PAGADOR = c.String(nullable: false, maxLength: 150),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COD_CUENTA = c.String(nullable: false, maxLength: 20),
                        COBRADO = c.Int(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 1),
                        TIPO_COMPROB = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROB = c.Int(nullable: false),
                        FECHA_PAGO = c.DateTime(nullable: false),
                        FECHA_COBRO = c.DateTime(nullable: false),
                        IMPRIMIO = c.Int(nullable: false),
                        CONCILIADO = c.Int(nullable: false),
                        ANULADO = c.Int(nullable: false),
                        FECHANUL = c.DateTime(nullable: false),
                        TIPO_TRAN = c.String(nullable: false, maxLength: 2),
                        MES_CONCILIA = c.Int(nullable: false),
                        CON_TRANSFER = c.Int(nullable: false),
                        NUM_TRANSFER = c.Int(nullable: false),
                        CEDU_RUC = c.String(nullable: false, maxLength: 13),
                        VAL_TRANSFER = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_CONCILIA = c.DateTime(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM, t.NUM_FILA })
                .ForeignKey("dbo.CO_ASIENTOSCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM }, cascadeDelete: false)
                .ForeignKey("dbo.CO_LINEACREDITO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CUENTA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CUENTA });
            
            CreateTable(
                "dbo.CO_LINEACREDITO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CUENTA = c.String(nullable: false, maxLength: 20),
                        NOMBRE_CUENTA = c.String(nullable: false, maxLength: 80),
                        CODIGO_CG = c.String(maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CUENTA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.CO_CUENBANCOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CUENTA = c.Int(nullable: false),
                        CODIGO_BANCO = c.Int(nullable: false),
                        CUENTA_CORRIENTE = c.String(nullable: false, maxLength: 15),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        DESCRIPCION = c.String(nullable: false, maxLength: 150),
                        CONCILIA = c.Boolean(nullable: false),
                        CTA_DEBITO = c.String(maxLength: 50),
                        CTA_CREDITO = c.String(maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA })
                .ForeignKey("dbo.BANCOS", t => t.CODIGO_BANCO, cascadeDelete: false)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.CODIGO_BANCO);
            
            CreateTable(
                "dbo.BANCOS",
                c => new
                    {
                        CODIGO_BANCO = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 40),
                        CODIGO_ENVIO = c.Int(nullable: false),
                        TIPO = c.Int(nullable: false),
                        CONDICION = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CODIGO_BANCO);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUMCEDRUC_PROV = c.String(nullable: false, maxLength: 20),
                        TIPO_IDENTI = c.Int(nullable: false),
                        TIPO_PROV = c.Int(nullable: false),
                        NOMBRE = c.String(nullable: false, maxLength: 150),
                        DIRECCION = c.String(nullable: false, maxLength: 250),
                        TELEFONO = c.String(nullable: false, maxLength: 50),
                        CODIGO_ENVIO = c.Int(nullable: false),
                        CODIGO_BANCO = c.Int(nullable: false),
                        NUM_CUENTA = c.String(nullable: false, maxLength: 18),
                        TIPO_CUENTA = c.Int(nullable: false),
                        TIPO_IDENTITRAN = c.Int(nullable: false),
                        RUCCEDU_TRAN = c.String(nullable: false, maxLength: 13),
                        RAZONSOCIAL = c.String(nullable: false, maxLength: 150),
                        CXC_PROVEED = c.String(maxLength: 50),
                        CXC_ANTICIPREC = c.String(maxLength: 50),
                        CXP_PROVEED = c.String(maxLength: 50),
                        CXP_ANTICIPENT = c.String(maxLength: 50),
                        CONTRIBESPECIAL = c.Boolean(nullable: false),
                        OBLIGA_CONTAB = c.Boolean(nullable: false),
                        INSTIT_PUB = c.Int(nullable: false),
                        SEXO = c.Int(nullable: false),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        CODI_CLASRET = c.String(maxLength: 10),
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.COD_PROV)
                .ForeignKey("dbo.BANCOS", t => t.CODIGO_BANCO, cascadeDelete: false)
                .ForeignKey("dbo.Parroquias", t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA }, cascadeDelete: false)
                .Index(t => t.CODIGO_BANCO)
                .Index(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA });
            
            CreateTable(
                "dbo.Parroquias",
                c => new
                    {
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA })
                .ForeignKey("dbo.Cantones", t => new { t.ID_PROVINCIA, t.ID_CANTON }, cascadeDelete: false)
                .ForeignKey("dbo.Provincias", t => t.ID_PROVINCIA, cascadeDelete: false)
                .Index(t => new { t.ID_PROVINCIA, t.ID_CANTON });
            
            CreateTable(
                "dbo.Provincias",
                c => new
                    {
                        ID_PROVINCIA = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_PROVINCIA);
            
            CreateTable(
                "dbo.Cantones",
                c => new
                    {
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON })
                .ForeignKey("dbo.Provincias", t => t.ID_PROVINCIA, cascadeDelete: false)
                .Index(t => t.ID_PROVINCIA);
            
            CreateTable(
                "dbo.RP_RECEPFONDOS",
                c => new
                    {
                        ID_RECEPFONDO = c.Int(nullable: false, identity: true),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        RUC_RECEPFONDO = c.String(nullable: false, maxLength: 13),
                        ID_PORCENT = c.Int(nullable: false),
                        NUM_PATRONAL = c.String(nullable: false, maxLength: 20),
                        NUMDOC_REPLEGAL = c.String(nullable: false, maxLength: 13),
                        NUMDOC_OFIPAGADOR = c.String(nullable: false, maxLength: 13),
                    })
                .PrimaryKey(t => t.ID_RECEPFONDO)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .ForeignKey("dbo.RP_PORCAPORTACION", t => t.ID_PORCENT, cascadeDelete: false)
                .Index(t => t.COD_PROV)
                .Index(t => t.ID_PORCENT);
            
            CreateTable(
                "dbo.RP_PORCAPORTACION",
                c => new
                    {
                        ID_PORCENT = c.Int(nullable: false, identity: true),
                        TIPO = c.String(nullable: false, maxLength: 1),
                        ID_TIPOSEGURO = c.Int(nullable: false),
                        APORTE_PERS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        APORTE_PATRO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGURO_CESANTIA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SECAP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IECE = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID_PORCENT)
                .ForeignKey("dbo.RP_TIPOSEGURO", t => t.ID_TIPOSEGURO, cascadeDelete: false)
                .Index(t => t.ID_TIPOSEGURO);
            
            CreateTable(
                "dbo.RP_TIPOSEGURO",
                c => new
                    {
                        ID_TIPOSEGURO = c.Int(nullable: false, identity: true),
                        NOMBRE_TIPOSEGURO = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID_TIPOSEGURO);
            
            CreateTable(
                "dbo.RP_TIPOROL",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                        NUM_DIAS = c.Int(nullable: false),
                        SECCION = c.String(nullable: false, maxLength: 1),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        CODIGO_CG = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOROL })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION)
                .Index(t => t.COD_PROV);
            
            CreateTable(
                "dbo.RP_ASIGNACUENTAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        PAEG_CODIGO = c.Int(nullable: false),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        TIPO = c.Int(nullable: false),
                        CODIGO_CGASTO = c.String(nullable: false, maxLength: 50),
                        TIPO_CUENTA = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOROL, t.COD_RUBRO, t.ACTI_CODIGO })
                .ForeignKey("dbo.RP_TIPOROL", t => new { t.ID_INSTITUCION, t.ID_TIPOROL }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOROL });
            
            CreateTable(
                "dbo.RP_EMPLEADOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false, identity: true),
                        ID_CARGO = c.Int(nullable: false),
                        NUM_CEDULA = c.String(nullable: false, maxLength: 10),
                        NUM_CEDMIL = c.String(maxLength: 12),
                        APELLIDOS = c.String(nullable: false, maxLength: 30),
                        NOMBRES = c.String(nullable: false, maxLength: 30),
                        IESS = c.String(maxLength: 1),
                        FECHA_NAC = c.DateTime(nullable: false),
                        ID_NACIONALIDAD = c.Int(nullable: false),
                        ESTADO_CIVIL = c.Int(nullable: false),
                        ISTRUCCION = c.Int(nullable: false),
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                        DIRECCIONDOM = c.String(nullable: false, maxLength: 250),
                        NUM_TARJETA = c.Int(nullable: false),
                        TIPOSANGRE = c.String(nullable: false, maxLength: 4),
                        SEXO = c.Int(nullable: false),
                        FECHA_ENTRA = c.DateTime(nullable: false),
                        FECHA_REINGRESO = c.DateTime(),
                        FECHA_SALE = c.DateTime(),
                        ID_BANCO = c.Int(nullable: false),
                        TIPO_CUENTA = c.Int(nullable: false),
                        NUM_CUENTA = c.String(nullable: false, maxLength: 10),
                        TELEFONO = c.String(nullable: false, maxLength: 10),
                        DISCAPACIDAD = c.Boolean(nullable: false),
                        PORC_DISCAP = c.Int(nullable: false),
                        EMAIL = c.String(nullable: false, maxLength: 150),
                        REMUNUNIF = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUELDO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_RELALABORAL = c.Int(nullable: false),
                        ID_GRADO = c.Int(nullable: false),
                        ID_NIVEL = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        TALLA_ROPA = c.Int(nullable: false),
                        TALLA_CALZADO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ID_TIPOROL = c.Int(nullable: false),
                        ACTIVO = c.Boolean(nullable: false),
                        OBSERVA = c.String(maxLength: 250),
                        FOTO = c.Binary(),
                        CODIGO_CG = c.String(maxLength: 50),
                        OTROS1 = c.Int(nullable: false),
                        OTROS2 = c.Int(nullable: false),
                        OTROS3 = c.Int(nullable: false),
                        OTROS4 = c.Int(nullable: false),
                        OTROS5 = c.Int(nullable: false),
                        OTROS6 = c.Int(nullable: false),
                        OTROS7 = c.Int(nullable: false),
                        OTROS8 = c.Int(nullable: false),
                        OTROS9 = c.Int(nullable: false),
                        OTROS10 = c.Int(nullable: false),
                        OTROS11 = c.Int(nullable: false),
                        OTROS12 = c.Int(nullable: false),
                        FACTOR_IR = c.Int(nullable: false),
                        FECHA1 = c.DateTime(),
                        FECHA2 = c.DateTime(),
                        FECHA3 = c.DateTime(),
                        PATH_FOTO = c.String(maxLength: 250),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO })
                .ForeignKey("dbo.Nacionalidades", t => t.ID_NACIONALIDAD, cascadeDelete: false)
                .ForeignKey("dbo.RP_CARGOS", t => new { t.ID_INSTITUCION, t.ID_CARGO }, cascadeDelete: false)
                .ForeignKey("dbo.RP_RELACIONLABORAL", t => t.ID_RELALABORAL, cascadeDelete: false)
                .ForeignKey("dbo.RP_TIPOROL", t => new { t.ID_INSTITUCION, t.ID_TIPOROL }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.APELLIDOS, t.NOMBRES }, unique: true)
                .Index(t => new { t.ID_INSTITUCION, t.ID_CARGO })
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOROL })
                .Index(t => t.NUM_CEDULA, unique: true, name: "IX_ID_INSTITUCION_NUM_CEDULA")
                .Index(t => t.ID_NACIONALIDAD)
                .Index(t => t.ID_RELALABORAL);
            
            CreateTable(
                "dbo.Nacionalidades",
                c => new
                    {
                        ID_NACIONALIDAD = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID_NACIONALIDAD);
            
            CreateTable(
                "dbo.POA_PLAN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        DETALE = c.String(nullable: false, maxLength: 250),
                        FECHA_PLAN = c.DateTime(nullable: false),
                        ESTADO = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO });
            
            CreateTable(
                "dbo.POA_PLANOBJETIVOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        ID_EMPLEADO = c.Int(nullable: false),
                        COD_TIPOPROY = c.Int(nullable: false),
                        COD_OE_PLANIF = c.String(nullable: false, maxLength: 10),
                        FECHA_INICIAL = c.DateTime(nullable: false),
                        FECHA_FINAL = c.DateTime(nullable: false),
                        DESC_OBJETIVO = c.String(nullable: false, maxLength: 300),
                        OBS_OBJETIVO = c.String(maxLength: 300),
                        ESTADO = c.Int(),
                        ES_GAPR = c.Boolean(nullable: false),
                        CANTIDAD_META = c.Int(nullable: false),
                        DESCRIPCION_META = c.String(maxLength: 300),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO })
                .ForeignKey("dbo.POA_PLAN", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.POA_TIPOPROYECTOS", t => new { t.ID_INSTITUCION, t.COD_TIPOPROY }, cascadeDelete: false)
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.COD_TIPOPROY })
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO });
            
            CreateTable(
                "dbo.POA_FONDOXOBJTVO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        ID_FONDO = c.Int(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.ID_FONDO })
                .ForeignKey("dbo.POA_PLANOBJETIVOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_FONDOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO });
            
            CreateTable(
                "dbo.PR_FONDOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_FONDO = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 90),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.POA_INDICADOROBJ",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        COD_INDICADOROJ = c.String(nullable: false, maxLength: 15),
                        TIPOOBJ = c.Int(nullable: false),
                        MEDIOSVERIF = c.String(nullable: false, maxLength: 250),
                        VAL_ENERO = c.Int(nullable: false),
                        VAL_FEBRERO = c.Int(nullable: false),
                        VAL_MARZO = c.Int(nullable: false),
                        VAL_ABRIL = c.Int(nullable: false),
                        VAL_MAYO = c.Int(nullable: false),
                        VAL_JUNIO = c.Int(nullable: false),
                        VAL_JULIO = c.Int(nullable: false),
                        VAL_AGOSTO = c.Int(nullable: false),
                        VAL_SEPTIEMBRE = c.Int(nullable: false),
                        VAL_OCTUBRE = c.Int(nullable: false),
                        VAL_NOVIEMBRE = c.Int(nullable: false),
                        VAL_DICIEMBRE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.COD_INDICADOROJ })
                .ForeignKey("dbo.POA_PLANOBJETIVOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO });
            
            CreateTable(
                "dbo.POA_OBJTIVORECURSOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        TRE_CODIGO = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
                        CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CARACTERISITCAS = c.String(nullable: false, maxLength: 250),
                        FECHA_INI = c.DateTime(nullable: false),
                        CONTRAT_NUEVA = c.Boolean(nullable: false),
                        MES_CONTRAT = c.Int(nullable: false),
                        TIPO_ASIGPAC = c.Int(nullable: false),
                        OBSERVACIONES = c.String(maxLength: 250),
                        VAL_ENERO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_FEBRERO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_MARZO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_ABRIL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_MAYO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_JUNIO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_JULIO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_AGOSTO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_SEPTIEMBRE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_OCTUBRE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_NOVIEMBRE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_DICIEMBRE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.TRE_CODIGO, t.CAT_CODIGO })
                .ForeignKey("dbo.TipoRecursos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Catalogos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CAT_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.POA_PLANOBJETIVOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CAT_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO });
            
            CreateTable(
                "dbo.Catalogos",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
                        CAT_NOMBRE = c.String(nullable: false, maxLength: 250),
                        CAT_VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        TRE_CODIGO = c.Int(nullable: false),
                        TIPO_CALCULO = c.Int(nullable: false),
                        TIPO = c.String(nullable: false, maxLength: 1),
                        TIPO_GASTO = c.Int(nullable: false),
                        NORMALIZADO = c.Int(nullable: false),
                        APLICA_SOLPAGO = c.Int(nullable: false),
                        PERTENECE_PAC = c.Int(nullable: false),
                        CONSOLIDA_PAC = c.Int(nullable: false),
                        ESTADO = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_CPC = c.String(maxLength: 9),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CAT_CODIGO })
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.TipoRecursos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO });
            
            CreateTable(
                "dbo.OrganicoFs",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NIV_PADRE = c.Int(nullable: false),
                        NIV_HIJO = c.Int(nullable: false),
                        ORG_CLAVE = c.String(nullable: false, maxLength: 50),
                        ORG_NOMBRE = c.String(nullable: false, maxLength: 150),
                        DIRECTIVO = c.Boolean(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_SOLICDISPPRESUP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_DOC = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NUM_SOLIC = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        FECHA_SOLICITA = c.DateTime(nullable: false),
                        CON_PAC = c.Boolean(nullable: false),
                        COD_PLANOBJTVO = c.String(maxLength: 15),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC })
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO });
            
            CreateTable(
                "dbo.PR_DETALSOLICDISPPRES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_DOC = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NUM_SOLIC = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 128),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC, t.CAT_CODIGO })
                .ForeignKey("dbo.PR_SOLICDISPPRESUP", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC });
            
            CreateTable(
                "dbo.TECHOSPRESUPs",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        TECHO_CODIGO = c.Int(nullable: false, identity: true),
                        VALOR_CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_PORCENTAJE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO, t.TECHO_CODIGO })
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO });
            
            CreateTable(
                "dbo.TipoRecursos",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        TRE_CODIGO = c.Int(nullable: false),
                        TRE_NOMBRE = c.String(nullable: false, maxLength: 100),
                        ORG_CODIGO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO })
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO });
            
            CreateTable(
                "dbo.POA_TIPOPROYECTOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        COD_TIPOPROY = c.Int(nullable: false, identity: true),
                        TIPO = c.Int(nullable: false),
                        NOM_TIPOPROY = c.String(nullable: false, maxLength: 300),
                        COD_TIPOPROYP = c.Int(nullable: false),
                        ESTADO_TP = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.COD_TIPOPROY })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_CARGASFAMILIARES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ID_CARGA = c.String(nullable: false, maxLength: 15),
                        APELLIDOSNOMBRES = c.String(nullable: false, maxLength: 50),
                        SEXO = c.String(nullable: false, maxLength: 15),
                        FECHA_NACIM = c.DateTime(nullable: false),
                        ESTUDIA = c.Boolean(nullable: false),
                        OBERVACIONES = c.String(maxLength: 250),
                        DISCAPACITADO = c.Boolean(nullable: false),
                        PORC_DISC = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO, t.ID_CARGA, t.APELLIDOSNOMBRES })
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO });
            
            CreateTable(
                "dbo.RP_CARGOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_CARGO = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 150),
                        SUELDOBASICO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OTROS1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OTROS2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OTROS3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OTROS4 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OTROS5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OTROS6 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_CARGO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_DETALLEROL",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        MES_PROC = c.Int(nullable: false),
                        ANIO_PROC = c.Int(nullable: false),
                        ID_NOMINA = c.Int(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TIPO = c.Int(nullable: false),
                        FORMULA = c.Boolean(nullable: false),
                        ORDEN = c.Int(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false),
                        NUM_IDENTIF = c.Int(nullable: false),
                        COMPIMPRENTA = c.Boolean(nullable: false),
                        CERRADO = c.Boolean(nullable: false),
                        INC_ENLIQUIDA = c.Boolean(nullable: false),
                        ACUM_VALPROV = c.Boolean(nullable: false),
                        MES_PROVINI = c.Int(nullable: false),
                        MES_PROVFIN = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO, t.COD_RUBRO, t.MES_PROC, t.ANIO_PROC, t.ID_NOMINA })
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: false)
                .ForeignKey("dbo.RP_RUBROS", t => new { t.ID_INSTITUCION, t.COD_RUBRO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO })
                .Index(t => new { t.ID_INSTITUCION, t.COD_RUBRO });
            
            CreateTable(
                "dbo.RP_RUBROS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        NOM_RUBRO = c.String(nullable: false, maxLength: 20),
                        DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        TIPO = c.Int(nullable: false),
                        MOSTRAR = c.Boolean(nullable: false),
                        FORMULA = c.Boolean(nullable: false),
                        VALOR = c.String(nullable: false, maxLength: 1000),
                        VALOR_REAL = c.String(maxLength: 1000),
                        ORDEN = c.Int(nullable: false),
                        ENERO = c.Boolean(nullable: false),
                        FEBRERO = c.Boolean(nullable: false),
                        MARZO = c.Boolean(nullable: false),
                        ABRIL = c.Boolean(nullable: false),
                        MAYO = c.Boolean(nullable: false),
                        JUNIO = c.Boolean(nullable: false),
                        JULIO = c.Boolean(nullable: false),
                        AGOSTO = c.Boolean(nullable: false),
                        SEPTIEMBRE = c.Boolean(nullable: false),
                        OCTUBRE = c.Boolean(nullable: false),
                        NOVIEMBRE = c.Boolean(nullable: false),
                        DICIEMBRE = c.Boolean(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 1),
                        EXTRAROL = c.Boolean(nullable: false),
                        COMPIMPRENTA = c.Boolean(nullable: false),
                        RETCONHIST = c.Boolean(nullable: false),
                        NUM_IDENTIF = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        ELIMINADO = c.Boolean(nullable: false),
                        INC_ENLIQUIDA = c.Boolean(nullable: false),
                        ACUM_VALPROV = c.Boolean(nullable: false),
                        MES_PROVINI = c.Int(),
                        MES_PROVFIN = c.Int(),
                        OBSERVACIONES = c.String(maxLength: 200),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.COD_RUBRO })
                .ForeignKey("dbo.RP_TIPOROL", t => new { t.ID_INSTITUCION, t.ID_TIPOROL }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOROL });
            
            CreateTable(
                "dbo.RP_ROLESXRUBRO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        ID_TIPOROL = c.Int(nullable: false),
                        ACCESO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.COD_RUBRO, t.ID_TIPOROL })
                .ForeignKey("dbo.RP_RUBROS", t => new { t.ID_INSTITUCION, t.COD_RUBRO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.COD_RUBRO });
            
            CreateTable(
                "dbo.RP_HORASEXT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ANIO = c.Int(nullable: false),
                        MES = c.Int(nullable: false),
                        HEXT_25 = c.Int(nullable: false),
                        HEXT_50 = c.Int(nullable: false),
                        HEXT_75 = c.Int(nullable: false),
                        HEXT_100 = c.Int(nullable: false),
                        FALTAS = c.Int(nullable: false),
                        ATRASOS = c.Int(nullable: false),
                        PERMISOS = c.Int(nullable: false),
                        OTROS1 = c.Int(nullable: false),
                        OTROS2 = c.Int(nullable: false),
                        OTROS3 = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO, t.ANIO, t.MES })
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO });
            
            CreateTable(
                "dbo.RP_OTROSMOV",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        PERIODO = c.Int(nullable: false),
                        TIPO = c.Int(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR4 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR6 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR7 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR8 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR9 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR10 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO, t.PERIODO })
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO });
            
            CreateTable(
                "dbo.RP_RELACIONLABORAL",
                c => new
                    {
                        ID_RELALABORAL = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_RELALABORAL);
            
            CreateTable(
                "dbo.CO_CONCILIACIONES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CUENTA = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        MES_CONCILIA = c.Int(nullable: false),
                        SALDO_ESTADO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CHEQ_GIR_NC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NCDP_NI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SALDO_FIN_BANC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOT_DATCUENTA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOT_DATSISTEMA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CERRADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA, t.NUM_TRANSAC, t.MES_CONCILIA })
                .ForeignKey("dbo.CO_CUENBANCOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA });
            
            CreateTable(
                "dbo.CO_PARAMETROS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ANIO_CODIGO = c.String(nullable: false, maxLength: 4),
                        MESINI = c.Int(nullable: false),
                        MESPROC = c.Int(nullable: false),
                        MASKCUENPATRI = c.String(nullable: false, maxLength: 50),
                        MASKCUENRESUL = c.String(nullable: false, maxLength: 50),
                        ACT = c.String(nullable: false, maxLength: 5),
                        PAS = c.String(nullable: false, maxLength: 5),
                        PAT = c.String(nullable: false, maxLength: 5),
                        ING1 = c.String(nullable: false, maxLength: 5),
                        ING2 = c.String(maxLength: 5),
                        ING3 = c.String(maxLength: 5),
                        GTO1 = c.String(nullable: false, maxLength: 5),
                        GTO2 = c.String(maxLength: 5),
                        GTO3 = c.String(maxLength: 5),
                        ORDENDEU = c.String(maxLength: 5),
                        ORDENACRE = c.String(maxLength: 5),
                        RESUMIG = c.String(maxLength: 5),
                        CTARESINGGASTO = c.String(maxLength: 50),
                        CTARESEJERVIGENTE = c.String(maxLength: 50),
                        CTARRESEJERANTERIOR = c.String(maxLength: 50),
                        COMPDIAR = c.Int(nullable: false),
                        COMPEGRE = c.Int(nullable: false),
                        COMPINGRE = c.Int(nullable: false),
                        NUMASIEN = c.Int(nullable: false),
                        NUMASIREFO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTDISP = c.Int(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        NUM_TRANSFER = c.Int(nullable: false),
                        NUM_DEVEN = c.Int(nullable: false),
                        PORIVA = c.Int(nullable: false),
                        NUM_DECIMAL = c.Int(nullable: false),
                        NUM_PEDIDOCOMPRAL = c.Int(nullable: false),
                        NUM_ORDENPAGO = c.Int(nullable: false),
                        COD_ITEM = c.Int(nullable: false),
                        DEVENGAR_CONTAB = c.Boolean(nullable: false),
                        PAGAR_CONTAB = c.Boolean(nullable: false),
                        IVA_GASTO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ANIO_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.FC_CAJAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CAJA = c.Int(nullable: false),
                        ESTABLECIM = c.String(nullable: false, maxLength: 3),
                        PTOEMISION = c.String(nullable: false, maxLength: 3),
                        DESDE = c.Int(nullable: false),
                        HASTA = c.Int(nullable: false),
                        SEC_ACTUAL = c.Int(nullable: false),
                        ESTADO = c.Boolean(nullable: false),
                        COMP_ELECT = c.Boolean(nullable: false),
                        NUM_LINEAS = c.Int(nullable: false),
                        IMPRESORA = c.String(maxLength: 3),
                        PAPERNAME = c.String(maxLength: 3),
                        ALTO = c.Int(),
                        ANCHO = c.Int(),
                        MTOP = c.Int(),
                        MLEFT = c.Int(),
                        MBOTT = c.Int(),
                        MRIGHT = c.Int(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.FC_FACTURAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CAJA = c.Int(nullable: false),
                        NUM_FACTURA = c.Single(nullable: false),
                        NUM_TRANSAC = c.Single(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUMCEDRUC_PROV = c.String(nullable: false, maxLength: 20),
                        NOMBRE = c.String(nullable: false, maxLength: 150),
                        DIRECCION = c.String(nullable: false, maxLength: 250),
                        TELEFONO = c.String(nullable: false, maxLength: 50),
                        FECHA_FACTURA = c.String(nullable: false),
                        VALOR_MERCADERIA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PORC_DESCUENTO = c.Int(nullable: false),
                        VALOR_DESCEUNTO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_FLETE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PROPINA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RECARGOSI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RECARGOCI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PORC_IVA = c.Int(nullable: false),
                        VALOR_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BASE0 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BASEIVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_ICE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ABONO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SALDO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NUM_ASIENTO = c.Int(nullable: false),
                        CODIGO_CUENTA = c.String(maxLength: 50),
                        USUARIO = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_FACTURA })
                .ForeignKey("dbo.FC_CAJAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA }, cascadeDelete: false)
                .ForeignKey("dbo.FC_CIERRES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_TRANSAC }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_TRANSAC });
            
            CreateTable(
                "dbo.FC_CIERRES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CAJA = c.Int(nullable: false),
                        NUM_TRANSAC = c.Single(nullable: false),
                        DESDE = c.DateTime(nullable: false),
                        HASTA = c.DateTime(nullable: false),
                        USUARIO = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_TRANSAC })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.FC_DETALLEFACTURAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CAJA = c.Int(nullable: false),
                        NUM_FACTURA = c.Single(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        COD_ITEM = c.String(nullable: false, maxLength: 50),
                        NUMERO_LOTE = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        PORC_IVA = c.Int(nullable: false),
                        PORC_ICE = c.Int(nullable: false),
                        CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PORC_DESCUENTO = c.Int(nullable: false),
                        COSTO_PROM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COSTO_UNIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUBTOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_ICE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_DESC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_CADUCA = c.DateTime(nullable: false),
                        OBSERVA = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_FACTURA, t.ID_BODEGA, t.COD_ITEM, t.NUMERO_LOTE })
                .ForeignKey("dbo.FC_FACTURAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_FACTURA }, cascadeDelete: false)
                .ForeignKey("dbo.IN_ITEMS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_FACTURA })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM });
            
            CreateTable(
                "dbo.IN_ITEMS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        COD_ITEM = c.String(nullable: false, maxLength: 50),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        COD_CAGEN = c.String(nullable: false, maxLength: 10),
                        COD_PRESEN = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
                        NOM_ITEM = c.String(nullable: false, maxLength: 90),
                        MARCA_ITEM = c.String(nullable: false, maxLength: 50),
                        APLIC_ITEM = c.String(maxLength: 100),
                        TIPO_COSTEO = c.Int(nullable: false),
                        COSTO_PROD = c.String(nullable: false, maxLength: 1),
                        APLICA_IVA = c.Boolean(nullable: false),
                        PROD_PERE = c.Boolean(nullable: false),
                        PROD_DESC = c.Boolean(nullable: false),
                        PROD_NUM_SERIE = c.Boolean(nullable: false),
                        PROD_NUM_GAR = c.Boolean(nullable: false),
                        SECCION = c.String(maxLength: 50),
                        PERCHA = c.String(maxLength: 50),
                        FILA = c.String(maxLength: 50),
                        COLUMNA = c.String(maxLength: 50),
                        STOCK_MIN = c.Int(nullable: false),
                        STOCK_MAX = c.Int(nullable: false),
                        PUNTO_REORDEN = c.Int(nullable: false),
                        PRECIO1 = c.Int(nullable: false),
                        PRECIO2 = c.Int(nullable: false),
                        FECHA_ULT_COMPRA = c.DateTime(nullable: false),
                        COD_CONSIG = c.String(maxLength: 20),
                        CODIGO_CGDB = c.String(maxLength: 50),
                        CODIGO_CGCR = c.String(maxLength: 50),
                        COD_LINEA = c.Int(nullable: false),
                        COD_SUBLINEA = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM })
                .ForeignKey("dbo.IN_BODEGAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA }, cascadeDelete: false)
                .ForeignKey("dbo.IN_CARACGEN", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN }, cascadeDelete: false)
                .ForeignKey("dbo.IN_PRESENTA", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN, t.COD_PRESEN }, cascadeDelete: false)
                .ForeignKey("dbo.IN_CLASES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE }, cascadeDelete: false)
                .ForeignKey("dbo.IN_TIPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO }, cascadeDelete: false)
                .ForeignKey("dbo.IN_SUBTIPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO, t.ID_SUBTIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN, t.COD_PRESEN })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO, t.ID_SUBTIPO });
            
            CreateTable(
                "dbo.IN_BODEGAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        NOM_BODEGA = c.String(nullable: false, maxLength: 50),
                        TIPO_BODEGA = c.Int(nullable: false),
                        RESP_BODEGA = c.String(maxLength: 50),
                        DIR_BODEGA = c.String(maxLength: 250),
                        TELF_BODEGA = c.String(maxLength: 10),
                        NUM_ING_BODEGA = c.Single(nullable: false),
                        NUM_EGR_BODEGA = c.Single(nullable: false),
                        NUM_ORD_COMPRA = c.Single(nullable: false),
                        NUM_NTA_ENT = c.Single(nullable: false),
                        NUMAUT_INGBOD = c.Boolean(nullable: false),
                        NUMAUT_EGRBOD = c.Boolean(nullable: false),
                        NUMAUT_ORDCOMP = c.Boolean(nullable: false),
                        NUMAUT_NOTENT = c.Boolean(nullable: false),
                        INCRE1_INGBOD = c.Boolean(nullable: false),
                        INCRE1_EGRBOD = c.Boolean(nullable: false),
                        INCRE1_ORDCOMP = c.Boolean(nullable: false),
                        INCRE1_NOTENT = c.Boolean(nullable: false),
                        COD_INI_PROD = c.Single(nullable: false),
                        COD_AUTOMAT = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_CUENTASCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        CC_INVENT = c.String(maxLength: 50),
                        CC_CXP_CONSUMO = c.String(maxLength: 50),
                        CC_GASTO_CONSUMO = c.String(maxLength: 50),
                        CC_DONACION_CONSUMO = c.String(maxLength: 50),
                        CC_ACUMOBRAS_I = c.String(maxLength: 50),
                        CC_ACUMPROGRAM_I = c.String(maxLength: 50),
                        CC_CXP_I = c.String(maxLength: 50),
                        CC_GASTO_I = c.String(maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.ID_CLASE, t.ID_TIPO })
                .ForeignKey("dbo.IN_BODEGAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA }, cascadeDelete: false)
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA });
            
            CreateTable(
                "dbo.IN_USERSXBOD",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        USUA_LOGIN = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.USUA_LOGIN })
                .ForeignKey("dbo.IN_BODEGAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA });
            
            CreateTable(
                "dbo.IN_CARACGEN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CAGEN = c.String(nullable: false, maxLength: 10),
                        DESCRIP_CAGEN = c.String(nullable: false, maxLength: 100),
                        CARACT = c.String(nullable: false, maxLength: 5),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_PRESENTA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CAGEN = c.String(nullable: false, maxLength: 10),
                        COD_PRESEN = c.Int(nullable: false),
                        DESCRIP_CAGEN = c.String(nullable: false, maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN, t.COD_PRESEN })
                .ForeignKey("dbo.IN_CARACGEN", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN });
            
            CreateTable(
                "dbo.IN_CARGA_INI",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        COD_ITEM = c.String(nullable: false, maxLength: 50),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        CANTIDAD = c.Single(nullable: false),
                        COSTO = c.Single(nullable: false),
                        VALOR_TOTAL = c.Single(nullable: false),
                        ACTIVADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM, t.NUM_FILA })
                .ForeignKey("dbo.IN_ITEMS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM });
            
            CreateTable(
                "dbo.IN_CLASES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        DESRIPCION = c.String(nullable: false, maxLength: 100),
                        TIPO_USO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_TIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        NOM_TIPO = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO })
                .ForeignKey("dbo.IN_CLASES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE });
            
            CreateTable(
                "dbo.IN_SUBTIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        NOM_SUBTIPO = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO, t.ID_SUBTIPO })
                .ForeignKey("dbo.IN_TIPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO });
            
            CreateTable(
                "dbo.FC_DETALLESERVICIOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CAJA = c.Int(nullable: false),
                        NUM_FACTURA = c.Single(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SERVICIO = c.Int(nullable: false),
                        NUMERO_LOTE = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        PORC_IVA = c.Int(nullable: false),
                        PORC_ICE = c.Int(nullable: false),
                        CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PORC_DESCUENTO = c.Int(nullable: false),
                        COSTO_UNIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUBTOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_ICE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_DESC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OBSERVA = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_FACTURA, t.ID_TIPO, t.ID_SERVICIO, t.NUMERO_LOTE })
                .ForeignKey("dbo.FC_FACTURAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_FACTURA }, cascadeDelete: false)
                .ForeignKey("dbo.FC_SERVICIOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_TIPO, t.ID_SERVICIO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.NUM_FACTURA })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_TIPO, t.ID_SERVICIO });
            
            CreateTable(
                "dbo.FC_SECUENCIAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CAJA = c.Int(nullable: false),
                        ID_SECUENCIA = c.Int(nullable: false),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        COMP_ELECT = c.Boolean(nullable: false),
                        AUTORIZACION = c.String(maxLength: 20),
                        ESTABLECIM = c.String(nullable: false, maxLength: 3),
                        PTOEMISION = c.String(nullable: false, maxLength: 3),
                        DESDE = c.Int(nullable: false),
                        HASTA = c.Int(nullable: false),
                        F_VIGENCIA = c.DateTime(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA, t.ID_SECUENCIA })
                .ForeignKey("dbo.FC_CAJAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CAJA });
            
            CreateTable(
                "dbo.FC_TIPOSERVICIO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                        AGRUPAR = c.Int(nullable: false),
                        CUENTACONTABLE = c.String(maxLength: 50),
                        ENVIADO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_TIPO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.FC_SERVICIOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SERVICIO = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        PRECIO1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PRECIO2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PRECIO3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PRECIO4 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PRECIO5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PAGAIVA = c.Int(nullable: false),
                        CARACTERISTICAS = c.String(nullable: false, maxLength: 255),
                        CUENTACPEA = c.String(maxLength: 50),
                        CUENTACP = c.String(maxLength: 50),
                        CUENTAIG = c.String(maxLength: 50),
                        CUENTADESC = c.String(maxLength: 50),
                        CUENTADEV = c.String(maxLength: 50),
                        PARTIDA = c.Int(nullable: false),
                        ACTIVO = c.Int(nullable: false),
                        CUENTAPRV = c.String(maxLength: 50),
                        N_SERIE = c.Int(nullable: false),
                        N_SERIEINICIAL = c.Int(nullable: false),
                        N_SERIEACTUAL = c.Int(nullable: false),
                        NOM_ARCHIVO = c.String(maxLength: 100),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_TIPO, t.ID_SERVICIO })
                .ForeignKey("dbo.FC_TIPOSERVICIO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_TIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_TIPO });
            
            CreateTable(
                "dbo.FIRMAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        USUA_FIRMA = c.String(nullable: false, maxLength: 15),
                        CODIGO_DOC = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        NOMBRE = c.String(nullable: false, maxLength: 50),
                        CARGO = c.String(nullable: false, maxLength: 50),
                        TIPO = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.USUA_FIRMA, t.CODIGO_DOC, t.NUM_FILA })
                .ForeignKey("dbo.Documentos", t => t.CODIGO_DOC, cascadeDelete: false)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.CODIGO_DOC);
            
            CreateTable(
                "dbo.Documentos",
                c => new
                    {
                        CODIGO_DOC = c.Int(nullable: false, identity: true),
                        NOMBRE_DOC = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.CODIGO_DOC);
            
            CreateTable(
                "dbo.IN_COMPRAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_COMPRA = c.Int(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        FECHA_COMPRA = c.DateTime(nullable: false),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        TIPO_SRI = c.String(nullable: false, maxLength: 2),
                        FECHA_DOC = c.DateTime(nullable: false),
                        NUM_DOC = c.String(nullable: false, maxLength: 15),
                        AUTORIZACION = c.String(nullable: false, maxLength: 49),
                        FECHACAD_DOC = c.DateTime(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.COD_PROV, t.NUM_DOC }, unique: true);
            
            CreateTable(
                "dbo.IN_DETCOMPRA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_COMPRA = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
                        APLICA_IVA = c.Boolean(nullable: false),
                        PORC_IVA = c.Int(nullable: false),
                        CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COSTO_UNIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUBTOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COSTO_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAL_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA, t.NUM_FILA })
                .ForeignKey("dbo.IN_COMPRAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA });
            
            CreateTable(
                "dbo.IN_LINEANEGOCIO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_LINEA = c.Int(nullable: false),
                        DESC_LINEA = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_SUBLINEANEGOCIO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_LINEA = c.Int(nullable: false),
                        COD_SUBLINEA = c.Int(nullable: false),
                        DESC_SUBLINEA = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA, t.COD_SUBLINEA })
                .ForeignKey("dbo.IN_LINEANEGOCIO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA });
            
            CreateTable(
                "dbo.IN_PARAM",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        PORC_IVA = c.Int(nullable: false),
                        AMPLI_DESC = c.Int(nullable: false),
                        UNID_OPER = c.Boolean(nullable: false),
                        ORD_PROD = c.Boolean(nullable: false),
                        NAUT_PROD = c.Boolean(nullable: false),
                        CONT_BOD = c.Boolean(nullable: false),
                        CONT_TSP = c.Int(nullable: false),
                        NSEP_DEVO = c.Boolean(nullable: false),
                        NSEP_TRAN = c.Boolean(nullable: false),
                        NSEP_STOCK = c.Boolean(nullable: false),
                        COD_PROD = c.Int(nullable: false),
                        ORD_X_COD = c.Boolean(nullable: false),
                        REC_CON_IVA = c.Boolean(nullable: false),
                        UTIL_DEC_CANTI = c.Int(nullable: false),
                        UTIL_DEC_CUNI = c.Int(nullable: false),
                        UTIL_DEC_CTOT = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_FILA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_TIPOSTRAN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_TIPOTRAN = c.Int(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 2),
                        DESCRIP_TIPOTRAN = c.String(nullable: false, maxLength: 100),
                        ENV_CO_CC = c.Int(nullable: false),
                        CON_IVA = c.Boolean(nullable: false),
                        CON_DONANTE = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_TIPOTRAN })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.Mnu_Roles",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        CODIGO_ROL = c.Int(nullable: false, identity: true),
                        NOMBRE_ROL = c.String(nullable: false, maxLength: 30),
                        USUA_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false, storeType: "date"),
                        USUA_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false, storeType: "date"),
                        PERIODOS_ID_INSTITUCION = c.Int(),
                        PERIODOS_PERI_CODIGO = c.Int(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.CODIGO_ROL })
                .ForeignKey("dbo.Periodos", t => new { t.PERIODOS_ID_INSTITUCION, t.PERIODOS_PERI_CODIGO })
                .Index(t => new { t.PERIODOS_ID_INSTITUCION, t.PERIODOS_PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_RELACONTAB",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        TIPO = c.Int(nullable: false),
                        CODIGO_PARTIDA = c.Int(nullable: false),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        CODIGO_CGB = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_FILA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.RP_SECUENCIAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        MES_PROC = c.Int(nullable: false),
                        ANIO_PROC = c.Int(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false),
                        TIPO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                        ROLCERRADO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.MES_PROC, t.ANIO_PROC, t.ID_TIPOROL, t.TIPO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.RP_SETUP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        ORDEN = c.Int(nullable: false),
                        MES_PROC = c.Int(nullable: false),
                        ANIO_PROC = c.Int(nullable: false),
                        ID_EMPRESAT = c.Int(nullable: false),
                        NUM_ANTICIPO = c.Int(nullable: false),
                        NUM_ABONO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_FILA })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_TGTOSPERS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ANIO = c.Int(nullable: false),
                        GVIV = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GVEST = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GEDU = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GSAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GALI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ANIO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_TIPOSANTICIPO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        DESCRIPCION_ANTICIPO = c.String(nullable: false, maxLength: 60),
                        LIMITE_CUOTAS = c.Int(nullable: false),
                        PORC_DESCDIC = c.Int(nullable: false),
                        LIMITE_MONTO = c.Int(nullable: false),
                        TIPO_SUELDO = c.Int(nullable: false),
                        ID_TIPONOMINA = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOANTICIPO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_TIRENTA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ANIO = c.Int(nullable: false),
                        TIPO = c.Int(nullable: false),
                        FILA = c.Int(nullable: false),
                        FB = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IFB = c.Int(nullable: false),
                        IFE = c.Int(nullable: false),
                        ENVIADO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ANIO, t.TIPO, t.FILA })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_VARIABLES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        COD_VAR = c.String(nullable: false, maxLength: 10),
                        NOMBRE_VAR = c.String(nullable: false, maxLength: 50),
                        ORDEN = c.Int(nullable: false),
                        DESCRIPCION = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.COD_VAR })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.CO_CTASPPAGAR",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_TRANSAC = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        APLICADO = c.Boolean(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 3),
                        ANTICIPO = c.Boolean(nullable: false),
                        TM_FDO_TER = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PROV);
            
            CreateTable(
                "dbo.CO_DETCTASPPAG",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUM_FILA = c.String(nullable: false, maxLength: 20),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        TIPOSRI = c.String(nullable: false, maxLength: 2),
                        FECHA = c.DateTime(nullable: false),
                        FECHA_DOC = c.DateTime(nullable: false),
                        NUM_DOC = c.String(nullable: false, maxLength: 20),
                        VALOR_DOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IVA_PORC = c.Int(nullable: false),
                        TOT_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CTA_X_PAG = c.String(nullable: false, maxLength: 50),
                        CTA_DEBITO = c.String(nullable: false, maxLength: 50),
                        ASIENTO = c.Int(nullable: false),
                        RFIVA = c.Boolean(nullable: false),
                        PORC_APLICA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AD = c.Boolean(nullable: false),
                        IG = c.Boolean(nullable: false),
                        AUTORIZACION = c.String(maxLength: 49),
                        FECHACAD_DOC = c.DateTime(nullable: false),
                        REPOSICION = c.String(maxLength: 50),
                        ICE = c.Int(nullable: false),
                        COD_SUSTENTO = c.String(maxLength: 2),
                        TIPO_MOV = c.String(maxLength: 3),
                        APLICADO = c.Boolean(nullable: false),
                        ANULADO = c.Boolean(nullable: false),
                        NUMT_CONTAB = c.Int(nullable: false),
                        FORMA_PAGO = c.String(maxLength: 2),
                        ORIGEN = c.String(nullable: false, maxLength: 2),
                        ANTICIPO = c.Boolean(nullable: false),
                        TM_FDO_TER = c.Boolean(nullable: false),
                        NUM_FDO_TER = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA })
                .ForeignKey("dbo.CO_CTASPPAGAR", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC });
            
            CreateTable(
                "dbo.CO_RETENCIONES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUM_FILA = c.String(nullable: false, maxLength: 20),
                        CODIG_RET = c.String(nullable: false, maxLength: 20),
                        ASIENTO_NUM = c.Int(nullable: false),
                        RUC_CI = c.String(nullable: false, maxLength: 13),
                        NUM_DOCUM = c.String(nullable: false, maxLength: 20),
                        FECHA_EMISION = c.DateTime(nullable: false),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        CODICLASRET = c.String(nullable: false, maxLength: 10),
                        TIPO_PTOEMIRET = c.Int(nullable: false),
                        COD_PTOEMIRET = c.Int(nullable: false),
                        NUM_RETENCION = c.String(nullable: false, maxLength: 16),
                        PORC_RET = c.Int(nullable: false),
                        TIPO_RET = c.String(nullable: false, maxLength: 2),
                        VALOR_DOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BASE_IMP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_RET = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_EMI = c.DateTime(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        EMITIDO = c.Boolean(nullable: false),
                        PAGADO = c.Boolean(nullable: false),
                        ASIENTO_PAGO = c.Int(nullable: false),
                        FECHA_PAGO = c.DateTime(nullable: false),
                        CHECK_PAGO = c.Boolean(nullable: false),
                        FECHA_CADUCA = c.DateTime(nullable: false),
                        COD_SUSTENTO = c.String(nullable: false, maxLength: 2),
                        NUMT_TRANSAC = c.Int(nullable: false),
                        CODIGO_CUENTA = c.String(maxLength: 50),
                        CODIGO_CUENTACP = c.String(maxLength: 50),
                        ANULADA = c.Boolean(nullable: false),
                        GENERADO = c.Boolean(nullable: false),
                        FIRMADO = c.Boolean(nullable: false),
                        AUTORIZADO = c.Boolean(nullable: false),
                        EMAIL = c.Boolean(nullable: false),
                        CLAVE_ACCESO = c.String(maxLength: 49),
                        AUTORIZACION_SRI = c.String(maxLength: 49),
                        FECHA_AUTORIZA = c.DateTime(nullable: false),
                        AMBIENTE = c.String(maxLength: 1),
                        EMISION = c.String(maxLength: 1),
                        CUENTAXPAGAR = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA, t.CODIG_RET })
                .ForeignKey("dbo.CO_DETCTASPPAG", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA });
            
            CreateTable(
                "dbo.DETALLEGRESOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PAEG_CODIGO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        VALOR_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        POA = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO, t.ACTI_CODIGO, t.ITEM_CLAVE })
                .ForeignKey("dbo.PartEgresoes", t => new { t.ID_INSTITUCION, t.PAEG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PAEG_CODIGO });
            
            CreateTable(
                "dbo.PartEgresoes",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PAEG_CODIGO = c.Int(nullable: false, identity: true),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAEG_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAEG_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ITEM_ORGAN = c.String(nullable: false, maxLength: 3),
                        ITEM_CORRE = c.String(nullable: false, maxLength: 3),
                        ITEM_FUNC = c.Int(nullable: false),
                        ITEM_ORIGTO = c.Int(nullable: false),
                        ITEM_UNIDOPTIVA = c.Int(nullable: false),
                        CTO_COSTO = c.Int(nullable: false),
                        SCTO_COSTO = c.Int(nullable: false),
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PAEG_CODIGO })
                .ForeignKey("dbo.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PAEG_CLAVE }, unique: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.Grupoes",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        GRUP_NOMBRE = c.String(nullable: false, maxLength: 100),
                        TIPO = c.String(maxLength: 1),
                        NIVEL = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_SUBITEM = c.String(nullable: false, maxLength: 3),
                        ITEM_NOMBRE = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ITEM_CLAVE })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .ForeignKey("dbo.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION)
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.DETALLEINGRESOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PAIN_CODIGO = c.Int(nullable: false),
                        DETI_VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAIN_CODIGO })
                .ForeignKey("dbo.PartIngresoes", t => new { t.ID_INSTITUCION, t.PAIN_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PAIN_CODIGO });
            
            CreateTable(
                "dbo.PartIngresoes",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PAIN_CODIGO = c.Int(nullable: false, identity: true),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAIN_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAIN_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ORGANISMO = c.Int(nullable: false),
                        CORRELAT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PAIN_CODIGO })
                .ForeignKey("dbo.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PAIN_CLAVE }, unique: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.Mnu_Opciones",
                c => new
                    {
                        COD_MODULO = c.Int(nullable: false),
                        COD_MENU = c.String(nullable: false, maxLength: 20),
                        DESC_MENU = c.String(maxLength: 80),
                        MAIN_NIVEL = c.Int(nullable: false),
                        NOM_MENU = c.String(maxLength: 10),
                        SUB_NIVEL = c.Int(nullable: false),
                        EVENTO = c.String(maxLength: 30),
                        CONTROLADOR = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => new { t.COD_MODULO, t.COD_MENU })
                .ForeignKey("dbo.Mnu_Modulos", t => t.COD_MODULO, cascadeDelete: false)
                .Index(t => t.COD_MODULO);
            
            CreateTable(
                "dbo.Mnu_Modulos",
                c => new
                    {
                        COD_MODULO = c.Int(nullable: false),
                        NOM_MODULO = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.COD_MODULO);
            
            CreateTable(
                "dbo.POA_EAI",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_EAI = c.Int(nullable: false),
                        NOM_EAI = c.String(nullable: false, maxLength: 1000),
                        META_EAI = c.String(nullable: false, maxLength: 500),
                        REFE_EAI = c.String(nullable: false, maxLength: 15),
                        PRIOR_EAI = c.Int(nullable: false),
                        FECHAINI_EAI = c.DateTime(nullable: false),
                        FECHAFIN_EAI = c.DateTime(nullable: false),
                        OBSERVA_EAI = c.String(maxLength: 1000),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_EAI });
            
            CreateTable(
                "dbo.POA_ED_PDOT",
                c => new
                    {
                        COD_ED_PDOT = c.Int(nullable: false, identity: true),
                        NOM_ED_PDOT = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.COD_ED_PDOT);
            
            CreateTable(
                "dbo.POA_OE_PDOT",
                c => new
                    {
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        NOM_OE_PDOT = c.String(nullable: false, maxLength: 300),
                        ACTIVO = c.Int(nullable: false),
                        COD_ED_PDOT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_OE_PDOT)
                .ForeignKey("dbo.POA_ED_PDOT", t => t.COD_ED_PDOT, cascadeDelete: false)
                .Index(t => t.COD_ED_PDOT);
            
            CreateTable(
                "dbo.POA_POL_PDOT",
                c => new
                    {
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_POL_PDOT = c.String(nullable: false, maxLength: 4),
                        NOM_POL_PDOT = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT })
                .ForeignKey("dbo.POA_OE_PDOT", t => t.COD_OE_PDOT, cascadeDelete: false)
                .Index(t => t.COD_OE_PDOT);
            
            CreateTable(
                "dbo.POA_META_PDOT",
                c => new
                    {
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_POL_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_META_PDOT = c.String(nullable: false, maxLength: 4),
                        NOM_POL_PDOT = c.String(nullable: false, maxLength: 300),
                        NOM_IND_PDOT = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT })
                .ForeignKey("dbo.POA_POL_PDOT", t => new { t.COD_OE_PDOT, t.COD_POL_PDOT }, cascadeDelete: false)
                .Index(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT });
            
            CreateTable(
                "dbo.POA_OE_PLANIF",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_OE_PLANIF = c.String(nullable: false, maxLength: 10),
                        UNI_MEDIDA = c.Int(nullable: false),
                        COD_PNVB = c.Int(nullable: false),
                        COD_OEI = c.Int(nullable: false),
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_EPG = c.Int(nullable: false),
                        COD_POL_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_META_PDOT = c.String(nullable: false, maxLength: 4),
                        DESCRIPCION = c.String(nullable: false, maxLength: 250),
                        IND_MEDIO = c.String(maxLength: 250),
                        MIN_BASE = c.String(maxLength: 250),
                        MET_GTON_OBJTVO = c.String(maxLength: 250),
                        ACTIVO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_OE_PLANIF })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.POA_EPG", t => t.COD_EPG, cascadeDelete: false)
                .ForeignKey("dbo.POA_META_PDOT", t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT }, cascadeDelete: false)
                .ForeignKey("dbo.POA_OE_PDOT", t => t.COD_OE_PDOT, cascadeDelete: false)
                .ForeignKey("dbo.POA_OEI", t => t.COD_OEI, cascadeDelete: false)
                .ForeignKey("dbo.POA_PNBV", t => t.COD_PNVB, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PNVB)
                .Index(t => t.COD_OEI)
                .Index(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT })
                .Index(t => t.COD_EPG);
            
            CreateTable(
                "dbo.POA_EPG",
                c => new
                    {
                        COD_EPG = c.Int(nullable: false, identity: true),
                        NOM_EPG = c.String(nullable: false, maxLength: 100),
                        COD_PR_PDOT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_EPG)
                .ForeignKey("dbo.POA_PR_PDOT", t => t.COD_PR_PDOT, cascadeDelete: false)
                .Index(t => t.COD_PR_PDOT);
            
            CreateTable(
                "dbo.POA_PR_PDOT",
                c => new
                    {
                        COD_PR_PDOT = c.Int(nullable: false, identity: true),
                        NOM_PR_PDOT = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.COD_PR_PDOT);
            
            CreateTable(
                "dbo.POA_OEI",
                c => new
                    {
                        COD_OEI = c.Int(nullable: false, identity: true),
                        NOM_OEI = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.COD_OEI);
            
            CreateTable(
                "dbo.POA_PNBV",
                c => new
                    {
                        COD_PNVB = c.Int(nullable: false, identity: true),
                        NOM_PNVB = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.COD_PNVB);
            
            CreateTable(
                "dbo.PR_ACTIVIDADES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .ForeignKey("dbo.PR_PROYECTOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO });
            
            CreateTable(
                "dbo.PR_PROYECTOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROY_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO })
                .ForeignKey("dbo.PR_SUBPROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO });
            
            CreateTable(
                "dbo.PR_SUBPROGRAMAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 230),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO })
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO });
            
            CreateTable(
                "dbo.PR_CERTDISPRESUP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTIF = c.Int(nullable: false),
                        TIPO_CERTIF = c.String(nullable: false, maxLength: 2),
                        FECHA_CERTIF = c.DateTime(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NUM_PEDIDO = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        DETALLE_CERTIF = c.String(nullable: false, maxLength: 500),
                        ANULADO = c.Boolean(nullable: false),
                        CERRADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PROV);
            
            CreateTable(
                "dbo.PR_DETCERTDISP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTIF = c.Int(nullable: false),
                        PAEG_CODIGO = c.Int(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TIPO_CERTIF = c.String(nullable: false, maxLength: 2),
                        ID_FONDO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF, t.PAEG_CODIGO })
                .ForeignKey("dbo.PR_CERTDISPRESUP", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF }, cascadeDelete: false)
                .ForeignKey("dbo.PR_FONDOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PARTEGRESOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO });
            
            CreateTable(
                "dbo.PR_PARTEGRESOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PAEG_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAEG_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAEG_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ITEM_ORGAN = c.String(nullable: false, maxLength: 3),
                        ITEM_CORRE = c.String(nullable: false, maxLength: 3),
                        ITEM_FUNC = c.Int(nullable: false),
                        ITEM_ORIGTO = c.Int(nullable: false),
                        ITEM_UNIDOPTIVA = c.Int(nullable: false),
                        CTO_COSTO = c.Int(nullable: false),
                        SCTO_COSTO = c.Int(nullable: false),
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_ACTIVIDADES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_GRUPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PROYECTOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_SUBPROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.PR_GRUPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        GRUP_NOMBRE = c.String(nullable: false, maxLength: 100),
                        TIPO = c.String(maxLength: 1),
                        NIVEL = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_ITEMS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_SUBITEM = c.String(nullable: false, maxLength: 3),
                        ITEM_NOMBRE = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ITEM_CLAVE })
                .ForeignKey("dbo.PR_GRUPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.PR_PROGRAMAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_DOCUMCERTDISP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTIF = c.Int(nullable: false),
                        COD_DOCUM = c.String(nullable: false, maxLength: 15),
                        NUM_DOCUM = c.String(nullable: false, maxLength: 20),
                        OBSERVA_DOCUM = c.String(maxLength: 50),
                        FECHA = c.DateTime(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF, t.COD_DOCUM, t.NUM_DOCUM })
                .ForeignKey("dbo.PR_CERTDISPRESUP", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF });
            
            CreateTable(
                "dbo.PR_DETGASTO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        PAEG_CODIGO = c.Int(nullable: false),
                        TIPO_TRAN = c.String(nullable: false, maxLength: 3),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_IFONDO = c.Int(nullable: false),
                        RETENCION = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.PAEG_CODIGO })
                .ForeignKey("dbo.PR_MOVGASTO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PARTEGRESOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO });
            
            CreateTable(
                "dbo.PR_MOVGASTO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_TRANSAC = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        NUM_ASIENTO = c.Int(nullable: false),
                        NUM_DISPONIB = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        BENEFICIARIO = c.String(nullable: false, maxLength: 100),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        TIPO_DOCUM = c.String(nullable: false, maxLength: 2),
                        PROG_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(maxLength: 15),
                        ACTI_CODIGO = c.String(maxLength: 15),
                        CERRADO = c.Boolean(nullable: false),
                        ANULADO = c.Boolean(nullable: false),
                        NUMEXTERNO = c.String(maxLength: 24),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PROV);
            
            CreateTable(
                "dbo.PR_DOCUMMOVGASTO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_DOCUM = c.String(nullable: false, maxLength: 15),
                        NUM_DOCUM = c.String(nullable: false, maxLength: 15),
                        OBSERVA_DOCUM = c.String(maxLength: 50),
                        FECHA = c.DateTime(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_DOCUM, t.NUM_DOCUM })
                .ForeignKey("dbo.PR_MOVGASTO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC });
            
            CreateTable(
                "dbo.PR_DETINGRESO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        PAIN_CODIGO = c.Int(nullable: false),
                        TIPO_TRANSAC = c.String(nullable: false, maxLength: 2),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_IFONDO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.PAIN_CODIGO })
                .ForeignKey("dbo.PR_MOVINGRESO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PARTINGRESOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAIN_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAIN_CODIGO });
            
            CreateTable(
                "dbo.PR_MOVINGRESO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_TRANSAC = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        NUM_ASIENTO = c.Int(nullable: false),
                        NUM_DISPONIB = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        BENEFICIARIO = c.String(nullable: false, maxLength: 100),
                        DETALLE = c.String(nullable: false, maxLength: 255),
                        TIPO_DOCUM = c.String(nullable: false, maxLength: 2),
                        PROG_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(maxLength: 15),
                        ACTI_CODIGO = c.String(maxLength: 15),
                        CERRADO = c.Boolean(nullable: false),
                        ANULADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_DOCUMMOVINGRESO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_DOCUM = c.String(nullable: false, maxLength: 15),
                        NUM_DOCUM = c.String(nullable: false, maxLength: 15),
                        OBSERVA_DOCUM = c.String(maxLength: 50),
                        FECHA = c.DateTime(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_DOCUM, t.NUM_DOCUM })
                .ForeignKey("dbo.PR_MOVINGRESO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC });
            
            CreateTable(
                "dbo.PR_PARTINGRESOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PAIN_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAIN_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAIN_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ORGANISMO = c.Int(nullable: false),
                        CORRELAT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAIN_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_ACTIVIDADES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_GRUPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_PROYECTOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.PR_SUBPROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RP_ABONOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ANTICIPO = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        NUM_ABONO = c.Int(nullable: false),
                        FECHA_ABONNO = c.DateTime(nullable: false),
                        DETALLE_ABONO = c.String(nullable: false, maxLength: 255),
                        CAPITAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        INTERES = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MORA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ASIENTO = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO, t.NUM_ABONO })
                .ForeignKey("dbo.RP_ANTICIPOS", t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO });
            
            CreateTable(
                "dbo.RP_ANTICIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ANTICIPO = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        FECHA_ANTICIPO = c.DateTime(nullable: false),
                        MONTO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DETALLE_ANTICIPO = c.String(nullable: false, maxLength: 400),
                        VALOR_ANTICIPO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NUM_CUOTAS = c.Int(nullable: false),
                        DIVIDENDO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_CUOTADIC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ABONO_ANTICIPO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SALDO_ANTICIPO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_AUTORIZADO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DESDE = c.Int(nullable: false),
                        HASTA = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        ESTADO = c.String(nullable: false, maxLength: 1),
                        NUMP_CONTAB = c.Int(nullable: false),
                        NUM_TRANSFER = c.Int(nullable: false),
                        TIPO = c.String(nullable: false, maxLength: 1),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO })
                .ForeignKey("dbo.RP_TIPOSANTICIPO", t => new { t.ID_INSTITUCION, t.ID_TIPOANTICIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOANTICIPO });
            
            CreateTable(
                "dbo.RP_CUOTASANTICIPO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ANTICIPO = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        ANIOMESCUOTA = c.Int(nullable: false),
                        FECHA_VENCE = c.DateTime(nullable: false),
                        CAPITAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        INTERES = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_CUOTA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ABONO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PAGOROL = c.Boolean(nullable: false),
                        DESCONTADO = c.Boolean(nullable: false),
                        ANIOMESDESC = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO, t.ANIOMESCUOTA })
                .ForeignKey("dbo.RP_ANTICIPOS", t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO });
            
            CreateTable(
                "dbo.TiposDocs",
                c => new
                    {
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        DESCRIPCION = c.String(nullable: false, maxLength: 60),
                        TIPO_SRI = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.COD_TIPO_DOC);
            
            CreateTable(
                "dbo.TiposSRIs",
                c => new
                    {
                        CODIGO = c.String(nullable: false, maxLength: 2),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CODIGO);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        NumIdentif = c.String(),
                        TypeUser = c.String(),
                        Active = c.Boolean(nullable: false),
                        FechaReg = c.DateTime(nullable: false),
                        CODIGO_ROL = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RP_ANTICIPOS", new[] { "ID_INSTITUCION", "ID_TIPOANTICIPO" }, "dbo.RP_TIPOSANTICIPO");
            DropForeignKey("dbo.RP_CUOTASANTICIPO", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" }, "dbo.RP_ANTICIPOS");
            DropForeignKey("dbo.RP_ABONOS", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" }, "dbo.RP_ANTICIPOS");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO" }, "dbo.PR_SUBPROGRAMAS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.PR_PROYECTOS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" }, "dbo.PR_PROGRAMAS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" }, "dbo.PR_GRUPOS");
            DropForeignKey("dbo.PR_DETINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAIN_CODIGO" }, "dbo.PR_PARTINGRESOS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.PR_ACTIVIDADES");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_DOCUMMOVINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" }, "dbo.PR_MOVINGRESO");
            DropForeignKey("dbo.PR_DETINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" }, "dbo.PR_MOVINGRESO");
            DropForeignKey("dbo.PR_MOVINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_DETGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAEG_CODIGO" }, "dbo.PR_PARTEGRESOS");
            DropForeignKey("dbo.PR_MOVGASTO", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.PR_DOCUMMOVGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" }, "dbo.PR_MOVGASTO");
            DropForeignKey("dbo.PR_DETGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" }, "dbo.PR_MOVGASTO");
            DropForeignKey("dbo.PR_MOVGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_CERTDISPRESUP", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.PR_DOCUMCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" }, "dbo.PR_CERTDISPRESUP");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO" }, "dbo.PR_SUBPROGRAMAS");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.PR_PROYECTOS");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" }, "dbo.PR_PROGRAMAS");
            DropForeignKey("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" }, "dbo.PR_PROGRAMAS");
            DropForeignKey("dbo.PR_PROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" }, "dbo.PR_GRUPOS");
            DropForeignKey("dbo.PR_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" }, "dbo.PR_GRUPOS");
            DropForeignKey("dbo.PR_GRUPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAEG_CODIGO" }, "dbo.PR_PARTEGRESOS");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.PR_ACTIVIDADES");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" }, "dbo.PR_FONDOS");
            DropForeignKey("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" }, "dbo.PR_CERTDISPRESUP");
            DropForeignKey("dbo.PR_CERTDISPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_PROYECTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO" }, "dbo.PR_SUBPROGRAMAS");
            DropForeignKey("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.PR_ACTIVIDADES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.PR_PROYECTOS");
            DropForeignKey("dbo.POA_POL_PDOT", "COD_OE_PDOT", "dbo.POA_OE_PDOT");
            DropForeignKey("dbo.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" }, "dbo.POA_POL_PDOT");
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_PNVB", "dbo.POA_PNBV");
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_OEI", "dbo.POA_OEI");
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_OE_PDOT", "dbo.POA_OE_PDOT");
            DropForeignKey("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" }, "dbo.POA_META_PDOT");
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_EPG", "dbo.POA_EPG");
            DropForeignKey("dbo.POA_EPG", "COD_PR_PDOT", "dbo.POA_PR_PDOT");
            DropForeignKey("dbo.POA_OE_PLANIF", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.POA_OE_PDOT", "COD_ED_PDOT", "dbo.POA_ED_PDOT");
            DropForeignKey("dbo.Mnu_Opciones", "COD_MODULO", "dbo.Mnu_Modulos");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" }, "dbo.SubProgramas");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" }, "dbo.Proyectoes");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.PartIngresoes", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "dbo.Grupoes");
            DropForeignKey("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.Actividads");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" }, "dbo.SubProgramas");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" }, "dbo.Proyectoes");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.PartEgresoes", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "dbo.Grupoes");
            DropForeignKey("dbo.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "dbo.Grupoes");
            DropForeignKey("dbo.Items", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Grupoes", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.DETALLEGRESOS", new[] { "ID_INSTITUCION", "PAEG_CODIGO" }, "dbo.PartEgresoes");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.Actividads");
            DropForeignKey("dbo.CO_CTASPPAGAR", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.CO_CTASPPAGAR", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, "dbo.CO_DETCTASPPAG");
            DropForeignKey("dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" }, "dbo.CO_CTASPPAGAR");
            DropForeignKey("dbo.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" }, "dbo.SubProgramas");
            DropForeignKey("dbo.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.RP_VARIABLES", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_TIRENTA", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_TIPOSANTICIPO", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_TGTOSPERS", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_SETUP", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Programas", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_SECUENCIAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_RELACONTAB", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.Periodos", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.IN_TIPOSTRAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_PARAM", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_LINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_SUBLINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_LINEA" }, "dbo.IN_LINEANEGOCIO");
            DropForeignKey("dbo.IN_COMPRAS", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" }, "dbo.IN_COMPRAS");
            DropForeignKey("dbo.FIRMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.FIRMAS", "CODIGO_DOC", "dbo.Documentos");
            DropForeignKey("dbo.FC_TIPOSERVICIO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.FC_SERVICIOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_TIPO" }, "dbo.FC_TIPOSERVICIO");
            DropForeignKey("dbo.FC_DETALLESERVICIOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_TIPO", "ID_SERVICIO" }, "dbo.FC_SERVICIOS");
            DropForeignKey("dbo.FC_CAJAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.FC_SECUENCIAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA" }, "dbo.FC_CAJAS");
            DropForeignKey("dbo.FC_DETALLESERVICIOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA", "NUM_FACTURA" }, "dbo.FC_FACTURAS");
            DropForeignKey("dbo.IN_CLASES", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_SUBTIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" }, "dbo.IN_TIPOS");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" }, "dbo.IN_SUBTIPOS");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" }, "dbo.IN_TIPOS");
            DropForeignKey("dbo.IN_TIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" }, "dbo.IN_CLASES");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" }, "dbo.IN_CLASES");
            DropForeignKey("dbo.IN_CARGA_INI", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" }, "dbo.IN_ITEMS");
            DropForeignKey("dbo.IN_CARACGEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" }, "dbo.IN_PRESENTA");
            DropForeignKey("dbo.IN_PRESENTA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" }, "dbo.IN_CARACGEN");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" }, "dbo.IN_CARACGEN");
            DropForeignKey("dbo.IN_BODEGAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_USERSXBOD", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" }, "dbo.IN_BODEGAS");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" }, "dbo.IN_BODEGAS");
            DropForeignKey("dbo.IN_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_CUENTASCONT", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.IN_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" }, "dbo.IN_BODEGAS");
            DropForeignKey("dbo.FC_DETALLEFACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" }, "dbo.IN_ITEMS");
            DropForeignKey("dbo.FC_DETALLEFACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA", "NUM_FACTURA" }, "dbo.FC_FACTURAS");
            DropForeignKey("dbo.FC_CIERRES", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.FC_FACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA", "NUM_TRANSAC" }, "dbo.FC_CIERRES");
            DropForeignKey("dbo.FC_FACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA" }, "dbo.FC_CAJAS");
            DropForeignKey("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_CUENBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_CONCILIACIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CUENTA" }, "dbo.CO_CUENBANCOS");
            DropForeignKey("dbo.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_TIPOROL" }, "dbo.RP_TIPOROL");
            DropForeignKey("dbo.RP_EMPLEADOS", "ID_RELALABORAL", "dbo.RP_RELACIONLABORAL");
            DropForeignKey("dbo.RP_OTROSMOV", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropForeignKey("dbo.RP_HORASEXT", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropForeignKey("dbo.RP_RUBROS", new[] { "ID_INSTITUCION", "ID_TIPOROL" }, "dbo.RP_TIPOROL");
            DropForeignKey("dbo.RP_ROLESXRUBRO", new[] { "ID_INSTITUCION", "COD_RUBRO" }, "dbo.RP_RUBROS");
            DropForeignKey("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "COD_RUBRO" }, "dbo.RP_RUBROS");
            DropForeignKey("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropForeignKey("dbo.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_CARGO" }, "dbo.RP_CARGOS");
            DropForeignKey("dbo.RP_CARGOS", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_CARGASFAMILIARES", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropForeignKey("dbo.POA_PLAN", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropForeignKey("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropForeignKey("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "COD_TIPOPROY" }, "dbo.POA_TIPOPROYECTOS");
            DropForeignKey("dbo.POA_TIPOPROYECTOS", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO" }, "dbo.POA_PLAN");
            DropForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" }, "dbo.POA_PLANOBJETIVOS");
            DropForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" }, "dbo.Catalogos");
            DropForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "dbo.TipoRecursos");
            DropForeignKey("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "dbo.TipoRecursos");
            DropForeignKey("dbo.TECHOSPRESUPs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.PR_DETALSOLICDISPPRES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_DOC", "ORG_CODIGO", "NUM_SOLIC" }, "dbo.PR_SOLICDISPPRESUP");
            DropForeignKey("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.POA_INDICADOROBJ", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" }, "dbo.POA_PLANOBJETIVOS");
            DropForeignKey("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" }, "dbo.PR_FONDOS");
            DropForeignKey("dbo.PR_FONDOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" }, "dbo.POA_PLANOBJETIVOS");
            DropForeignKey("dbo.POA_PLAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.RP_EMPLEADOS", "ID_NACIONALIDAD", "dbo.Nacionalidades");
            DropForeignKey("dbo.RP_ASIGNACUENTAS", new[] { "ID_INSTITUCION", "ID_TIPOROL" }, "dbo.RP_TIPOROL");
            DropForeignKey("dbo.RP_TIPOROL", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.RP_TIPOROL", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_PORCAPORTACION", "ID_TIPOSEGURO", "dbo.RP_TIPOSEGURO");
            DropForeignKey("dbo.RP_RECEPFONDOS", "ID_PORCENT", "dbo.RP_PORCAPORTACION");
            DropForeignKey("dbo.RP_RECEPFONDOS", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.Parroquias", "ID_PROVINCIA", "dbo.Provincias");
            DropForeignKey("dbo.Cantones", "ID_PROVINCIA", "dbo.Provincias");
            DropForeignKey("dbo.Parroquias", new[] { "ID_PROVINCIA", "ID_CANTON" }, "dbo.Cantones");
            DropForeignKey("dbo.Proveedores", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" }, "dbo.Parroquias");
            DropForeignKey("dbo.Proveedores", "CODIGO_BANCO", "dbo.BANCOS");
            DropForeignKey("dbo.CO_CUENBANCOS", "CODIGO_BANCO", "dbo.BANCOS");
            DropForeignKey("dbo.CO_ASIENTOSCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_LINEACREDITO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CUENTA" }, "dbo.CO_LINEACREDITO");
            DropForeignKey("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" }, "dbo.CO_ASIENTOSCONT");
            DropForeignKey("dbo.CO_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" }, "dbo.CO_CUENTASCONT");
            DropForeignKey("dbo.CO_CLASIFRETEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_RET" }, "dbo.CO_CLASIFRETEN");
            DropForeignKey("dbo.CO_RELACION", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_RELACION", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" }, "dbo.CO_CUENTASCONT");
            DropForeignKey("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" }, "dbo.CO_CUENTASCONT");
            DropForeignKey("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" }, "dbo.CO_ASIENTOSCONT");
            DropForeignKey("dbo.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" }, "dbo.Proyectoes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.RP_CUOTASANTICIPO", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" });
            DropIndex("dbo.RP_ANTICIPOS", new[] { "ID_INSTITUCION", "ID_TIPOANTICIPO" });
            DropIndex("dbo.RP_ABONOS", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" });
            DropIndex("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_DOCUMMOVINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" });
            DropIndex("dbo.PR_MOVINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_DETINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAIN_CODIGO" });
            DropIndex("dbo.PR_DETINGRESO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" });
            DropIndex("dbo.PR_DOCUMMOVGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" });
            DropIndex("dbo.PR_MOVGASTO", new[] { "COD_PROV" });
            DropIndex("dbo.PR_MOVGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_DETGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAEG_CODIGO" });
            DropIndex("dbo.PR_DETGASTO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" });
            DropIndex("dbo.PR_DOCUMCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" });
            DropIndex("dbo.PR_PROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" });
            DropIndex("dbo.PR_GRUPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" });
            DropIndex("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAEG_CODIGO" });
            DropIndex("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" });
            DropIndex("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" });
            DropIndex("dbo.PR_CERTDISPRESUP", new[] { "COD_PROV" });
            DropIndex("dbo.PR_CERTDISPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" });
            DropIndex("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.PR_PROYECTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO" });
            DropIndex("dbo.PR_ACTIVIDADES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" });
            DropIndex("dbo.POA_EPG", new[] { "COD_PR_PDOT" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_EPG" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_OEI" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_PNVB" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" });
            DropIndex("dbo.POA_POL_PDOT", new[] { "COD_OE_PDOT" });
            DropIndex("dbo.POA_OE_PDOT", new[] { "COD_ED_PDOT" });
            DropIndex("dbo.Mnu_Opciones", new[] { "COD_MODULO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
            DropIndex("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" });
            DropIndex("dbo.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.Items", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.Grupoes", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PAEG_CLAVE" });
            DropIndex("dbo.DETALLEGRESOS", new[] { "ID_INSTITUCION", "PAEG_CODIGO" });
            DropIndex("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" });
            DropIndex("dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" });
            DropIndex("dbo.CO_CTASPPAGAR", new[] { "COD_PROV" });
            DropIndex("dbo.CO_CTASPPAGAR", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.RP_VARIABLES", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_TIRENTA", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_TIPOSANTICIPO", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_TGTOSPERS", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_SETUP", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_SECUENCIAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_RELACONTAB", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            DropIndex("dbo.IN_TIPOSTRAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_PARAM", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_SUBLINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_LINEA" });
            DropIndex("dbo.IN_LINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" });
            DropIndex("dbo.IN_COMPRAS", new[] { "COD_PROV", "NUM_DOC" });
            DropIndex("dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.FIRMAS", new[] { "CODIGO_DOC" });
            DropIndex("dbo.FIRMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.FC_SERVICIOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_TIPO" });
            DropIndex("dbo.FC_TIPOSERVICIO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.FC_SECUENCIAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA" });
            DropIndex("dbo.FC_DETALLESERVICIOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_TIPO", "ID_SERVICIO" });
            DropIndex("dbo.FC_DETALLESERVICIOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA", "NUM_FACTURA" });
            DropIndex("dbo.IN_SUBTIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" });
            DropIndex("dbo.IN_TIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" });
            DropIndex("dbo.IN_CLASES", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_CARGA_INI", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" });
            DropIndex("dbo.IN_PRESENTA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" });
            DropIndex("dbo.IN_CARACGEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_USERSXBOD", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" });
            DropIndex("dbo.IN_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" });
            DropIndex("dbo.IN_BODEGAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" });
            DropIndex("dbo.FC_DETALLEFACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" });
            DropIndex("dbo.FC_DETALLEFACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA", "NUM_FACTURA" });
            DropIndex("dbo.FC_CIERRES", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.FC_FACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA", "NUM_TRANSAC" });
            DropIndex("dbo.FC_FACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CAJA" });
            DropIndex("dbo.FC_CAJAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_CONCILIACIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CUENTA" });
            DropIndex("dbo.RP_OTROSMOV", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropIndex("dbo.RP_HORASEXT", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropIndex("dbo.RP_ROLESXRUBRO", new[] { "ID_INSTITUCION", "COD_RUBRO" });
            DropIndex("dbo.RP_RUBROS", new[] { "ID_INSTITUCION", "ID_TIPOROL" });
            DropIndex("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "COD_RUBRO" });
            DropIndex("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropIndex("dbo.RP_CARGOS", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_CARGASFAMILIARES", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropIndex("dbo.POA_TIPOPROYECTOS", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("dbo.TECHOSPRESUPs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("dbo.PR_DETALSOLICDISPPRES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_DOC", "ORG_CODIGO", "NUM_SOLIC" });
            DropIndex("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            DropIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" });
            DropIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" });
            DropIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            DropIndex("dbo.POA_INDICADOROBJ", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" });
            DropIndex("dbo.PR_FONDOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" });
            DropIndex("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" });
            DropIndex("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropIndex("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "COD_TIPOPROY" });
            DropIndex("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO" });
            DropIndex("dbo.POA_PLAN", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropIndex("dbo.POA_PLAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.RP_EMPLEADOS", new[] { "ID_RELALABORAL" });
            DropIndex("dbo.RP_EMPLEADOS", new[] { "ID_NACIONALIDAD" });
            DropIndex("dbo.RP_EMPLEADOS", "IX_ID_INSTITUCION_NUM_CEDULA");
            DropIndex("dbo.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_TIPOROL" });
            DropIndex("dbo.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_CARGO" });
            DropIndex("dbo.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "APELLIDOS", "NOMBRES" });
            DropIndex("dbo.RP_ASIGNACUENTAS", new[] { "ID_INSTITUCION", "ID_TIPOROL" });
            DropIndex("dbo.RP_TIPOROL", new[] { "COD_PROV" });
            DropIndex("dbo.RP_TIPOROL", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_PORCAPORTACION", new[] { "ID_TIPOSEGURO" });
            DropIndex("dbo.RP_RECEPFONDOS", new[] { "ID_PORCENT" });
            DropIndex("dbo.RP_RECEPFONDOS", new[] { "COD_PROV" });
            DropIndex("dbo.Cantones", new[] { "ID_PROVINCIA" });
            DropIndex("dbo.Parroquias", new[] { "ID_PROVINCIA", "ID_CANTON" });
            DropIndex("dbo.Proveedores", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" });
            DropIndex("dbo.Proveedores", new[] { "CODIGO_BANCO" });
            DropIndex("dbo.CO_CUENBANCOS", new[] { "CODIGO_BANCO" });
            DropIndex("dbo.CO_CUENBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_LINEACREDITO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CUENTA" });
            DropIndex("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" });
            DropIndex("dbo.CO_CLASIFRETEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" });
            DropIndex("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_RET" });
            DropIndex("dbo.CO_RELACION", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" });
            DropIndex("dbo.CO_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" });
            DropIndex("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" });
            DropIndex("dbo.CO_ASIENTOSCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TIPO_COMPROB", "NUM_COMPROB" });
            DropIndex("dbo.Periodos", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.Programas", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" });
            DropIndex("dbo.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TiposSRIs");
            DropTable("dbo.TiposDocs");
            DropTable("dbo.RP_CUOTASANTICIPO");
            DropTable("dbo.RP_ANTICIPOS");
            DropTable("dbo.RP_ABONOS");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PR_PARTINGRESOS");
            DropTable("dbo.PR_DOCUMMOVINGRESO");
            DropTable("dbo.PR_MOVINGRESO");
            DropTable("dbo.PR_DETINGRESO");
            DropTable("dbo.PR_DOCUMMOVGASTO");
            DropTable("dbo.PR_MOVGASTO");
            DropTable("dbo.PR_DETGASTO");
            DropTable("dbo.PR_DOCUMCERTDISP");
            DropTable("dbo.PR_PROGRAMAS");
            DropTable("dbo.PR_ITEMS");
            DropTable("dbo.PR_GRUPOS");
            DropTable("dbo.PR_PARTEGRESOS");
            DropTable("dbo.PR_DETCERTDISP");
            DropTable("dbo.PR_CERTDISPRESUP");
            DropTable("dbo.PR_SUBPROGRAMAS");
            DropTable("dbo.PR_PROYECTOS");
            DropTable("dbo.PR_ACTIVIDADES");
            DropTable("dbo.POA_PNBV");
            DropTable("dbo.POA_OEI");
            DropTable("dbo.POA_PR_PDOT");
            DropTable("dbo.POA_EPG");
            DropTable("dbo.POA_OE_PLANIF");
            DropTable("dbo.POA_META_PDOT");
            DropTable("dbo.POA_POL_PDOT");
            DropTable("dbo.POA_OE_PDOT");
            DropTable("dbo.POA_ED_PDOT");
            DropTable("dbo.POA_EAI");
            DropTable("dbo.Mnu_Modulos");
            DropTable("dbo.Mnu_Opciones");
            DropTable("dbo.PartIngresoes");
            DropTable("dbo.DETALLEINGRESOS");
            DropTable("dbo.Items");
            DropTable("dbo.Grupoes");
            DropTable("dbo.PartEgresoes");
            DropTable("dbo.DETALLEGRESOS");
            DropTable("dbo.CO_RETENCIONES");
            DropTable("dbo.CO_DETCTASPPAG");
            DropTable("dbo.CO_CTASPPAGAR");
            DropTable("dbo.RP_VARIABLES");
            DropTable("dbo.RP_TIRENTA");
            DropTable("dbo.RP_TIPOSANTICIPO");
            DropTable("dbo.RP_TGTOSPERS");
            DropTable("dbo.RP_SETUP");
            DropTable("dbo.RP_SECUENCIAS");
            DropTable("dbo.PR_RELACONTAB");
            DropTable("dbo.Mnu_Roles");
            DropTable("dbo.IN_TIPOSTRAN");
            DropTable("dbo.IN_PARAM");
            DropTable("dbo.IN_SUBLINEANEGOCIO");
            DropTable("dbo.IN_LINEANEGOCIO");
            DropTable("dbo.IN_DETCOMPRA");
            DropTable("dbo.IN_COMPRAS");
            DropTable("dbo.Documentos");
            DropTable("dbo.FIRMAS");
            DropTable("dbo.FC_SERVICIOS");
            DropTable("dbo.FC_TIPOSERVICIO");
            DropTable("dbo.FC_SECUENCIAS");
            DropTable("dbo.FC_DETALLESERVICIOS");
            DropTable("dbo.IN_SUBTIPOS");
            DropTable("dbo.IN_TIPOS");
            DropTable("dbo.IN_CLASES");
            DropTable("dbo.IN_CARGA_INI");
            DropTable("dbo.IN_PRESENTA");
            DropTable("dbo.IN_CARACGEN");
            DropTable("dbo.IN_USERSXBOD");
            DropTable("dbo.IN_CUENTASCONT");
            DropTable("dbo.IN_BODEGAS");
            DropTable("dbo.IN_ITEMS");
            DropTable("dbo.FC_DETALLEFACTURAS");
            DropTable("dbo.FC_CIERRES");
            DropTable("dbo.FC_FACTURAS");
            DropTable("dbo.FC_CAJAS");
            DropTable("dbo.CO_PARAMETROS");
            DropTable("dbo.CO_CONCILIACIONES");
            DropTable("dbo.RP_RELACIONLABORAL");
            DropTable("dbo.RP_OTROSMOV");
            DropTable("dbo.RP_HORASEXT");
            DropTable("dbo.RP_ROLESXRUBRO");
            DropTable("dbo.RP_RUBROS");
            DropTable("dbo.RP_DETALLEROL");
            DropTable("dbo.RP_CARGOS");
            DropTable("dbo.RP_CARGASFAMILIARES");
            DropTable("dbo.POA_TIPOPROYECTOS");
            DropTable("dbo.TipoRecursos");
            DropTable("dbo.TECHOSPRESUPs");
            DropTable("dbo.PR_DETALSOLICDISPPRES");
            DropTable("dbo.PR_SOLICDISPPRESUP");
            DropTable("dbo.OrganicoFs");
            DropTable("dbo.Catalogos");
            DropTable("dbo.POA_OBJTIVORECURSOS");
            DropTable("dbo.POA_INDICADOROBJ");
            DropTable("dbo.PR_FONDOS");
            DropTable("dbo.POA_FONDOXOBJTVO");
            DropTable("dbo.POA_PLANOBJETIVOS");
            DropTable("dbo.POA_PLAN");
            DropTable("dbo.Nacionalidades");
            DropTable("dbo.RP_EMPLEADOS");
            DropTable("dbo.RP_ASIGNACUENTAS");
            DropTable("dbo.RP_TIPOROL");
            DropTable("dbo.RP_TIPOSEGURO");
            DropTable("dbo.RP_PORCAPORTACION");
            DropTable("dbo.RP_RECEPFONDOS");
            DropTable("dbo.Cantones");
            DropTable("dbo.Provincias");
            DropTable("dbo.Parroquias");
            DropTable("dbo.Proveedores");
            DropTable("dbo.BANCOS");
            DropTable("dbo.CO_CUENBANCOS");
            DropTable("dbo.CO_LINEACREDITO");
            DropTable("dbo.CO_DETALLEBANCOS");
            DropTable("dbo.CO_CLASIFRETEN");
            DropTable("dbo.CO_RELARETCONT");
            DropTable("dbo.CO_RELACION");
            DropTable("dbo.CO_CUENTASCONT");
            DropTable("dbo.CO_DETALLEASIENTOS");
            DropTable("dbo.CO_ASIENTOSCONT");
            DropTable("dbo.Periodos");
            DropTable("dbo.Instituciones");
            DropTable("dbo.Programas");
            DropTable("dbo.SubProgramas");
            DropTable("dbo.Proyectoes");
            DropTable("dbo.Actividads");
        }
    }
}
