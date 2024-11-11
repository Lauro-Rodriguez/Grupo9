namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd26 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RP_ACCIONPERSONAL",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_EMPLEADO = c.Int(nullable: false),
                        NUM_ACCION = c.Int(nullable: false),
                        FECHA_REGISTRO = c.DateTime(nullable: false),
                        TIPO_RESOLUCION = c.Int(nullable: false),
                        FECHA_RIGE = c.DateTime(nullable: false),
                        DETALLE = c.String(nullable: false, maxLength: 500),
                        ID_TIPOMOV = c.Int(nullable: false),
                        ORG_CODIGO = c.Int(nullable: false),
                        ID_CARGO = c.Int(nullable: false),
                        PARTIDA_PRESUP = c.String(maxLength: 50),
                        ID_EMPDIRECTOR = c.Int(nullable: false),
                        ID_EMPMAXAUTO = c.Int(nullable: false),
                        NUM_RESOLUCION = c.String(nullable: false, maxLength: 100),
                        AUTORIZADO = c.Boolean(nullable: false),
                        USER_CREA = c.String(nullable: false, maxLength: 50),
                        FECHA_CREA = c.DateTime(nullable: false),
                        USER_MODIF = c.String(maxLength: 50),
                        FECHA_MODIF = c.DateTime(),
                        USER_AUTORIZA = c.String(maxLength: 50),
                        FECHA_AUTORIZA = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO, t.NUM_ACCION })
                .ForeignKey("dbo.RP_EMPLEADOS", t => new { t.ID_INSTITUCION, t.ID_EMPLEADO }, cascadeDelete: true)
                .ForeignKey("dbo.RP_TIPOSMOV", t => new { t.ID_INSTITUCION, t.ID_TIPOMOV }, cascadeDelete: true)
                .Index(t => new { t.ID_INSTITUCION, t.ID_EMPLEADO })
                .Index(t => new { t.ID_INSTITUCION, t.ID_TIPOMOV });
            
            CreateTable(
                "dbo.RP_TIPOSMOV",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_TIPOMOV = c.Int(nullable: false, identity: true),
                        TIPO = c.Int(nullable: false),
                        DESCRIPCION = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_TIPOMOV })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            CreateTable(
                "dbo.RP_GRUPOCUPACIONAL",
                c => new
                    {
                        ID_INSTITUCION = c.Int(nullable: false),
                        ID_GRUPOCUPA = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.ID_INSTITUCION, t.ID_GRUPOCUPA })
                .ForeignKey("dbo.Instituciones", t => t.ID_INSTITUCION, cascadeDelete: true)
                .Index(t => t.ID_INSTITUCION);
            
            AddColumn("dbo.POA_PLANOBJETIVOS", "FINALIZADO", c => c.Boolean(nullable: false));
            AddColumn("dbo.POA_PLANOBJETIVOS", "FECHA_FINALIZA", c => c.DateTime(nullable: false));
            AddColumn("dbo.POA_PLANOBJETIVOS", "APROBADO", c => c.Boolean(nullable: false));
            AddColumn("dbo.POA_PLANOBJETIVOS", "FECHA_APRUEBA", c => c.DateTime(nullable: false));
            AddColumn("dbo.RP_CARGOS", "ID_GRUPOCUPA", c => c.Int(nullable: false));
            AddColumn("dbo.RP_SETUP", "NUM_ACCIONPERSONAL", c => c.Int(nullable: false));
            AddColumn("dbo.RP_SETUP", "EXPLICACION", c => c.String(maxLength: 250));
            AddColumn("dbo.RP_SETUP", "BASE_LEGAL", c => c.String(maxLength: 250));
            AddColumn("dbo.RP_SETUP", "REFERENCIA", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RP_GRUPOCUPACIONAL", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_ACCIONPERSONAL", new[] { "ID_INSTITUCION", "ID_TIPOMOV" }, "dbo.RP_TIPOSMOV");
            DropForeignKey("dbo.RP_TIPOSMOV", "ID_INSTITUCION", "dbo.Instituciones");
            DropForeignKey("dbo.RP_ACCIONPERSONAL", new[] { "ID_INSTITUCION", "ID_EMPLEADO" }, "dbo.RP_EMPLEADOS");
            DropIndex("dbo.RP_GRUPOCUPACIONAL", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_TIPOSMOV", new[] { "ID_INSTITUCION" });
            DropIndex("dbo.RP_ACCIONPERSONAL", new[] { "ID_INSTITUCION", "ID_TIPOMOV" });
            DropIndex("dbo.RP_ACCIONPERSONAL", new[] { "ID_INSTITUCION", "ID_EMPLEADO" });
            DropColumn("dbo.RP_SETUP", "REFERENCIA");
            DropColumn("dbo.RP_SETUP", "BASE_LEGAL");
            DropColumn("dbo.RP_SETUP", "EXPLICACION");
            DropColumn("dbo.RP_SETUP", "NUM_ACCIONPERSONAL");
            DropColumn("dbo.RP_CARGOS", "ID_GRUPOCUPA");
            DropColumn("dbo.POA_PLANOBJETIVOS", "FECHA_APRUEBA");
            DropColumn("dbo.POA_PLANOBJETIVOS", "APROBADO");
            DropColumn("dbo.POA_PLANOBJETIVOS", "FECHA_FINALIZA");
            DropColumn("dbo.POA_PLANOBJETIVOS", "FINALIZADO");
            DropTable("dbo.RP_GRUPOCUPACIONAL");
            DropTable("dbo.RP_TIPOSMOV");
            DropTable("dbo.RP_ACCIONPERSONAL");
        }
    }
}
