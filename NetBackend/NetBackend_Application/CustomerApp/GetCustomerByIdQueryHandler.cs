using MediatR;
using Microsoft.EntityFrameworkCore;
using NetBackend_Database;


namespace NetBackend_Application.CustomerApp
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomersByIdQuery, CustomerResult?>
    {
        private readonly AppDbContext _dbContext;
        private readonly AppMapper _mapper;

        public GetCustomerByIdQueryHandler(AppDbContext dbContext, AppMapper appMapper)
        {
            this._dbContext = dbContext;
            _mapper = appMapper;
        }
        public async Task<CustomerResult?> Handle(GetCustomersByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == request.CustomerId, cancellationToken);
            return customer == null ? null : _mapper.CustomerToCustomerResponse(customer);
        }
    }
}
