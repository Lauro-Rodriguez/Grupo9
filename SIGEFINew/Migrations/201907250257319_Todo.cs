namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Todo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Catalogos", new[] { "ORGANICOF_ID_INSTITUCION", "ORGANICOF_PERI_CODIGO", "ORGANICOF_ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.Catalogos", new[] { "TIPORECURSOS_ID_INSTITUCION", "TIPORECURSOS_PERI_CODIGO", "TIPORECURSOS_TRE_CODIGO" }, "dbo.TipoRecursos");
            DropIndex("dbo.Catalogos", new[] { "ORGANICOF_ID_INSTITUCION", "ORGANICOF_PERI_CODIGO", "ORGANICOF_ORG_CODIGO" });
            DropIndex("dbo.Catalogos", new[] { "TIPORECURSOS_ID_INSTITUCION", "TIPORECURSOS_PERI_CODIGO", "TIPORECURSOS_TRE_CODIGO" });
            DropColumn("dbo.Catalogos", "ID_INSTITUCION");
            DropColumn("dbo.Catalogos", "ORG_CODIGO");
            DropColumn("dbo.Catalogos", "ID_INSTITUCION");
            DropColumn("dbo.Catalogos", "TRE_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "ORGANICOF_ID_INSTITUCION", newName: "ID_INSTITUCION");
            RenameColumn(table: "dbo.Catalogos", name: "ORGANICOF_PERI_CODIGO", newName: "PERI_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "ORGANICOF_ORG_CODIGO", newName: "ORG_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "TIPORECURSOS_ID_INSTITUCION", newName: "ID_INSTITUCION");
            RenameColumn(table: "dbo.Catalogos", name: "TIPORECURSOS_PERI_CODIGO", newName: "PERI_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "TIPORECURSOS_TRE_CODIGO", newName: "TRE_CODIGO");
            DropPrimaryKey("dbo.Catalogos");
            AlterColumn("dbo.Catalogos", "ID_INSTITUCION", c => c.Int(nullable: false));
            AlterColumn("dbo.Catalogos", "PERI_CODIGO", c => c.Int(nullable: false));
            AlterColumn("dbo.Catalogos", "ORG_CODIGO", c => c.Int(nullable: false));
            AlterColumn("dbo.Catalogos", "ID_INSTITUCION", c => c.Int(nullable: false));
            AlterColumn("dbo.Catalogos", "PERI_CODIGO", c => c.Int(nullable: false));
            AlterColumn("dbo.Catalogos", "TRE_CODIGO", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" });
            CreateIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            CreateIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            AddForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, cascadeDelete: true);
            AddForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "dbo.TipoRecursos");
            DropForeignKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            DropIndex("dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropPrimaryKey("dbo.Catalogos");
            AlterColumn("dbo.Catalogos", "TRE_CODIGO", c => c.Int());
            AlterColumn("dbo.Catalogos", "PERI_CODIGO", c => c.Int());
            AlterColumn("dbo.Catalogos", "ID_INSTITUCION", c => c.Int());
            AlterColumn("dbo.Catalogos", "ORG_CODIGO", c => c.Int());
            AlterColumn("dbo.Catalogos", "PERI_CODIGO", c => c.Int());
            AlterColumn("dbo.Catalogos", "ID_INSTITUCION", c => c.Int());
            AddPrimaryKey("dbo.Catalogos", new[] { "ID_INSTITUCION", "CAT_CODIGO" });
            RenameColumn(table: "dbo.Catalogos", name: "TRE_CODIGO", newName: "TIPORECURSOS_TRE_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "PERI_CODIGO", newName: "TIPORECURSOS_PERI_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "ID_INSTITUCION", newName: "TIPORECURSOS_ID_INSTITUCION");
            RenameColumn(table: "dbo.Catalogos", name: "ORG_CODIGO", newName: "ORGANICOF_ORG_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "PERI_CODIGO", newName: "ORGANICOF_PERI_CODIGO");
            RenameColumn(table: "dbo.Catalogos", name: "ID_INSTITUCION", newName: "ORGANICOF_ID_INSTITUCION");
            AddColumn("dbo.Catalogos", "TRE_CODIGO", c => c.Int(nullable: false));
            AddColumn("dbo.Catalogos", "ID_INSTITUCION", c => c.Int(nullable: false));
            AddColumn("dbo.Catalogos", "ORG_CODIGO", c => c.Int(nullable: false));
            AddColumn("dbo.Catalogos", "ID_INSTITUCION", c => c.Int(nullable: false));
            CreateIndex("dbo.Catalogos", new[] { "TIPORECURSOS_ID_INSTITUCION", "TIPORECURSOS_PERI_CODIGO", "TIPORECURSOS_TRE_CODIGO" });
            CreateIndex("dbo.Catalogos", new[] { "ORGANICOF_ID_INSTITUCION", "ORGANICOF_PERI_CODIGO", "ORGANICOF_ORG_CODIGO" });
            AddForeignKey("dbo.Catalogos", new[] { "TIPORECURSOS_ID_INSTITUCION", "TIPORECURSOS_PERI_CODIGO", "TIPORECURSOS_TRE_CODIGO" }, "dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            AddForeignKey("dbo.Catalogos", new[] { "ORGANICOF_ID_INSTITUCION", "ORGANICOF_PERI_CODIGO", "ORGANICOF_ORG_CODIGO" }, "dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
        }
    }
}
