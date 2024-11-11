namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parroquias", "ID_PROVINCIA", "dbo.Provincias");
            CreateTable(
                "dbo.Barrios",
                c => new
                    {
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                        ID_BARRIO = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA, t.ID_BARRIO })
                .ForeignKey("dbo.Parroquias", t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA }, cascadeDelete: true)
                .Index(t => new { t.ID_PROVINCIA, t.ID_CANTON, t.ID_PARROQUIA });
            
            AddColumn("dbo.POA_PLANOBJETIVOS", "ID_PROVINCIA", c => c.Int(nullable: false));
            AddColumn("dbo.POA_PLANOBJETIVOS", "ID_CANTON", c => c.Int(nullable: false));
            AddColumn("dbo.POA_PLANOBJETIVOS", "ID_PARROQUIA", c => c.Int(nullable: false));
            AddColumn("dbo.POA_PLANOBJETIVOS", "ID_BARRIO", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Barrios", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" }, "dbo.Parroquias");
            DropIndex("dbo.Barrios", new[] { "ID_PROVINCIA", "ID_CANTON", "ID_PARROQUIA" });
            DropColumn("dbo.POA_PLANOBJETIVOS", "ID_BARRIO");
            DropColumn("dbo.POA_PLANOBJETIVOS", "ID_PARROQUIA");
            DropColumn("dbo.POA_PLANOBJETIVOS", "ID_CANTON");
            DropColumn("dbo.POA_PLANOBJETIVOS", "ID_PROVINCIA");
            DropTable("dbo.Barrios");
            AddForeignKey("dbo.Parroquias", "ID_PROVINCIA", "dbo.Provincias", "ID_PROVINCIA", cascadeDelete: true);
        }
    }
}
