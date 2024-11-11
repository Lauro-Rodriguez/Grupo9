namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd39 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IN_COMPRAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_COMPRA = c.Int(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        FECHA_COMPRA = c.DateTime(nullable: false),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        TIPO_SRI = c.String(nullable: false, maxLength: 2),
                        FECHA_DOC = c.DateTime(nullable: false),
                        NUM_DOC = c.String(nullable: false, maxLength: 15),
                        AUTORIZACION = c.String(nullable: false, maxLength: 49),
                        FECHACAD_DOC = c.DateTime(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.COD_PROV, t.NUM_DOC }, unique: true);
            
            CreateTable(
                "dbo.IN_DETCOMPRA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_COMPRA = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
                        APLICA_IVA = c.Boolean(nullable: false),
                        PORC_IVA = c.Int(nullable: false),
                        CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COSTO_UNIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUBTOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COSTO_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA, t.NUM_FILA })
                .ForeignKey("dbo.IN_COMPRAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_COMPRA });
            
            CreateTable(
                "dbo.IN_PARAM",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false),
                        PORC_IVA = c.Int(nullable: false),
                        AMPLI_DESC = c.Int(nullable: false),
                        UNID_OPER = c.Boolean(nullable: false),
                        ORD_PROD = c.Boolean(nullable: false),
                        NAUT_PROD = c.Boolean(nullable: false),
                        CONT_BOD = c.Boolean(nullable: false),
                        CONT_TSP = c.Int(nullable: false),
                        NSEP_DEVO = c.Boolean(nullable: false),
                        NSEP_TRAN = c.Boolean(nullable: false),
                        NSEP_STOCK = c.Boolean(nullable: false),
                        COD_PROD = c.Int(nullable: false),
                        ORD_X_COD = c.Boolean(nullable: false),
                        REC_CON_IVA = c.Boolean(nullable: false),
                        UTIL_DEC_CANTI = c.Int(nullable: false),
                        UTIL_DEC_CUNI = c.Int(nullable: false),
                        UTIL_DEC_CTot = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_FILA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_PARAM", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_COMPRAS", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" }, "dbo.IN_COMPRAS");
            DropIndex("dbo.IN_PARAM", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA" });
            DropIndex("dbo.IN_COMPRAS", new[] { "COD_PROV", "NUM_DOC" });
            DropIndex("dbo.IN_COMPRAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.IN_PARAM");
            DropTable("dbo.IN_DETCOMPRA");
            DropTable("dbo.IN_COMPRAS");
        }
    }
}
