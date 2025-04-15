namespace Data_Layer.Models
{
    /// <summary>
    /// Enumeration representing the various statuses a payment can have.
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Payment was successful.
        /// </summary>
        Success,

        /// <summary>
        /// Payment is pending and has not yet been completed.
        /// </summary>
        Pending,

        /// <summary>
        /// Payment has been cancelled and will not be processed.
        /// </summary>
        Failed
    }
}
