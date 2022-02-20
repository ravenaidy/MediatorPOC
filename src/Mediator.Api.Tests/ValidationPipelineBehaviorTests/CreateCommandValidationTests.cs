using FluentAssertions;
using FluentValidation;
using Mediator.Api.PipeLineBehaviors;
using Mediator.Api.Validations.Account;
using Mediator.Core.Account.Commands;
using Mediator.Core.Account.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mediator.Api.Tests.ValidationPipelineBehaviorTests
{
    public class CreateCommandValidationTests
    {
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

        [Theory]
        [InlineData("", "")]
        [InlineData("", "password")]
        [InlineData("ravenaidy", "")]
        [InlineData("james", "This@I45Iene#$")]
        [InlineData("jsdfkhsdflhsaldfhalsdhflkashdflkahsdlkfhsalkfsakfdfsalkdh", "This@I45Iene#$")]
        public async Task CreateAccountValidationPipeBehavior_Invalid_Parameters_ThrowsValidationException(string username, string password)
        {
            // Arrange
            var repository = new Mock<IAccountRepository>();
            var validator = new CreateAccountCommandValidator(repository.Object);            
            repository.Setup(m => m.UserNameExists(username)).ReturnsAsync(true);
            var createAccountCommand = new CreateAccountCommand { UserName = username, Password = password };
            var validators = new List<IValidator<CreateAccountCommand>>
            {
                new CreateAccountCommandValidator(repository.Object),
            };
            var behavior = new ValidationBehavior<CreateAccountCommand, Core.Models.Account>(validators);

            // Act            
            Func<Task<Core.Models.Account>> action = async () => await behavior.Handle(createAccountCommand, CancellationToken.None, null);

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
