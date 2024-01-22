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
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResult?>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;
        private readonly AppMapper _mapper;

        public UpdateCustomerCommandHandler(AppDbContext appDbContext, ILogger<UpdateCustomerCommandHandler> logger, AppMapper mapper)
        {
            _dbContext = appDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomerResult?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (customer == null)
                return null;

            customer.LastUpdated = DateTime.Now;

            _mapper.UpdateCustomerCommandToCustomer(request, customer);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Customer {customer.Id} Updated");

            return _mapper.CustomerToCustomerResponse(customer);
        }
    }
}
