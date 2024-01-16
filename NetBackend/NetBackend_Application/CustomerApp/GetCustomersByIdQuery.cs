using MediatR;

namespace NetBackend_Application.CustomerApp
{
    public class GetCustomersByIdQuery : IRequest<CustomerResponse>
    {
        public Guid CustomerId { get; }
        public GetCustomersByIdQuery(Guid id)
        {
            CustomerId = id;
        }
    }
}
