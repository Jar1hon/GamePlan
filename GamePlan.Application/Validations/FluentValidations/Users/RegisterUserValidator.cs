using FluentValidation;
using GamePlan.Application.Resources;
using GamePlan.Domain.Dto.User;

namespace GamePlan.Application.Validations.FluentValidations.Users
{
	public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
	{
		public RegisterUserValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
			RuleFor(x => x.Password)
				.Equal(x => x.PasswordConfirm)
				.WithMessage(ErrorMessage.PasswordsNotEquals)
				.NotEmpty()
				.WithMessage(ErrorMessage.PasswordIsEmpty)
				.MinimumLength(8).WithMessage(ErrorMessage.PasswordLessThen8Char)
				.Matches("[A-Z]").WithMessage(ErrorMessage.PasswordHaventUpperCase)
				.Matches("[a-z]").WithMessage(ErrorMessage.PasswordHaventLowerCase)
				.Matches("[0-9]").WithMessage(ErrorMessage.PasswordHaventNumber)
				.Matches("[^a-zA-Z0-9]").WithMessage(ErrorMessage.PasswordHaventSpecChar);
		}
	}
}
