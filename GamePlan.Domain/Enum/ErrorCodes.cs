namespace GamePlan.Domain.Enum
{
	public enum ErrorCodes
	{
		ReportsNotFound = 0,
		ReportNotFound = 1,
		ReportAlreadyExist = 2,

		UserNotFound = 11,
		UserAlreadyExists = 12,
		UnauthorizedAccess = 13,
		UserAlreadyExistsWithThisRole = 14,



		PasswordsNotEquals = 21,
		IncorrectPassword = 22,

		RoleAlreadyExists = 31,
		RoleNotFound = 32,

		InternalServerError = 10
	}
}