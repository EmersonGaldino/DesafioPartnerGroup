using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioPartnerGroup.Models
{
    public class PatrimonioModel
    {
        public int? ID { get; set; }
        public int? MarcaID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? Tombo { get; set; }
    }
}