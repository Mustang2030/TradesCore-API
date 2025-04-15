namespace Data_Layer.Models
{
    /// <summary>
    /// Enumeration representing the various statuses an order can have.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order is pending and has not yet been processed.
        /// </summary>
        Pending,

        /// <summary>
        /// The order is currently being processed.
        /// </summary>
        Processing,

        /// <summary>
        /// The order has been shipped and is on its way to the customer.
        /// </summary>
        InTransit,

        /// <summary>
        /// The order has been delivered to the customer.
        /// </summary>
        Delivered,

        /// <summary>
        /// The order has been cancelled and will not be processed.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The order has been returned by the customer.
        /// </summary>
        Refunded
    }
}