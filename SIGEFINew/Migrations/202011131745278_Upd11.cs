namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CO_ASIENTOSCONT", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_DETALLEASIENTOS", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_CUENTASCONT", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_CLASIFRETEN", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_DETALLEBANCOS", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_LINEACREDITO", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_CUENBANCOS", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_CONCILIACIONES", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_CTASPPAGAR", "FECHA_MODIF", c => c.DateTime());
            AlterColumn("dbo.CO_DETCTASPPAG", "FECHA_MODIF", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CO_DETCTASPPAG", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_CTASPPAGAR", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_CONCILIACIONES", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_CUENBANCOS", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_LINEACREDITO", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_DETALLEBANCOS", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_CLASIFRETEN", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_CUENTASCONT", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_DETALLEASIENTOS", "FECHA_MODIF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CO_ASIENTOSCONT", "FECHA_MODIF", c => c.DateTime(nullable: false));
        }
    }
}
