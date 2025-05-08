using static Service_Layer.ResultService.ResultService;
using Repository_Layer.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service_Layer.IEmailService;
using Data_Layer.Utilities;
using Data_Layer.Models;
using Data_Layer.Data;

namespace Repository_Layer.Repositories
{
    /// <summary>
    /// Repository for user data access.
    /// </summary>
    /// <param name="context">
    /// The database context for the application.
    /// </param>
    /// <param name="userManager">
    /// The user manager for managing user-related operations.
    /// </param>
    /// <param name="emailService">
    /// The email service for generating email links.
    /// </param>
    /// <param name="emailSender">
    /// The email sender for sending emails.
    /// </param>
    public class UserRepo(TradesCoreDbContext context, UserManager<TradesCoreUser> userManager, IEmailService emailService, IEmailSender emailSender) : IUserRepo
    {
        /// <summary>
        /// The user not found error message.
        /// </summary>
        private const string userNotFound = "User Not Found!";

        /// <summary>
        /// Retrieves a user with the specified id.
        /// </summary>
        /// <param name="id">
        /// The unique id of the user to add.
        /// </param>
        /// <returns>
        /// The result of the operation, with the user if successful.
        /// </returns>
        public async Task<OperationResult<TradesCoreUser>> GetUserAsync(string id)
        {
            try
            {
                var user = await context.Users.FindAsync(id) ?? throw new(userNotFound);
                return OperationResult<TradesCoreUser>.SuccessResult(user);
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Retrieves a user with the specified email address.
        /// </summary>
        /// <param name="email">
        /// The email address of the user to retrieve.
        /// </param>
        /// <returns>
        /// The result of the operation, with the user if successful.
        /// </returns>
        public async Task<OperationResult<TradesCoreUser>> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email) ?? throw new(userNotFound);
                return OperationResult<TradesCoreUser>.SuccessResult(user);
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Retrieves all users in the database.
        /// </summary>
        /// <returns>
        /// The result of the operation, with a list of users if successful.
        /// </returns>
        public async Task<OperationResult<List<TradesCoreUser>>> GetAllUsersAsync()
        {
            try
            {
                var users = await context.Users.ToListAsync();
                return OperationResult<List<TradesCoreUser>>.SuccessResult(users);
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<List<TradesCoreUser>>.Failure(ex.Message) :
                    OperationResult<List<TradesCoreUser>>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
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
        public async Task<OperationResult<TradesCoreUser>> UpdateUserAsync(TradesCoreUser user)
        {
            try
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email!)
                    ?? throw new(userNotFound);

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;

                var result = IdResult(await userManager.UpdateAsync(existingUser));
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
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
        public async Task<OperationResult<TradesCoreUser>> UpdateEmailAsync(string userId, string newEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(newEmail);
                if (user is not null) throw new("Email already in use.");

                user = await userManager.FindByIdAsync(userId) ?? throw new(userNotFound);
                if (user.Email == newEmail) throw new("New email is the same as the current email.");

                string token = await userManager.GenerateChangeEmailTokenAsync(user, newEmail);

                var result = emailService.GenerateLink(userId, "ConfirmNewEmail", "Account", token, newEmail);
                if (!result.Success) throw new(result.ErrorMessage);

                if (result.Data is null) throw new("URL generation failed!");

                var emailResult = await emailSender.SendEmailAsync(
                    toEmail: newEmail, 
                    subject: "Confirm your new email",
                    message: "Good day From TradesCore,<br>" +
                    "Please confirm your new email address by clicking the link below:<br>" +
                    "<a href='" + result.Data + "'>Confirm Email</a><br>" +
                    "If you did not request this change, please ignore this email.<br>" +
                    "Thank you for using our service!<br>"
                );
                if (!emailResult.Success) throw new(emailResult.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Confirms the email change for a user.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose email address is to be updated.
        /// </param>
        /// <param name="token">
        /// The token used to confirm the email change.
        /// </param>
        /// <param name="newEmail">
        /// The new email address to be assigned to the user.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public async Task<OperationResult<TradesCoreUser>> ConfirmEmailChangeAsync(string userId, string token, string newEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(newEmail);
                if (user is not null) throw new("Email already in use.");

                user = await userManager.FindByIdAsync(userId) ?? throw new(userNotFound);
                if (user.Email == newEmail) throw new("New email is the same as the current email.");

                var result = IdResult(await userManager.ChangeEmailAsync(user, newEmail, token));
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

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
        public async Task<OperationResult<TradesCoreUser>> UpdatePhoneNumberAsync(string userId, string newPhoneNumber)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId) ?? throw new(userNotFound);
                if (user.PhoneNumber == newPhoneNumber) throw new("New phone number is the same as the current phone number.");

                var token = await userManager.GenerateChangePhoneNumberTokenAsync(user, newPhoneNumber);

                var result = IdResult(await userManager.ChangePhoneNumberAsync(user, newPhoneNumber, token));
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Finds a user by their unique identifier and deletes them.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the user to delete.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public async Task<OperationResult<TradesCoreUser>> DeleteUserAsync(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id) ?? throw new(userNotFound);

                var result = IdResult(await userManager.DeleteAsync(user));
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Finds an existing user by their email address and deletes them.
        /// </summary>
        /// <param name="email">
        /// The email of the user to delete.
        /// </param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public async Task<OperationResult<TradesCoreUser>> DeleteUserByEmailAsync(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email) ?? throw new(userNotFound);

                var result = IdResult(await userManager.DeleteAsync(user));
                if (!result.Success) throw new(result.ErrorMessage);

                return OperationResult<TradesCoreUser>.SuccessResult();
            }
            catch (Exception ex)
            {
                return ex.InnerException is null ?
                    OperationResult<TradesCoreUser>.Failure(ex.Message) :
                    OperationResult<TradesCoreUser>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }
    }
}
