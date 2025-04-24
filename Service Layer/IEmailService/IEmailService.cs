using Data_Layer.Utilities;

namespace Service_Layer.IEmailService
{
    public interface IEmailService
    {
        /// <summary>
        /// Generates a link for the user to confirm their email address.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose email address is to be confirmed.
        /// </param>
        /// <param name="actionMethod">
        /// The name of the action method to be called when the link is clicked.
        /// </param>
        /// <param name="controller">
        /// The name of the controller to be called when the link is clicked.
        /// </param>
        /// <param name="token">
        /// The email confirmation token to be included in the link.
        /// </param>
        /// <returns>
        /// The result of the operation, including the generated link if successful.
        /// </returns>
        OperationResult<string> GenerateLink(string userId, string actionMethod, string controller, string token);

        /// <summary>
        /// Generates a link for the user to confirm their updated email address.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose email address is to be confirmed.
        /// </param>
        /// <param name="actionMethod">
        /// The name of the action method to be called when the link is clicked.
        /// </param>
        /// <param name="controller">
        /// The name of the controller to be called when the link is clicked.
        /// </param>
        /// <param name="token">
        /// The email confirmation token to be included in the link.
        /// </param>
        /// <param name="newEmail">
        /// The new email address to be confirmed.
        /// </param>
        /// <returns>
        /// The result of the operation, including the generated link if successful.
        /// </returns>
        OperationResult<string> GenerateLink(string userId, string actionMethod, string controller, string token, string newEmail);
    }
}
