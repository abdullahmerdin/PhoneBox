using Microsoft.EntityFrameworkCore;
using PhoneBox.Context;
using PhoneBox.Entities;
using PhoneBox.Repositories.Abstracts;

namespace PhoneBox.Repositories.Concretes
{
    public class CustomerRepository : GenericRepository<Customer, AppDbContext>, ICustomerRepository
    {
        readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
