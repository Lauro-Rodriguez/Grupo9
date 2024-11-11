namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd46 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RP_DETALLEROL",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        MES_PROC = c.Int(nullable: false),
                        ANIO_PROC = c.Int(nullable: false),
                        ID_NOMINA = c.Int(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TIPO = c.Int(nullable: false),
                        FORMULA = c.Boolean(nullable: false),
                        ORDEN = c.Int(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false),
                        NUM_IDENTIF = c.Int(nullable: false),
                        COMPIMPRENTA = c.Boolean(nullable: false),
                        CERRADO = c.Boolean(nullable: false),
                        INC_ENLIQUIDA = c.Boolean(nullable: false),
                        ACUM_VALPROV = c.Boolean(nullable: false),
                        MES_PROVINI = c.Int(nullable: false),
                        MES_PROVFIN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO, t.COD_RUBRO, t.MES_PROC, t.ANIO_PROC, t.ID_NOMINA })
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: false)
                .ForeignKey("dbo.RP_RUBROS", t => new { t.ID_INSTITUCION, t.COD_RUBRO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO })
                .Index(t => new { t.ID_INSTITUCION, t.COD_RUBRO });
            
            CreateTable(
                "dbo.RP_RUBROS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        NOM_RUBRO = c.String(nullable: false, maxLength: 20),
                        DESCRIPCION = c.String(nullable: false, maxLength: 100),
                        TIPO = c.Int(nullable: false),
                        MOSTRAR = c.Boolean(nullable: false),
                        FORMULA = c.Boolean(nullable: false),
                        VALOR = c.String(nullable: false, maxLength: 1000),
                        VALOR_REAL = c.String(maxLength: 1000),
                        ORDEN = c.Int(nullable: false),
                        ENERO = c.Boolean(nullable: false),
                        FEBRERO = c.Boolean(nullable: false),
                        MARZO = c.Boolean(nullable: false),
                        ABRIL = c.Boolean(nullable: false),
                        MAYO = c.Boolean(nullable: false),
                        JUNIO = c.Boolean(nullable: false),
                        JULIO = c.Boolean(nullable: false),
                        AGOSTO = c.Boolean(nullable: false),
                        SEPTIEMBRE = c.Boolean(nullable: false),
                        OCTUBRE = c.Boolean(nullable: false),
                        NOVIEMBRE = c.Boolean(nullable: false),
                        DICIEMBRE = c.Boolean(nullable: false),
                        ID_TIPOROL = c.Int(nullable: false),
                        TIPO_MOV = c.String(nullable: false, maxLength: 1),
                        EXTRAROL = c.Boolean(nullable: false),
                        COMPIMPRENTA = c.Boolean(nullable: false),
                        RETCONHIST = c.Boolean(nullable: false),
                        NUM_IDENTIF = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        ELIMINADO = c.Boolean(nullable: false),
                        INC_ENLIQUIDA = c.Boolean(nullable: false),
                        ACUM_VALPROV = c.Boolean(nullable: false),
                        MES_PROVINI = c.Int(nullable: false),
                        MES_PROVFIN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.COD_RUBRO })
                .ForeignKey("dbo.RP_TIPOROL", t => new { t.ID_INSTITUCION, t.ID_TIPOROL }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOROL });
            
            CreateTable(
                "dbo.RP_TIPOSANTICIPO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        DESCRIPCION_ANTICIPO = c.String(nullable: false, maxLength: 60),
                        LIMITE_CUOTAS = c.Int(nullable: false),
                        PORC_DESCDIC = c.Int(nullable: false),
                        LIMITE_MONTO = c.Int(nullable: false),
                        TIPO_SUELDO = c.Int(nullable: false),
                        ID_TIPONOMINA = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOANTICIPO })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: false)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_ABONOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ANTICIPO = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        NUM_ABONO = c.Int(nullable: false),
                        FECHA_ABONNO = c.DateTime(nullable: false),
                        DETALLE_ABONO = c.String(nullable: false, maxLength: 255),
                        CAPITAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        INTERES = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MORA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ASIENTO = c.Int(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO, t.NUM_ABONO })
                .ForeignKey("dbo.RP_ANTICIPOS", t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO });
            
            CreateTable(
                "dbo.RP_ANTICIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ANTICIPO = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        FECHA_ANTICIPO = c.DateTime(nullable: false),
                        MONTO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DETALLE_ANTICIPO = c.String(nullable: false, maxLength: 400),
                        VALOR_ANTICIPO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NUM_CUOTAS = c.Int(nullable: false),
                        DIVIDENDO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_CUOTADIC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ABONO_ANTICIPO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SALDO_ANTICIPO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_AUTORIZADO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DESDE = c.Int(nullable: false),
                        HASTA = c.Int(nullable: false),
                        COD_RUBRO = c.String(nullable: false, maxLength: 10),
                        ESTADO = c.String(nullable: false, maxLength: 1),
                        NUMP_CONTAB = c.Int(nullable: false),
                        NUM_TRANSFER = c.Int(nullable: false),
                        TIPO = c.String(nullable: false, maxLength: 1),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO })
                .ForeignKey("dbo.RP_TIPOSANTICIPO", t => new { t.ID_INSTITUCION, t.ID_TIPOANTICIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOANTICIPO });
            
            CreateTable(
                "dbo.RP_CUOTASANTICIPO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        NUM_ANTICIPO = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        ID_TIPOANTICIPO = c.Int(nullable: false),
                        ANIOMESCUOTA = c.Int(nullable: false),
                        FECHA_VENCE = c.DateTime(nullable: false),
                        CAPITAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        INTERES = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VALOR_CUOTA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ABONO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PAGOROL = c.Boolean(nullable: false),
                        DESCONTADO = c.Boolean(nullable: false),
                        ANIOMESDESC = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO, t.ANIOMESCUOTA })
                .ForeignKey("dbo.RP_ANTICIPOS", t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO }, cascadeDelete: false)
                .Index(t => new { t.ID_INSTITUCION, t.NUM_ANTICIPO, t.ID_EMPLEADO, t.ID_TIPOANTICIPO });
            
            CreateTable(
                "dbo.RP_PORCAPORTACION",
                c => new
                    {
                        ID_PORCENT = c.Int(nullable: false, identity: true),
                        TIPO = c.String(nullable: false, maxLength: 1),
                        ID_TIPOSEGURO = c.Int(nullable: false),
                        APORTE_PERS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        APORTE_PATRO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGURO_CESANTIA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SECAP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IECE = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID_PORCENT)
                .ForeignKey("dbo.RP_TIPOSEGURO", t => t.ID_TIPOSEGURO, cascadeDelete: false)
                .Index(t => t.ID_TIPOSEGURO);
            
            CreateTable(
                "dbo.RP_RECEPFONDOS",
                c => new
                    {
                        ID_RECEPFONDO = c.Int(nullable: false, identity: true),
                        RUC_RECEPFONDO = c.String(nullable: false, maxLength: 13),
                        NOMBRE_RECEPFONDO = c.String(nullable: false, maxLength: 100),
                        ID_PORCENT = c.Int(nullable: false),
                        NUM_PATRONAL = c.String(nullable: false, maxLength: 20),
                        DOC_REPLEGAL = c.String(nullable: false, maxLength: 15),
                        NUMDOC_REPLEGAL = c.String(nullable: false, maxLength: 13),
                        NOM_REPLEGAL = c.String(nullable: false, maxLength: 50),
                        DOC_OFIPAGADOR = c.String(nullable: false, maxLength: 15),
                        NUMDOC_OFIPAGADOR = c.String(nullable: false, maxLength: 13),
                        NOM_OFIPAGADOR = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_RECEPFONDO)
                .ForeignKey("dbo.RP_PORCAPORTACION", t => t.ID_PORCENT, cascadeDelete: false)
                .Index(t => t.ID_PORCENT);
            
            CreateTable(
                "dbo.RP_TIPOSEGURO",
                c => new
                    {
                        ID_TIPOSEGURO = c.Int(nullable: false, identity: true),
                        NOMBRE_TIPOSEGURO = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID_TIPOSEGURO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RP_PORCAPORTACION", "ID_TIPOSEGURO", "dbo.RP_TIPOSEGURO");
            DropForeignKey("dbo.RP_RECEPFONDOS", "ID_PORCENT", "dbo.RP_PORCAPORTACION");
            DropForeignKey("dbo.RP_ANTICIPOS", new[] { "ID_INSTITUCION", "ID_TIPOANTICIPO" }, "dbo.RP_TIPOSANTICIPO");
            DropForeignKey("dbo.RP_CUOTASANTICIPO", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" }, "dbo.RP_ANTICIPOS");
            DropForeignKey("dbo.RP_ABONOS", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" }, "dbo.RP_ANTICIPOS");
            DropForeignKey("dbo.RP_TIPOSANTICIPO", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_RUBROS", new[] { "ID_INSTITUCION", "ID_TIPOROL" }, "dbo.RP_TIPOROL");
            DropForeignKey("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "COD_RUBRO" }, "dbo.RP_RUBROS");
            DropForeignKey("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropIndex("dbo.RP_RECEPFONDOS", new[] { "ID_PORCENT" });
            DropIndex("dbo.RP_PORCAPORTACION", new[] { "ID_TIPOSEGURO" });
            DropIndex("dbo.RP_CUOTASANTICIPO", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" });
            DropIndex("dbo.RP_ANTICIPOS", new[] { "ID_INSTITUCION", "ID_TIPOANTICIPO" });
            DropIndex("dbo.RP_ABONOS", new[] { "ID_INSTITUCION", "NUM_ANTICIPO", "ID_EMPLEADO", "ID_TIPOANTICIPO" });
            DropIndex("dbo.RP_TIPOSANTICIPO", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_RUBROS", new[] { "ID_INSTITUCION", "ID_TIPOROL" });
            DropIndex("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "COD_RUBRO" });
            DropIndex("dbo.RP_DETALLEROL", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropTable("dbo.RP_TIPOSEGURO");
            DropTable("dbo.RP_RECEPFONDOS");
            DropTable("dbo.RP_PORCAPORTACION");
            DropTable("dbo.RP_CUOTASANTICIPO");
            DropTable("dbo.RP_ANTICIPOS");
            DropTable("dbo.RP_ABONOS");
            DropTable("dbo.RP_TIPOSANTICIPO");
            DropTable("dbo.RP_RUBROS");
            DropTable("dbo.RP_DETALLEROL");
        }
    }
}
