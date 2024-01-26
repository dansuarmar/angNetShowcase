using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetBackend_Api.Controllers;
using NetBackend_Api_Controllers.Contracts;
using NetBackend_Application.CustomerApp;
using NetBackend_UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBackend_UnitTests.ControllerTests
{
    public class CustomerController_PUT_Tests
    {
        CustomerController _controller;
        IMediator _fakeMediator;

        public CustomerController_PUT_Tests()
        {
            _fakeMediator = A.Fake<IMediator>();
            _controller = new CustomerController(_fakeMediator);
        }

        [Fact]
        public async void PutCustomer_ShouldReturnOk()
        {
            //Arrange
            var request = new UpsertCustomerRequest()
            {
                firstName = "First Name",
                email = "this@mail.com",
                lastName = "Last Name",
                phone = "Phone",
                description = "Description"
            };
            A.CallTo(() => _fakeMediator.Send(A<CreateCustomerCommand>._, A<CancellationToken>._)).Returns(Task.FromResult(new CustomerResult()
            {
                FirstName = request.firstName,
                LastName = request.lastName,
                Description = request.description,
                Email = request.email,
                Phone = request.phone,
            }));

            //Act
            DataAnnotationValidationHelper.Validate(request, _controller);
            var response = await _controller.Put(Guid.NewGuid(), request) as CreatedAtActionResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<CreatedAtActionResult>(response);
            Assert.IsType<CustomerResponse>(response.Value);
            var result = (CustomerResponse)response.Value;
            Assert.Equal(request.firstName, result.firstName);
            Assert.Equal(request.email, result.email);
        }

        [Fact]
        public async void PutCustomer_ShouldReturnError_WhenFirstNameOrEmailMissing()
        {
            //Arrange
            var request = new UpsertCustomerRequest()
            {
                firstName = "",
                email = "",
                lastName = "",
                phone = "",
                description = ""
            };

            //Act
            DataAnnotationValidationHelper.Validate(request, _controller);
            var response = await _controller.Put(Guid.NewGuid(), request);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestResult>(response);
        }
    }
}
