namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd29 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_FASESPROYS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        COD_FASE = c.Int(nullable: false, identity: true),
                        COD_CPTO = c.Int(nullable: false),
                        COD_INDICADOROJ = c.String(),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        VALOR = c.Int(nullable: false),
                        ORDEN = c.Int(nullable: false),
                        ESTADO = c.String(nullable: false, maxLength: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.COD_FASE })
                .ForeignKey("dbo.POA_CPTOEJECUCION", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CPTO }, cascadeDelete: true)
                .ForeignKey("dbo.POA_PLANOBJETIVOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CPTO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO });
            
            CreateTable(
                "dbo.POA_CPTOEJECUCION",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CPTO = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 250),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CPTO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_FASESPROYS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" }, "dbo.POA_PLANOBJETIVOS");
            DropForeignKey("dbo.POA_FASESPROYS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CPTO" }, "dbo.POA_CPTOEJECUCION");
            DropForeignKey("dbo.POA_CPTOEJECUCION", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.POA_CPTOEJECUCION", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.POA_FASESPROYS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" });
            DropIndex("dbo.POA_FASESPROYS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CPTO" });
            DropTable("dbo.POA_CPTOEJECUCION");
            DropTable("dbo.POA_FASESPROYS");
        }
    }
}
