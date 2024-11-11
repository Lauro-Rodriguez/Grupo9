namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Direc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganicoFs", "DIRECTIVO", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrganicoFs", "DIRECTIVO");
        }
    }
}
