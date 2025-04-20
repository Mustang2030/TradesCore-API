using Data_Layer.Models;
using Data_Layer.Utilities;

namespace Repository_Layer.IRepositories
{
    /// <summary>
    /// Interface for user data access.
    /// </summary>
    public interface IUserRepo
    {
        /// <summary>
        /// Retrieves a user with the specified id.
        /// </summary>
        /// <param name="id">
        /// The unique id of the user to add.
        /// </param>
        /// <returns>
        /// The result of the operation, with the user if successful.
        /// </returns>
        Task<OperationResult<TradesCoreUser>> GetUser(string id);

        /// <summary>
        /// Retrieves all users in the database.
        /// </summary>
        /// <returns>
        /// The result of the operation, with a list of users if successful.
        /// </returns>
        Task<OperationResult<List<TradesCoreUser>>> GetAllUsers();

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">
        /// The user object with which to update the existing user.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        Task<OperationResult<TradesCoreUser>> UpdateUser(TradesCoreUser user);

        /// <summary>
        /// Updates the email address of an existing user.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose email address is to be updated.
        /// </param>
        /// <param name="newEmail">
        /// The new email address to be assigned to the user.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        //Task<OperationResult<TradesCoreUser>> UpdateEmail(string userId, string newEmail);

        /// <summary>
        /// Updates the phone number of an existing user.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose phone number is to be updated.
        /// </param>
        /// <param name="newPhoneNumber">
        /// The new phone number of be assigned to the user.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        //Task<OperationResult<TradesCoreUser>> UpdatePhoneNumber(string userId, string newPhoneNumber);

        /// <summary>
        /// Deletes an existing user.
        /// </summary>
        /// <param name="email">
        /// The email of the user to delete.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        Task<OperationResult<TradesCoreUser>> DeleteUserAsync(string email);
    }
}
