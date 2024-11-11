namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd28 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_PYHISTORIAL",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PLAN = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        COD_PLANOBJTVO = c.String(nullable: false, maxLength: 15),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        CODIGO_DOC = c.Int(nullable: false),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        ESTADO_PY = c.String(nullable: false, maxLength: 2),
                        FECHA_GENERA = c.DateTime(nullable: false),
                        USER_GENERA = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PLAN, t.ORG_CODIGO, t.COD_PLANOBJTVO, t.NUM_FILA })
                .ForeignKey("dbo.Documentos", t => t.CODIGO_DOC, cascadeDelete: true)
                .Index(t => t.CODIGO_DOC);
            
            AddColumn("dbo.POA_PLANOBJETIVOS", "ESTADO_PY", c => c.String(nullable: false, maxLength: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_PYHISTORIAL", "CODIGO_DOC", "dbo.Documentos");
            DropIndex("dbo.POA_PYHISTORIAL", new[] { "CODIGO_DOC" });
            DropColumn("dbo.POA_PLANOBJETIVOS", "ESTADO_PY");
            DropTable("dbo.POA_PYHISTORIAL");
        }
    }
}
