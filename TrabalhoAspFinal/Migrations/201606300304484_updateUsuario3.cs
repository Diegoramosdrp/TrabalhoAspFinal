namespace TrabalhoAspFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUsuario3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PermissaoUsuarios", newName: "UsuarioPermissaos");
            DropForeignKey("dbo.Pessoas", "Usuario_UsuarioId", "dbo.Usuario");
            DropIndex("dbo.Pessoas", new[] { "Usuario_UsuarioId" });
            DropPrimaryKey("dbo.UsuarioPermissaos");
            AddColumn("dbo.Pessoas", "UsuarioId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UsuarioPermissaos", new[] { "Usuario_UsuarioId", "Permissao_PermissaoId" });
            DropColumn("dbo.Pessoas", "Administrador");
            DropColumn("dbo.Pessoas", "Usuario_UsuarioId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoas", "Usuario_UsuarioId", c => c.Int());
            AddColumn("dbo.Pessoas", "Administrador", c => c.Boolean(nullable: false));
            DropPrimaryKey("dbo.UsuarioPermissaos");
            DropColumn("dbo.Pessoas", "UsuarioId");
            AddPrimaryKey("dbo.UsuarioPermissaos", new[] { "Permissao_PermissaoId", "Usuario_UsuarioId" });
            CreateIndex("dbo.Pessoas", "Usuario_UsuarioId");
            AddForeignKey("dbo.Pessoas", "Usuario_UsuarioId", "dbo.Usuario", "UsuarioId");
            RenameTable(name: "dbo.UsuarioPermissaos", newName: "PermissaoUsuarios");
        }
    }
}
