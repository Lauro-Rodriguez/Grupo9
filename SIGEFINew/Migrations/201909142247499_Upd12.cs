namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TECHOSPRESUPs",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        TECHO_CODIGO = c.Int(nullable: false, identity: true),
                        VALOR_CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_PORCENTAJE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO, t.TECHO_CODIGO })
                .ForeignKey("dbo.OrganicoFs", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ORG_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TECHOSPRESUPs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropIndex("dbo.TECHOSPRESUPs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropTable("dbo.TECHOSPRESUPs");
        }
    }
}
