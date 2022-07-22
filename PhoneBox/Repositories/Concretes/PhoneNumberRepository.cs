using PhoneBox.Context;
using PhoneBox.Entities;
using PhoneBox.Repositories.Abstracts;

namespace PhoneBox.Repositories.Concretes
{
    public class PhoneNumberRepository : GenericRepository<PhoneNumber, AppDbContext>, IPhoneNumberRepository
    {
    }
}
