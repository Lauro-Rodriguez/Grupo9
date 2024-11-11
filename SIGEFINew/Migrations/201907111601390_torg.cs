namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class torg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganicoFs",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NIV_PADRE = c.Int(nullable: false),
                        NIV_HIJO = c.Int(nullable: false),
                        ORG_CLAVE = c.String(nullable: false, maxLength: 50),
                        ORG_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.OrganicoFs");
        }
    }
}
