namespace TrabalhoAspFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsuario : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pessoas", "Login");
            DropColumn("dbo.Pessoas", "Senha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoas", "Senha", c => c.String(maxLength: 20));
            AddColumn("dbo.Pessoas", "Login", c => c.String(maxLength: 15));
        }
    }
}
