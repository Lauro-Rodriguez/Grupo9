namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_VALORESXFACTURA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUM_FILA = c.String(nullable: false, maxLength: 20),
                        TIPO_BS = c.Int(nullable: false),
                        VALOR_DOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IVA_PORC = c.Int(nullable: false),
                        TOT_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA, t.TIPO_BS })
                .ForeignKey("dbo.CO_DETCTASPPAG", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_VALORESXFACTURA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, "dbo.CO_DETCTASPPAG");
            DropIndex("dbo.CO_VALORESXFACTURA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" });
            DropTable("dbo.CO_VALORESXFACTURA");
        }
    }
}
