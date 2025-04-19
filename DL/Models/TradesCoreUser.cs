using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a user in the e-commerce system.
    /// </summary>
    public class TradesCoreUser : IdentityUser
    {
        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; }

        public override string UserName => $"{FirstName}_{LastName}";

        /// <summary>
        /// The role of the user (e.g., Admin, Customer).
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Navigation Property to the Review table.
        /// </summary>
        public List<Review>? Reviews { get; set; }

        /// <summary>
        /// Navigation Property to the Order table.
        /// </summary>
        public List<Order>? Orders { get; set; }

        /// <summary>
        /// Navigation Property to the Cart table.
        /// </summary>
        public Cart? Cart { get; set; }

        public RefreshToken? RefreshToken { get; set; }
    }
}
