using Data_Layer.Models;

namespace Data_Layer.DTOs
{
    public class ProductDto
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
        /// The image URL of the product.
        /// </summary>
        public string ImageUrl { get; set; }

        #region
        /// <summary>
        /// Navigation Property to the Category table.
        /// </summary>
        public List<Category>? Categories { get; set; }
        #endregion
    }
}
