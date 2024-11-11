namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proveedores", "CODIGO_BANCO", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proveedores", "CODIGO_BANCO");
        }
    }
}
