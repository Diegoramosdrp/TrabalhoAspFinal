using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoAspFinal.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
            Permissoes = new List<Permissao>();//MODIFIQUEI AQUI
        }

        [Key]
        public int UsuarioId { get; set; }

        [Display(Name = "Login :")]
        [Required(ErrorMessage = "*")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha :")]
        [Required(ErrorMessage = "*")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        [Display(Name = "Confirmação De Senha :")]
        [NotMapped]
        public string ConfirmacaoSenha { get; set; }

        [ScaffoldColumn(false)]
        public int PessoaId { get; set; }

        public virtual List<Permissao> Permissoes { get; set; }//MODIFIQUEI AQUI
    }
}