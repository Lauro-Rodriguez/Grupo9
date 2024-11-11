namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TipoRecursos", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
        }
    }
}
