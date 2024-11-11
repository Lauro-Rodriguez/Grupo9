namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mnuopc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SIGEFI2.Cantones",
                c => new
                    {
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ID_CANTON = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON })
                .ForeignKey("SIGEFI2.Provincias", t => t.ID_PROVINCIA, cascadeDelete: true)
                .Index(t => t.ID_PROVINCIA);
            
            CreateTable(
                "SIGEFI2.Provincias",
                c => new
                    {
                        ID_PROVINCIA = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_PROVINCIA);
            
            CreateTable(
                "SIGEFI2.CGCuentas",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        CODIGO_CG = c.String(),
                        NOMBRE_CG = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "SIGEFI2.Instituciones",
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
                "SIGEFI2.Periodos",
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
                .ForeignKey("SIGEFI2.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "SIGEFI2.Mnu_Roles",
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
                .ForeignKey("SIGEFI2.Periodos", t => new { t.PERIODOS_ID_INSTITUCION, t.PERIODOS_PERI_CODIGO })
                .Index(t => new { t.PERIODOS_ID_INSTITUCION, t.PERIODOS_PERI_CODIGO });
            
            CreateTable(
                "SIGEFI2.MnuModulos",
                c => new
                    {
                        COD_MODULO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOM_MODULO = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.COD_MODULO);
            
            CreateTable(
                "SIGEFI2.Mnu_Opciones",
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
                .ForeignKey("SIGEFI2.Mnu_Modulos", t => t.COD_MODULO, cascadeDelete: true)
                .ForeignKey("SIGEFI2.MnuModulos", t => t.COD_MODULO, cascadeDelete: true)
                .Index(t => t.COD_MODULO);
            
            CreateTable(
                "SIGEFI2.Mnu_Modulos",
                c => new
                    {
                        COD_MODULO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOM_MODULO = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.COD_MODULO);
            
            CreateTable(
                "SIGEFI2.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "SIGEFI2.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("SIGEFI2.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("SIGEFI2.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "SIGEFI2.AspNetUsers",
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
                "SIGEFI2.AspNetUserClaims",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SIGEFI2.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "SIGEFI2.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("SIGEFI2.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("SIGEFI2.AspNetUserRoles", "UserId", "SIGEFI2.AspNetUsers");
            DropForeignKey("SIGEFI2.AspNetUserLogins", "UserId", "SIGEFI2.AspNetUsers");
            DropForeignKey("SIGEFI2.AspNetUserClaims", "UserId", "SIGEFI2.AspNetUsers");
            DropForeignKey("SIGEFI2.AspNetUserRoles", "RoleId", "SIGEFI2.AspNetRoles");
            DropForeignKey("SIGEFI2.Mnu_Opciones", "COD_MODULO", "SIGEFI2.MnuModulos");
            DropForeignKey("SIGEFI2.Mnu_Opciones", "COD_MODULO", "SIGEFI2.Mnu_Modulos");
            DropForeignKey("SIGEFI2.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "SIGEFI2.Periodos");
            DropForeignKey("SIGEFI2.Periodos", "ID_INSTITUCION", "SIGEFI2.Instituciones");
            DropForeignKey("SIGEFI2.Cantones", "ID_PROVINCIA", "SIGEFI2.Provincias");
            DropIndex("SIGEFI2.AspNetUserLogins", new[] { "UserId" });
            DropIndex("SIGEFI2.AspNetUserClaims", new[] { "UserId" });
            DropIndex("SIGEFI2.AspNetUsers", "UserNameIndex");
            DropIndex("SIGEFI2.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("SIGEFI2.AspNetUserRoles", new[] { "UserId" });
            DropIndex("SIGEFI2.AspNetRoles", "RoleNameIndex");
            DropIndex("SIGEFI2.Mnu_Opciones", new[] { "COD_MODULO" });
            DropIndex("SIGEFI2.Mnu_Roles", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            DropIndex("SIGEFI2.Periodos", new[] { "ID_INSTITUCION" });
            DropIndex("SIGEFI2.Cantones", new[] { "ID_PROVINCIA" });
            DropTable("SIGEFI2.AspNetUserLogins");
            DropTable("SIGEFI2.AspNetUserClaims");
            DropTable("SIGEFI2.AspNetUsers");
            DropTable("SIGEFI2.AspNetUserRoles");
            DropTable("SIGEFI2.AspNetRoles");
            DropTable("SIGEFI2.Mnu_Modulos");
            DropTable("SIGEFI2.Mnu_Opciones");
            DropTable("SIGEFI2.MnuModulos");
            DropTable("SIGEFI2.Mnu_Roles");
            DropTable("SIGEFI2.Periodos");
            DropTable("SIGEFI2.Instituciones");
            DropTable("SIGEFI2.CGCuentas");
            DropTable("SIGEFI2.Provincias");
            DropTable("SIGEFI2.Cantones");
        }
    }
}
