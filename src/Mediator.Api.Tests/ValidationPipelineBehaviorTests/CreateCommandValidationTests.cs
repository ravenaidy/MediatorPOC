using FluentAssertions;
using Mediator.Api.Validations.Account;
using Mediator.Core.Account.Commands;
using Mediator.Core.Account.Contracts;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Mediator.Api.Tests.ValidationPipelineBehaviorTests
{
    public class CreateCommandValidationTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", "password")]
        [InlineData("ravenaidy", "")]
        [InlineData("james", "This@I45Iene#$")]        
        public void CreateAccountValidationPipeBehavior_Invalid_Parameters_Returns_False(string username, string password)
        {
            // Arrange
            var repository = new Mock<IAccountRepository>();
            repository.Setup(m => m.UserNameExists(username)).ReturnsAsync(true);
            var validator = new CreateAccountCommandValidator(repository.Object);

            var createAccountCommand = new CreateAccountCommand { UserName = username, Password = password };

            // Act
            var result = validator.Validate(createAccountCommand);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CreateAccountValidationPipeBehavior_Valid_Parameters_Returns_True()
        {
            // Arrange
            var username = "ravenaidy";
            var password = "Un1qu3P@55w0rD";            
            var repository = new Mock<IAccountRepository>();
            repository.Setup(m => m.UserNameExists(username)).ReturnsAsync(false);
            var validator = new CreateAccountCommandValidator(repository.Object);
            var createAccountCommand = new CreateAccountCommand { UserName = username, Password = password };

            // Act
            var result = validator.Validate(createAccountCommand);

            // Assert
            result.IsValid.Should().BeTrue();
            repository.Verify(m => m.UserNameExists(username), Times.Once());
        }
    }
}
