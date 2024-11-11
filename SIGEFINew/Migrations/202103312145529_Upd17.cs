namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IN_PARAM", "ING_MANUAL_ALERTSTOCK", c => c.Boolean(nullable: false));
            AddColumn("dbo.IN_PARAM", "ALERTSTOCK_MINMAX", c => c.Boolean(nullable: false));
            AddColumn("dbo.IN_PARAM", "CTA_INVERSION", c => c.Int(nullable: false));
            AddColumn("dbo.IN_PARAM", "PERM_QING_M_QORD", c => c.Boolean(nullable: false));
            AddColumn("dbo.IN_PARAM", "CAM_P_UNITARIO", c => c.Boolean(nullable: false));
            AddColumn("dbo.IN_PARAM", "NOT_TRANSINCONT", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IN_PARAM", "NOT_TRANSINCONT");
            DropColumn("dbo.IN_PARAM", "CAM_P_UNITARIO");
            DropColumn("dbo.IN_PARAM", "PERM_QING_M_QORD");
            DropColumn("dbo.IN_PARAM", "CTA_INVERSION");
            DropColumn("dbo.IN_PARAM", "ALERTSTOCK_MINMAX");
            DropColumn("dbo.IN_PARAM", "ING_MANUAL_ALERTSTOCK");
        }
    }
}
