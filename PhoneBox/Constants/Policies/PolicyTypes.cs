namespace PhoneBox.Constants.Policies
{
    public static class PolicyTypes
    {
        public static List<string> Policies { get; } = new() 
        {
            "GetAllUsers",
            "AddUser",
            "DeleteUser",

            "GetAllUserRoles",
            "AddUserRole",
            "UpdateUserRole",
            "DeleteUserRole",
            "AssignUserRole",

            "GetAllCustomers",
            "AddCustomer",
            "UpdateCustomer",
            "DeleteCustomer"
        };
    }
}
