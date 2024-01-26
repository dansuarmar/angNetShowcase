using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetBackend_Api_Controllers;
using NetBackend_Api_Controllers.Contracts;
using NetBackend_Application.CustomerApp;
using NetBackend_Application.Helpers;

namespace NetBackend_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken, 
            string? search,
            string? sort,
            string? order,
            int page = 1,
            int size = 50
            )
        {
            var query = new GetAllCustomersQuery() 
            {
                SearchTerm = search,
                SortColumn = sort,
                SortOrder = order,
                Page = page,
                Size = size,
            };
            PagedResult<CustomerResult> result = await _mediator.Send(query, cancellationToken);
            PagedResponse<CustomerResponse> apiResponse = ApiMapper.CustomerPageResulttoPagedResponse(result);

            return Ok(apiResponse);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById(Guid customerId, CancellationToken cancellationToken)
        {
            var query = new GetCustomersByIdQuery(customerId);
            CustomerResult result = await _mediator.Send(query, cancellationToken);
            CustomerResponse? response =  ApiMapper.CustomerResultToCustomerResponse(result);

            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UpsertCustomerRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            CustomerResult result = await _mediator.Send(ApiMapper.CreateCustomerRequesttoCreateCustomerCommand(request));
            CustomerResponse? response = ApiMapper.CustomerResultToCustomerResponse(result);
            return CreatedAtAction("GetById", new { customerId = response?.id }, response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PatchCustomerCommand command)
        {
            command.Id = id;
            CustomerResult? result = await _mediator.Send(command);
            CustomerResponse? response = ApiMapper.CustomerResultToCustomerResponse(result);
            return response != null ? CreatedAtAction("GetById", new { customerId = id }, response) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpsertCustomerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = ApiMapper.CreateCustomerRequesttoUpdateCustomerCommand(request);
            command.Id = id;
            CustomerResult? result = await _mediator.Send(command);
            CustomerResponse? response = ApiMapper.CustomerResultToCustomerResponse(result);
            return response != null ? CreatedAtAction("GetById", new { customerId = id }, response) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCustomerCommand() { Id = id };
            bool result = await _mediator.Send(command);
            return result ? Ok(true) : NotFound();
        }
    }
}
