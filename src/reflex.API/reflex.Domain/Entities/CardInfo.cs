using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.Entities
{
    public class CardInfo : BaseEntity
    {
        public int UserId { get; set; }
        public string EncrypytedCardinfo {get; set; }
    }
}
