using reflex.Domain;
using reflex.Domain.Interface;
using reflex.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Persistence.Repository
{
    public class OtpRepository : BaseRepository<Otp>, IOtpRepository
    {
        public OtpRepository(AppDbContext context) : base(context)
        {
        }
    }
}
