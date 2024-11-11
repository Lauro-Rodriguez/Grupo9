namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" }, "dbo.POA_META_PDOT");
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" });
            AlterColumn("dbo.POA_OE_PLANIF", "COD_POL_PDOT", c => c.String(maxLength: 4));
            AlterColumn("dbo.POA_OE_PLANIF", "COD_META_PDOT", c => c.String(maxLength: 4));
            CreateIndex("dbo.POA_OE_PLANIF", "COD_OE_PDOT");
        }
        
        public override void Down()
        {
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT" });
            AlterColumn("dbo.POA_OE_PLANIF", "COD_META_PDOT", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.POA_OE_PLANIF", "COD_POL_PDOT", c => c.String(nullable: false, maxLength: 4));
            CreateIndex("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" });
            AddForeignKey("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" }, "dbo.POA_META_PDOT", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" }, cascadeDelete: true);
        }
    }
}
