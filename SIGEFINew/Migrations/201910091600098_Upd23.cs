namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd23 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proveedores", "USER_MODIF", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proveedores", "USER_MODIF", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
