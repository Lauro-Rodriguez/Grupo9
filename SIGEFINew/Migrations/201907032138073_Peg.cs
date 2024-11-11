namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Peg : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PAEG_CLAVE" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PartEgresoes", new[] { "ID_INSTITUCION", "PAEG_CLAVE" });
        }
    }
}
