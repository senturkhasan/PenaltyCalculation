using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCore2.Models
{
    public class EfCounrtytRepository : ICounrtyRepository
    {
        private ApplicationDbContext _context;

        public EfCounrtytRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Country> Countries => _context.Countries;
    }
}
