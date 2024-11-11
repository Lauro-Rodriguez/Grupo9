namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd28 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PR_RELACONTAB",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        TIPO = c.Int(nullable: false),
                        CODIGO_PARTIDA = c.Int(nullable: false),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        CODIGO_CGB = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_FILA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PR_RELACONTAB", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.PR_RELACONTAB", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.PR_RELACONTAB");
        }
    }
}
