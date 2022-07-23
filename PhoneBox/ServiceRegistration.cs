using PhoneBox.Repositories.Abstracts;
using PhoneBox.Repositories.Concretes;

namespace PhoneBox
{
    public static class ServiceRegistration
    {
        public static void AddPhoneBoxServices(this IServiceCollection serviceCollection)
        {
          
            serviceCollection.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
           
        }
    }
}
