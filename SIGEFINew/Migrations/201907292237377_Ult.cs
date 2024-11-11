namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ult : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_POL_PDOT", "COD_OE_PDOT", "dbo.POA_OE_PDOT");
            DropForeignKey("dbo.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" }, "dbo.POA_POL_PDOT");
            DropIndex("dbo.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT" });
            DropIndex("dbo.POA_POL_PDOT", new[] { "COD_OE_PDOT" });
            DropTable("dbo.POA_META_PDOT");
            DropTable("dbo.POA_POL_PDOT");
        }
    }
}
