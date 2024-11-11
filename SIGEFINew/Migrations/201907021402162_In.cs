namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class In : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
        }
    }
}
