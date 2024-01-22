using MediatR;
using Microsoft.Extensions.Logging;
using NetBackend_Database;

namespace NetBackend_Application.CustomerApp
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResult>
    {
        public readonly AppDbContext _context;
        public readonly ILogger<CreateCustomerCommandHandler> _logger;
        public readonly AppMapper _mapper;

        public CreateCustomerCommandHandler(AppDbContext context, ILogger<CreateCustomerCommandHandler> logger, AppMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<CustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.CreateCustomerCommandToCustomer(request);
            var response = await _context.Customers.AddAsync(customer);
            _context.SaveChanges();
            _logger.LogInformation($"Customer with Id {response.Entity.Id} created.");
            return _mapper.CustomerToCustomerResponse(response.Entity);
        }
    }
}
