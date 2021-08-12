namespace TrabalhoAspFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUsuario2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsuarioPermissaos", newName: "PermissaoUsuarios");
            DropForeignKey("dbo.Usuario", "PessoaId", "dbo.Pessoas");
            DropIndex("dbo.Usuario", new[] { "PessoaId" });
            DropPrimaryKey("dbo.PermissaoUsuarios");
            AddColumn("dbo.Pessoas", "Usuario_UsuarioId", c => c.Int());
            AddPrimaryKey("dbo.PermissaoUsuarios", new[] { "Permissao_PermissaoId", "Usuario_UsuarioId" });
            CreateIndex("dbo.Pessoas", "Usuario_UsuarioId");
            AddForeignKey("dbo.Pessoas", "Usuario_UsuarioId", "dbo.Usuario", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoas", "Usuario_UsuarioId", "dbo.Usuario");
            DropIndex("dbo.Pessoas", new[] { "Usuario_UsuarioId" });
            DropPrimaryKey("dbo.PermissaoUsuarios");
            DropColumn("dbo.Pessoas", "Usuario_UsuarioId");
            AddPrimaryKey("dbo.PermissaoUsuarios", new[] { "Usuario_UsuarioId", "Permissao_PermissaoId" });
            CreateIndex("dbo.Usuario", "PessoaId");
            AddForeignKey("dbo.Usuario", "PessoaId", "dbo.Pessoas", "PessoaId", cascadeDelete: true);
            RenameTable(name: "dbo.PermissaoUsuarios", newName: "UsuarioPermissaos");
        }
    }
}
