namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Presup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SIGEFI3.Actividads",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .ForeignKey("SIGEFI3.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO });
            
            CreateTable(
                "SIGEFI3.Proyectoes",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROY_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO })
                .ForeignKey("SIGEFI3.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO });
            
            CreateTable(
                "SIGEFI3.SubProgramas",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SUBP_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 230),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO })
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
                "SIGEFI3.Provincias",
                c => new
                    {
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_PROVINCIA);
            
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
                "SIGEFI3.Parroquias",
                c => new
                    {
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CANTON = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ITEM_PARROQUIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DESCRIPCION = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ITEM_PARROQUIA })
                .ForeignKey("SIGEFI3.Provincias", t => t.ID_PROVINCIA, cascadeDelete: true)
                .Index(t => t.ID_PROVINCIA);
            
            CreateTable(
                "SIGEFI3.PartEgresoes",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PAEG_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
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
                .ForeignKey("SIGEFI3.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO });
            
            CreateTable(
                "SIGEFI3.PartIngresoes",
                c => new
                    {
                        ID_INSTITUCION = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PAIN_CODIGO = c.Decimal(nullable: false, precision: 10, scale: 0),
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
                .ForeignKey("SIGEFI3.Actividads", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Grupoes", t => new { t.ID_INSTITUCION, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.Proyectoes", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("SIGEFI3.SubProgramas", t => new { t.ID_INSTITUCION, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO });
            
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
                "SIGEFI3.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        NumIdentif = c.String(),
                        TypeUser = c.String(),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        FechaReg = c.DateTime(nullable: false),
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
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" }, "SIGEFI3.SubProgramas");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" }, "SIGEFI3.Proyectoes");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "SIGEFI3.Programas");
            DropForeignKey("SIGEFI3.PartIngresoes", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "SIGEFI3.Grupoes");
            DropForeignKey("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "SIGEFI3.Actividads");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" }, "SIGEFI3.SubProgramas");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" }, "SIGEFI3.Proyectoes");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "SIGEFI3.Programas");
            DropForeignKey("SIGEFI3.PartEgresoes", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "SIGEFI3.Grupoes");
            DropForeignKey("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "SIGEFI3.Actividads");
            DropForeignKey("SIGEFI3.Parroquias", "ID_PROVINCIA", "SIGEFI3.Provincias");
            DropForeignKey("SIGEFI3.Mnu_Opciones", "COD_MODULO", "SIGEFI3.MnuModulos");
            DropForeignKey("SIGEFI3.Mnu_Opciones", "COD_MODULO", "SIGEFI3.Mnu_Modulos");
            DropForeignKey("SIGEFI3.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "SIGEFI3.Grupoes");
            DropForeignKey("SIGEFI3.Items", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.Grupoes", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.Cantones", "ID_PROVINCIA", "SIGEFI3.Provincias");
            DropForeignKey("SIGEFI3.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" }, "SIGEFI3.SubProgramas");
            DropForeignKey("SIGEFI3.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "SIGEFI3.Programas");
            DropForeignKey("SIGEFI3.Programas", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "SIGEFI3.Periodos");
            DropForeignKey("SIGEFI3.Periodos", "ID_INSTITUCION", "SIGEFI3.Instituciones");
            DropForeignKey("SIGEFI3.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" }, "SIGEFI3.Proyectoes");
            DropIndex("SIGEFI3.AspNetUserLogins", new[] { "UserId" });
            DropIndex("SIGEFI3.AspNetUserClaims", new[] { "UserId" });
            DropIndex("SIGEFI3.AspNetUsers", "UserNameIndex");
            DropIndex("SIGEFI3.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("SIGEFI3.AspNetUserRoles", new[] { "UserId" });
            DropIndex("SIGEFI3.AspNetRoles", "RoleNameIndex");
            DropIndex("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("SIGEFI3.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("SIGEFI3.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("SIGEFI3.Parroquias", new[] { "ID_PROVINCIA" });
            DropIndex("SIGEFI3.Mnu_Opciones", new[] { "COD_MODULO" });
            DropIndex("SIGEFI3.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("SIGEFI3.Items", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.Grupoes", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.Cantones", new[] { "ID_PROVINCIA" });
            DropIndex("SIGEFI3.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            DropIndex("SIGEFI3.Periodos", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.Programas", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI3.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("SIGEFI3.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" });
            DropIndex("SIGEFI3.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" });
            DropTable("SIGEFI3.AspNetUserLogins");
            DropTable("SIGEFI3.AspNetUserClaims");
            DropTable("SIGEFI3.AspNetUsers");
            DropTable("SIGEFI3.AspNetUserRoles");
            DropTable("SIGEFI3.AspNetRoles");
            DropTable("SIGEFI3.PartIngresoes");
            DropTable("SIGEFI3.PartEgresoes");
            DropTable("SIGEFI3.Parroquias");
            DropTable("SIGEFI3.Mnu_Modulos");
            DropTable("SIGEFI3.Mnu_Opciones");
            DropTable("SIGEFI3.MnuModulos");
            DropTable("SIGEFI3.Items");
            DropTable("SIGEFI3.Grupoes");
            DropTable("SIGEFI3.Provincias");
            DropTable("SIGEFI3.Cantones");
            DropTable("SIGEFI3.Mnu_Roles");
            DropTable("SIGEFI3.Periodos");
            DropTable("SIGEFI3.Instituciones");
            DropTable("SIGEFI3.Programas");
            DropTable("SIGEFI3.SubProgramas");
            DropTable("SIGEFI3.Proyectoes");
            DropTable("SIGEFI3.Actividads");
        }
    }
}
