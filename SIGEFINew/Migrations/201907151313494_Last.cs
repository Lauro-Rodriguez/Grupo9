namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Last : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoRecursos",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        TRE_CODIGO = c.Int(nullable: false),
                        TRE_NOMBRE = c.String(nullable: false, maxLength: 100),
                        ORG_CODIGO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TRE_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropForeignKey("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropIndex("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.TipoRecursos");
        }
    }
}
