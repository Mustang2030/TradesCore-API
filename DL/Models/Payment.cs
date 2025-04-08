using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
    public class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public double Amount { get; set; }

        public string PaymentMethod { get; set; }

        [AllowedValues("Success", "Failed", "Pending")]
        public string Status { get; set; }

        public DateTime PaymentDate { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(Order))]
        public string OrderId { get; set; }
        #endregion

        #region Navigation Property
        public Order Order { get; set; }
        #endregion
    }
}
