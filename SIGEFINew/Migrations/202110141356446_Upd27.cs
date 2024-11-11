namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd27 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RP_CARGOS", new[] { "ID_INSTITUCION", "ID_GRUPOCUPA" });
            AddForeignKey("dbo.RP_CARGOS", new[] { "ID_INSTITUCION", "ID_GRUPOCUPA" }, "dbo.RP_GRUPOCUPACIONAL", new[] { "ID_INSTITUCION", "ID_GRUPOCUPA" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RP_CARGOS", new[] { "ID_INSTITUCION", "ID_GRUPOCUPA" }, "dbo.RP_GRUPOCUPACIONAL");
            DropIndex("dbo.RP_CARGOS", new[] { "ID_INSTITUCION", "ID_GRUPOCUPA" });
        }
    }
}
