namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FORMAS_PAGO",
                c => new
                    {
                        CODIGO = c.String(nullable: false, maxLength: 2),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CODIGO);
            
            CreateTable(
                "dbo.SUSTENTOTRIBUTs",
                c => new
                    {
                        COD_SUSTENTO = c.String(nullable: false, maxLength: 2),
                        DESCRIPCION = c.String(nullable: false, maxLength: 120),
                    })
                .PrimaryKey(t => t.COD_SUSTENTO);
            
            AddColumn("dbo.PR_PARTEGRESOS", "CODI_SIGEF", c => c.String(maxLength: 30));
            AddColumn("dbo.PR_PARTEGRESOS", "NOMBRE_SIGEF", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PR_PARTEGRESOS", "NOMBRE_SIGEF");
            DropColumn("dbo.PR_PARTEGRESOS", "CODI_SIGEF");
            DropTable("dbo.SUSTENTOTRIBUTs");
            DropTable("dbo.FORMAS_PAGO");
        }
    }
}
