using Data_Layer.Utilities;
using Microsoft.AspNetCore.Identity;

namespace Service_Layer.ResultService
{
    public static class ResultService
    {
        /// <summary>
        /// Method to process the results <see cref="IdentityResult"/> operation.
        /// </summary>
        /// <param name="result">
        /// The <see cref="IdentityResult"/> to process.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{T}"/> containing the result of the operation.
        /// </returns>
        public static OperationResult<IdentityResult> IdResult(IdentityResult result)
        {
            if (result.Succeeded)
            {
                return OperationResult<IdentityResult>.SuccessResult();
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(err => err.Description));
                return OperationResult<IdentityResult>.Failure($"Failed to create user: {errors}");
            }
        }
    }
}
