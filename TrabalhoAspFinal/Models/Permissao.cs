using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoAspFinal.Models
{
    public class Permissao
    {
        public int PermissaoId { get; set; }
        public string PermissaoNome { get; set; }
        public List<Usuario> PermissaoUsuarios { get; set; }
    }
}
