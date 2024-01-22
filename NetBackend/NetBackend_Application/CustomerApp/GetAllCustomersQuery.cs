using MediatR;
using NetBackend_Application.Helpers;

namespace NetBackend_Application.CustomerApp
{
    public class GetAllCustomersQuery : IRequest<PagedResult<CustomerResult>>
    {
        public string? SearchTerm { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
    }
}
