namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd5 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
            CreateIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" });
            AddForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, cascadeDelete: true);
            AddForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" }, "dbo.Catalogos", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" }, "dbo.Catalogos");
            DropForeignKey("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" }, "dbo.TipoRecursos");
            DropIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CAT_CODIGO" });
            DropIndex("dbo.POA_OBJTIVORECURSOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TRE_CODIGO" });
        }
    }
}
