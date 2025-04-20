using Repository_Layer.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Service_Layer.ResultService.ResultService;
using Data_Layer.Utilities;
using Data_Layer.Models;
using Data_Layer.Data;

namespace Repository_Layer.Repositories
{
    /// <summary>
    /// Repository class for user data access.
    /// </summary>
    public class UserRepo(TradesCoreDbContext context, UserManager<TradesCoreUser> userManager) : IUserRepo
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
        public async Task<OperationResult<TradesCoreUser>> GetUser(string id)
        {
            try
            {
                var user = await context.Users.FindAsync(id) 
                    ?? throw new("Could not find a user with the specified id.");

                return OperationResult<TradesCoreUser>.SuccessResult(user);
            }
            catch (Exception e)
            {
                return OperationResult<TradesCoreUser>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
        }

        /// <summary>
        /// Retrieves all users in the database.
        /// </summary>
        /// <returns>
        /// The result of the operation, with a list of users if successful.
        /// </returns>
        public async Task<OperationResult<List<TradesCoreUser>>> GetAllUsers()
        {
            try
            {
                var users = await context.Users.ToListAsync();
                return OperationResult<List<TradesCoreUser>>.SuccessResult(users);
            }
            catch (Exception e)
            {
                return OperationResult<List<TradesCoreUser>>.Failure(e.Message + "\nInner Exception: " + e.InnerException);
            }
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">
        /// The user object with which to update the existing user.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public async Task<OperationResult<TradesCoreUser>> UpdateUser(TradesCoreUser user)
        {
            try
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email)
                    ?? throw new("Could not find a user with the specified email.");

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;

                context.Update(existingUser);
                await context.SaveChangesAsync();

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

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
        //public async Task<OperationResult<TradesCoreUser>> UpdateEmail(string userId, string newEmail)
        //{
        //    try
        //    {
        //        var user = await context.Users.FindAsync(userId) ??
        //            throw new("Could not find a user with the specified id");

        //        string token = await userManager.GenerateChangeEmailTokenAsync(user, newEmail);

        //        // Email the token 
        //    }
        //    catch (Exception ex)
        //    {
        //        return OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
        //    }
        //}

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
        public async Task<OperationResult<TradesCoreUser>> DeleteUserAsync(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email) ??
                    throw new("Could not find a user with the specified id.");

                var result = IdResult(await userManager.DeleteAsync(user));
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }
    }
}
