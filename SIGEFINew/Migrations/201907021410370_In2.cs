namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class In2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
            CreateIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
            CreateIndex("dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CLAVE" });
        }
    }
}
