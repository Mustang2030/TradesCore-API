using System.Text.Json.Serialization;

namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a shopping cart for a user.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Unique identifier for the cart.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        #region Foreign Key
        /// <summary>
        /// Foreign key to the User table.
        /// </summary>
        public string UserId { get; set; }
        #endregion

        #region Navigation Property
        /// <summary>
        /// Navigation property to the User table.
        /// </summary>
        [JsonIgnore]
        public TradesCoreUser? User { get; set; }

        /// <summary>
        /// Navigation property to the Product table.
        /// </summary>
        public List<Product>? Items { get;set; }
        #endregion
    }
}
