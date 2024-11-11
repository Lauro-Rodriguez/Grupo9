namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PR_SOLICDISPPRESUP", "USER_ACTUAL", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "ANULADO", c => c.Boolean(nullable: false));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "NUMDOC_REFER", c => c.String(maxLength: 50));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "APROBADO", c => c.Boolean(nullable: false));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "FECHA_APRUEBA", c => c.DateTime(nullable: false));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "USER_APRUEBA", c => c.String(maxLength: 15));
            AddColumn("dbo.PR_SOLICDISPPRESUP", "NUM_DISPONIB", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PR_SOLICDISPPRESUP", "NUM_DISPONIB");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "USER_APRUEBA");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "FECHA_APRUEBA");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "APROBADO");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "NUMDOC_REFER");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "ANULADO");
            DropColumn("dbo.PR_SOLICDISPPRESUP", "USER_ACTUAL");
        }
    }
}
