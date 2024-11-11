namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, "dbo.CO_DETCTASPPAG");
            DropPrimaryKey("dbo.CO_DETCTASPPAG");
            AlterColumn("dbo.CO_DETCTASPPAG", "NUM_FILA", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" });
            AddForeignKey("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, "dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, "dbo.CO_DETCTASPPAG");
            DropPrimaryKey("dbo.CO_DETCTASPPAG");
            AlterColumn("dbo.CO_DETCTASPPAG", "NUM_FILA", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" });
            AddForeignKey("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, "dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, cascadeDelete: true);
        }
    }
}
