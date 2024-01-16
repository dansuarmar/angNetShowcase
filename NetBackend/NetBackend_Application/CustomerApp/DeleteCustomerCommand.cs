using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_Application.CustomerApp
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
