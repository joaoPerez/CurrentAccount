using CurrentAccount.Core.Shared.Result;

namespace CurrentAccount.Core.Shared
{
	public record RecordedDateValue
	{
		private static readonly string _invalidDateError = "The provided date time is invalid";

		private RecordedDateValue(DateTime recordedDate)
		{
			RecordedDate = recordedDate;
		}

		public DateTime RecordedDate { get; init; }

		public static ResultModel<RecordedDateValue> Create(DateTime date)
		{
			if (!IsValidDateTime(date))
			{
				return ResultModel<RecordedDateValue>.Failure(_invalidDateError);
			}

			return ResultModel<RecordedDateValue>.Success(new RecordedDateValue(date));
		}

		private static bool IsValidDateTime(DateTime date)
		{
			if (date > DateTime.Now)
			{
				return false;
			}

			return true;
		}
	}
}
