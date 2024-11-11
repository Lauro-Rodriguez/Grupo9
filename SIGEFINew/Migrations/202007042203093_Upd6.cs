namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PR_MOVGASTO", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.PR_MOVINGRESO", "FECHA_MODIF", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PR_MOVINGRESO", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PR_MOVGASTO", "FECHA_MODIF", c => c.DateTime(nullable: false));
        }
    }
}
