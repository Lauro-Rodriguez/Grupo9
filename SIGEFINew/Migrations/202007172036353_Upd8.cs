namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd8 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TR_HISTORIAL");
            AddColumn("dbo.TR_HISTORIAL", "NUM_FILA", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TR_HISTORIAL", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_DOC", "ORG_CODIGO", "NUM_DOCUM", "NUM_FILA" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TR_HISTORIAL");
            DropColumn("dbo.TR_HISTORIAL", "NUM_FILA");
            AddPrimaryKey("dbo.TR_HISTORIAL", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_DOC", "ORG_CODIGO", "NUM_DOCUM" });
        }
    }
}
