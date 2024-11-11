namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cantones", "ID_PROVINCIA", "dbo.Provincias");
            DropIndex("dbo.Cantones", new[] { "ID_PROVINCIA" });
            DropPrimaryKey("dbo.Cantones");
            DropPrimaryKey("dbo.Provincias");
            AlterColumn("dbo.Cantones", "ID_PROVINCIA", c => c.Decimal(nullable: false, precision: 18, scale: 0, storeType: "numeric"));
            AlterColumn("dbo.Cantones", "ID_CANTON", c => c.Decimal(nullable: false, precision: 18, scale: 0, storeType: "numeric"));
            AlterColumn("dbo.Provincias", "ID_PROVINCIA", c => c.Decimal(nullable: false, precision: 18, scale: 0, identity: true, storeType: "numeric"));
            AddPrimaryKey("dbo.Cantones", new[] { "ID_PROVINCIA", "ID_CANTON" });
            AddPrimaryKey("dbo.Provincias", "ID_PROVINCIA");
            CreateIndex("dbo.Cantones", "ID_PROVINCIA");
            AddForeignKey("dbo.Cantones", "ID_PROVINCIA", "dbo.Provincias", "ID_PROVINCIA", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cantones", "ID_PROVINCIA", "dbo.Provincias");
            DropIndex("dbo.Cantones", new[] { "ID_PROVINCIA" });
            DropPrimaryKey("dbo.Provincias");
            DropPrimaryKey("dbo.Cantones");
            AlterColumn("dbo.Provincias", "ID_PROVINCIA", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.Cantones", "ID_CANTON", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AlterColumn("dbo.Cantones", "ID_PROVINCIA", c => c.Decimal(nullable: false, precision: 18, scale: 2, storeType: "numeric"));
            AddPrimaryKey("dbo.Provincias", "ID_PROVINCIA");
            AddPrimaryKey("dbo.Cantones", new[] { "ID_PROVINCIA", "ID_CANTON" });
            CreateIndex("dbo.Cantones", "ID_PROVINCIA");
            AddForeignKey("dbo.Cantones", "ID_PROVINCIA", "dbo.Provincias", "ID_PROVINCIA", cascadeDelete: true);
        }
    }
}
