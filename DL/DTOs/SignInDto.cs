namespace Data_Layer.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for user sign-in information.
    /// </summary>
    public class SignInDto
    {
        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The IP address from which the user is signing in.
        /// </summary>
        public string SignedInFromIp { get; set; }
    }
}
