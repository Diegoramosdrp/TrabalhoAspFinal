namespace TrabalhoAspFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsuario1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        PessoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Pessoas", t => t.PessoaId, cascadeDelete: true)
                .Index(t => t.PessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "PessoaId", "dbo.Pessoas");
            DropIndex("dbo.Usuario", new[] { "PessoaId" });
            DropTable("dbo.Usuario");
        }
    }
}
