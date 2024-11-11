namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd22 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IN_DETCOMPRA", "CAT_CODIGO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IN_DETCOMPRA", "CAT_CODIGO", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
