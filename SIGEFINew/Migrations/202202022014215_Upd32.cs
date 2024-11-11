namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RP_SETUP", "VAL_CBASICA", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RP_SETUP", "NUM_CBASICA", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RP_SETUP", "NUM_CBASICA");
            DropColumn("dbo.RP_SETUP", "VAL_CBASICA");
        }
    }
}
