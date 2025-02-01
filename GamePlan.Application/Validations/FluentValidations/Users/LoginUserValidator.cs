using FluentValidation;
using GamePlan.Domain.Dto.User;

namespace GamePlan.Application.Validations.FluentValidations.Users
{
	public class LoginUserValidator : AbstractValidator<LoginUserDto>
	{
		public LoginUserValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}
