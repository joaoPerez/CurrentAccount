using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.Customer
{
	public record BusinessTypeValue
	{
		private static readonly string _businessMessageError = "The informed business is invalid";

		// This is only an example of businesses, the best way is to get this from a database an
		// instantied into memory or external cache.
		private static readonly string[] businesses = { "Individual", "SmallBusiness", "Corporation", "NonProfit"};

		private BusinessTypeValue(string businessType) 
		{
			Business = businessType;
		}

		public string Business { get; init; }

		public static ResultModel<BusinessTypeValue> Create(string business)
		{
			if(!IsValidBusinessType(business))
			{
				return ResultModel<BusinessTypeValue>.Failure(_businessMessageError);
			}

			return ResultModel<BusinessTypeValue>.Success(new BusinessTypeValue(business));
		}

		private static bool IsValidBusinessType(string businessType)
		{
			if(string.IsNullOrEmpty(businessType)
			|| !businesses.Contains(businessType)) 
			{ 
				return false;
			}

			return true;
		}
	}
}
