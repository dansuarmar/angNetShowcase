using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetBackend_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_Application.CustomerApp
{
    public class PatchCustomerCommandHandler : IRequestHandler<PatchCustomerCommand, CustomerResponse?>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<PatchCustomerCommandHandler> _logger;
        private readonly AppMapper _mapper;

        public PatchCustomerCommandHandler(AppDbContext appDbContext, ILogger<PatchCustomerCommandHandler> logger, AppMapper mapper)
        {
            _dbContext = appDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomerResponse?> Handle(PatchCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (customer == null)
                return null;

            customer.LastUpdated = DateTime.Now;

            _mapper.PatchCustomerCommandToCustomer(request, customer);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Customer {customer.Id} Patched.");

            return _mapper.CustomerToCustomerResponse(customer);
        }
    }
}
