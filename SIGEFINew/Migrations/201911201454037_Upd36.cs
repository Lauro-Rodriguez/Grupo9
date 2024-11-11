namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd36 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_LINEACREDITO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CUENTA = c.String(nullable: false, maxLength: 20),
                        NOMBRE_CUENTA = c.String(nullable: false, maxLength: 80),
                        CODIGO_CG = c.String(maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CUENTA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.CO_CUENBANCOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CUENTA = c.Int(nullable: false),
                        CODIGO_BANCO = c.Int(nullable: false),
                        CUENTA_CORRIENTE = c.String(nullable: false, maxLength: 15),
                        CODIGO_CG = c.String(nullable: false, maxLength: 50),
                        DESCRIPCION = c.String(nullable: false, maxLength: 150),
                        CONCILIA = c.Boolean(nullable: false),
                        CTA_DEBITO = c.String(maxLength: 50),
                        CTA_CREDITO = c.String(maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.BANCOS", t => t.CODIGO_BANCO, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.CODIGO_BANCO);
            
            CreateTable(
                "dbo.CO_CONCILIACIONES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        CODIGO_CUENTA = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        MES_CONCILIA = c.Int(nullable: false),
                        SALDO_ESTADO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CHEQ_GIR_NC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NCDP_NI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SALDO_FIN_BANC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOT_DATCUENTA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TOT_DATSISTEMA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CERRADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA, t.NUM_TRANSAC, t.MES_CONCILIA })
                .ForeignKey("dbo.CO_CUENBANCOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.CODIGO_CUENTA });
            
            CreateIndex("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CUENTA" });
            AddForeignKey("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CUENTA" }, "dbo.CO_LINEACREDITO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CUENTA" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_CUENBANCOS", "CODIGO_BANCO", "dbo.BANCOS");
            DropForeignKey("dbo.CO_CUENBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_CONCILIACIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CUENTA" }, "dbo.CO_CUENBANCOS");
            DropForeignKey("dbo.CO_LINEACREDITO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CUENTA" }, "dbo.CO_LINEACREDITO");
            DropIndex("dbo.CO_CONCILIACIONES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "CODIGO_CUENTA" });
            DropIndex("dbo.CO_CUENBANCOS", new[] { "CODIGO_BANCO" });
            DropIndex("dbo.CO_CUENBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_LINEACREDITO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.CO_DETALLEBANCOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CUENTA" });
            DropTable("dbo.CO_CONCILIACIONES");
            DropTable("dbo.CO_CUENBANCOS");
            DropTable("dbo.CO_LINEACREDITO");
        }
    }
}
