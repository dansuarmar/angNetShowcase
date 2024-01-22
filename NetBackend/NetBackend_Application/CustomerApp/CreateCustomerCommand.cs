using MediatR;
using System.ComponentModel.DataAnnotations;

namespace NetBackend_Application.CustomerApp
{
    public class CreateCustomerCommand : CustomerDTO, IRequest<CustomerResult> 
    {
    }
}
