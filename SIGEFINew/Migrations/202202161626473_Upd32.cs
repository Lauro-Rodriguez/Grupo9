namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd32 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_DETFACTURA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUM_DOC = c.String(nullable: false, maxLength: 20),
                        TIPO_GASTO = c.Int(nullable: false),
                        VALOR_DOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IVA_PORC = c.Int(nullable: false),
                        TOT_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CTA_X_PAG = c.String(nullable: false, maxLength: 50),
                        CTA_DEBITO = c.String(nullable: false, maxLength: 50),
                        IG = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PROV, t.NUM_DOC, t.TIPO_GASTO })
                .ForeignKey("dbo.CO_FACTURAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PROV, t.NUM_DOC }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PROV, t.NUM_DOC });
            
            AddColumn("dbo.CO_FACTURAS", "NUM_COMPROMISO", c => c.Int(nullable: false));
            AddColumn("dbo.CO_FACTURAS", "CTA_X_PAG", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.CO_FACTURAS", "CTA_DEBITO", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.CO_FACTURAS", "COD_SUSTENTO", c => c.String(maxLength: 2));
            AddColumn("dbo.CO_FACTURAS", "FORMA_PAGO", c => c.String(maxLength: 2));
            AddColumn("dbo.CO_PARAMETROS", "COD_SUSTENTO", c => c.String(maxLength: 2));
            AddColumn("dbo.CO_PARAMETROS", "FORMA_PAGO", c => c.String(maxLength: 2));
            AddColumn("dbo.RP_SETUP", "VAL_CBASICA", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RP_SETUP", "NUM_CBASICA", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_DETFACTURA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PROV", "NUM_DOC" }, "dbo.CO_FACTURAS");
            DropIndex("dbo.CO_DETFACTURA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PROV", "NUM_DOC" });
            DropColumn("dbo.RP_SETUP", "NUM_CBASICA");
            DropColumn("dbo.RP_SETUP", "VAL_CBASICA");
            DropColumn("dbo.CO_PARAMETROS", "FORMA_PAGO");
            DropColumn("dbo.CO_PARAMETROS", "COD_SUSTENTO");
            DropColumn("dbo.CO_FACTURAS", "FORMA_PAGO");
            DropColumn("dbo.CO_FACTURAS", "COD_SUSTENTO");
            DropColumn("dbo.CO_FACTURAS", "CTA_DEBITO");
            DropColumn("dbo.CO_FACTURAS", "CTA_X_PAG");
            DropColumn("dbo.CO_FACTURAS", "NUM_COMPROMISO");
            DropTable("dbo.CO_DETFACTURA");
        }
    }
}
