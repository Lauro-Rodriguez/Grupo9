namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes");
            DropPrimaryKey("dbo.PartIngresoes");
            AlterColumn("dbo.PartIngresoes", "PAIN_CODIGO", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CODIGO" });
            AddForeignKey("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes");
            DropPrimaryKey("dbo.PartIngresoes");
            AlterColumn("dbo.PartIngresoes", "PAIN_CODIGO", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CODIGO" });
            AddForeignKey("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, cascadeDelete: true);
        }
    }
}
