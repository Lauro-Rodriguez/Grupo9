namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CO_DETCTASPPAG", "CODIGO_RET", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CO_DETCTASPPAG", "CODIGO_RET");
        }
    }
}
