namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actividads",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .ForeignKey("dbo.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO });
            
            CreateTable(
                "dbo.Proyectoes",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROY_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO })
                .ForeignKey("dbo.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO });
            
            CreateTable(
                "dbo.SubProgramas",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 230),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO })
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
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
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
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
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
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
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
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
                "dbo.Cantones",
                c => new
                    {
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON })
                .ForeignKey("dbo.Provincias", t => t.ID_PROVINCIA, cascadeDelete: true)
                .Index(t => t.ID_PROVINCIA);
            
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
                .ForeignKey("dbo.Provincias", t => t.ID_PROVINCIA, cascadeDelete: true)
                .ForeignKey("dbo.Cantones", t => new { t.ID_PROVINCIA, t.ID_CANTON }, cascadeDelete: true)
                .Index(t => t.ID_PROVINCIA)
                .Index(t => new { t.ID_PROVINCIA, t.ID_CANTON });
            
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
                        INSTIT_PUB = c.Int(nullable: false),
                        SEXO = c.Int(nullable: false),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        CODI_CLASRET = c.String(maxLength: 10),
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(nullable: false, maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.COD_PROV)
                .ForeignKey("dbo.Parroquias", t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA }, cascadeDelete: true)
                .Index(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA });
            
            CreateTable(
                "dbo.Provincias",
                c => new
                    {
                        ID_PROVINCIA = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_PROVINCIA);
            
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
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CAT_CODIGO })
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.TipoRecursos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO }, cascadeDelete: true)
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
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
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
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO });
            
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
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
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
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("dbo.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION)
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.MnuModulos",
                c => new
                    {
                        COD_MODULO = c.Int(nullable: false),
                        NOM_MODULO = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.COD_MODULO);
            
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
                .ForeignKey("dbo.Mnu_Modulos", t => t.COD_MODULO, cascadeDelete: true)
                .ForeignKey("dbo.MnuModulos", t => t.COD_MODULO, cascadeDelete: true)
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
                .ForeignKey("dbo.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PAEG_CLAVE }, unique: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO });
            
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
                .ForeignKey("dbo.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PAIN_CLAVE }, unique: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO });
            
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
                .ForeignKey("dbo.POA_ED_PDOT", t => t.COD_ED_PDOT, cascadeDelete: true)
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
                .ForeignKey("dbo.POA_OE_PDOT", t => t.COD_OE_PDOT, cascadeDelete: true)
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
                .ForeignKey("dbo.POA_POL_PDOT", t => new { t.COD_OE_PDOT, t.COD_POL_PDOT }, cascadeDelete: true)
                .Index(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT });
            
            CreateTable(
                "dbo.POA_EPG",
                c => new
                    {
                        COD_EPG = c.Int(nullable: false, identity: true),
                        NOM_EPG = c.String(nullable: false, maxLength: 100),
                        COD_PR_PDOT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COD_EPG)
                .ForeignKey("dbo.POA_PR_PDOT", t => t.COD_PR_PDOT, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.POA_EPG", "COD_PR_PDOT", "dbo.POA_PR_PDOT");
            DropForeignKey("dbo.POA_POL_PDOT", "COD_OE_PDOT", "dbo.POA_OE_PDOT");
            DropForeignKey("dbo.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" }, "dbo.POA_POL_PDOT");
            DropForeignKey("dbo.POA_OE_PDOT", "COD_ED_PDOT", "dbo.POA_ED_PDOT");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" }, "dbo.SubProgramas");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.Proyectoes");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.PartIngresoes", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "dbo.Grupoes");
            DropForeignKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.Actividads");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" }, "dbo.SubProgramas");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.Proyectoes");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.PartEgresoes", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "dbo.Grupoes");
            DropForeignKey("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.Actividads");
            DropForeignKey("dbo.Mnu_Opciones", "COD_MODULO", "dbo.MnuModulos");
            DropForeignKey("dbo.Mnu_Opciones", "COD_MODULO", "dbo.Mnu_Modulos");
            DropForeignKey("dbo.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "dbo.Grupoes");
            DropForeignKey("dbo.Items", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Grupoes", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "dbo.TipoRecursos");
            DropForeignKey("dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.Parroquias", new[] { "ID_PROVINCIA", "ID_CANTON" }, "dbo.Cantones");
            DropForeignKey("dbo.Parroquias", "ID_PROVINCIA", "dbo.Provincias");
            DropForeignKey("dbo.Cantones", "ID_PROVINCIA", "dbo.Provincias");
            DropForeignKey("dbo.Proveedores", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" }, "dbo.Parroquias");
            DropForeignKey("dbo.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" }, "dbo.SubProgramas");
            DropForeignKey("dbo.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.Programas", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.Periodos", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.Proyectoes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.POA_EPG", new[] { "COD_PR_PDOT" });
            DropIndex("dbo.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" });
            DropIndex("dbo.POA_POL_PDOT", new[] { "COD_OE_PDOT" });
            DropIndex("dbo.POA_OE_PDOT", new[] { "COD_ED_PDOT" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PAEG_CLAVE" });
            DropIndex("dbo.Mnu_Opciones", new[] { "COD_MODULO" });
            DropIndex("dbo.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.Items", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.Grupoes", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            DropIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("dbo.Proveedores", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" });
            DropIndex("dbo.Parroquias", new[] { "ID_PROVINCIA", "ID_CANTON" });
            DropIndex("dbo.Parroquias", new[] { "ID_PROVINCIA" });
            DropIndex("dbo.Cantones", new[] { "ID_PROVINCIA" });
            DropIndex("dbo.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            DropIndex("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.Periodos", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.Programas", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" });
            DropIndex("dbo.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TiposSRIs");
            DropTable("dbo.TiposDocs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.POA_PNBV");
            DropTable("dbo.POA_OEI");
            DropTable("dbo.POA_PR_PDOT");
            DropTable("dbo.POA_EPG");
            DropTable("dbo.POA_META_PDOT");
            DropTable("dbo.POA_POL_PDOT");
            DropTable("dbo.POA_OE_PDOT");
            DropTable("dbo.POA_ED_PDOT");
            DropTable("dbo.POA_EAI");
            DropTable("dbo.PartIngresoes");
            DropTable("dbo.PartEgresoes");
            DropTable("dbo.Mnu_Modulos");
            DropTable("dbo.Mnu_Opciones");
            DropTable("dbo.MnuModulos");
            DropTable("dbo.Items");
            DropTable("dbo.Grupoes");
            DropTable("dbo.TipoRecursos");
            DropTable("dbo.OrganicoFs");
            DropTable("dbo.Catalogos");
            DropTable("dbo.Provincias");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Parroquias");
            DropTable("dbo.Cantones");
            DropTable("dbo.Mnu_Roles");
            DropTable("dbo.CO_PARAMETROS");
            DropTable("dbo.Periodos");
            DropTable("dbo.Instituciones");
            DropTable("dbo.Programas");
            DropTable("dbo.SubProgramas");
            DropTable("dbo.Proyectoes");
            DropTable("dbo.Actividads");
        }
    }
}
