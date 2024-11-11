namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd32 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FIRMAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        USUA_FIRMA = c.String(nullable: false, maxLength: 15),
                        CODIGO_DOC = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        NOMBRE = c.String(nullable: false, maxLength: 50),
                        CARGO = c.String(nullable: false, maxLength: 50),
                        TIPO = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.USUA_FIRMA, t.CODIGO_DOC, t.NUM_FILA })
                .ForeignKey("dbo.Documentos", t => t.CODIGO_DOC, cascadeDelete: true)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.CODIGO_DOC);
            
            CreateTable(
                "dbo.Documentos",
                c => new
                    {
                        CODIGO_DOC = c.Int(nullable: false, identity: true),
                        NOMBRE_DOC = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.CODIGO_DOC);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FIRMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.FIRMAS", "CODIGO_DOC", "dbo.Documentos");
            DropIndex("dbo.FIRMAS", new[] { "CODIGO_DOC" });
            DropIndex("dbo.FIRMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.Documentos");
            DropTable("dbo.FIRMAS");
        }
    }
}
