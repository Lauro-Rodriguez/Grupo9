namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BANCOS",
                c => new
                    {
                        CODIGO_BANCO = c.Int(nullable: false, identity: true),
                        DESCRIPCION = c.String(nullable: false, maxLength: 40),
                        CODIGO_ENVIO = c.Int(nullable: false),
                        TIPO = c.Int(nullable: false),
                        CONDICION = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CODIGO_BANCO);
            
            CreateIndex("dbo.Proveedores", "CODIGO_BANCO");
            AddForeignKey("dbo.Proveedores", "CODIGO_BANCO", "dbo.BANCOS", "CODIGO_BANCO", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proveedores", "CODIGO_BANCO", "dbo.BANCOS");
            DropIndex("dbo.Proveedores", new[] { "CODIGO_BANCO" });
            DropTable("dbo.BANCOS");
        }
    }
}
