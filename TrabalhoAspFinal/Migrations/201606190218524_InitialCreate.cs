namespace TrabalhoAspFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        PessoaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                        Idade = c.Int(nullable: false),
                        Telefone = c.String(nullable: false),
                        Endereco = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        Imagem = c.String(nullable: false),
                        Favorita = c.Boolean(nullable: false),
                        Login = c.String(maxLength: 15),
                        Senha = c.String(maxLength: 20),
                        Administrador = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PessoaId);
            
            CreateTable(
                "dbo.Mensagens",
                c => new
                    {
                        MensagemId = c.Int(nullable: false, identity: true),
                        Mensagem = c.String(nullable: false, maxLength: 2500),
                        ContatoId = c.Int(nullable: false),
                        Pessoa_PessoaId = c.Int(),
                    })
                .PrimaryKey(t => t.MensagemId)
                .ForeignKey("dbo.Pessoas", t => t.Pessoa_PessoaId)
                .Index(t => t.Pessoa_PessoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensagens", "Pessoa_PessoaId", "dbo.Pessoas");
            DropIndex("dbo.Mensagens", new[] { "Pessoa_PessoaId" });
            DropTable("dbo.Mensagens");
            DropTable("dbo.Pessoas");
        }
    }
}
