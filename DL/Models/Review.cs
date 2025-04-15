using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a review for a product in the e-commerce system.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Unique identifier for the review.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The title of the review.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The rating given by the user for the product.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// The content of the review.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The date and time when the review was created.
        /// </summary>
        public DateTime ReviewDate { get; set; }

        #region Foreign Key
        /// <summary>
        /// Foreign key to the User table.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Foreign key to the Product table.
        /// </summary>
        public string ProductId { get; set; }
        #endregion

        #region Navigation Property
        /// <summary>
        /// Navigation property to the User table.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Navigation property to the Product table.
        /// </summary>
        public Product? Product { get; set; }
        #endregion
    }
}
