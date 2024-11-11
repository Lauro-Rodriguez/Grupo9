namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PR_SOLICDISPPRESUP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_DOC = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NUM_SOLIC = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        FECHA_SOLICITA = c.DateTime(nullable: false),
                        CON_PAC = c.Boolean(nullable: false),
                        COD_PLANOBJTVO = c.String(maxLength: 15),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_DETALSOLICDISPPRES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_DOC = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NUM_SOLIC = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 128),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC, t.CAT_CODIGO })
                .ForeignKey("dbo.PR_SOLICDISPPRESUP", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_SOLIC });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PR_DETALSOLICDISPPRES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_DOC", "ORG_CODIGO", "NUM_SOLIC" }, "dbo.PR_SOLICDISPPRESUP");
            DropForeignKey("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.PR_DETALSOLICDISPPRES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_DOC", "ORG_CODIGO", "NUM_SOLIC" });
            DropIndex("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.PR_DETALSOLICDISPPRES");
            DropTable("dbo.PR_SOLICDISPPRESUP");
        }
    }
}
