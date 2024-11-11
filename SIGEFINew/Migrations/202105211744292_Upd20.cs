namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" }, "dbo.IN_COMPRAS");
            DropIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" });
            DropPrimaryKey("dbo.IN_DETCOMPRA");
            DropPrimaryKey("dbo.IN_COMPRAS");
            AddColumn("dbo.IN_DETCOMPRA", "ID_BODEGA", c => c.Int(nullable: false));
            AddColumn("dbo.IN_COMPRAS", "ID_BODEGA", c => c.Int(nullable: false));
            AddColumn("dbo.IN_COMPRAS", "COD_SUSTENTO", c => c.String(maxLength: 2));
            AddColumn("dbo.IN_COMPRAS", "FORMA_PAGO", c => c.String(maxLength: 2));
            AddColumn("dbo.IN_COMPRAS", "NUM_INV", c => c.Int());
            AddColumn("dbo.IN_COMPRAS", "NUM_AF", c => c.Int());
            AddColumn("dbo.IN_COMPRAS", "NUM_CXP", c => c.Int());
            AddPrimaryKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_COMPRA", "COD_ITEM", "NUM_FILA" });
            AddPrimaryKey("dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_COMPRA" });
            CreateIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_COMPRA" });
            AddForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_COMPRA" }, "dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_COMPRA" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_COMPRA" }, "dbo.IN_COMPRAS");
            DropIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_COMPRA" });
            DropPrimaryKey("dbo.IN_COMPRAS");
            DropPrimaryKey("dbo.IN_DETCOMPRA");
            DropColumn("dbo.IN_COMPRAS", "NUM_CXP");
            DropColumn("dbo.IN_COMPRAS", "NUM_AF");
            DropColumn("dbo.IN_COMPRAS", "NUM_INV");
            DropColumn("dbo.IN_COMPRAS", "FORMA_PAGO");
            DropColumn("dbo.IN_COMPRAS", "COD_SUSTENTO");
            DropColumn("dbo.IN_COMPRAS", "ID_BODEGA");
            DropColumn("dbo.IN_DETCOMPRA", "ID_BODEGA");
            AddPrimaryKey("dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" });
            AddPrimaryKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM", "NUM_COMPRA", "NUM_FILA" });
            CreateIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" });
            AddForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" }, "dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" }, cascadeDelete: true);
        }
    }
}
