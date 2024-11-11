namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ID_INSTITUCION", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ID_INSTITUCION");
        }
    }
}
