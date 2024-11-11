namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd13 : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO, t.ACTI_CODIGO, t.ITEM_CLAVE })
                .ForeignKey("dbo.PartEgresoes", t => new { t.ID_INSTITUCION, t.PAEG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PAEG_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETALLEGRESOS", new[] { "ID_INSTITUCION", "PAEG_CODIGO" }, "dbo.PartEgresoes");
            DropIndex("dbo.DETALLEGRESOS", new[] { "ID_INSTITUCION", "PAEG_CODIGO" });
            DropTable("dbo.DETALLEGRESOS");
        }
    }
}
