using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetBackend_Api.Controllers;
using NetBackend_Api_Controllers.Contracts;
using NetBackend_Application.CustomerApp;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetBackend_UnitTests.Helpers;

namespace NetBackend_UnitTests.ControllerTests
{
    public class CustomerController_POST_Tests
    {
        CustomerController _controller;
        IMediator _fakeMediator;

        public CustomerController_POST_Tests()
        {
            _fakeMediator = A.Fake<IMediator>();
            _controller = new CustomerController(_fakeMediator);
        }

        [Fact]
        public async void CreateCustomer_ShouldReturnOk()
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
                LastName = request.lastName!,
                Description = request.description!,
                Email = request.email,
                Phone = request.phone!,
            }));

            //Act
            DataAnnotationValidationHelper.Validate(request, _controller);
            var response = await _controller.Post(request) as CreatedAtActionResult;

            //Assert
            Assert.NotNull(response);
            Assert.IsType<CreatedAtActionResult>(response);
            Assert.IsType<CustomerResponse>(response.Value);
            var result = (CustomerResponse)response.Value;
            Assert.Equal(request.firstName, result.firstName);
            Assert.Equal(request.email, result.email);
        }

        [Fact]
        public async void CreateCustomer_ShouldReturnError_WhenFirstNameOrEmailMissing()
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
            var response = await _controller.Post(request);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestResult>(response);
        }
    }
}
