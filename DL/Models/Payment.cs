namespace Data_Layer.Models
{
    /// <summary>
    /// Represents a payment made for an order.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Unique identifier for the payment.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The amount of money paid.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// The method used for payment.
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CreditOrDebit;

        /// <summary>
        /// The status of the payment.
        /// </summary>
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        /// <summary>
        /// The date and time when the payment was made.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        #region Foreign Key
        /// <summary>
        /// Foreign key to the Order table.
        /// </summary>
        public string OrderId { get; set; }
        #endregion

        #region Navigation Property
        /// <summary>
        /// Navigation property to the Order table.
        /// </summary>
        public Order? Order { get; set; }
        #endregion
    }
}
