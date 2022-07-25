using PhoneBox.Entities.Identity;
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
                //CustomersController's Claim Policies
                options.AddPolicy("GetAllCustomers", policy => policy.RequireClaim(CustomClaimTypes.Permission, "GetAllCustomers"));
                options.AddPolicy("AddCustomer", policy => policy.RequireClaim(CustomClaimTypes.Permission, "AddCustomer"));
                options.AddPolicy("UpdateCustomer", policy => policy.RequireClaim(CustomClaimTypes.Permission, "UpdateCustomer"));
                options.AddPolicy("DeleteCustomer", policy => policy.RequireClaim(CustomClaimTypes.Permission, "DeleteCustomer"));

                //UserRolesController's Claim Policies
                options.AddPolicy("GetAllUserRoles", policy => policy.RequireClaim(CustomClaimTypes.Permission, "GetAllUserRoles"));
                options.AddPolicy("AddUserRole", policy => policy.RequireClaim(CustomClaimTypes.Permission, "AddUserRole"));
                options.AddPolicy("UpdateUserRole", policy => policy.RequireClaim(CustomClaimTypes.Permission, "UpdateUserRole"));
                options.AddPolicy("DeleteUserRole", policy => policy.RequireClaim(CustomClaimTypes.Permission, "DeleteUserRole"));
                options.AddPolicy("AssignUserRole", policy => policy.RequireClaim(CustomClaimTypes.Permission, "AssignUserRole"));

                //UsersController's Claim Policies
                options.AddPolicy("GetAllUsers", policy => policy.RequireClaim(CustomClaimTypes.Permission, "GetAllUsers"));
                options.AddPolicy("AddUser", policy => policy.RequireClaim(CustomClaimTypes.Permission, "AddUser"));
                options.AddPolicy("UpdateUser", policy => policy.RequireClaim(CustomClaimTypes.Permission, "UpdateUser"));
                options.AddPolicy("DeleteUser", policy => policy.RequireClaim(CustomClaimTypes.Permission, "DeleteUser"));

            });
        }
    }
}
