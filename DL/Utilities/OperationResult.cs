namespace Data_Layer.Utilities
{
    /// <summary>
    /// Generic class to represent the result of an operation.
    /// </summary>
    /// <typeparam name="T">The class object whose data is to be returned with the success flag.</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Contains the error message if the operation failed.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Contains the data returned from the operation if it was successful.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Creates a new instance of the OperationResult class with the results of a successful operation.
        /// </summary>
        /// <param name="data">The data resulting from the successful operation to be returned.</param>
        /// <returns>An OperationResult object with a positive success flag and the desired data.</returns>
        public static OperationResult<T> SuccessResult(T data) => new() { Success = true, Data = data };

        /// <summary>
        /// Creates a new instance of the OperationResult class with only the success flag.
        /// </summary>
        /// <returns>An OperationResult object with ony the positive success flag.</returns>
        public static OperationResult<T> SuccessResult() => new() { Success = true };

        /// <summary>
        /// Creates a new instance of the OperationResult class with the results of a failed operation.
        /// </summary>
        /// <param name="errorMessage">The error message explaining the failure of the operation.</param>
        /// <returns>An OperationResult object with a negative success flag and the relevant error message</returns>
        public static OperationResult<T> Failure(string errorMessage) => new() { Success = false, ErrorMessage = errorMessage };
    }
}
