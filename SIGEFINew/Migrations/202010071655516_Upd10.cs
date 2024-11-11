namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CO_CLASIFRETEN", "PORCENTAJE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CO_CLASIFRETEN", "PORCENTAJE", c => c.Int(nullable: false));
        }
    }
}
