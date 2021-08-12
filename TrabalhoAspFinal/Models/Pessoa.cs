using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoAspFinal.Models
{
    [Table("Pessoas")]
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(5, ErrorMessage = "O Nome Deve Conter No Mínimo 5 Caracteres")]
        [MaxLength(30, ErrorMessage = "O Nome Deve Conter No Máximo 30 Caracteres")]
        [Display(Name = "Nome :")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Range(10, 90, ErrorMessage = "A Idade Deverá Ser Entre 10 E 90 Anos")]
        [Display(Name = "Idade :")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telefone Invalido")]
        [Display(Name = "Telefone :")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(5, ErrorMessage = "O Endereço Deve Conter No Mínimo 5 Caracteres")]
        [MaxLength(30, ErrorMessage = "O Endereço Deve Conter No Máximo 30 Caracteres")]
        [Display(Name = "Endereço :")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [EmailAddress(ErrorMessage = "Email Invalido")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagem :")]
        public string Imagem { get; set; }

        [ScaffoldColumn(false)]
        public bool Favorita { get; set; }

        [ScaffoldColumn(false)]
        public int UsuarioId { get; set; }

        public virtual List<MensagemContato> Mensagens { get; set; }
    }
}