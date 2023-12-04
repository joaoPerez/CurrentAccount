﻿namespace CurrentAccount.Core.Shared.Result
{
    public record ResultModel<T>
    {
        public bool IsSuccess { get; private init; }
        public T Value { get; private init; }
        public string ErrorMessage { get; private init; }

        private ResultModel() { }

        public static ResultModel<T> Success(T value) => new()
        {
            IsSuccess = true,
            Value = value
        };

        public static ResultModel<T> Failure(string errorMessage) => new()
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}
