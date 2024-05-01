using reflex.Domain.Interface;
using reflex.Domain;
using reflex.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reflex.Domain.Entities;

namespace reflex.Persistence.Repository
{
    public class CardRepository : BaseRepository<CardInfo>, ICardRepository
    {
        public CardRepository(AppDbContext context) : base(context)
        {
        }
    }
}
