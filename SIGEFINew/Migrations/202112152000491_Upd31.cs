namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd31 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            CreateTable(
                "dbo.IN_DETALLETRANSAC",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        NUM_TRANSACCION = c.Int(nullable: false),
                        TIPO_TRAN = c.String(nullable: false, maxLength: 1),
                        TIPO_INGRESO = c.String(nullable: false, maxLength: 2),
                        NUM_FILA = c.Int(nullable: false),
                        COD_ITEM = c.String(maxLength: 50),
                        CANTIDAD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COSTO_UNIT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUBTOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PORC_DESC = c.Int(nullable: false),
                        VALOR_DESC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PORC_IVA = c.Int(nullable: false),
                        TOTAL_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOTAL_GENERAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_CADUCA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.NUM_TRANSACCION, t.TIPO_TRAN, t.TIPO_INGRESO, t.NUM_FILA })
                .ForeignKey("dbo.IN_ITEMS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM })
                .ForeignKey("dbo.IN_TRANSACCIONES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.NUM_TRANSACCION, t.TIPO_TRAN, t.TIPO_INGRESO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.COD_ITEM })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.NUM_TRANSACCION, t.TIPO_TRAN, t.TIPO_INGRESO });
            
            CreateTable(
                "dbo.IN_TRANSACCIONES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_BODEGA = c.Int(nullable: false),
                        NUM_TRANSACCION = c.Int(nullable: false),
                        TIPO_TRAN = c.String(nullable: false, maxLength: 1),
                        TIPO_INGRESO = c.String(nullable: false, maxLength: 2),
                        NUM_COMPRA = c.Int(nullable: false),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_MOV = c.String(maxLength: 10),
                        TIPO_PAGO = c.String(maxLength: 2),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        ID_UNIDOPERATIVA = c.Int(),
                        AUTORIZADO_POR = c.String(nullable: false, maxLength: 20),
                        USO = c.Int(),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        ID_OBJETO = c.Int(),
                        TIPO_SRI = c.String(nullable: false, maxLength: 2),
                        FECHA_DOC = c.DateTime(nullable: false),
                        NUM_DOC = c.String(nullable: false, maxLength: 15),
                        FECHACAD_DOC = c.DateTime(),
                        AUTORIZACION = c.String(maxLength: 49),
                        NUM_CXP = c.Int(nullable: false),
                        ANULADO = c.Boolean(nullable: false),
                        CERRADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_BODEGA, t.NUM_TRANSACCION, t.TIPO_TRAN, t.TIPO_INGRESO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PROV);
            
            CreateIndex("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            AddForeignKey("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_TRANSACCIONES", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.IN_TRANSACCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_DETALLETRANSAC", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_TRANSACCION", "TIPO_TRAN", "TIPO_INGRESO" }, "dbo.IN_TRANSACCIONES");
            DropForeignKey("dbo.IN_DETALLETRANSAC", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" }, "dbo.IN_ITEMS");
            DropForeignKey("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" }, "dbo.OrganicoFs");
            DropIndex("dbo.IN_TRANSACCIONES", new[] { "COD_PROV" });
            DropIndex("dbo.IN_TRANSACCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_DETALLETRANSAC", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "NUM_TRANSACCION", "TIPO_TRAN", "TIPO_INGRESO" });
            DropIndex("dbo.IN_DETALLETRANSAC", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_BODEGA", "COD_ITEM" });
            DropIndex("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ORG_CODIGO" });
            DropTable("dbo.IN_TRANSACCIONES");
            DropTable("dbo.IN_DETALLETRANSAC");
            CreateIndex("dbo.IN_UNIOPE", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
        }
    }
}
