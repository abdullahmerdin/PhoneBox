using Microsoft.EntityFrameworkCore;
using PhoneBox.Context;
using PhoneBox.Repositories.Abstracts;
using PhoneBox.Repositories.Concretes;

namespace PhoneBox
{
    public static class ServiceRegistration
    {
        public static void AddPhoneBoxServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<DbContext, AppDbContext>();

            serviceCollection.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
        }
    }
}
