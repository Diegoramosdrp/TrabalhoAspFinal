namespace TrabalhoAspFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePessoa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoas", "Imagem", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoas", "Imagem", c => c.String(nullable: false));
        }
    }
}
