namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd9 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DETALLEINGRESOS", newName: "PR_INIDETINGRESOS");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PR_INIDETINGRESOS", newName: "DETALLEINGRESOS");
        }
    }
}
