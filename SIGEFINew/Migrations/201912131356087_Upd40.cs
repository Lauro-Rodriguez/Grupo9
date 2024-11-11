namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd40 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IN_DETCOMPRA", "VAL_IVA", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IN_DETCOMPRA", "VAL_IVA");
        }
    }
}
