namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proveedores", "OBLIGA_CONTAB", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proveedores", "OBLIGA_CONTAB");
        }
    }
}
