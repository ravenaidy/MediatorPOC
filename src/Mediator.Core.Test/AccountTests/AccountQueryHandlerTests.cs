using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Mediator.Core.Account.Contracts;
using Mediator.Core.Account.Handlers;
using Mediator.Core.Account.Queries;
using Moq;
using Xunit;

namespace Mediator.Core.Test.AccountTests
{
    public class AccountQueryHandlerTests
    {
        [Fact]
        public async Task AccountGetAccountsQueryHandler_DBFails_ThrowsException()
        {
            // Arrange
            var getAccounts = new GetAllAccountsQuery();
            var accountRepository = new Mock<IAccountRepository>();
            var queryHandler = new GetAllAccountsQueryHandler(accountRepository.Object);
            
            accountRepository.Setup(m => m.GetAllAccount()).Throws<Exception>();
            
            // Act
            Func<Task<List<Models.Account>>> action = async () => await queryHandler.Handle(getAccounts, CancellationToken.None);
            
            // Assert
            await action.Should().ThrowAsync<Exception>();
        }
        
        [Fact]
        public async Task AccountGetAccountsQueryHandler_ValidRequest_Returns_NoAccounts()
        {
            // Arrange
            var getAccounts = new GetAllAccountsQuery();
            var accountRepository = new Mock<IAccountRepository>();
            var queryHandler = new GetAllAccountsQueryHandler(accountRepository.Object);
            
            accountRepository.Setup(m => m.GetAllAccount()).ReturnsAsync((List<Models.Account>)null);
            
            // Act
            var action = await queryHandler.Handle(getAccounts, CancellationToken.None);
            
            // Assert
            action.Should().BeNull();
        }
        [Fact]
        public async Task AccountGetAccountsQueryHandler_ValidRequest_Returns_Accounts()
        {
            // Arrange
            var getAccounts = new GetAllAccountsQuery();
            var accountRepository = new Mock<IAccountRepository>();
            var queryHandler = new GetAllAccountsQueryHandler(accountRepository.Object);
            var accounts = new List<Models.Account>
            {
                new()
                {
                    AccountId = 1, Password = "password", Username = "username", IsActive = true,
                    CreatedDate = DateTime.UtcNow.AddMonths(-9)
                },
                new()
                {
                    AccountId = 2, Password = "password1", Username = "username1", IsActive = false,
                    CreatedDate = DateTime.UtcNow.AddMonths(-7)
                },
            };
            
            accountRepository.Setup(m => m.GetAllAccount()).ReturnsAsync(accounts);
            
            // Act
            var action = await queryHandler.Handle(getAccounts, CancellationToken.None);
            
            // Assert
            action.Should().HaveCount(2);
        }
    }
}