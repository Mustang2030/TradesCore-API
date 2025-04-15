namespace Data_Layer.Models
{
    /// <summary>
    /// Represents an order placed by a user.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The status of the order.
        /// </summary>
        public OrderStatus Status { get; set; }

        #region Foreign Key
        /// <summary>
        /// Foreign key to the User table.
        /// </summary>
        public string UserId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Navigation property to the User table.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Navigation property to the Product table.
        /// </summary>
        public List<Product> Items { get; set; }

        /// <summary>
        /// Navigation property to the Payment table.
        /// </summary>
        public Payment Payment { get; set; }
        #endregion
    }
}
