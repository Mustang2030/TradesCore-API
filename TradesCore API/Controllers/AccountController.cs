using Microsoft.AspNetCore.Authorization;
using Repository_Layer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Data_Layer.Models;
using Data_Layer.DTOs;
using AutoMapper;

namespace TradesCore_API.Controllers
{
    /// <summary>
    /// API controller that handles user account management operations.
    /// </summary>
    /// <param name="userRepo">
    /// The repository that handles user data access operations.
    /// </param>
    /// <param name="mapper">
    /// The AutoMapper instance used for mapping between DTOs and domain models.
    /// </param>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserRepo userRepo, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// API endpoint for retrieving all users in the database.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of users if successful, or an error message if not.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await userRepo.GetAllUsersAsync();
                if (!result.Success) return BadRequest(result.ErrorMessage);

                List<UserDto> users = [];

                foreach (var user in result.Data!)
                {
                    users.Add(mapper.Map<UserDto>(user));
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for retrieving a user by their unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the user to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the user information if successful, or an error message if not.
        /// </returns>
        [Authorize]
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                var result = await userRepo.GetUserAsync(id);
                if (!result.Success) return BadRequest(result.ErrorMessage);
                return Ok(mapper.Map<UserDto>(result.Data!));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for updating a user's information.
        /// </summary>
        /// <param name="user">
        /// The new information the user is to be updated with.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the update operation.
        /// </returns>
        [Authorize]
        [HttpPatch("update-user")]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            try
            {
                var result = await userRepo.UpdateUserAsync(mapper.Map<TradesCoreUser>(user));
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for changing a user's phone number.
        /// </summary>
        /// <param name="userId">
        /// The unique identifier of the user whose phone number is to be changed.
        /// </param>
        /// <param name="newPhoneNumber">
        /// The new phone number to be assigned to the user.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the phone number change operation.
        /// </returns>
        [Authorize]
        [HttpPatch("change-phonenumber")]
        public async Task<IActionResult> ChangePhoneNumber(string userId, string newPhoneNumber)
        {
            try
            {
                var result = await userRepo.UpdatePhoneNumberAsync(userId, newPhoneNumber);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for changing a user's email address.
        /// </summary>
        /// <param name="userId">
        /// The unique identifier of the user whose email address is to be changed.
        /// </param>
        /// <param name="newEmail">
        /// The new email address to be assigned to the user.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the email change operation.
        /// </returns>
        [Authorize]
        [HttpPatch("change-email")]
        public async Task<IActionResult> ChangeEmail(string userId, string newEmail)
        {
            try
            {
                var result = await userRepo.UpdateEmailAsync(userId, newEmail);
                if (!result.Success) throw new(result.ErrorMessage);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for confirming a user's email change.
        /// </summary>
        /// <param name="userId">
        /// The unique identifier of the user whose email address is to be confirmed.
        /// </param>
        /// <param name="token">
        /// The token used to confirm the email change.
        /// </param>
        /// <param name="newEmail">
        /// The new email address to be assigned to the user.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the email confirmation operation.
        /// </returns>
        [Authorize]
        [HttpGet("confirm-new-email")]
        public async Task<IActionResult> ConfirmNewEmail(string userId, string token, string newEmail)
        {
            try
            {
                var result = await userRepo.ConfirmEmailChangeAsync(userId, token, newEmail);
                if (!result.Success) throw new(result.ErrorMessage);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API endpoint for deleting a user by their email address.
        /// </summary>
        /// <param name="email">
        /// The email address of the user to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                var result = await userRepo.DeleteUserAsync(email);
                if (!result.Success) throw new(result.ErrorMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
