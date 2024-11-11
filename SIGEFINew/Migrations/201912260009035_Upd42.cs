namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd42 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IN_BODEGAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        NOM_BODEGA = c.String(nullable: false, maxLength: 50),
                        TIPO_BODEGA = c.Int(nullable: false),
                        RESP_BODEGA = c.String(maxLength: 50),
                        DIR_BODEGA = c.String(maxLength: 250),
                        TELF_BODEGA = c.String(maxLength: 10),
                        NUM_ING_BODEGA = c.Single(nullable: false),
                        NUM_EGR_BODEGA = c.Single(nullable: false),
                        NUM_ORD_COMPRA = c.Single(nullable: false),
                        NUM_NTA_ENT = c.Single(nullable: false),
                        NUMAUT_INGBOD = c.Boolean(nullable: false),
                        NUMAUT_EGRBOD = c.Boolean(nullable: false),
                        NUMAUT_ORDCOMP = c.Boolean(nullable: false),
                        NUMAUT_NOTENT = c.Boolean(nullable: false),
                        INCRE1_INGBOD = c.Boolean(nullable: false),
                        INCRE1_EGRBOD = c.Boolean(nullable: false),
                        INCRE1_ORDCOMP = c.Boolean(nullable: false),
                        INCRE1_NOTENT = c.Boolean(nullable: false),
                        COD_INI_PROD = c.Single(nullable: false),
                        COD_AUTOMAT = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_CUENTASCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        CC_INVENT = c.String(maxLength: 50),
                        CC_CXP_CONSUMO = c.String(maxLength: 50),
                        CC_GASTO_CONSUMO = c.String(maxLength: 50),
                        CC_DONACION_CONSUMO = c.String(maxLength: 50),
                        CC_ACUMOBRAS_I = c.String(maxLength: 50),
                        CC_ACUMPROGRAM_I = c.String(maxLength: 50),
                        CC_CXP_I = c.String(maxLength: 50),
                        CC_GASTO_I = c.String(maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.ID_CLASE, t.ID_TIPO })
                .ForeignKey("dbo.IN_BODEGAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA }, cascadeDelete: true)
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA });
            
            CreateTable(
                "dbo.IN_ITEMS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        COD_ITEM = c.String(nullable: false, maxLength: 50),
                        NOM_ITEM = c.String(nullable: false, maxLength: 90),
                        MARCA_ITEM = c.String(nullable: false, maxLength: 50),
                        APLIC_ITEM = c.String(maxLength: 100),
                        TIPO_COSTEO = c.Int(nullable: false),
                        COSTO_PROD = c.String(nullable: false, maxLength: 1),
                        APLICA_IVA = c.Boolean(nullable: false),
                        PROD_PERE = c.Boolean(nullable: false),
                        PROD_DESC = c.Boolean(nullable: false),
                        PROD_NUM_SERIE = c.Boolean(nullable: false),
                        PROD_NUM_GAR = c.Boolean(nullable: false),
                        STOCK_MIN = c.Int(nullable: false),
                        STOCK_MAX = c.Int(nullable: false),
                        PUNTO_REORDEN = c.Int(nullable: false),
                        PRECIO1 = c.Int(nullable: false),
                        PRECIO2 = c.Int(nullable: false),
                        FECHA_ULT_COMPRA = c.DateTime(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM })
                .ForeignKey("dbo.IN_BODEGAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA });
            
            CreateTable(
                "dbo.IN_CARGA_INI",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        COD_ITEM = c.String(nullable: false, maxLength: 50),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        CANTIDAD = c.Single(nullable: false),
                        COSTO = c.Single(nullable: false),
                        VALOR_TOTAL = c.Single(nullable: false),
                        ACTIVADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM, t.NUM_FILA })
                .ForeignKey("dbo.IN_ITEMS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_BODEGAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_CARGA_INI", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" }, "dbo.IN_ITEMS");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" }, "dbo.IN_BODEGAS");
            DropForeignKey("dbo.IN_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_CUENTASCONT", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.IN_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" }, "dbo.IN_BODEGAS");
            DropIndex("dbo.IN_CARGA_INI", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" });
            DropIndex("dbo.IN_CUENTASCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA" });
            DropIndex("dbo.IN_BODEGAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.IN_CARGA_INI");
            DropTable("dbo.IN_ITEMS");
            DropTable("dbo.IN_CUENTASCONT");
            DropTable("dbo.IN_BODEGAS");
        }
    }
}
