using System.Text.Json.Serialization;

namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a product in the e-commerce system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// The quantity of the product to be ordered.
        /// </summary>
        public int OrderQuantity { get; set; } = 1;

        /// <summary>
        /// The image URL of the product.
        /// </summary>
        public string ImageUrl { get; set; }

        #region Foreign Key
        //public string UserId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Navigation Property to the Category table.
        /// </summary>
        public List<Category>? Categories { get; set; }

        //public User User { get; set; }

        /// <summary>
        /// Navigation property to the Order table.
        /// </summary>
        public List<Order>? Orders { get; set; }

        /// <summary>
        /// Navigation property to the Payment table.
        /// </summary>
        public List<Review>? Reviews { get; set; }

        /// <summary>
        /// Navigation property to the Cart table.
        /// </summary>
        [JsonIgnore]
        public List<Cart>? Carts { get; set; }
        #endregion
    }
}
