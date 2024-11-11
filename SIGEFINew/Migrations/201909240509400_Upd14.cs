namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DETALLEGRESOS", "USER_CREA", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.DETALLEGRESOS", "FECHA_CREA", c => c.DateTime(nullable: false));
            AddColumn("dbo.DETALLEGRESOS", "USER_MODIF", c => c.String(maxLength: 15));
            AddColumn("dbo.DETALLEGRESOS", "FECHA_MODIF", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DETALLEGRESOS", "FECHA_MODIF");
            DropColumn("dbo.DETALLEGRESOS", "USER_MODIF");
            DropColumn("dbo.DETALLEGRESOS", "FECHA_CREA");
            DropColumn("dbo.DETALLEGRESOS", "USER_CREA");
        }
    }
}
