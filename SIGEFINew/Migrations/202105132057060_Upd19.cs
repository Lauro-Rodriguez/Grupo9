namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd19 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IN_DETCOMPRA");
            CreateTable(
                "dbo.IN_PRODUCTOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_ITEM = c.String(nullable: false, maxLength: 50),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        COD_CAGEN = c.String(nullable: false, maxLength: 10),
                        COD_PRESEN = c.Int(nullable: false),
                        CAT_CODIGO = c.String(nullable: false, maxLength: 10),
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
                        SECCION = c.String(maxLength: 50),
                        PERCHA = c.String(maxLength: 50),
                        FILA = c.String(maxLength: 50),
                        COLUMNA = c.String(maxLength: 50),
                        STOCK_MIN = c.Int(nullable: false),
                        STOCK_MAX = c.Int(nullable: false),
                        PUNTO_REORDEN = c.Int(nullable: false),
                        PRECIO1 = c.Int(nullable: false),
                        PRECIO2 = c.Int(nullable: false),
                        FECHA_ULT_COMPRA = c.DateTime(nullable: false),
                        COD_CONSIG = c.String(maxLength: 20),
                        CODIGO_CGDB = c.String(maxLength: 50),
                        CODIGO_CGCR = c.String(maxLength: 50),
                        COD_LINEA = c.Int(nullable: false),
                        COD_SUBLINEA = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_ITEM })
                .ForeignKey("dbo.IN_CARACGEN", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN }, cascadeDelete: true)
                .ForeignKey("dbo.IN_CLASES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE }, cascadeDelete: true)
                .ForeignKey("dbo.IN_PRESENTA", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN, t.COD_PRESEN }, cascadeDelete: true)
                .ForeignKey("dbo.IN_SUBTIPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO, t.ID_SUBTIPO }, cascadeDelete: true)
                .ForeignKey("dbo.IN_TIPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN, t.COD_PRESEN })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO, t.ID_SUBTIPO });
            
            AddColumn("dbo.IN_DETCOMPRA", "COD_ITEM", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM", "NUM_COMPRA", "NUM_FILA" });
            CreateIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" });
            AddForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" }, "dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" }, "dbo.IN_TIPOS");
            DropForeignKey("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" }, "dbo.IN_SUBTIPOS");
            DropForeignKey("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" }, "dbo.IN_PRESENTA");
            DropForeignKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" }, "dbo.IN_PRODUCTOS");
            DropForeignKey("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" }, "dbo.IN_CLASES");
            DropForeignKey("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" }, "dbo.IN_CARACGEN");
            DropIndex("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_ITEM" });
            DropIndex("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" });
            DropIndex("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" });
            DropIndex("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" });
            DropIndex("dbo.IN_PRODUCTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" });
            DropPrimaryKey("dbo.IN_DETCOMPRA");
            DropColumn("dbo.IN_DETCOMPRA", "COD_ITEM");
            DropTable("dbo.IN_PRODUCTOS");
            AddPrimaryKey("dbo.IN_DETCOMPRA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_COMPRA", "NUM_FILA" });
        }
    }
}
