namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SIGEFI3.Actividads",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .ForeignKey("SIGEFI3.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO });
            
            CreateTable(
                "SIGEFI3.Proyectoes",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROY_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO })
                .ForeignKey("SIGEFI3.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO });
            
            CreateTable(
                "SIGEFI3.SubProgramas",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 230),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO })
                .ForeignKey("SIGEFI3.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO });
            
            CreateTable(
                "SIGEFI3.Programas",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PROG_CODIGO })
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "SIGEFI3.Instituciones",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TIPO_PRESUP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
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
                        FECHA_INICIO = c.DateTime(nullable: false),
                        CONTRIB_ESP = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID_INSTITUCION);
            
            CreateTable(
                "SIGEFI3.Periodos",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PERI_DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        ACTIVO = c.Decimal(nullable: false, precision: 1, scale: 0),
                        FECHA_CREA = c.DateTime(),
                        CERRADO = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "SIGEFI3.CO_PARAMETROS",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ANIO_CODIGO = c.String(nullable: false, maxLength: 4),
                        MESINI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MESPROC = c.Decimal(nullable: false, precision: 10, scale: 0),
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
                        COMPDIAR = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COMPEGRE = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COMPINGRE = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUMASIEN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUMASIREFO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_TRANSAC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_CERTDISP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_COMPROMISO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_TRANSFER = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_DEVEN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PORIVA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_DECIMAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_PEDIDOCOMPRAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_ORDENPAGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_ITEM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DEVENGAR_CONTAB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PAGAR_CONTAB = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IVA_GASTO = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ANIO_CODIGO })
                .ForeignKey("SIGEFI3.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "SIGEFI3.Mnu_Roles",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CODIGO_ROL = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NOMBRE_ROL = c.String(nullable: false, maxLength: 30),
                        USUA_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USUA_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                        PERIODOS_ID_INSTITUCION = c.Decimal(precision: 10, scale: 0),
                        PERIODOS_PERI_CODIGO = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.CODIGO_ROL })
                .ForeignKey("SIGEFI3.Periodos", t => new { t.PERIODOS_ID_INSTITUCION, t.PERIODOS_PERI_CODIGO })
                .Index(t => new { t.PERIODOS_ID_INSTITUCION, t.PERIODOS_PERI_CODIGO });
            
            CreateTable(
                "SIGEFI3.POA_PLAN",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_PLAN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ORG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_EMPLEADO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DETALE = c.String(nullable: false, maxLength: 250),
                        FECHA_PLAN = c.DateTime(nullable: false),
                        ESTADO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                        RP_EMPLEADOS_ID_INSTITUCION = c.Decimal(precision: 10, scale: 0),
                        RP_EMPLEADOS_ID_CARGO = c.Decimal(precision: 10, scale: 0),
                        RP_EMPLEADOS_ID_EMPLEADO = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO })
                .ForeignKey("SIGEFI3.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.RP_EMPLEADOS", t => new { t.RP_EMPLEADOS_ID_INSTITUCION, t.RP_EMPLEADOS_ID_CARGO, t.RP_EMPLEADOS_ID_EMPLEADO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.RP_EMPLEADOS_ID_INSTITUCION, t.RP_EMPLEADOS_ID_CARGO, t.RP_EMPLEADOS_ID_EMPLEADO });
            
            CreateTable(
                "SIGEFI3.RP_EMPLEADOS",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CARGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_EMPLEADO = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NUM_CEDULA = c.String(nullable: false, maxLength: 10),
                        NUM_CEDMIL = c.String(maxLength: 12),
                        APELLIDOS = c.String(nullable: false, maxLength: 30),
                        NOMBRES = c.String(nullable: false, maxLength: 30),
                        IESS = c.String(maxLength: 1),
                        FECHA_NAC = c.DateTime(nullable: false),
                        ID_NACIONALIDAD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ESTADO_CIVIL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ISTRUCCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CANTON = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_PARROQUIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DIRECCIONDOM = c.String(nullable: false, maxLength: 250),
                        NUM_TARJETA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TIPOSANGRE = c.String(nullable: false, maxLength: 4),
                        SEXO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FECHA_ENTRA = c.DateTime(nullable: false),
                        FECHA_REINGRESO = c.DateTime(nullable: false),
                        FECHA_SALE = c.DateTime(nullable: false),
                        ID_BANCO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TIPO_CUENTA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_CUENTA = c.String(nullable: false, maxLength: 10),
                        TELEFONO = c.String(nullable: false, maxLength: 10),
                        DISCAPACIDAD = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PORC_DISCAP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EMAIL = c.String(nullable: false, maxLength: 150),
                        REMUNUNIF = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUELDO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_RELALABORAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_GRADO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_NIVEL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ORG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TALLA_ROPA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TALLA_CALZADO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ID_TIPOROL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ACTIVO = c.Decimal(nullable: false, precision: 1, scale: 0),
                        OBSERVA = c.String(nullable: false, maxLength: 250),
                        LOG_USER = c.String(nullable: false, maxLength: 15),
                        FECHAMODIF = c.DateTime(nullable: false),
                        FOTO = c.Binary(),
                        CODIGO_CG = c.String(maxLength: 50),
                        OTROS1 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS2 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS3 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS4 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS5 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS6 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS7 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS8 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS9 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS10 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS11 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        OTROS12 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FACTOR_IR = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FECHA1 = c.DateTime(nullable: false),
                        FECHA2 = c.DateTime(nullable: false),
                        FECHA3 = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_CARGO, t.ID_EMPLEADO })
                .ForeignKey("SIGEFI3.RP_CARGOS", t => new { t.ID_INSTITUCION, t.ID_CARGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.RP_RELACIONLABORAL", t => t.ID_RELALABORAL, cascadeDelete: true)
                .ForeignKey("SIGEFI3.RP_TIPOROL", t => new { t.ID_INSTITUCION, t.ID_TIPOROL }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.ID_CARGO })
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOROL })
                .Index(t => t.ID_RELALABORAL);
            
            CreateTable(
                "SIGEFI3.RP_CARGOS",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CARGO = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
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
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "SIGEFI3.RP_RELACIONLABORAL",
                c => new
                    {
                        ID_RELALABORAL = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_RELALABORAL);
            
            CreateTable(
                "SIGEFI3.RP_TIPOROL",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_TIPOROL = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                        NUM_DIAS = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SECCION = c.String(nullable: false, maxLength: 1),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOROL })
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Proveedores", t => t.COD_PROV, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION)
                .Index(t => t.COD_PROV);
            
            CreateTable(
                "SIGEFI3.Proveedores",
                c => new
                    {
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUMCEDRUC_PROV = c.String(nullable: false, maxLength: 20),
                        TIPO_IDENTI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TIPO_PROV = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOMBRE = c.String(nullable: false, maxLength: 150),
                        DIRECCION = c.String(nullable: false, maxLength: 250),
                        TELEFONO = c.String(nullable: false, maxLength: 50),
                        CODIGO_ENVIO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NUM_CUENTA = c.String(nullable: false, maxLength: 18),
                        TIPO_CUENTA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TIPO_IDENTITRAN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RUCCEDU_TRAN = c.String(nullable: false, maxLength: 13),
                        RAZONSOCIAL = c.String(nullable: false, maxLength: 150),
                        CXC_PROVEED = c.String(maxLength: 50),
                        CXC_ANTICIPREC = c.String(maxLength: 50),
                        CXP_PROVEED = c.String(maxLength: 50),
                        CXP_ANTICIPENT = c.String(maxLength: 50),
                        CONTRIBESPECIAL = c.Decimal(nullable: false, precision: 1, scale: 0),
                        INSTIT_PUB = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SEXO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EMAIL = c.String(nullable: false, maxLength: 50),
                        CODI_CLASRET = c.String(maxLength: 10),
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CANTON = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_PARROQUIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(nullable: false, maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.COD_PROV)
                .ForeignKey("SIGEFI3.Parroquias", t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA }, cascadeDelete: true)
                .Index(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA });
            
            CreateTable(
                "SIGEFI3.Parroquias",
                c => new
                    {
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CANTON = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_PARROQUIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA })
                .ForeignKey("SIGEFI3.Cantones", t => new { t.ID_PROVINCIA, t.ID_CANTON }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Provincias", t => t.ID_PROVINCIA, cascadeDelete: true)
                .Index(t => new { t.ID_PROVINCIA, t.ID_CANTON });
            
            CreateTable(
                "SIGEFI3.Provincias",
                c => new
                    {
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_PROVINCIA);
            
            CreateTable(
                "SIGEFI3.Cantones",
                c => new
                    {
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CANTON = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON })
                .ForeignKey("SIGEFI3.Provincias", t => t.ID_PROVINCIA, cascadeDelete: true)
                .Index(t => t.ID_PROVINCIA);
            
            CreateTable(
                "SIGEFI3.Catalogos",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
                        CAT_NOMBRE = c.String(nullable: false, maxLength: 250),
                        CAT_VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        TRE_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TIPO_CALCULO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TIPO = c.String(nullable: false, maxLength: 1),
                        TIPO_GASTO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NORMALIZADO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        APLICA_SOLPAGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERTENECE_PAC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CONSOLIDA_PAC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ESTADO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ORG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CAT_CODIGO })
                .ForeignKey("SIGEFI3.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.TipoRecursos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO });
            
            CreateTable(
                "SIGEFI3.OrganicoFs",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ORG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NIV_PADRE = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NIV_HIJO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ORG_CLAVE = c.String(nullable: false, maxLength: 50),
                        ORG_NOMBRE = c.String(nullable: false, maxLength: 150),
                        DIRECTIVO = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO })
                .ForeignKey("SIGEFI3.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "SIGEFI3.TipoRecursos",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRE_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRE_NOMBRE = c.String(nullable: false, maxLength: 100),
                        ORG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO })
                .ForeignKey("SIGEFI3.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO });
            
            CreateTable(
                "SIGEFI3.Grupoes",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        GRUP_NOMBRE = c.String(nullable: false, maxLength: 100),
                        TIPO = c.String(maxLength: 1),
                        NIVEL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO })
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "SIGEFI3.Items",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_SUBITEM = c.String(nullable: false, maxLength: 3),
                        ITEM_NOMBRE = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ITEM_CLAVE })
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION)
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO });
            
            CreateTable(
                "SIGEFI3.MnuModulos",
                c => new
                    {
                        COD_MODULO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOM_MODULO = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.COD_MODULO);
            
            CreateTable(
                "SIGEFI3.Mnu_Opciones",
                c => new
                    {
                        COD_MODULO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_MENU = c.String(nullable: false, maxLength: 20),
                        DESC_MENU = c.String(maxLength: 80),
                        MAIN_NIVEL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOM_MENU = c.String(maxLength: 10),
                        SUB_NIVEL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EVENTO = c.String(maxLength: 30),
                        CONTROLADOR = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => new { t.COD_MODULO, t.COD_MENU })
                .ForeignKey("SIGEFI3.Mnu_Modulos", t => t.COD_MODULO, cascadeDelete: true)
                .ForeignKey("SIGEFI3.MnuModulos", t => t.COD_MODULO, cascadeDelete: true)
                .Index(t => t.COD_MODULO);
            
            CreateTable(
                "SIGEFI3.Mnu_Modulos",
                c => new
                    {
                        COD_MODULO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOM_MODULO = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.COD_MODULO);
            
            CreateTable(
                "SIGEFI3.PartEgresoes",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PAEG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAEG_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAEG_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ITEM_ORGAN = c.String(nullable: false, maxLength: 3),
                        ITEM_CORRE = c.String(nullable: false, maxLength: 3),
                        ITEM_FUNC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ITEM_ORIGTO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ITEM_UNIDOPTIVA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CTO_COSTO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SCTO_COSTO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CANTON = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_PARROQUIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PAEG_CODIGO })
                .ForeignKey("SIGEFI3.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PAEG_CLAVE }, unique: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO });
            
            CreateTable(
                "SIGEFI3.PartIngresoes",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PAIN_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAIN_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAIN_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ORGANISMO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CORRELAT = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PAIN_CODIGO })
                .ForeignKey("SIGEFI3.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PAIN_CLAVE }, unique: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROG_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO });
            
            CreateTable(
                "SIGEFI3.POA_EAI",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_EAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOM_EAI = c.String(nullable: false, maxLength: 1000),
                        META_EAI = c.String(nullable: false, maxLength: 500),
                        REFE_EAI = c.String(nullable: false, maxLength: 15),
                        PRIOR_EAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FECHAINI_EAI = c.DateTime(nullable: false),
                        FECHAFIN_EAI = c.DateTime(nullable: false),
                        OBSERVA_EAI = c.String(maxLength: 1000),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_EAI });
            
            CreateTable(
                "SIGEFI3.POA_ED_PDOT",
                c => new
                    {
                        COD_ED_PDOT = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NOM_ED_PDOT = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.COD_ED_PDOT);
            
            CreateTable(
                "SIGEFI3.POA_OE_PDOT",
                c => new
                    {
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        NOM_OE_PDOT = c.String(nullable: false, maxLength: 300),
                        ACTIVO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_ED_PDOT = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.COD_OE_PDOT)
                .ForeignKey("SIGEFI3.POA_ED_PDOT", t => t.COD_ED_PDOT, cascadeDelete: true)
                .Index(t => t.COD_ED_PDOT);
            
            CreateTable(
                "SIGEFI3.POA_POL_PDOT",
                c => new
                    {
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_POL_PDOT = c.String(nullable: false, maxLength: 4),
                        NOM_POL_PDOT = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT })
                .ForeignKey("SIGEFI3.POA_OE_PDOT", t => t.COD_OE_PDOT, cascadeDelete: true)
                .Index(t => t.COD_OE_PDOT);
            
            CreateTable(
                "SIGEFI3.POA_META_PDOT",
                c => new
                    {
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_POL_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_META_PDOT = c.String(nullable: false, maxLength: 4),
                        NOM_POL_PDOT = c.String(nullable: false, maxLength: 300),
                        NOM_IND_PDOT = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT })
                .ForeignKey("SIGEFI3.POA_POL_PDOT", t => new { t.COD_OE_PDOT, t.COD_POL_PDOT }, cascadeDelete: true)
                .Index(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT });
            
            CreateTable(
                "SIGEFI3.POA_OE_PLANIF",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PERI_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_OE_PLANIF = c.String(nullable: false, maxLength: 10),
                        UNI_MEDIDA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_PNVB = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_OEI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_EPG = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COD_POL_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_META_PDOT = c.String(nullable: false, maxLength: 4),
                        DESCRIPCION = c.String(nullable: false, maxLength: 250),
                        IND_MEDIO = c.String(maxLength: 250),
                        MIN_BASE = c.String(maxLength: 250),
                        MET_GTON_OBJTVO = c.String(maxLength: 250),
                        ACTIVO = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_OE_PLANIF })
                .ForeignKey("SIGEFI3.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.POA_EPG", t => t.COD_EPG, cascadeDelete: true)
                .ForeignKey("SIGEFI3.POA_META_PDOT", t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.POA_OE_PDOT", t => t.COD_OE_PDOT, cascadeDelete: true)
                .ForeignKey("SIGEFI3.POA_OEI", t => t.COD_OEI, cascadeDelete: true)
                .ForeignKey("SIGEFI3.POA_PNBV", t => t.COD_PNVB, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PNVB)
                .Index(t => t.COD_OEI)
                .Index(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT })
                .Index(t => t.COD_EPG);
            
            CreateTable(
                "SIGEFI3.POA_EPG",
                c => new
                    {
                        COD_EPG = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NOM_EPG = c.String(nullable: false, maxLength: 100),
                        COD_PR_PDOT = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.COD_EPG)
                .ForeignKey("SIGEFI3.POA_PR_PDOT", t => t.COD_PR_PDOT, cascadeDelete: true)
                .Index(t => t.COD_PR_PDOT);
            
            CreateTable(
                "SIGEFI3.POA_PR_PDOT",
                c => new
                    {
                        COD_PR_PDOT = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NOM_PR_PDOT = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.COD_PR_PDOT);
            
            CreateTable(
                "SIGEFI3.POA_OEI",
                c => new
                    {
                        COD_OEI = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NOM_OEI = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.COD_OEI);
            
            CreateTable(
                "SIGEFI3.POA_PNBV",
                c => new
                    {
                        COD_PNVB = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NOM_PNVB = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.COD_PNVB);
            
            CreateTable(
                "SIGEFI3.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "SIGEFI3.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("SIGEFI3.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("SIGEFI3.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "SIGEFI3.TiposDocs",
                c => new
                    {
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        DESCRIPCION = c.String(nullable: false, maxLength: 60),
                        TIPO_SRI = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.COD_TIPO_DOC);
            
            CreateTable(
                "SIGEFI3.TiposSRIs",
                c => new
                    {
                        CODIGO = c.String(nullable: false, maxLength: 2),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CODIGO);
            
            CreateTable(
                "SIGEFI3.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        NumIdentif = c.String(),
                        TypeUser = c.String(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        FechaReg = c.DateTime(nullable: false),
                        CODIGO_ROL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_EMPLEADO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TwoFactorEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        AccessFailedCount = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "SIGEFI3.AspNetUserClaims",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SIGEFI3.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "SIGEFI3.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("SIGEFI3.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SIGEFI3.AspNetUserRoles", "UserId", "SIGEFI3.AspNetUsers");
            DropForeignKey("SIGEFI3.AspNetUserLogins", "UserId", "SIGEFI3.AspNetUsers");
            DropForeignKey("SIGEFI3.AspNetUserClaims", "UserId", "SIGEFI3.AspNetUsers");
            DropForeignKey("SIGEFI3.AspNetUserRoles", "RoleId", "SIGEFI3.AspNetRoles");
            DropForeignKey("SIGEFI3.POA_POL_PDOT", "COD_OE_PDOT", "SIGEFI3.POA_OE_PDOT");
            DropForeignKey("SIGEFI3.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" }, "SIGEFI3.POA_POL_PDOT");
            DropForeignKey("SIGEFI3.POA_OE_PLANIF", "COD_PNVB", "SIGEFI3.POA_PNBV");
            DropForeignKey("SIGEFI3.POA_OE_PLANIF", "COD_OEI", "SIGEFI3.POA_OEI");
            DropForeignKey("SIGEFI3.POA_OE_PLANIF", "COD_OE_PDOT", "SIGEFI3.POA_OE_PDOT");
            DropForeignKey("SIGEFI3.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" }, "SIGEFI3.POA_META_PDOT");
            DropForeignKey("SIGEFI3.POA_OE_PLANIF", "COD_EPG", "SIGEFI3.POA_EPG");
            DropForeignKey("SIGEFI3.POA_EPG", "COD_PR_PDOT", "SIGEFI3.POA_PR_PDOT");
            DropForeignKey("SIGEFI3.POA_OE_PLANIF", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "SIGEFI3.Periodos");
            DropForeignKey("SIGEFI3.POA_OE_PDOT", "COD_ED_PDOT", "SIGEFI3.POA_ED_PDOT");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" }, "SIGEFI3.SubProgramas");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" }, "SIGEFI3.Proyectoes");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "SIGEFI3.Programas");
            DropForeignKey("SIGEFI3.PartIngresoes", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "SIGEFI3.Grupoes");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "SIGEFI3.Actividads");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" }, "SIGEFI3.SubProgramas");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" }, "SIGEFI3.Proyectoes");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "SIGEFI3.Programas");
            DropForeignKey("SIGEFI3.PartEgresoes", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "SIGEFI3.Grupoes");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "SIGEFI3.Actividads");
            DropForeignKey("SIGEFI3.Mnu_Opciones", "COD_MODULO", "SIGEFI3.MnuModulos");
            DropForeignKey("SIGEFI3.Mnu_Opciones", "COD_MODULO", "SIGEFI3.Mnu_Modulos");
            DropForeignKey("SIGEFI3.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "SIGEFI3.Grupoes");
            DropForeignKey("SIGEFI3.Items", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.Grupoes", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "SIGEFI3.Periodos");
            DropForeignKey("SIGEFI3.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "SIGEFI3.OrganicoFs");
            DropForeignKey("SIGEFI3.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "SIGEFI3.TipoRecursos");
            DropForeignKey("SIGEFI3.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "SIGEFI3.Periodos");
            DropForeignKey("SIGEFI3.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "SIGEFI3.OrganicoFs");
            DropForeignKey("SIGEFI3.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" }, "SIGEFI3.SubProgramas");
            DropForeignKey("SIGEFI3.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "SIGEFI3.Programas");
            DropForeignKey("SIGEFI3.Programas", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.POA_PLAN", new[] { "RP_EMPLEADOS_ID_INSTITUCION", "RP_EMPLEADOS_ID_CARGO", "RP_EMPLEADOS_ID_EMPLEADO" }, "SIGEFI3.RP_EMPLEADOS");
            DropForeignKey("SIGEFI3.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_TIPOROL" }, "SIGEFI3.RP_TIPOROL");
            DropForeignKey("SIGEFI3.RP_TIPOROL", "COD_PROV", "SIGEFI3.Proveedores");
            DropForeignKey("SIGEFI3.Parroquias", "ID_PROVINCIA", "SIGEFI3.Provincias");
            DropForeignKey("SIGEFI3.Cantones", "ID_PROVINCIA", "SIGEFI3.Provincias");
            DropForeignKey("SIGEFI3.Parroquias", new[] { "ID_PROVINCIA", "ID_CANTON" }, "SIGEFI3.Cantones");
            DropForeignKey("SIGEFI3.Proveedores", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" }, "SIGEFI3.Parroquias");
            DropForeignKey("SIGEFI3.RP_TIPOROL", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.RP_EMPLEADOS", "ID_RELALABORAL", "SIGEFI3.RP_RELACIONLABORAL");
            DropForeignKey("SIGEFI3.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_CARGO" }, "SIGEFI3.RP_CARGOS");
            DropForeignKey("SIGEFI3.RP_CARGOS", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.POA_PLAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "SIGEFI3.Periodos");
            DropForeignKey("SIGEFI3.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "SIGEFI3.Periodos");
            DropForeignKey("SIGEFI3.Periodos", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "SIGEFI3.Periodos");
            DropForeignKey("SIGEFI3.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" }, "SIGEFI3.Proyectoes");
            DropIndex("SIGEFI3.AspNetUserLogins", new[] { "UserId" });
            DropIndex("SIGEFI3.AspNetUserClaims", new[] { "UserId" });
            DropIndex("SIGEFI3.AspNetUsers", "UserNameIndex");
            DropIndex("SIGEFI3.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("SIGEFI3.AspNetUserRoles", new[] { "UserId" });
            DropIndex("SIGEFI3.AspNetRoles", "RoleNameIndex");
            DropIndex("SIGEFI3.POA_EPG", new[] { "COD_PR_PDOT" });
            DropIndex("SIGEFI3.POA_OE_PLANIF", new[] { "COD_EPG" });
            DropIndex("SIGEFI3.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" });
            DropIndex("SIGEFI3.POA_OE_PLANIF", new[] { "COD_OEI" });
            DropIndex("SIGEFI3.POA_OE_PLANIF", new[] { "COD_PNVB" });
            DropIndex("SIGEFI3.POA_OE_PLANIF", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("SIGEFI3.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" });
            DropIndex("SIGEFI3.POA_POL_PDOT", new[] { "COD_OE_PDOT" });
            DropIndex("SIGEFI3.POA_OE_PDOT", new[] { "COD_ED_PDOT" });
            DropIndex("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
            DropIndex("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "PAEG_CLAVE" });
            DropIndex("SIGEFI3.Mnu_Opciones", new[] { "COD_MODULO" });
            DropIndex("SIGEFI3.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("SIGEFI3.Items", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.Grupoes", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("SIGEFI3.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("SIGEFI3.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            DropIndex("SIGEFI3.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("SIGEFI3.Cantones", new[] { "ID_PROVINCIA" });
            DropIndex("SIGEFI3.Parroquias", new[] { "ID_PROVINCIA", "ID_CANTON" });
            DropIndex("SIGEFI3.Proveedores", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" });
            DropIndex("SIGEFI3.RP_TIPOROL", new[] { "COD_PROV" });
            DropIndex("SIGEFI3.RP_TIPOROL", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.RP_CARGOS", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.RP_EMPLEADOS", new[] { "ID_RELALABORAL" });
            DropIndex("SIGEFI3.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_TIPOROL" });
            DropIndex("SIGEFI3.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_CARGO" });
            DropIndex("SIGEFI3.POA_PLAN", new[] { "RP_EMPLEADOS_ID_INSTITUCION", "RP_EMPLEADOS_ID_CARGO", "RP_EMPLEADOS_ID_EMPLEADO" });
            DropIndex("SIGEFI3.POA_PLAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("SIGEFI3.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            DropIndex("SIGEFI3.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("SIGEFI3.Periodos", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.Programas", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("SIGEFI3.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO" });
            DropIndex("SIGEFI3.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROG_CODIGO", "PROY_CODIGO" });
            DropTable("SIGEFI3.AspNetUserLogins");
            DropTable("SIGEFI3.AspNetUserClaims");
            DropTable("SIGEFI3.AspNetUsers");
            DropTable("SIGEFI3.TiposSRIs");
            DropTable("SIGEFI3.TiposDocs");
            DropTable("SIGEFI3.AspNetUserRoles");
            DropTable("SIGEFI3.AspNetRoles");
            DropTable("SIGEFI3.POA_PNBV");
            DropTable("SIGEFI3.POA_OEI");
            DropTable("SIGEFI3.POA_PR_PDOT");
            DropTable("SIGEFI3.POA_EPG");
            DropTable("SIGEFI3.POA_OE_PLANIF");
            DropTable("SIGEFI3.POA_META_PDOT");
            DropTable("SIGEFI3.POA_POL_PDOT");
            DropTable("SIGEFI3.POA_OE_PDOT");
            DropTable("SIGEFI3.POA_ED_PDOT");
            DropTable("SIGEFI3.POA_EAI");
            DropTable("SIGEFI3.PartIngresoes");
            DropTable("SIGEFI3.PartEgresoes");
            DropTable("SIGEFI3.Mnu_Modulos");
            DropTable("SIGEFI3.Mnu_Opciones");
            DropTable("SIGEFI3.MnuModulos");
            DropTable("SIGEFI3.Items");
            DropTable("SIGEFI3.Grupoes");
            DropTable("SIGEFI3.TipoRecursos");
            DropTable("SIGEFI3.OrganicoFs");
            DropTable("SIGEFI3.Catalogos");
            DropTable("SIGEFI3.Cantones");
            DropTable("SIGEFI3.Provincias");
            DropTable("SIGEFI3.Parroquias");
            DropTable("SIGEFI3.Proveedores");
            DropTable("SIGEFI3.RP_TIPOROL");
            DropTable("SIGEFI3.RP_RELACIONLABORAL");
            DropTable("SIGEFI3.RP_CARGOS");
            DropTable("SIGEFI3.RP_EMPLEADOS");
            DropTable("SIGEFI3.POA_PLAN");
            DropTable("SIGEFI3.Mnu_Roles");
            DropTable("SIGEFI3.CO_PARAMETROS");
            DropTable("SIGEFI3.Periodos");
            DropTable("SIGEFI3.Instituciones");
            DropTable("SIGEFI3.Programas");
            DropTable("SIGEFI3.SubProgramas");
            DropTable("SIGEFI3.Proyectoes");
            DropTable("SIGEFI3.Actividads");
        }
    }
}
