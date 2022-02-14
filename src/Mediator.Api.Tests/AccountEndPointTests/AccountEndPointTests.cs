using AutoMapper;
using Mediator.Api.AccountEndPoints;
using Mediator.Api.Tests.Helpers;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Mediator.Core.Account.Commands;

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
        public async Task AccountCreateEndPoint_Invalid_Parameters_Returns_BadResult()
        {
            // Arrange
            var account = new Core.Models.Account
            {
                AccountId = 1,
                Password = "password",
                UserName = "raven"
            };

            var createAccountRequest = new CreateAccountRequest { UserName = "", Password = "" };
            var createAccountCommand = new CreateAccountCommand { UserName = "", Password = "" };
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(createAccountCommand, CancellationToken.None)).ReturnsAsync(account);
            var accountCreateEndPoint = new Create(mediator.Object, _mapper);


            // Act
            var result = await accountCreateEndPoint.HandleAsync(createAccountRequest);

            // Assert
            result.Should().Be(account);
        }
    }
}
