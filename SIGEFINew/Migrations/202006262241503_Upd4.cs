namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Periodos", "FECHA_APROB", c => c.DateTime());
            AddColumn("dbo.Periodos", "NUMDOC_APROB", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Periodos", "NUMDOC_APROB");
            DropColumn("dbo.Periodos", "FECHA_APROB");
        }
    }
}
