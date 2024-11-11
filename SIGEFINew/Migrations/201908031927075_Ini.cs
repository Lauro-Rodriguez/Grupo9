namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POA_OE_PLANIF",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_OE_PLANIF = c.String(nullable: false, maxLength: 10),
                        UNI_MEDIDA = c.Int(nullable: false),
                        COD_PNVB = c.Int(nullable: false),
                        COD_OEI = c.Int(nullable: false),
                        COD_OE_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_EPG = c.Int(nullable: false),
                        COD_POL_PDOT = c.String(nullable: false, maxLength: 4),
                        COD_META_PDOT = c.String(nullable: false, maxLength: 4),
                        DESCRIPCION = c.String(nullable: false, maxLength: 250),
                        IND_MEDIO = c.String(maxLength: 250),
                        MIN_BASE = c.String(maxLength: 250),
                        MET_GTON_OBJTVO = c.String(maxLength: 250),
                        ACTIVO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_OE_PLANIF })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.POA_EPG", t => t.COD_EPG, cascadeDelete: true)
                .ForeignKey("dbo.POA_META_PDOT", t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT }, cascadeDelete: true)
                .ForeignKey("dbo.POA_OE_PDOT", t => t.COD_OE_PDOT, cascadeDelete: true)
                .ForeignKey("dbo.POA_OEI", t => t.COD_OEI, cascadeDelete: true)
                .ForeignKey("dbo.POA_PNBV", t => t.COD_PNVB, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PNVB)
                .Index(t => t.COD_OEI)
                .Index(t => new { t.COD_OE_PDOT, t.COD_POL_PDOT, t.COD_META_PDOT })
                .Index(t => t.COD_EPG);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_PNVB", "dbo.POA_PNBV");
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_OEI", "dbo.POA_OEI");
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_OE_PDOT", "dbo.POA_OE_PDOT");
            DropForeignKey("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" }, "dbo.POA_META_PDOT");
            DropForeignKey("dbo.POA_OE_PLANIF", "COD_EPG", "dbo.POA_EPG");
            DropForeignKey("dbo.POA_OE_PLANIF", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_EPG" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_OE_PDOT", "COD_POL_PDOT", "COD_META_PDOT" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_OEI" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "COD_PNVB" });
            DropIndex("dbo.POA_OE_PLANIF", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropTable("dbo.POA_OE_PLANIF");
        }
    }
}
