namespace Data_Layer.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for user information.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string? Username { get; }

        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The role of the user (e.g., Admin, Customer).
        /// </summary>
        public string Role { get; set; }
    }
}
