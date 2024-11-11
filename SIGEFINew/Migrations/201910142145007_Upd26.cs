namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POA_PLANOBJETIVOS", "CANTIDAD_META", c => c.Int(nullable: false));
            AddColumn("dbo.POA_PLANOBJETIVOS", "DESCRIPCION_META", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POA_PLANOBJETIVOS", "DESCRIPCION_META");
            DropColumn("dbo.POA_PLANOBJETIVOS", "CANTIDAD_META");
        }
    }
}
