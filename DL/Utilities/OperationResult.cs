namespace Data_Layer.Utilities
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public static OperationResult<T> SuccessResult(T data) => new() { Success = true, Data = data };
        public static OperationResult<T> SuccessResult() => new() { Success = true };
        public static OperationResult<T> Failure(string errorMessage) => new() { Success = false, ErrorMessage = errorMessage };
    }
}
