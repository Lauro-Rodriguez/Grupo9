namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PR_INIDETINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes");
            DropIndex("dbo.PR_INIDETINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" });
            DropTable("dbo.PR_INIDETINGRESOS");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PR_INIDETINGRESOS",
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
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAIN_CODIGO });
            
            CreateIndex("dbo.PR_INIDETINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" });
            AddForeignKey("dbo.PR_INIDETINGRESOS", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, "dbo.PartIngresoes", new[] { "ID_INSTITUCION", "PAIN_CODIGO" }, cascadeDelete: true);
        }
    }
}
