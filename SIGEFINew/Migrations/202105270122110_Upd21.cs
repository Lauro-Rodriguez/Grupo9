namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd21 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" });
            AddForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" }, "dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" }, "dbo.IN_PRODUCTOS");
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" });
        }
    }
}
