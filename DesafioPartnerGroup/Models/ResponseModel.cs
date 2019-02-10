using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioPartnerGroup.Models
{
    public class ResponseModel<T>
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool Erro { get; set; }
        public string MensagemErro { get; set; }
    }
}