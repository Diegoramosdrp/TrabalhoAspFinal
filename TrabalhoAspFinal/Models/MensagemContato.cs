using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TrabalhoAspFinal.Models
{
    [Table("Mensagens")]
    public class MensagemContato
    {
        [Key]
        public int MensagemId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(5, ErrorMessage = "A Mensagem Deve Conter No Mínimo 5 Caracteres")]
        [MaxLength(2500, ErrorMessage = "A Mensagem Deve Conter No Máximo 2500 Caracteres")]
        [Display(Name = "Mensagem : ")]
        [DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int ContatoId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
