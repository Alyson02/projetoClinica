using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projetoClinica.Models
{
    public class ModelLogin
    {
        public string usuario { get; set; }
        public string senha { get; set; }
        public string confirmaSenha { get; set; }
        public string codTipoUsuario { get; set; }
    }
}