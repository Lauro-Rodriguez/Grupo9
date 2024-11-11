namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class npg : DbMigration
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
                "dbo.Provincias",
                c => new
                    {
                        ID_PROVINCIA = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_PROVINCIA);
            
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
                .Index(t => t.ID_PROVINCIA);
            
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
                .Index(t => new { t.ID_INSTITUCION, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.GRUP_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO });
            
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
            DropForeignKey("dbo.Parroquias", "ID_PROVINCIA", "dbo.Provincias");
            DropForeignKey("dbo.Mnu_Opciones", "COD_MODULO", "dbo.MnuModulos");
            DropForeignKey("dbo.Mnu_Opciones", "COD_MODULO", "dbo.Mnu_Modulos");
            DropForeignKey("dbo.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" }, "dbo.Grupoes");
            DropForeignKey("dbo.Items", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Grupoes", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Cantones", "ID_PROVINCIA", "dbo.Provincias");
            DropForeignKey("dbo.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" }, "dbo.SubProgramas");
            DropForeignKey("dbo.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.Programas", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.Periodos", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.Proyectoes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.Parroquias", new[] { "ID_PROVINCIA" });
            DropIndex("dbo.Mnu_Opciones", new[] { "COD_MODULO" });
            DropIndex("dbo.Items", new[] { "ID_INSTITUCION", "GRUP_CODIGO" });
            DropIndex("dbo.Items", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.Grupoes", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.Cantones", new[] { "ID_PROVINCIA" });
            DropIndex("dbo.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            DropIndex("dbo.Periodos", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.Programas", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.SubProgramas", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.Proyectoes", new[] { "ID_INSTITUCION", "SUBP_CODIGO" });
            DropIndex("dbo.Actividads", new[] { "ID_INSTITUCION", "SUBP_CODIGO", "PROY_CODIGO" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PartIngresoes");
            DropTable("dbo.PartEgresoes");
            DropTable("dbo.Parroquias");
            DropTable("dbo.Mnu_Modulos");
            DropTable("dbo.Mnu_Opciones");
            DropTable("dbo.MnuModulos");
            DropTable("dbo.Items");
            DropTable("dbo.Grupoes");
            DropTable("dbo.Provincias");
            DropTable("dbo.Cantones");
            DropTable("dbo.Mnu_Roles");
            DropTable("dbo.Periodos");
            DropTable("dbo.Instituciones");
            DropTable("dbo.Programas");
            DropTable("dbo.SubProgramas");
            DropTable("dbo.Proyectoes");
            DropTable("dbo.Actividads");
        }
    }
}
