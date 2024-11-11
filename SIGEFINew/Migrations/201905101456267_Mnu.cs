namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mnu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SIGEFIWEB.MnuModulos",
                c => new
                    {
                        COD_MODULO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOM_MODULO = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.COD_MODULO);
            
        }
        
        public override void Down()
        {
            DropTable("SIGEFIWEB.MnuModulos");
        }
    }
}
