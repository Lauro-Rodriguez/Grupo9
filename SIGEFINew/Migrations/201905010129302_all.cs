namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Periodos");
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "NumIdentif", c => c.String());
            AddColumn("dbo.AspNetUsers", "TypeUser", c => c.String());
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "FechaReg", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Periodos", "PERI_CODIGO", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Periodos", new[] { "PERI_CODIGO", "ID_INSTITUCION" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Periodos");
            AlterColumn("dbo.Periodos", "PERI_CODIGO", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "FechaReg");
            DropColumn("dbo.AspNetUsers", "Active");
            DropColumn("dbo.AspNetUsers", "TypeUser");
            DropColumn("dbo.AspNetUsers", "NumIdentif");
            DropColumn("dbo.AspNetUsers", "FullName");
            AddPrimaryKey("dbo.Periodos", new[] { "PERI_CODIGO", "ID_INSTITUCION" });
        }
    }
}
