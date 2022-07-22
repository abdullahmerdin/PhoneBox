using Microsoft.EntityFrameworkCore;
using PhoneBox.Context;
using PhoneBox.Entities;
using PhoneBox.Repositories.Abstracts;

namespace PhoneBox.Repositories.Concretes
{
    public class PhoneNumberRepository : GenericRepository<PhoneNumber, AppDbContext>, IPhoneNumberRepository
    {
        readonly AppDbContext _context;
        public PhoneNumberRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public PhoneNumber GetByIdWithDetails(int id)
        {
            return _context.Set<PhoneNumber>().Include(x => x.AppUser).SingleOrDefault(x => x.Id == id);
        }
    }
}
