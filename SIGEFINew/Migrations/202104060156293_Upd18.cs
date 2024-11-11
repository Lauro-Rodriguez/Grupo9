namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IN_BODEGAS", "NUM_DEV_INGRE", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_DEV_EGRE", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_TRAN_INGRE", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_TRAN_EGRE", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_SIN_STOCKI", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_SIN_STOCKE", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_GUIA_REMI", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_ORDEN_PROD", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "NUM_PED_MAT", c => c.Single(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "FECHA_ULT_REG", c => c.DateTime());
            AddColumn("dbo.IN_BODEGAS", "USA_CODBAR", c => c.Boolean(nullable: false));
            AddColumn("dbo.IN_BODEGAS", "IVA_COSTOPROD", c => c.Boolean(nullable: false));
            AddColumn("dbo.IN_USERSXBOD", "RESP", c => c.Boolean(nullable: false));
            AddColumn("dbo.IN_USERSXBOD", "FECHA_DESDE", c => c.DateTime(nullable: false));
            AddColumn("dbo.IN_USERSXBOD", "FECHA_HASTA", c => c.DateTime(nullable: false));
            DropColumn("dbo.IN_BODEGAS", "RESP_BODEGA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IN_BODEGAS", "RESP_BODEGA", c => c.String(maxLength: 50));
            DropColumn("dbo.IN_USERSXBOD", "FECHA_HASTA");
            DropColumn("dbo.IN_USERSXBOD", "FECHA_DESDE");
            DropColumn("dbo.IN_USERSXBOD", "RESP");
            DropColumn("dbo.IN_BODEGAS", "IVA_COSTOPROD");
            DropColumn("dbo.IN_BODEGAS", "USA_CODBAR");
            DropColumn("dbo.IN_BODEGAS", "FECHA_ULT_REG");
            DropColumn("dbo.IN_BODEGAS", "NUM_PED_MAT");
            DropColumn("dbo.IN_BODEGAS", "NUM_ORDEN_PROD");
            DropColumn("dbo.IN_BODEGAS", "NUM_GUIA_REMI");
            DropColumn("dbo.IN_BODEGAS", "NUM_SIN_STOCKE");
            DropColumn("dbo.IN_BODEGAS", "NUM_SIN_STOCKI");
            DropColumn("dbo.IN_BODEGAS", "NUM_TRAN_EGRE");
            DropColumn("dbo.IN_BODEGAS", "NUM_TRAN_INGRE");
            DropColumn("dbo.IN_BODEGAS", "NUM_DEV_EGRE");
            DropColumn("dbo.IN_BODEGAS", "NUM_DEV_INGRE");
        }
    }
}
