namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_INDICADOROBJ",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        COD_INDICADOROJ = c.String(nullable: false, maxLength: 15),
                        TIPOOBJ = c.Int(nullable: false),
                        MEDIOSVERIF = c.String(nullable: false, maxLength: 250),
                        VAL_ENERO = c.Int(nullable: false),
                        VAL_FEBRERO = c.Int(nullable: false),
                        VAL_MARZO = c.Int(nullable: false),
                        VAL_ABRIL = c.Int(nullable: false),
                        VAL_MAYO = c.Int(nullable: false),
                        VAL_JUNIO = c.Int(nullable: false),
                        VAL_JULIO = c.Int(nullable: false),
                        VAL_AGOSTO = c.Int(nullable: false),
                        VAL_SEPTIEMBRE = c.Int(nullable: false),
                        VAL_OCTUBRE = c.Int(nullable: false),
                        VAL_NOVIEMBRE = c.Int(nullable: false),
                        VAL_DICIEMBRE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.COD_INDICADOROJ })
                .ForeignKey("dbo.POA_PLANOBJETIVOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_INDICADOROBJ", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" }, "dbo.POA_PLANOBJETIVOS");
            DropIndex("dbo.POA_INDICADOROBJ", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" });
            DropTable("dbo.POA_INDICADOROBJ");
        }
    }
}
