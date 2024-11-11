namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_EVALUACION",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        COD_FASE = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        FECHA = c.DateTime(nullable: false),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        ESTADO_EVAL = c.Int(),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.COD_FASE, t.NUM_FILA })
                .ForeignKey("dbo.POA_FASESPROYS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.COD_FASE }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.COD_FASE });
            
            CreateTable(
                "dbo.DOCUMSPDFS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO = c.String(nullable: false, maxLength: 20),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        MODULO = c.String(nullable: false, maxLength: 50),
                        PATH = c.String(nullable: false, maxLength: 250),
                        NOM_ARCHIVO = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO, t.NUM_FILA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.NOM_ARCHIVO, unique: true);
            
            CreateTable(
                "dbo.IN_UNIOPE",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_UNIDOPERATIVA = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        ORG_CODIGO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_UNIDOPERATIVA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            AddColumn("dbo.POA_FASESPROYS", "FECHA", c => c.DateTime(nullable: false));
            AddColumn("dbo.POA_FASESPROYS", "DEVENGADO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.IN_COMPRAS", "ID_UNIDOPERATIVA", c => c.Int());
            AddColumn("dbo.TR_HISTORIAL", "ID_EMPLEADO", c => c.Int(nullable: false));
            AddColumn("dbo.TR_HISTORIAL", "ORG_ENVIO", c => c.Int(nullable: false));
            DropColumn("dbo.POA_FASESPROYS", "ORDEN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POA_FASESPROYS", "ORDEN", c => c.Int(nullable: false));
            DropForeignKey("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.DOCUMSPDFS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.POA_EVALUACION", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO", "COD_FASE" }, "dbo.POA_FASESPROYS");
            DropIndex("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.DOCUMSPDFS", new[] { "NOM_ARCHIVO" });
            DropIndex("dbo.DOCUMSPDFS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.POA_EVALUACION", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO", "COD_FASE" });
            DropColumn("dbo.TR_HISTORIAL", "ORG_ENVIO");
            DropColumn("dbo.TR_HISTORIAL", "ID_EMPLEADO");
            DropColumn("dbo.IN_COMPRAS", "ID_UNIDOPERATIVA");
            DropColumn("dbo.POA_FASESPROYS", "DEVENGADO");
            DropColumn("dbo.POA_FASESPROYS", "FECHA");
            DropTable("dbo.IN_UNIOPE");
            DropTable("dbo.DOCUMSPDFS");
            DropTable("dbo.POA_EVALUACION");
        }
    }
}
