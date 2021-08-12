namespace TrabalhoAspFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLogin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissaos",
                c => new
                    {
                        PermissaoId = c.Int(nullable: false, identity: true),
                        PermissaoNome = c.String(),
                    })
                .PrimaryKey(t => t.PermissaoId);
            
            CreateTable(
                "dbo.UsuarioPermissaos",
                c => new
                    {
                        Usuario_UsuarioId = c.Int(nullable: false),
                        Permissao_PermissaoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_UsuarioId, t.Permissao_PermissaoId })
                .ForeignKey("dbo.Usuario", t => t.Usuario_UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Permissaos", t => t.Permissao_PermissaoId, cascadeDelete: true)
                .Index(t => t.Usuario_UsuarioId)
                .Index(t => t.Permissao_PermissaoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioPermissaos", "Permissao_PermissaoId", "dbo.Permissaos");
            DropForeignKey("dbo.UsuarioPermissaos", "Usuario_UsuarioId", "dbo.Usuario");
            DropIndex("dbo.UsuarioPermissaos", new[] { "Permissao_PermissaoId" });
            DropIndex("dbo.UsuarioPermissaos", new[] { "Usuario_UsuarioId" });
            DropTable("dbo.UsuarioPermissaos");
            DropTable("dbo.Permissaos");
        }
    }
}
