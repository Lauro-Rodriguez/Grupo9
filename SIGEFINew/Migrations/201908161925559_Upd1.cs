namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_FONDOXOBJTVO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        ID_FONDO = c.Int(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.ID_FONDO })
                .ForeignKey("dbo.POA_PLANOBJETIVOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_FONDOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO });
            
            CreateTable(
                "dbo.PR_FONDOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_FONDO = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 90),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" }, "dbo.PR_FONDOS");
            DropForeignKey("dbo.PR_FONDOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" }, "dbo.POA_PLANOBJETIVOS");
            DropIndex("dbo.PR_FONDOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" });
            DropIndex("dbo.POA_FONDOXOBJTVO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" });
            DropTable("dbo.PR_FONDOS");
            DropTable("dbo.POA_FONDOXOBJTVO");
        }
    }
}
