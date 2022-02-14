using AutoMapper;
using FluentAssertions;
using Mediator.Api.Tests.Helpers;
using Mediator.Core.Account.Commands;
using Mediator.Core.Account.Contracts;
using Mediator.Core.Account.Handlers;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mediator.Core.Test.AccountTests
{
    public class AccountHandlerTests
    {
        private readonly Mapper _mapper;

        public AccountHandlerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMappingProfile>()));
        }

        [Fact]
        public async Task AccountCreateHandler_Returns_Account()
        {
            // Arrange
            var account = new Models.Account
            {
                AccountId = 1,
                UserName = "raven",
                Password = "password"
            };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(m => m.AddAccount(It.IsAny<Models.Account>())).ReturnsAsync(account);
            var accountCreateHandler = new CreateAccountHandler(accountRepository.Object, _mapper);

            // Act
            var result = await accountCreateHandler.Handle(new CreateAccountCommand { UserName = "raven", Password = "password" }, CancellationToken.None);

            // Assert
            result.Should().Be(account);
        }
    }
}
