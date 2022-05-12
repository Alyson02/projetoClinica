using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projetoClinica.Models
{
    public class ModelAtendimento
    {
        public string codAtendimento { get; set; }
        public string dataAtend { get; set; }
        public string horaAtend { get; set; }
        public string codMedico { get; set; }
        public string codPac { get; set; }
        public string confAgendamento { get; set; }
    }
}