namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd34 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CO_CLASIFRETEN", "TIPO_CONTRIB", c => c.Int(nullable: false));
            DropColumn("dbo.CO_CLASIFRETEN", "CONTRIB_ESP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CO_CLASIFRETEN", "CONTRIB_ESP", c => c.Boolean(nullable: false));
            DropColumn("dbo.CO_CLASIFRETEN", "TIPO_CONTRIB");
        }
    }
}
