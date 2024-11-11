namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Periodos", "NUMDOC_APROB", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Periodos", "NUMDOC_APROB", c => c.String());
        }
    }
}
