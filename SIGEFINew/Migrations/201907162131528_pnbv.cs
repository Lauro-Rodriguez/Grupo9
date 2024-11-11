namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pnbv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_PNBV",
                c => new
                    {
                        COD_PNVB = c.Int(nullable: false, identity: true),
                        NOM_PNVB = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.COD_PNVB);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.POA_PNBV");
        }
    }
}
