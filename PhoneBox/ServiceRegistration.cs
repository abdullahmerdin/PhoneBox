using PhoneBox.Repositories.Abstracts;
using PhoneBox.Repositories.Concretes;

namespace PhoneBox
{
    public static class ServiceRegistration
    {
        public static void AddPhoneBoxServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        public static void AddClaimAuthorizationPolicies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthorization(options =>
            {
                options.AddPolicy("GetAllPhoneNumber", policy => policy.RequireClaim("getAllPhoneNumber"));
                }
            );
        }
    }
}
