namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a product category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Navigation Property of the products associated with this category.
        /// </summary>
        public List<Product>? Products { get; set; }
    }   
}
