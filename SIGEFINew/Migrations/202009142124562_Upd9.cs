namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "FECHA_MODIF", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "FECHA_MODIF", c => c.DateTime(nullable: false));
        }
    }
}
