namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd33 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_ASIENTOSCONT",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ASIENTO_NUM = c.Int(nullable: false),
                        ASIENTO_FECHA = c.DateTime(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        TIPO_COMPROB = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROB = c.Int(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        CEDU_RUC = c.String(nullable: false, maxLength: 13),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        ASIENTO_BENEF = c.String(nullable: false, maxLength: 150),
                        ASIENTO_TIPO_DOC_BANCO = c.String(maxLength: 2),
                        ASIENTO_NUM_DOC_BANCO = c.Int(nullable: false),
                        ASIENTO_DETALLE = c.String(nullable: false, maxLength: 1000),
                        CODIGO_BANCO = c.Int(nullable: false),
                        ESTADO = c.Int(nullable: false),
                        CON_BANCO = c.Int(nullable: false),
                        INCLUIR_BANCOS = c.Int(nullable: false),
                        CON_SPI = c.Int(nullable: false),
                        FECHA_ANUL = c.DateTime(nullable: false),
                        ANULADO = c.Int(nullable: false),
                        CERRADO = c.Int(nullable: false),
                        NUMEXTERNO = c.String(maxLength: 25),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.TIPO_COMPROB, t.NUM_COMPROB }, unique: true);
            
            CreateTable(
                "dbo.CO_DETALLEASIENTOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ASIENTO_NUM = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        DEBE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HABER = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ANULADO = c.Int(nullable: false),
                        CERRADO = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM, t.NUM_FILA })
                .ForeignKey("dbo.CO_ASIENTOSCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM }, cascadeDelete: true)
                .ForeignKey("dbo.CO_CUENTASCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CG });
            
            CreateTable(
                "dbo.CO_DETALLEBANCOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ASIENTO_NUM = c.Int(nullable: false),
                        NUM_FILA = c.Int(nullable: false, identity: true),
                        NUM_TRANSAC = c.Int(nullable: false),
                        TIPO_DOCUMENTO_TESORERIA = c.String(nullable: false, maxLength: 2),
                        NUM_DOCUMENTO_TESORERIA = c.String(nullable: false, maxLength: 12),
                        BENEFICIARIO_PAGADOR = c.String(nullable: false, maxLength: 150),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COD_CUENTA = c.String(nullable: false, maxLength: 20),
                        COBRADO = c.Int(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 1),
                        TIPO_COMPROB = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROB = c.Int(nullable: false),
                        FECHA_PAGO = c.DateTime(nullable: false),
                        FECHA_COBRO = c.DateTime(nullable: false),
                        IMPRIMIO = c.Int(nullable: false),
                        CONCILIADO = c.Int(nullable: false),
                        ANULADO = c.Int(nullable: false),
                        FECHANUL = c.DateTime(nullable: false),
                        TIPO_TRAN = c.String(nullable: false, maxLength: 2),
                        MES_CONCILIA = c.Int(nullable: false),
                        CON_TRANSFER = c.Int(nullable: false),
                        NUM_TRANSFER = c.Int(nullable: false),
                        CEDU_RUC = c.String(nullable: false, maxLength: 13),
                        VAL_TRANSFER = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_CONCILIA = c.DateTime(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM, t.NUM_FILA })
                .ForeignKey("dbo.CO_ASIENTOSCONT", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ASIENTO_NUM });
            
            CreateTable(
                "dbo.CO_CLASIFRETEN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_RET = c.String(nullable: false, maxLength: 20),
                        DESCRIP_RET = c.String(nullable: false, maxLength: 100),
                        TIPO_RET = c.Int(nullable: false),
                        PORCENTAJE = c.Int(nullable: false),
                        CTA_DEBITO = c.String(maxLength: 50),
                        CTA_CREDITO = c.String(maxLength: 50),
                        COMP100 = c.String(maxLength: 20),
                        TIPO_GASTO = c.Int(nullable: false),
                        CONTRIB_ESP = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_RET })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_CLASIFRETEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_ASIENTOSCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" }, "dbo.CO_ASIENTOSCONT");
            DropForeignKey("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" }, "dbo.CO_CUENTASCONT");
            DropForeignKey("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" }, "dbo.CO_ASIENTOSCONT");
            DropIndex("dbo.CO_CLASIFRETEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" });
            DropIndex("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CG" });
            DropIndex("dbo.CO_DETALLEASIENTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ASIENTO_NUM" });
            DropIndex("dbo.CO_ASIENTOSCONT", new[] { "ID_INSTITUCION", "PERI_CODIGO", "TIPO_COMPROB", "NUM_COMPROB" });
            DropTable("dbo.CO_CLASIFRETEN");
            DropTable("dbo.CO_DETALLEBANCOS");
            DropTable("dbo.CO_DETALLEASIENTOS");
            DropTable("dbo.CO_ASIENTOSCONT");
        }
    }
}
