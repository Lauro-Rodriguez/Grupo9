namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd38 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PR_MOVGASTO", "COD_PROV");
            AddForeignKey("dbo.PR_MOVGASTO", "COD_PROV", "dbo.Proveedores", "COD_PROV", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PR_MOVGASTO", "COD_PROV", "dbo.Proveedores");
            DropIndex("dbo.PR_MOVGASTO", new[] { "COD_PROV" });
        }
    }
}
