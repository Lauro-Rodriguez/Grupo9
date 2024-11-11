namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd27 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PR_ACTIVIDADES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        ACTI_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .ForeignKey("dbo.PR_PROYECTOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO });
            
            CreateTable(
                "dbo.PR_PROYECTOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROY_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO })
                .ForeignKey("dbo.PR_SUBPROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO });
            
            CreateTable(
                "dbo.PR_SUBPROGRAMAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        SUBP_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 230),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO })
                .ForeignKey("dbo.Programas", t => new { t.ID_INSTITUCION, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_PROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PROG_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO });
            
            CreateTable(
                "dbo.PR_GRUPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        GRUP_NOMBRE = c.String(nullable: false, maxLength: 100),
                        TIPO = c.String(maxLength: 1),
                        NIVEL = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_ITEMS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_CODIGO = c.String(nullable: false, maxLength: 10),
                        ITEM_SUBITEM = c.String(nullable: false, maxLength: 3),
                        ITEM_NOMBRE = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ITEM_CLAVE })
                .ForeignKey("dbo.PR_GRUPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO });
            
            CreateTable(
                "dbo.PR_PARTEGRESOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PAEG_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAEG_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAEG_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ITEM_ORGAN = c.String(nullable: false, maxLength: 3),
                        ITEM_CORRE = c.String(nullable: false, maxLength: 3),
                        ITEM_FUNC = c.Int(nullable: false),
                        ITEM_ORIGTO = c.Int(nullable: false),
                        ITEM_UNIDOPTIVA = c.Int(nullable: false),
                        CTO_COSTO = c.Int(nullable: false),
                        SCTO_COSTO = c.Int(nullable: false),
                        ID_PROVINCIA = c.Int(nullable: false),
                        ID_CANTON = c.Int(nullable: false),
                        ID_PARROQUIA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_ACTIVIDADES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_GRUPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_PROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_PROYECTOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_SUBPROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO });
            
            CreateTable(
                "dbo.PR_DETCERTDISP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTIF = c.Int(nullable: false),
                        PAEG_CODIGO = c.Int(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TIPO_CERTIF = c.String(nullable: false, maxLength: 2),
                        ID_FONDO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF, t.PAEG_CODIGO })
                .ForeignKey("dbo.PR_CERTDISPRESUP", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF }, cascadeDelete: true)
                .ForeignKey("dbo.PR_FONDOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_PARTEGRESOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_FONDO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAEG_CODIGO });
            
            CreateTable(
                "dbo.PR_CERTDISPRESUP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTIF = c.Int(nullable: false),
                        TIPO_CERTIF = c.String(nullable: false, maxLength: 2),
                        FECHA_CERTIF = c.DateTime(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        NUM_PEDIDO = c.Int(nullable: false),
                        COD_PROV = c.String(nullable: false, maxLength: 20),
                        DETALLE_CERTIF = c.String(nullable: false, maxLength: 500),
                        ANULADO = c.Boolean(nullable: false),
                        CERRADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 15),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 15),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.COD_PROV, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => t.COD_PROV);
            
            CreateTable(
                "dbo.PR_DOCUMCERTDISP",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        NUM_TRANSAC = c.Int(nullable: false),
                        NUM_CERTIF = c.Int(nullable: false),
                        COD_DOCUM = c.String(nullable: false, maxLength: 15),
                        NUM_DOCUM = c.String(nullable: false, maxLength: 20),
                        OBSERVA_DOCUM = c.String(maxLength: 50),
                        FECHA = c.DateTime(nullable: false),
                        VALOR = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF, t.COD_DOCUM, t.NUM_DOCUM })
                .ForeignKey("dbo.PR_CERTDISPRESUP", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.NUM_TRANSAC, t.NUM_CERTIF });
            
            CreateTable(
                "dbo.PR_PROGRAMAS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PROG_CODIGO = c.Int(nullable: false),
                        PROG_NOMBRE = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.PR_PARTINGRESOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        PAIN_CODIGO = c.Int(nullable: false),
                        PROY_CODIGO = c.String(nullable: false, maxLength: 15),
                        PROG_CODIGO = c.Int(nullable: false),
                        ACTI_CODIGO = c.String(nullable: false, maxLength: 15),
                        SUBP_CODIGO = c.Int(nullable: false),
                        GRUP_CODIGO = c.String(nullable: false, maxLength: 10),
                        PAIN_CLAVE = c.String(nullable: false, maxLength: 50),
                        PAIN_NOMBRE = c.String(nullable: false, maxLength: 150),
                        ITEM_CLAVE = c.String(nullable: false, maxLength: 15),
                        ITEM_FINANC = c.String(nullable: false, maxLength: 3),
                        ORGANISMO = c.Int(nullable: false),
                        CORRELAT = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PAIN_CODIGO })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_ACTIVIDADES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_GRUPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_PROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_PROYECTOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO }, cascadeDelete: true)
                .ForeignKey("dbo.PR_SUBPROGRAMAS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.SUBP_CODIGO, t.PROY_CODIGO, t.ACTI_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.GRUP_CODIGO })
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.PROG_CODIGO });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO" }, "dbo.PR_SUBPROGRAMAS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.PR_PROYECTOS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" }, "dbo.PR_PROGRAMAS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" }, "dbo.PR_GRUPOS");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.PR_ACTIVIDADES");
            DropForeignKey("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO" }, "dbo.PR_SUBPROGRAMAS");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.PR_PROYECTOS");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" }, "dbo.PR_PROGRAMAS");
            DropForeignKey("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" }, "dbo.PR_PROGRAMAS");
            DropForeignKey("dbo.PR_PROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" }, "dbo.PR_GRUPOS");
            DropForeignKey("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAEG_CODIGO" }, "dbo.PR_PARTEGRESOS");
            DropForeignKey("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" }, "dbo.PR_FONDOS");
            DropForeignKey("dbo.PR_CERTDISPRESUP", "COD_PROV", "dbo.Proveedores");
            DropForeignKey("dbo.PR_DOCUMCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" }, "dbo.PR_CERTDISPRESUP");
            DropForeignKey("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" }, "dbo.PR_CERTDISPRESUP");
            DropForeignKey("dbo.PR_CERTDISPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" }, "dbo.PR_ACTIVIDADES");
            DropForeignKey("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" }, "dbo.PR_GRUPOS");
            DropForeignKey("dbo.PR_GRUPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.PR_PROYECTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO" }, "dbo.PR_SUBPROGRAMAS");
            DropForeignKey("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PROG_CODIGO" }, "dbo.Programas");
            DropForeignKey("dbo.PR_ACTIVIDADES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" }, "dbo.PR_PROYECTOS");
            DropIndex("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" });
            DropIndex("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" });
            DropIndex("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PR_PARTINGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_PROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_DOCUMCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" });
            DropIndex("dbo.PR_CERTDISPRESUP", new[] { "COD_PROV" });
            DropIndex("dbo.PR_CERTDISPRESUP", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PAEG_CODIGO" });
            DropIndex("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_FONDO" });
            DropIndex("dbo.PR_DETCERTDISP", new[] { "ID_INSTITUCION", "PERI_CODIGO", "NUM_TRANSAC", "NUM_CERTIF" });
            DropIndex("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" });
            DropIndex("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" });
            DropIndex("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO", "ACTI_CODIGO" });
            DropIndex("dbo.PR_PARTEGRESOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "GRUP_CODIGO" });
            DropIndex("dbo.PR_GRUPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "PROG_CODIGO" });
            DropIndex("dbo.PR_SUBPROGRAMAS", new[] { "ID_INSTITUCION", "PROG_CODIGO" });
            DropIndex("dbo.PR_PROYECTOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO" });
            DropIndex("dbo.PR_ACTIVIDADES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "SUBP_CODIGO", "PROY_CODIGO" });
            DropTable("dbo.PR_PARTINGRESOS");
            DropTable("dbo.PR_PROGRAMAS");
            DropTable("dbo.PR_DOCUMCERTDISP");
            DropTable("dbo.PR_CERTDISPRESUP");
            DropTable("dbo.PR_DETCERTDISP");
            DropTable("dbo.PR_PARTEGRESOS");
            DropTable("dbo.PR_ITEMS");
            DropTable("dbo.PR_GRUPOS");
            DropTable("dbo.PR_SUBPROGRAMAS");
            DropTable("dbo.PR_PROYECTOS");
            DropTable("dbo.PR_ACTIVIDADES");
        }
    }
}
