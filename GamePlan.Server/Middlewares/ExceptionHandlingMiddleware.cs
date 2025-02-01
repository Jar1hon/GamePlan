using GamePlan.Application.Resources;
using GamePlan.Domain.Enum;
using GamePlan.Domain.OperationException;
using GamePlan.Domain.Result;
using System.Resources;
using ILogger = Serilog.ILogger;

namespace GamePlan.Api.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpcontext)
		{
			try
			{
				await _next(httpcontext);
			}
			catch (OperationException exception)
			{
				await HandleExceptionAsync(httpcontext, exception);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpcontext, OperationException exception)
		{     
			var errorCode = exception.ErrorCode;

			var response = errorCode switch
			{
				(int)ErrorCodes.UserNotFound => new BaseResult() { ErrorMessage = ErrorMessage.UserNotFound, ErrorCode = errorCode },
				(int)ErrorCodes.IncorrectPassword => new BaseResult() { ErrorMessage = ErrorMessage.IncorrectPassword, ErrorCode = errorCode },
				(int)ErrorCodes.PasswordsNotEquals => new BaseResult() { ErrorMessage = ErrorMessage.PasswordsNotEquals, ErrorCode = errorCode },
				(int)ErrorCodes.UserAlreadyExists => new BaseResult() { ErrorMessage = ErrorMessage.UserAlreadyExists, ErrorCode = errorCode },
				
				_ => new BaseResult() { ErrorMessage = "Внутренняя ошибка сервера.", ErrorCode = (int)ErrorCodes.InternalServerError }
			};

			_logger.Error($"Код ошибки: {response.ErrorCode}, {response.ErrorMessage}");

		   httpcontext.Response.ContentType = "application/json";
			httpcontext.Response.StatusCode = (int)response.ErrorCode;
			await httpcontext.Response.WriteAsJsonAsync(response);
		}
	}
}
