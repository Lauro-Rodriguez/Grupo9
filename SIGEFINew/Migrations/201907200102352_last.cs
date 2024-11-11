namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CO_PARAMETROS", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.CO_PARAMETROS", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            DropColumn("dbo.CO_PARAMETROS", "ID_INSTITUCION");
            DropColumn("dbo.CO_PARAMETROS", "PERI_CODIGO");
            RenameColumn(table: "dbo.CO_PARAMETROS", name: "PERIODOS_ID_INSTITUCION", newName: "ID_INSTITUCION");
            RenameColumn(table: "dbo.CO_PARAMETROS", name: "PERIODOS_PERI_CODIGO", newName: "PERI_CODIGO");
            DropPrimaryKey("dbo.CO_PARAMETROS");
            AlterColumn("dbo.CO_PARAMETROS", "ID_INSTITUCION", c => c.Int(nullable: false));
            AlterColumn("dbo.CO_PARAMETROS", "PERI_CODIGO", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ANIO_CODIGO" });
            CreateIndex("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            AddForeignKey("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropPrimaryKey("dbo.CO_PARAMETROS");
            AlterColumn("dbo.CO_PARAMETROS", "PERI_CODIGO", c => c.Int());
            AlterColumn("dbo.CO_PARAMETROS", "ID_INSTITUCION", c => c.Int());
            AddPrimaryKey("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            RenameColumn(table: "dbo.CO_PARAMETROS", name: "PERI_CODIGO", newName: "PERIODOS_PERI_CODIGO");
            RenameColumn(table: "dbo.CO_PARAMETROS", name: "ID_INSTITUCION", newName: "PERIODOS_ID_INSTITUCION");
            AddColumn("dbo.CO_PARAMETROS", "PERI_CODIGO", c => c.Int(nullable: false));
            AddColumn("dbo.CO_PARAMETROS", "ID_INSTITUCION", c => c.Int(nullable: false));
            CreateIndex("dbo.CO_PARAMETROS", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" });
            AddForeignKey("dbo.CO_PARAMETROS", new[] { "PERIODOS_ID_INSTITUCION", "PERIODOS_PERI_CODIGO" }, "dbo.Periodos", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
        }
    }
}
