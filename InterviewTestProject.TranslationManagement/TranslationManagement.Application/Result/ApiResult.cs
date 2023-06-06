namespace TranslationManagement.Application.Result
{
    public class ApiResult<T>
    {
        public bool Success { get; }
        public T? Value { get; }
        public ResultErrorCode ErrorCode { get; }

        private ApiResult(bool success, T? value, ResultErrorCode errorCode)
        {
            Success = success;
            Value = value;
            ErrorCode = errorCode;
        }

        public static ApiResult<T> SuccessResult(T value)
        {
            return new ApiResult<T>(true, value, ResultErrorCode.None);
        }

        public static ApiResult<T> FailureResult(ResultErrorCode errorCode)
        {
            return new ApiResult<T>(false, default, errorCode);
        }

        public static ApiResult<T> BadRequestResult()
        {
            return FailureResult(ResultErrorCode.BadRequest);
        }

        public static ApiResult<T> ConflictResult()
        {
            return FailureResult(ResultErrorCode.Conflict);
        }

        public static ApiResult<T> NotFoundResult()
        {
            return FailureResult(ResultErrorCode.NotFound);
        }
    }
}