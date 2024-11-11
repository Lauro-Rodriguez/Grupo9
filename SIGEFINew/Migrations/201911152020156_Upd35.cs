namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd35 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_RELARETCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_RET = c.String(nullable: false, maxLength: 20),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET, t.CODIGO_CG })
                .ForeignKey("dbo.CO_CLASIFRETEN", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET }, cascadeDelete: true)
                .ForeignKey("dbo.CO_CUENTASCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" }, "dbo.CO_CUENTASCONT");
            DropForeignKey("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_RET" }, "dbo.CO_CLASIFRETEN");
            DropIndex("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" });
            DropIndex("dbo.CO_RELARETCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_RET" });
            DropTable("dbo.CO_RELARETCONT");
        }
    }
}
