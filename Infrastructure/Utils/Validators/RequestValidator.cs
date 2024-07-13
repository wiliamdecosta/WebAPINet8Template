using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Util.Validators
{
	public class RequestValidator
	{
		public RequestValidator() { }
		public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

		public bool Validate(object request)
		{
			var validationResults = new List<ValidationResult>();
			var context = new ValidationContext(request);

			if (!Validator.TryValidateObject(request, context, validationResults, true))
			{
				Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
				foreach (var validationResult in validationResults)
				{
					string memberName = validationResult.MemberNames.First();
					string firstLetterLowercase = char.ToLowerInvariant(memberName[0]).ToString();
					string restOfTheString = memberName.Substring(1);
					memberName = firstLetterLowercase + restOfTheString;
					_errors.Add(memberName, [validationResult.ErrorMessage]);
				}
				Errors = _errors;
				return false;
			}

			return true;
		}
	}
}
