namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contab1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CO_PARAMETROS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ANIO_CODIGO = c.String(nullable: false, maxLength: 4),
                        MESINI = c.Int(nullable: false),
                        MESPROC = c.Int(nullable: false),
                        MASKCUENPATRI = c.String(nullable: false, maxLength: 50),
                        MASKCUENRESUL = c.String(nullable: false, maxLength: 50),
                        ACT = c.String(nullable: false, maxLength: 5),
                        PAS = c.String(nullable: false, maxLength: 5),
                        PAT = c.String(nullable: false, maxLength: 5),
                        ING1 = c.String(nullable: false, maxLength: 5),
                        ING2 = c.String(maxLength: 5),
                        ING3 = c.String(maxLength: 5),
                        GTO1 = c.String(nullable: false, maxLength: 5),
                        GTO2 = c.String(maxLength: 5),
                        GTO3 = c.String(maxLength: 5),
                        ORDENDEU = c.String(maxLength: 5),
                        ORDENACRE = c.String(maxLength: 5),
                        RESUMIG = c.String(maxLength: 5),
                        CTARESINGGASTO = c.String(maxLength: 50),
                        CTARESEJERVIGENTE = c.String(maxLength: 50),
                        CTARRESEJERANTERIOR = c.String(maxLength: 50),
                        COMPDIAR = c.Int(nullable: false),
                        COMPEGRE = c.Int(nullable: false),
                        COMPINGRE = c.Int(nullable: false),
                        NUMASIEN = c.Int(nullable: false),
                        NUMASIREFO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTDISP = c.Int(nullable: false),
                        NUM_COMPROMISO = c.Int(nullable: false),
                        NUM_TRANSFER = c.Int(nullable: false),
                        NUM_DEVEN = c.Int(nullable: false),
                        PORIVA = c.Int(nullable: false),
                        NUM_DECIMAL = c.Int(nullable: false),
                        NUM_PEDIDOCOMPRAL = c.Int(nullable: false),
                        NUM_ORDENPAGO = c.Int(nullable: false),
                        COD_ITEM = c.Int(nullable: false),
                        DEVENGAR_CONTAB = c.Boolean(nullable: false),
                        PAGAR_CONTAB = c.Boolean(nullable: false),
                        IVA_GASTO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ANIO_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.CO_PARAMETROS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.CO_PARAMETROS");
        }
    }
}
