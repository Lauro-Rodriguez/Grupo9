namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_CUENTASCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        NOMBRE_CG = c.String(nullable: false, maxLength: 110),
                        NIVEL_CG = c.Int(nullable: false),
                        TIPO_CG = c.String(nullable: false, maxLength: 1),
                        PPCREDITO = c.String(maxLength: 50),
                        PPDEBITO = c.String(maxLength: 50),
                        SIGEF = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(nullable: false, maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.CO_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.CO_CUENTASCONT");
        }
    }
}
