namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TR_HISTORIAL",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_DOC = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NUM_DOCUM = c.Int(nullable: false),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        FECHA_GENERA = c.DateTime(nullable: false),
                        USER_GENERA = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_DOC, t.ORG_CODIGO, t.NUM_DOCUM })
                .ForeignKey("dbo.Documentos", t => t.CODIGO_DOC, cascadeDelete: false)
                .Index(t => t.CODIGO_DOC);
            
            AddColumn("dbo.PR_SOLICDISPPRESUP", "USER_ACTUAL", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "ANULADO", c => c.Boolean(nullable: false));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "NUMDOC_REFER", c => c.String(maxLength: 50));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "ESTADO", c => c.String(maxLength: 2));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "FECHA_FINALIZA", c => c.DateTime());
            AddColumn("dbo.PR_SOLICDISPPRESUP", "NUM_DISPONIB", c => c.Int(nullable: false));
            AlterColumn("dbo.PR_SOLICDISPPRESUP", "FECHA_MODIF", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TR_HISTORIAL", "CODIGO_DOC", "dbo.Documentos");
            DropIndex("dbo.TR_HISTORIAL", new[] { "CODIGO_DOC" });
            AlterColumn("dbo.PR_SOLICDISPPRESUP", "FECHA_MODIF", c => c.DateTime(nullable: false));
            DropColumn("dbo.PR_SOLICDISPPRESUP", "NUM_DISPONIB");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "FECHA_FINALIZA");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "ESTADO");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "NUMDOC_REFER");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "ANULADO");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "USER_ACTUAL");
            DropTable("dbo.TR_HISTORIAL");
        }
    }
}
