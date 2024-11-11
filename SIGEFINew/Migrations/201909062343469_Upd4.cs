namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_OBJTIVORECURSOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        TRE_CODIGO = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
                        CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CARACTERISITCAS = c.String(nullable: false, maxLength: 250),
                        FECHA_INI = c.DateTime(nullable: false),
                        CONTRAT_NUEVA = c.Boolean(nullable: false),
                        MES_CONTRAT = c.Int(nullable: false),
                        TIPO_ASIGPAC = c.Int(nullable: false),
                        OBSERVACIONES = c.String(maxLength: 250),
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
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.TRE_CODIGO, t.CAT_CODIGO })
                .ForeignKey("dbo.POA_PLANOBJETIVOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" }, "dbo.POA_PLANOBJETIVOS");
            DropIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_PLAN", "ORG_CODIGO", "COD_PLANOBJTVO" });
            DropTable("dbo.POA_OBJTIVORECURSOS");
        }
    }
}
