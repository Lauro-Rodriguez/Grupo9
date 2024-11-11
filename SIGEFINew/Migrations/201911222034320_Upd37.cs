namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd37 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            CreateIndex("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            AddForeignKey("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropIndex("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            CreateIndex("dbo.PR_SOLICDISPPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
        }
    }
}
