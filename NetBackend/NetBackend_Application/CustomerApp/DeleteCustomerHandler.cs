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
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<PatchCustomerCommand> _logger;

        public DeleteCustomerHandler(AppDbContext appDbContext, ILogger<PatchCustomerCommand> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (customer == null)
                return false;

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
