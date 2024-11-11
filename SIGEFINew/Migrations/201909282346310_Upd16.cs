namespace SIGEFINew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upd16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_ENERO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_FEBRERO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_MARZO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_ABRIL", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_MAYO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_JUNIO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_JULIO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_AGOSTO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_SEPTIEMBRE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_OCTUBRE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_NOVIEMBRE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_DICIEMBRE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_DICIEMBRE", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_NOVIEMBRE", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_OCTUBRE", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_SEPTIEMBRE", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_AGOSTO", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_JULIO", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_JUNIO", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_MAYO", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_ABRIL", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_MARZO", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_FEBRERO", c => c.Int(nullable: false));
            AlterColumn("dbo.POA_OBJTIVORECURSOS", "VAL_ENERO", c => c.Int(nullable: false));
        }
    }
}
