namespace Infrastructure.Util.Validators
{
	public class InvalidRequestValueException : Exception
	{
		public Dictionary<string, List<string>>? Errors { get; set; }
		public string ErrorMessage { get; set; }
		public InvalidRequestValueException(Dictionary<string, List<string>>? errors, string? message) : base(GetErrorMessage(message))
		{

			ErrorMessage = GetErrorMessage(message);
			Errors = errors;
		}

		public InvalidRequestValueException(Dictionary<string, List<string>>? errors) : base(GetErrorMessage(null))
		{

			ErrorMessage = GetErrorMessage(null);
			Errors = errors;
		}

		private static string GetErrorMessage(string? message)
		{
			return message ?? "INVALID_REQUEST_EXCEPTION";
		}
	}
}
