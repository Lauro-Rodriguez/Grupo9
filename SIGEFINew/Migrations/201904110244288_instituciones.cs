namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class instituciones : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Instituciones", "LOGO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instituciones", "LOGO", c => c.Binary(storeType: "image"));
        }
    }
}
