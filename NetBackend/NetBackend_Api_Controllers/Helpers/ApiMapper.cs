using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBackend_Api_Controllers.Contracts;
using NetBackend_Application.CustomerApp;
using NetBackend_Application.Helpers;

namespace NetBackend_Api_Controllers
{
    [Mapper(AllowNullPropertyAssignment = false)]
    internal static partial class ApiMapper
    {
        public static partial CustomerResponse? CustomerResultToCustomerResponse(CustomerResult? customerResult);

        public static partial PagedResponse<CustomerResponse> CustomerPageResulttoPagedResponse(PagedResult<CustomerResult> pagedResponse);

        public static partial CreateCustomerCommand CreateCustomerRequesttoCreateCustomerCommand(UpsertCustomerRequest createCustomerRequest);
        
        public static partial UpdateCustomerCommand CreateCustomerRequesttoUpdateCustomerCommand(UpsertCustomerRequest createCustomerRequest);
    }
}
