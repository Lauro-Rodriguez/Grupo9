namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POA_PLANOBJETIVOS", "ES_GAPR", c => c.Boolean(nullable: false));
            AddColumn("dbo.Catalogos", "COD_CPC", c => c.String(maxLength: 9));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalogos", "COD_CPC");
            DropColumn("dbo.POA_PLANOBJETIVOS", "ES_GAPR");
        }
    }
}
