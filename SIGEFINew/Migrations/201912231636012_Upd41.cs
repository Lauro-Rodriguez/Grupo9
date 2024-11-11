namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd41 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_CTASPPAGAR",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        FECHA_TRANSAC = c.DateTime(nullable: false),
                        TIPO_TRANSAC = c.String(nullable: false, maxLength: 2),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        DETALLE = c.String(nullable: false, maxLength: 1000),
                        APLICADO = c.Boolean(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 3),
                        ANTICIPO = c.Boolean(nullable: false),
                        TM_FDO_TER = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PROV);
            
            CreateTable(
                "dbo.CO_DETCTASPPAG",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUM_FILA = c.String(nullable: false, maxLength: 20),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        TIPOSRI = c.String(nullable: false, maxLength: 2),
                        FECHA = c.DateTime(nullable: false),
                        FECHA_DOC = c.DateTime(nullable: false),
                        NUM_DOC = c.String(nullable: false, maxLength: 20),
                        VALOR_DOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IVA_PORC = c.Int(nullable: false),
                        TOT_IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CTA_X_PAG = c.String(nullable: false, maxLength: 50),
                        CTA_DEBITO = c.String(nullable: false, maxLength: 50),
                        ASIENTO = c.Int(nullable: false),
                        RFIVA = c.Boolean(nullable: false),
                        PORC_APLICA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AD = c.Boolean(nullable: false),
                        IG = c.Boolean(nullable: false),
                        AUTORIZACION = c.String(maxLength: 49),
                        FECHACAD_DOC = c.DateTime(nullable: false),
                        REPOSICION = c.String(maxLength: 50),
                        ICE = c.Int(nullable: false),
                        COD_SUSTENTO = c.String(maxLength: 2),
                        TIPO_MOV = c.String(maxLength: 3),
                        APLICADO = c.Boolean(nullable: false),
                        ANULADO = c.Boolean(nullable: false),
                        NUMT_CONTAB = c.Int(nullable: false),
                        FORMA_PAGO = c.String(maxLength: 2),
                        ORIGEN = c.String(nullable: false, maxLength: 2),
                        ANTICIPO = c.Boolean(nullable: false),
                        TM_FDO_TER = c.Boolean(nullable: false),
                        NUM_FDO_TER = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA })
                .ForeignKey("dbo.CO_CTASPPAGAR", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC });
            
            CreateTable(
                "dbo.CO_RETENCIONES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        NUM_FILA = c.String(nullable: false, maxLength: 20),
                        CODIG_RET = c.String(nullable: false, maxLength: 20),
                        ASIENTO_NUM = c.Int(nullable: false),
                        RUC_CI = c.String(nullable: false, maxLength: 13),
                        NUM_DOCUM = c.String(nullable: false, maxLength: 20),
                        FECHA_EMISION = c.DateTime(nullable: false),
                        COD_TIPO_DOC = c.String(nullable: false, maxLength: 15),
                        CODICLASRET = c.String(nullable: false, maxLength: 10),
                        TIPO_PTOEMIRET = c.Int(nullable: false),
                        COD_PTOEMIRET = c.Int(nullable: false),
                        NUM_RETENCION = c.String(nullable: false, maxLength: 16),
                        PORC_RET = c.Int(nullable: false),
                        TIPO_RET = c.String(nullable: false, maxLength: 2),
                        VALOR_DOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BASE_IMP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_RET = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FECHA_EMI = c.DateTime(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        EMITIDO = c.Boolean(nullable: false),
                        PAGADO = c.Boolean(nullable: false),
                        ASIENTO_PAGO = c.Int(nullable: false),
                        FECHA_PAGO = c.DateTime(nullable: false),
                        CHECK_PAGO = c.Boolean(nullable: false),
                        FECHA_CADUCA = c.DateTime(nullable: false),
                        COD_SUSTENTO = c.String(nullable: false, maxLength: 2),
                        NUMT_TRANSAC = c.Int(nullable: false),
                        CODIGO_CUENTA = c.String(maxLength: 50),
                        CODIGO_CUENTACP = c.String(maxLength: 50),
                        ANULADA = c.Boolean(nullable: false),
                        GENERADO = c.Boolean(nullable: false),
                        FIRMADO = c.Boolean(nullable: false),
                        AUTORIZADO = c.Boolean(nullable: false),
                        EMAIL = c.Boolean(nullable: false),
                        CLAVE_ACCESO = c.String(maxLength: 49),
                        AUTORIZACION_SRI = c.String(maxLength: 49),
                        FECHA_AUTORIZA = c.DateTime(nullable: false),
                        AMBIENTE = c.String(maxLength: 1),
                        EMISION = c.String(maxLength: 1),
                        CUENTAXPAGAR = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA, t.CODIG_RET })
                .ForeignKey("dbo.CO_DETCTASPPAG", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.COD_PROV, t.NUM_FILA });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_CTASPPAGAR", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.CO_CTASPPAGAR", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" }, "dbo.CO_DETCTASPPAG");
            DropForeignKey("dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" }, "dbo.CO_CTASPPAGAR");
            DropIndex("dbo.CO_RETENCIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "COD_PROV", "NUM_FILA" });
            DropIndex("dbo.CO_DETCTASPPAG", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC" });
            DropIndex("dbo.CO_CTASPPAGAR", new[] { "COD_PROV" });
            DropIndex("dbo.CO_CTASPPAGAR", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.CO_RETENCIONES");
            DropTable("dbo.CO_DETCTASPPAG");
            DropTable("dbo.CO_CTASPPAGAR");
        }
    }
}
