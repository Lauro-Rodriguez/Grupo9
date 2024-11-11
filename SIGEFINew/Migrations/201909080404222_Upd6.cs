namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POA_OBJTIVORECURSOS", "USER_CREA", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.POA_OBJTIVORECURSOS", "FECHA_CREA", c => c.DateTime(nullable: false));
            AddColumn("dbo.POA_OBJTIVORECURSOS", "USER_MODIF", c => c.String(maxLength: 15));
            AddColumn("dbo.POA_OBJTIVORECURSOS", "FECHA_MODIF", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POA_OBJTIVORECURSOS", "FECHA_MODIF");
            DropColumn("dbo.POA_OBJTIVORECURSOS", "USER_MODIF");
            DropColumn("dbo.POA_OBJTIVORECURSOS", "FECHA_CREA");
            DropColumn("dbo.POA_OBJTIVORECURSOS", "USER_CREA");
        }
    }
}
