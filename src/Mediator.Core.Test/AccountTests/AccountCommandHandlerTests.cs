using System;
using AutoMapper;
using FluentAssertions;
using Mediator.Api.Tests.Helpers;
using Mediator.Core.Account.Commands;
using Mediator.Core.Account.Contracts;
using Mediator.Core.Account.Handlers;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Xunit;

namespace Mediator.Core.Test.AccountTests
{
    public class AccountCommandHandlerTests
    {
        private readonly Mapper _mapper;

        public AccountCommandHandlerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMappingProfile>()));
        }

        [Fact]
        public async Task AccountCreateHandler_DBFails_ThrowsException()
        {
            // Arrange
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(m => m.AddAccount(It.IsAny<Models.Account>()))
                .Throws<Exception>();
            var accountCreateHandler = new CreateAccountHandler(accountRepository.Object, _mapper);

            // Act
            var request = new CreateAccountCommand
            {
                UserName = "raven",
                Password = "password"
            };
            Func<Task<Unit>> action = async () => await accountCreateHandler.Handle(request, CancellationToken.None);
            
            // Assert
            await action.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AccountCreateHandler_Create_Account()
        {
            // Arrange
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(m => m.AddAccount(It.IsAny<Models.Account>())).Verifiable();
            var accountCreateHandler = new CreateAccountHandler(accountRepository.Object, _mapper);

            // Act
            var request = new CreateAccountCommand
            {
                UserName = "raven",
                Password = "password"
            };
            var result = await accountCreateHandler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
        }
    }
}
