using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Mediator.Api.AccountEndPoints;
using Mediator.Api.Tests.Helpers;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Mediator.Core.Account.Commands;
using Mediator.Core.Account.Queries;
using Mediator.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mediator.Api.Tests.AccountEndPointTests
{
    public class AccountEndPointTests
    {
        private readonly Mapper _mapper;

        public AccountEndPointTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMappingProfile>()));
        }
        
        [Fact]
        public async Task AccountCreateEndPoint_Valid_Parameters_Returns_OkResult()
        {
            // Arrange
            var account = new Account
            {
                AccountId = 1,
                Password = "password",
                Username = "raven"
            };
            var createAccountRequest = new CreateAccountRequest { UserName = account.Username, Password = account.Password };
            var createAccountCommand = new CreateAccountCommand { UserName = account.Username, Password = account.Password };

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(createAccountCommand, CancellationToken.None)).Verifiable();
            var accountCreateEndPoint = new Create(mediator.Object, _mapper);

            // Act
            var actionResult = await accountCreateEndPoint.HandleAsync(createAccountRequest);

            // Assert
            var result = (actionResult as OkResult);
            result.StatusCode.Should().Be((int) HttpStatusCode.OK);
            mediator.Verify(m => m.Send(createAccountCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task AccountGetAllEndpoint_Return_No_Results()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetAllAccountsQuery>(), CancellationToken.None))
                .ReturnsAsync((List<Account>) null);
            var getAllAccountsEndPoint = new GetAll(mediator.Object, _mapper);

            // Act
            var actionResult = await getAllAccountsEndPoint.HandleAsync();

            // Assert
            var result = actionResult.Result as NoContentResult;
            result.StatusCode.Should().Be((int) HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task AccountGetAllEndpoint_Return_Accounts()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new()
                {
                    AccountId = 1, Username = "ravenaidy", CreatedDate = DateTime.Now.AddMonths(-9), IsActive = true
                },
                new()
                {
                    AccountId = 2, Username = "james", CreatedDate = DateTime.Now.AddMonths(-10), IsActive = true
                },
                new()
                {
                    AccountId = 3, Username = "jonathan", CreatedDate = DateTime.Now.AddMonths(-4), IsActive = true
                }
            };
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetAllAccountsQuery>(), CancellationToken.None))
                .ReturnsAsync(accounts);
            var getAllAccountsEndPoint = new GetAll(mediator.Object, _mapper);

            // Act
            var actionResult = await getAllAccountsEndPoint.HandleAsync();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            result.StatusCode.Should().Be((int) HttpStatusCode.OK);
            var response = _mapper.Map<List<GetAllAccountsResponse>>(result.Value);
            result.Value.Should().BeEquivalentTo(response);
        }
    }
}
