namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd43 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IN_USERSXBOD",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        USUA_LOGIN = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.USUA_LOGIN })
                .ForeignKey("dbo.IN_BODEGAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_USERSXBOD", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" }, "dbo.IN_BODEGAS");
            DropIndex("dbo.IN_USERSXBOD", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" });
            DropTable("dbo.IN_USERSXBOD");
        }
    }
}
