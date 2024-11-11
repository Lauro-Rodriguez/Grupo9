namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PO_CONTRATOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CONTRATO = c.String(nullable: false, maxLength: 30),
                        COD_PLANOBJTVO = c.String(maxLength: 15),
                        NUM_CONTRATO = c.String(nullable: false, maxLength: 30),
                        CARGA_INI = c.Boolean(nullable: false),
                        TIPO_CONTRATO = c.String(nullable: false, maxLength: 2),
                        COD_CONTRATOP = c.String(maxLength: 30),
                        DESC_CONTRATO = c.String(nullable: false, maxLength: 500),
                        COD_CONTRATISTA = c.String(nullable: false, maxLength: 20),
                        MONTO_REF = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MONTO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PORC_ANTICIPO = c.Int(nullable: false),
                        FECHA_SUCRIP = c.DateTime(nullable: false),
                        FECHA_INICIA = c.DateTime(nullable: false),
                        PLAZO_DIAS = c.Int(nullable: false),
                        FECHA_FINALIZA = c.DateTime(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        VALOR_ANTICIPO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOMBRE_FISCALIZADOR = c.String(maxLength: 50),
                        NOMBRE_ADMINISTRADOR = c.String(maxLength: 50),
                        FECHA_RECPROV = c.DateTime(),
                        NUM_DOCRECPROV = c.String(maxLength: 50),
                        FECHA_RECDEFI = c.DateTime(),
                        NUM_DOCRECDEFI = c.String(maxLength: 50),
                        PLAZO_DIASAMP = c.Int(nullable: false),
                        FECHA_FINALIZA2 = c.DateTime(),
                        VALOR_ENTREGADO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PDF_RECPRV = c.String(maxLength: 100),
                        PDF_RECDEF = c.String(maxLength: 100),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CONTRATO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PO_CONTRATOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.PO_CONTRATOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.PO_CONTRATOS");
        }
    }
}
