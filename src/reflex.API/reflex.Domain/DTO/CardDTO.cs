using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.DTO
{
    public class CardDTO
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get;set; }
        public int CVV { get; set; }
    }
}
