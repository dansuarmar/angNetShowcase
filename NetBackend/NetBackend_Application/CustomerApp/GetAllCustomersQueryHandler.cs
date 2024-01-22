using MediatR;
using Microsoft.EntityFrameworkCore;
using NetBackend_Application.Helpers;
using NetBackend_Database;
using NetBackend_Database.Model;
using System.Linq;
using System.Linq.Expressions;

namespace NetBackend_Application.CustomerApp
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerResult>>
    {
        private readonly AppDbContext _dbContext;
        private readonly AppMapper _mapper;

        public GetAllCustomersQueryHandler(AppDbContext dbContext, AppMapper appMapper)
        {
            this._dbContext = dbContext;
            this._mapper = appMapper;
        }

        public async Task<PagedResult<CustomerResult>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customersQuery = _dbContext.Customers.AsQueryable();
            customersQuery = AddSearchQuery(request, customersQuery);
            customersQuery = AddSortQuery(request, customersQuery);

            var customerResponses = customersQuery.Select(m => _mapper.CustomerToCustomerResponse(m));

            var customers = await PagedResult<CustomerResult>.CreatePagedListAsync(
                customerResponses, 
                request.Page, 
                request.Size, 
                cancellationToken);

            return customers;
        }

        private static IQueryable<Customer> AddSortQuery(GetAllCustomersQuery request, IQueryable<Customer> customersQuery)
        {
            if (!string.IsNullOrWhiteSpace(request.SortOrder) && request.SortOrder.ToLower().StartsWith("desc"))
                customersQuery = customersQuery.OrderByDescending(GetSortExpression(request));
            else
                customersQuery = customersQuery.OrderBy(GetSortExpression(request));
            return customersQuery;
        }

        private static IQueryable<Customer> AddSearchQuery(GetAllCustomersQuery request, IQueryable<Customer> customersQuery)
        {
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                customersQuery = customersQuery.Where(m =>
                    m.FirstName.Contains(request.SearchTerm) ||
                    m.LastName.Contains(request.SearchTerm) ||
                    m.Description.Contains(request.SearchTerm));
            return customersQuery;
        }

        private static Expression<Func<Customer, object>> GetSortExpression(GetAllCustomersQuery request)
        {
            return request.SortColumn?.ToLower() switch
            {
                "firstname" => customer => customer.FirstName,
                "lastname" => customer => customer.LastName,
                "created" => customer => customer.Created,
                _ => customer => customer.Created,
            };
        }
    }
}
