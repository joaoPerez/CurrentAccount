using CurrentAccount.Core.Shared.Result;
using System.Text.RegularExpressions;

namespace CurrentAccount.Core.Shared
{
	public record NameWithNumValue(string Name)
	{
		private static readonly string _notValidNameMessage = "The provided name is not valid";
		private static readonly string _regexName = "^(\\b[A-Za-z0-9]*\\b(\\s+\\b[A-Za-z0-9]*\\b)*(\\.[A-Za-z])?)$\r\n";
		private static readonly byte _maxLengthName = 255;

		public static ResultModel<NameWithNumValue> Create(string name)
		{
			if (!IsValidName(name)) { return ResultModel<NameWithNumValue>.Failure(_notValidNameMessage); }
			return ResultModel<NameWithNumValue>.Success(new NameWithNumValue(name));
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