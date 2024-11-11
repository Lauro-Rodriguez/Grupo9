namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd45 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IN_TIPOSTRAN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_TIPOTRAN = c.Int(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 2),
                        DESCRIP_TIPOTRAN = c.String(nullable: false, maxLength: 100),
                        ENV_CO_CC = c.Int(nullable: false),
                        CON_IVA = c.Boolean(nullable: false),
                        CON_DONANTE = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_TIPOTRAN })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            AlterColumn("dbo.IN_CARACGEN", "DESCRIP_CAGEN", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_TIPOSTRAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.IN_TIPOSTRAN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            AlterColumn("dbo.IN_CARACGEN", "DESCRIP_CAGEN", c => c.String(nullable: false, maxLength: 50));
            DropTable("dbo.IN_TIPOSTRAN");
        }
    }
}
