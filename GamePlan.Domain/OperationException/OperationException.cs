using GamePlan.Domain.Enum;

namespace GamePlan.Domain.OperationException
{
	public class OperationException : Exception
	{
		public int ErrorCode { get; set; }
		public OperationException(int errorCodes)
		{
			ErrorCode = errorCodes;
		}

		public OperationException(int errorCodes, string message) : base(message)
		{
			ErrorCode = errorCodes;
		}
	}
}
