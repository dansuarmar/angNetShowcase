using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            PagedResponse<CustomerResponse> apiResponse = ApiMapper.CustomerListResulttoPagedResponse(result);

            return Ok(apiResponse);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById(Guid customerId)
        {
            var query = new GetCustomersByIdQuery(customerId);
            CustomerResult result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction("GetById", new { customerId = result.Id }, result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PatchCustomerCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return result != null ? CreatedAtAction("GetById", new { customerId = id }, result) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return result != null ? CreatedAtAction("GetById", new { customerId = id }, result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellation)
        {
            var command = new DeleteCustomerCommand() { Id = id };
            var result = await _mediator.Send(command, cancellation);
            return result ? Ok(true) : NotFound();
        }
    }
}
