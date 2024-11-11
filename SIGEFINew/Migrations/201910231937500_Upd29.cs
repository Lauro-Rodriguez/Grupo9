namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd29 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_RELACION",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        EJERCICIO = c.Int(nullable: false),
                        PPDEBITO = c.String(maxLength: 10),
                        PPCREDITO = c.String(maxLength: 10),
                        CUENTA_COBRO = c.String(maxLength: 10),
                        CUENTA_PAGO = c.String(maxLength: 10),
                        ENVIADO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG, t.EJERCICIO })
                .ForeignKey("dbo.CO_CUENTASCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG }, cascadeDelete: true)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_RELACION", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_RELACION", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" }, "dbo.CO_CUENTASCONT");
            DropIndex("dbo.CO_RELACION", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" });
            DropTable("dbo.CO_RELACION");
        }
    }
}
