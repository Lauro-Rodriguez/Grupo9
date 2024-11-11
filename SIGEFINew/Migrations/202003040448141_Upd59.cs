namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd59 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RP_TIPOSANTICIPO", "FECHA_MODIF", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RP_TIPOSANTICIPO", "FECHA_MODIF", c => c.DateTime());
        }
    }
}
