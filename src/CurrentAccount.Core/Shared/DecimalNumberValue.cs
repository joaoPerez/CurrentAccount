using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.Shared
{
	public record DecimalNumberValue
	{
		private static readonly int _maxDecimalValue = 100000;
		private static readonly byte _minDecimalValue = 0;
		private static readonly byte _maxDecimalPlaces = 2;

		private static readonly string _maxDecimalPlacesErrorMessage = $"The maximium decimal places are {_maxDecimalPlaces}";
		private static readonly string _maxDecimalValueErrorMessage = $"The maximium value is {_maxDecimalValue}";
		private static readonly string _minDecimalValueErrorMessage = $"The minimium value is {_minDecimalValue}";

		private DecimalNumberValue(decimal value)
		{
			Value = value;
		}

		public decimal Value { get; init; }

		public static ResultModel<DecimalNumberValue> Create(decimal value)
		{
			var (isValid, failure) = IsValidDecimalNumber(value);

			if (!isValid)
			{
				return ResultModel<DecimalNumberValue>.Failure(failure);
			}

			return ResultModel<DecimalNumberValue>.Success(new DecimalNumberValue(value));
		}

		private static (bool isValid, string failure) IsValidDecimalNumber(decimal value)
		{
			if (value > _maxDecimalValue)
			{
				return (false, _maxDecimalValueErrorMessage);
			}

			if (value < _minDecimalValue)
			{
				return (false, _minDecimalValueErrorMessage);
			}

			if (decimal.Round(value, _maxDecimalPlaces) != value)
			{
				return (false, _maxDecimalPlacesErrorMessage);
			}

			return (true, string.Empty);
		}
	}
}
