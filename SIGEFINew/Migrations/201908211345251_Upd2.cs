namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            AddForeignKey("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropIndex("dbo.POA_PLANOBJETIVOS", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
        }
    }
}
