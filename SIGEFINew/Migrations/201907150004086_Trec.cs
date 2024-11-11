namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trec : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TipoRecursos", "TRE_NOMBRE", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TipoRecursos", "TRE_NOMBRE", c => c.Int(nullable: false));
        }
    }
}
