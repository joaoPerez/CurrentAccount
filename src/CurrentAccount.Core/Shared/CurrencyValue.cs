using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.Shared
{
	public record CurrencyValue
	{
		private static readonly string _currencyMessageError = "The informed currency is invalid";

		// This is only an example of currencies, the best way is to get this from a database an
		// instantied into memory or external cache.
		private static readonly string[] currencies = { "USD", "EUR", "BRL", "GBP", "CAD" };

		private CurrencyValue(string currency) 
		{
			Currency = currency;
		}

		public string Currency { get; init; }

		public static ResultModel<CurrencyValue> Create(string currency)
		{
			if(!IsValidCurrency(currency))
			{
				return ResultModel<CurrencyValue>.Failure(_currencyMessageError);
			}

			return ResultModel<CurrencyValue>.Success(new CurrencyValue(currency));
		}

		private static bool IsValidCurrency(string currency)
		{
			if(string.IsNullOrEmpty(currency)
			|| !currencies.Contains(currency)) 
			{ 
				return false;
			}

			return true;
		}
	}
}
