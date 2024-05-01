using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.DTO
{
    public class KycDTO
    {
        public string? customerAddress { get; set; }
        public DateTime addressCreatedAt { get; set; }
        public DateTime addressUpdatedAt { get; set; }
        public int addressUpdateCount { get; set; }
        public int customerCountryId { get; set; }
        public int customerStateId { get; set; }
    }
}
