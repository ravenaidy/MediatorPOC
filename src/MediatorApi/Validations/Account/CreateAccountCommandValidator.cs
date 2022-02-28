using FluentValidation;
using Mediator.Core.Account.Commands;
using Mediator.Core.Account.Contracts;

namespace Mediator.Api.Validations.Account
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new System.ArgumentNullException(nameof(accountRepository));

            RuleFor(m => m.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

            RuleFor(m => m.UserName).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Must((m, cancellation) =>
                {
                    return _accountRepository.UserNameExists(m.UserName).Result == false;
                }).WithMessage("The provided Username has already been taken");
        }
    }
}
