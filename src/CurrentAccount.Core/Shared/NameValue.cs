using CurrentAccount.Core.Shared.Result;
using System.Text.RegularExpressions;

namespace CurrentAccount.Core.Shared
{
	public record NameValue(string Name)
	{
		private static readonly string _notValidNameMessage = "The provided name is not valid";
		private static readonly string _regexName = "^(\\b[A-Za-z]*\\b(\\s+\\b[A-Za-z]*\\b)*(\\.[A-Za-z])?)$";
		private static readonly byte _maxLengthName = 255;

		public static ResultModel<NameValue> Create(string name)
		{
			if (!IsValidName(name)) { return ResultModel<NameValue>.Failure(_notValidNameMessage); }
			return ResultModel<NameValue>.Success(new NameValue(name));
		}

		private static bool IsValidName(string name)
		{
			// Ensuring regex will have a light work
			if (string.IsNullOrEmpty(name) ||
			   name.Length > _maxLengthName
				) { return false; }

			return Regex.IsMatch(name, _regexName, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(250));
		}
	}
}