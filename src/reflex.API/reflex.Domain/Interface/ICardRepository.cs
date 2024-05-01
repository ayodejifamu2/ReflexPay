using reflex.Domain.Entities;
using reflex.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.Interface
{
    public interface ICardRepository : IBaseRepository<CardInfo>
    {
    }
}
