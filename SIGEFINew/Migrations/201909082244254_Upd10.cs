namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DETALLEINGRESOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PAIN_CODIGO = c.Int(nullable: false),
                        DETI_VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAIN_CODIGO })
                .ForeignKey("dbo.PartIngresoes", t => new { t.ID_INSTITUCION, t.PAIN_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PAIN_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes");
            DropIndex("dbo.DETALLEINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" });
            DropTable("dbo.DETALLEINGRESOS");
        }
    }
}
