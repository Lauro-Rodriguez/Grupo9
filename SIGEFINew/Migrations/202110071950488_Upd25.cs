namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_FACTURAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUM_DOC = c.String(nullable: false, maxLength: 20),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        TIPOSRI = c.String(nullable: false, maxLength: 2),
                        FECHA = c.DateTime(nullable: false),
                        FECHA_DOC = c.DateTime(nullable: false),
                        VALOR_DOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IVA_PORC = c.Int(nullable: false),
                        TOT_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IG = c.Boolean(nullable: false),
                        AUTORIZACION = c.String(maxLength: 49),
                        FECHACAD_DOC = c.DateTime(nullable: false),
                        APLICADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_PROV, t.NUM_DOC })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PROV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_FACTURAS", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.CO_FACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.CO_FACTURAS", new[] { "COD_PROV" });
            DropIndex("dbo.CO_FACTURAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.CO_FACTURAS");
        }
    }
}
