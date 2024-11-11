namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd44 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IN_CARACGEN",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CAGEN = c.String(nullable: false, maxLength: 10),
                        DESCRIP_CAGEN = c.String(nullable: false, maxLength: 50),
                        CARACT = c.String(nullable: false, maxLength: 5),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_PRESENTA",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_CAGEN = c.String(nullable: false, maxLength: 10),
                        COD_PRESEN = c.Int(nullable: false),
                        DESCRIP_CAGEN = c.String(nullable: false, maxLength: 50),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN, t.COD_PRESEN })
                .ForeignKey("dbo.IN_CARACGEN", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_CAGEN });
            
            CreateTable(
                "dbo.IN_CLASES",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        DESRIPCION = c.String(nullable: false, maxLength: 100),
                        TIPO_USO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_TIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        NOM_TIPO = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO })
                .ForeignKey("dbo.IN_CLASES", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE });
            
            CreateTable(
                "dbo.IN_SUBTIPOS",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        ID_CLASE = c.Int(nullable: false),
                        ID_TIPO = c.Int(nullable: false),
                        ID_SUBTIPO = c.Int(nullable: false),
                        NOM_SUBTIPO = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO, t.ID_SUBTIPO })
                .ForeignKey("dbo.IN_TIPOS", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.ID_CLASE, t.ID_TIPO });
            
            CreateTable(
                "dbo.IN_LINEANEGOCIO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_LINEA = c.Int(nullable: false),
                        DESC_LINEA = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA })
                .ForeignKey("dbo.Periodos", t => new { t.ID_INSTITUCION, t.PERI_CODIGO }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO });
            
            CreateTable(
                "dbo.IN_SUBLINEANEGOCIO",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        PERI_CODIGO = c.Int(nullable: false),
                        COD_LINEA = c.Int(nullable: false),
                        COD_SUBLINEA = c.Int(nullable: false),
                        DESC_SUBLINEA = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA, t.COD_SUBLINEA })
                .ForeignKey("dbo.IN_LINEANEGOCIO", t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.PERI_CODIGO, t.COD_LINEA });
            
            AddColumn("dbo.IN_ITEMS", "ID_CLASE", c => c.Int(nullable: false));
            AddColumn("dbo.IN_ITEMS", "ID_TIPO", c => c.Int(nullable: false));
            AddColumn("dbo.IN_ITEMS", "ID_SUBTIPO", c => c.Int(nullable: false));
            AddColumn("dbo.IN_ITEMS", "COD_CAGEN", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.IN_ITEMS", "COD_PRESEN", c => c.Int(nullable: false));
            AddColumn("dbo.IN_ITEMS", "CAT_CODIGO", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.IN_ITEMS", "SECCION", c => c.String(maxLength: 50));
            AddColumn("dbo.IN_ITEMS", "PERCHA", c => c.String(maxLength: 50));
            AddColumn("dbo.IN_ITEMS", "FILA", c => c.String(maxLength: 50));
            AddColumn("dbo.IN_ITEMS", "COLUMNA", c => c.String(maxLength: 50));
            AddColumn("dbo.IN_ITEMS", "COD_CONSIG", c => c.String(maxLength: 20));
            AddColumn("dbo.IN_ITEMS", "CODIGO_CGDB", c => c.String(maxLength: 50));
            AddColumn("dbo.IN_ITEMS", "CODIGO_CGCR", c => c.String(maxLength: 50));
            AddColumn("dbo.IN_ITEMS", "COD_LINEA", c => c.Int(nullable: false));
            AddColumn("dbo.IN_ITEMS", "COD_SUBLINEA", c => c.Int(nullable: false));
            CreateIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" });
            CreateIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" });
            CreateIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" });
            CreateIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" });
            CreateIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" });
            AddForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" }, "dbo.IN_CARACGEN", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" }, cascadeDelete: true);
            AddForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" }, "dbo.IN_PRESENTA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" }, cascadeDelete: true);
            AddForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" }, "dbo.IN_CLASES", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" }, cascadeDelete: true);
            AddForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" }, "dbo.IN_TIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" }, cascadeDelete: true);
            AddForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" }, "dbo.IN_SUBTIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IN_LINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_SUBLINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_LINEA" }, "dbo.IN_LINEANEGOCIO");
            DropForeignKey("dbo.IN_CLASES", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_SUBTIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" }, "dbo.IN_TIPOS");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" }, "dbo.IN_SUBTIPOS");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" }, "dbo.IN_TIPOS");
            DropForeignKey("dbo.IN_TIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" }, "dbo.IN_CLASES");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" }, "dbo.IN_CLASES");
            DropForeignKey("dbo.IN_CARACGEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" }, "dbo.Periodos");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" }, "dbo.IN_PRESENTA");
            DropForeignKey("dbo.IN_PRESENTA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" }, "dbo.IN_CARACGEN");
            DropForeignKey("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" }, "dbo.IN_CARACGEN");
            DropIndex("dbo.IN_SUBLINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_LINEA" });
            DropIndex("dbo.IN_LINEANEGOCIO", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_SUBTIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" });
            DropIndex("dbo.IN_TIPOS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" });
            DropIndex("dbo.IN_CLASES", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_PRESENTA", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" });
            DropIndex("dbo.IN_CARACGEN", new[] { "ID_INSTITUCION", "PERI_CODIGO" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO", "ID_SUBTIPO" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE", "ID_TIPO" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "ID_CLASE" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN", "COD_PRESEN" });
            DropIndex("dbo.IN_ITEMS", new[] { "ID_INSTITUCION", "PERI_CODIGO", "COD_CAGEN" });
            DropColumn("dbo.IN_ITEMS", "COD_SUBLINEA");
            DropColumn("dbo.IN_ITEMS", "COD_LINEA");
            DropColumn("dbo.IN_ITEMS", "CODIGO_CGCR");
            DropColumn("dbo.IN_ITEMS", "CODIGO_CGDB");
            DropColumn("dbo.IN_ITEMS", "COD_CONSIG");
            DropColumn("dbo.IN_ITEMS", "COLUMNA");
            DropColumn("dbo.IN_ITEMS", "FILA");
            DropColumn("dbo.IN_ITEMS", "PERCHA");
            DropColumn("dbo.IN_ITEMS", "SECCION");
            DropColumn("dbo.IN_ITEMS", "CAT_CODIGO");
            DropColumn("dbo.IN_ITEMS", "COD_PRESEN");
            DropColumn("dbo.IN_ITEMS", "COD_CAGEN");
            DropColumn("dbo.IN_ITEMS", "ID_SUBTIPO");
            DropColumn("dbo.IN_ITEMS", "ID_TIPO");
            DropColumn("dbo.IN_ITEMS", "ID_CLASE");
            DropTable("dbo.IN_SUBLINEANEGOCIO");
            DropTable("dbo.IN_LINEANEGOCIO");
            DropTable("dbo.IN_SUBTIPOS");
            DropTable("dbo.IN_TIPOS");
            DropTable("dbo.IN_CLASES");
            DropTable("dbo.IN_PRESENTA");
            DropTable("dbo.IN_CARACGEN");
        }
    }
}
