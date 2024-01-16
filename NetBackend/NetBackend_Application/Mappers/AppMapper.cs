using NetBackend_Database.Model;
using Riok.Mapperly.Abstractions;

namespace NetBackend_Application.CustomerApp
{
    [Mapper(AllowNullPropertyAssignment = false)]
    public partial class AppMapper
    {
        public partial CustomerResponse CustomerToCustomerResponse(Customer customer);

        public partial Customer CreateCustomerCommandToCustomer(CreateCustomerCommand createCustomerCommand);
        
        public partial void UpdateCustomerCommandToCustomer(UpdateCustomerCommand updateCustomerCommand, Customer customer);

        public partial void PatchCustomerCommandToCustomer(PatchCustomerCommand patchCustomerCommand, Customer customer);
    }
}
