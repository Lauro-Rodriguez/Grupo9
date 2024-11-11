namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd30 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CO_CUENTASCONT", "USER_MODIF", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CO_CUENTASCONT", "USER_MODIF", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
