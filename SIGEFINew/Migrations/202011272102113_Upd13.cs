namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GA_CONCEPTOSGARANTIA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_CONCEPTOGA = c.Int(nullable: false),
                        NOM_CONCEPTOGA = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_CONCEPTOGA })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.GA_GARANTIAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_GARANTIA = c.String(nullable: false, maxLength: 50),
                        ID_TIPOGARANTIA = c.Int(nullable: false),
                        NUM_REFGARANTIA = c.String(maxLength: 50),
                        NUM_PADREGARANTIA = c.String(nullable: false, maxLength: 50),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_REG = c.String(nullable: false, maxLength: 2),
                        COD_CONTRATISTA = c.String(nullable: false, maxLength: 20),
                        ID_CONCEPTOGA = c.Int(nullable: false),
                        ID_PROYOBJETO = c.Int(nullable: false),
                        ID_EMISOR = c.String(nullable: false, maxLength: 20),
                        VAL_GARANTIA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FEC_VIGDESDE = c.DateTime(nullable: false),
                        FEC_VIGHASTA = c.DateTime(nullable: false),
                        ESTADO = c.String(nullable: false, maxLength: 50),
                        DET_GARANTIA = c.String(nullable: false, maxLength: 1000),
                        REF_ACTUAL = c.String(maxLength: 50),
                        REF_ANTERIOR = c.String(maxLength: 50),
                        FEC_DEVOLUCION = c.DateTime(),
                        NUM_ASIENTO = c.Int(),
                        NUM_ASIENTODEV = c.Int(),
                        NUM_ASIENTOBAJA = c.Int(),
                        FEC_BAJA = c.DateTime(),
                        DET_DEVOLUCION = c.String(maxLength: 1000),
                        COD_CONTRATO = c.String(maxLength: 30),
                        NOMBRE_PDF = c.String(maxLength: 500),
                        ETREGADOA = c.String(maxLength: 50),
                        NUM_CI = c.String(maxLength: 13),
                        TIPO_DOC = c.String(maxLength: 60),
                        NUMERO_DOC = c.String(maxLength: 100),
                        FECHA_DOC = c.DateTime(),
                        NUM_ACTA = c.String(maxLength: 15),
                        FECHA_ACTA = c.DateTime(),
                        BAJA = c.Boolean(nullable: false),
                        VALOR_CONTABLE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_GARANTIA, t.ID_TIPOGARANTIA })
                .ForeignKey("dbo.GA_EMISORES", t => new { t.ID_INSTITUCION, t.ID_EMISOR }, cascadeDelete: false)
                .ForeignKey("dbo.GA_PROYOBJETO", t => new { t.ID_INSTITUCION, t.ID_PROYOBJETO }, cascadeDelete: false)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.GA_CONCEPTOSGARANTIA", t => new { t.ID_INSTITUCION, t.ID_CONCEPTOGA }, cascadeDelete: false)
                .ForeignKey("dbo.GA_TIPOSGARANTIA", t => new { t.ID_INSTITUCION, t.ID_TIPOGARANTIA }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMISOR })
                .Index(t => new { t.ID_INSTITUCION, t.ID_PROYOBJETO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.ID_CONCEPTOGA })
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOGARANTIA });
            
            CreateTable(
                "dbo.GA_EMISORES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMISOR = c.String(nullable: false, maxLength: 20),
                        NOM_EMISOR = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMISOR })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.GA_PROYOBJETO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_PROYOBJETO = c.Int(nullable: false),
                        NOM_PROYOBJETO = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_PROYOBJETO });
            
            CreateTable(
                "dbo.GA_ASIENTO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_GARANTIA = c.String(nullable: false, maxLength: 50),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        NUM_PADREGARANTIA = c.String(maxLength: 50),
                        BAJA = c.Boolean(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 1),
                        FECHA = c.DateTime(nullable: false),
                        ID_TIPOGARANTIA = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NUM_ASIENTO = c.Int(nullable: false),
                        ENVIADO = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_GARANTIA, t.CODIGO_CG })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.GA_EMAILS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_GARANTIA = c.String(nullable: false, maxLength: 50),
                        NUM_OFIMAIL = c.Int(nullable: false),
                        ID_EMISOR = c.String(nullable: false, maxLength: 20),
                        FECHA_ENVIO = c.DateTime(nullable: false),
                        PERIODO = c.Int(nullable: false),
                        EMAIL = c.String(nullable: false, maxLength: 150),
                        NUM_PADREGARANTIA = c.String(nullable: false, maxLength: 50),
                        ENVIADO = c.Int(nullable: false),
                        VALOR_RENOVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_GARANTIA, t.NUM_OFIMAIL, t.ID_EMISOR })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.GA_PARAMETROS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        NOTIF_GARECPV = c.Boolean(nullable: false),
                        NUMDIAS_GARECPV = c.Int(nullable: false),
                        NOTIF_GAENTPV = c.Boolean(nullable: false),
                        NUMDIAS_GAENTPV = c.Int(nullable: false),
                        AFEC_CONTAB = c.Boolean(nullable: false),
                        CTAORDENDEUD_GAREC = c.String(maxLength: 50),
                        CTAORDENACRE_GAREC = c.String(maxLength: 50),
                        CTAORDENDEUD_GAENT = c.String(maxLength: 50),
                        CTAORDENDACRE_GAENT = c.String(maxLength: 50),
                        RUTA_PDF = c.String(maxLength: 500),
                        EMAIL_ENVIO = c.String(maxLength: 150),
                        ASUNTO_EMAIL = c.String(maxLength: 200),
                        TEXTO_EMAIL = c.String(maxLength: 500),
                        PASSW_EMAIL = c.String(maxLength: 30),
                        NUM_OFIMAIL = c.Int(nullable: false),
                        NOM_REPLEGAL = c.String(maxLength: 100),
                        CARGO_REPLEGAL = c.String(maxLength: 100),
                        SMTP_HOST = c.String(maxLength: 15),
                        SMTP_PORT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_FILA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.GA_TIPOSGARANTIA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPOGARANTIA = c.Int(nullable: false),
                        NOM_TIPOGARANTIA = c.String(nullable: false, maxLength: 100),
                        VALORFIJO_RENOVA = c.Boolean(nullable: false),
                        CTAORDENDEUD_GAREC = c.String(maxLength: 50),
                        CTAORDENACRE_GAREC = c.String(maxLength: 50),
                        CTAORDENDEUD_GAENT = c.String(maxLength: 50),
                        CTAORDENDACRE_GAENT = c.String(maxLength: 50),
                        FIEL_CUMPLIM = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOGARANTIA })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            AlterColumn("dbo.IN_BODEGAS", "FECHA_MODIF", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GA_TIPOSGARANTIA", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_TIPOGARANTIA" }, "dbo.GA_TIPOSGARANTIA");
            DropForeignKey("dbo.GA_CONCEPTOSGARANTIA", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_CONCEPTOGA" }, "dbo.GA_CONCEPTOSGARANTIA");
            DropForeignKey("dbo.GA_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.GA_EMAILS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.GA_ASIENTO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_PROYOBJETO" }, "dbo.GA_PROYOBJETO");
            DropForeignKey("dbo.GA_EMISORES", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_EMISOR" }, "dbo.GA_EMISORES");
            DropIndex("dbo.GA_TIPOSGARANTIA", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.GA_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.GA_EMAILS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.GA_ASIENTO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.GA_EMISORES", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_TIPOGARANTIA" });
            DropIndex("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_CONCEPTOGA" });
            DropIndex("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_PROYOBJETO" });
            DropIndex("dbo.GA_GARANTIAS", new[] { "ID_INSTITUCION", "ID_EMISOR" });
            DropIndex("dbo.GA_CONCEPTOSGARANTIA", new[] { "ID_INSTITUCION" });
            AlterColumn("dbo.IN_BODEGAS", "FECHA_MODIF", c => c.DateTime(nullable: false));
            DropTable("dbo.GA_TIPOSGARANTIA");
            DropTable("dbo.GA_PARAMETROS");
            DropTable("dbo.GA_EMAILS");
            DropTable("dbo.GA_ASIENTO");
            DropTable("dbo.GA_PROYOBJETO");
            DropTable("dbo.GA_EMISORES");
            DropTable("dbo.GA_GARANTIAS");
            DropTable("dbo.GA_CONCEPTOSGARANTIA");
        }
    }
}
