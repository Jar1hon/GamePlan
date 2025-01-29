using GamePlan.Domain.Dto;
using GamePlan.Domain.Dto.User;
using GamePlan.Domain.Result;

namespace GamePlan.Domain.Interfaces.Services
{
	/// <summary>
	/// Сервис для регистрации/авторизации пользователей
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Регистрация пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<UserDto>> Register(RegisterUserDto dto);

		/// <summary>
		/// Авторизация пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
	}
}
